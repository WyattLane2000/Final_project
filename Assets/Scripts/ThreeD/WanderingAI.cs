using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyStates { alive, dead };
public class WanderingAI : MonoBehaviour
{

    private float obstacleRange = 5.0f;
    private float sphereRadius = 0.75f;

    private EnemyStates state;

    [SerializeField] private GameObject laserbeamPrefab;
    private GameObject laserbeam;
    public float fireRate = 2.0f;
    private float nextFire = 0.0f;

    [SerializeField] private NavMeshAgent agent;    // for moving on the NavMesh
    [SerializeField] private Transform target;      // the target to follow
    private float distanceToTarget = float.MaxValue;    // distance to target - default to far away
    private float chaseRange = 10f;                     // when target is closer than this, chase!
    
    private enum EnemyMovingState { IDLE, CHASE };
    private EnemyMovingState movingState;

    void SetState(EnemyMovingState newState)
    {
        movingState = newState;
    }
    
    bool isPaused = false;//to keep track of 3D pause state
    private void Awake()
    {
        //to keep track of 3D pause state
        Messenger.AddListener(GameEvent.THREED_PAUSED, PauseObject);
        Messenger.AddListener(GameEvent.THREED_RESUMED, ResumeObject);
        Messenger.AddListener(GameEvent.FAC_ENEMY_ON, FacOn);
    }
    private void OnDestroy()
    {
        //to keep track of 3D pause state
        Messenger.RemoveListener(GameEvent.THREED_PAUSED, PauseObject);
        Messenger.RemoveListener(GameEvent.THREED_RESUMED, ResumeObject);
        Messenger.RemoveListener(GameEvent.FAC_ENEMY_ON, FacOn);
    }
    // Start is called before the first frame update
    void Start()
    {
        state = EnemyStates.alive;
        SetState(EnemyMovingState.IDLE);      // start off in the IDLE
    }

    // Update is called once per frame
    void Update()
    {
        if (state != EnemyStates.dead && !isPaused) {
            distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
            // what happens here depends on the state we're currently in!
            switch (movingState)
            {
                case EnemyMovingState.IDLE: Update_Idle(); break;
                case EnemyMovingState.CHASE: Update_Chase(); break;
                default: Debug.Log("Invalid state!"); break;
            }
        }
        if (state == EnemyStates.dead) {
            Update_Idle();
        }
    }

    void Update_Idle()
    {
        agent.isStopped = true;                             // stop the agent (following)
    }

    void Update_Chase()
    {
        agent.isStopped = false;                            // start the agent (following)
        agent.SetDestination(target.transform.position);    // follow the target
        // generate Ray
        Ray ray = new Ray(transform.position, transform.forward);
        // Spherecast and determine if Enemy needs to turn
        RaycastHit hit;
        if (Physics.SphereCast(ray, sphereRadius, out hit))
        {
            GameObject hitObject = hit.transform.gameObject;
            if (hitObject.GetComponent<PlayerCharater>())
            {
                // Spherecast hit Player, fire laser!
                if (laserbeam == null && Time.time > nextFire)
                {
                    Messenger.Broadcast(GameEvent.PLAY_SFX);
                    nextFire = Time.time + fireRate;
                    laserbeam = Instantiate(laserbeamPrefab) as GameObject;
                    laserbeam.transform.position = transform.TransformPoint(0, 1.5f, 1.5f);
                    laserbeam.transform.rotation = transform.rotation;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseRange);  // draw a circle to show chase range
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        //range vector
        Vector3 rangeTest = transform.position + transform.forward * obstacleRange;
        //range line
        Debug.DrawLine(transform.position, rangeTest);
        //wire sphere
        Gizmos.DrawWireSphere(rangeTest, sphereRadius);
    }

    public void ChangeState(EnemyStates state)
    {
        this.state = state;
    }

    // Method to pause 
    void PauseObject()
    {
        isPaused = true;
        if (agent.isActiveAndEnabled && movingState == EnemyMovingState.CHASE)
        {
            agent.isStopped = true;
        }
    }

    // Method to resume
    void ResumeObject()
    {
        isPaused = false;
        if (agent.isActiveAndEnabled && movingState == EnemyMovingState.CHASE)
        {
            agent.isStopped = false;
        }
    }

    //method to set chase when player enters room
    void FacOn()
    {
        SetState(EnemyMovingState.CHASE);
    }
}
