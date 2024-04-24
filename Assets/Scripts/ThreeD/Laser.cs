using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float speed = 6f;
    [SerializeField] private Rigidbody rb;
    bool isPaused = false;//to keep track of 3D pause state
    private void Awake()
    {
        //to keep track of 3D pause state
        Messenger.AddListener(GameEvent.THREED_PAUSED, PauseObject);
        Messenger.AddListener(GameEvent.THREED_RESUMED, ResumeObject);
    }
    private void OnDestroy()
    {
        //to keep track of 3D pause state
        Messenger.RemoveListener(GameEvent.THREED_PAUSED, PauseObject);
        Messenger.RemoveListener(GameEvent.THREED_RESUMED, ResumeObject);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isPaused)
        {
            float toNewtons = 100;
            // OPTION 1: determine forward movement in world coordinates, then convert
            // it to movement in terms of the laser’s forward direction.
            // Vector3 movement = Vector3.forward * Time.deltaTime * speed * toNewtons;
            // convert between forward in world coords to forward in local coords
            // movement = transform.TransformDirection(movement);
            // OPTION 2: a bit simpler!
            // no conversion required since we’re using the laser’s local forward vector.
            Vector3 movement = transform.forward * Time.deltaTime * speed * toNewtons;
            rb.velocity = movement;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        PlayerCharater player = other.GetComponent<PlayerCharater>();
        if (player != null)
        {
            player.Hit();
        }
        Destroy(this.gameObject);
    }

    // Method to pause 
    void PauseObject()
    {
        isPaused = true;
    }

    // Method to resume
    void ResumeObject()
    {
        isPaused = false;
    }
}
