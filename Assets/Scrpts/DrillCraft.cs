using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillCraft : MonoBehaviour
{
    [SerializeField] GameObject DrillTip;
    float DrillTipRotSpeed = 75f;
    float DrillCraftTurnSpeed = 20f;
    float maxTurnAngle = 45f;

    private Quaternion initialRotation;
    private bool isTurning = false;
    private bool isPaused = false;
    private void Awake()
    {
        Messenger.AddListener(GameEvent.TWOD_PAUSED, PauseObject);
        Messenger.AddListener(GameEvent.TWOD_RESUMED, ResumeObject);
    }
    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.TWOD_PAUSED, PauseObject);
        Messenger.RemoveListener(GameEvent.TWOD_RESUMED, ResumeObject);
    }
    void Start()
    {
        initialRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused)
        {
            //Make the Drill rotate like it's digging(maybe add some particles later?)
            DrillTip.transform.Rotate(Vector3.up, DrillTipRotSpeed * Time.deltaTime, Space.World);

            float horizInput = Input.GetAxis("Horizontal");
            if (horizInput != 0f)
            {
                isTurning = true;
                // Get target rotation
                Quaternion targetRotation = Quaternion.Euler(0f, 0f, horizInput * maxTurnAngle);
                // Rotate
                transform.rotation = Quaternion.RotateTowards(transform.rotation, initialRotation * targetRotation, DrillCraftTurnSpeed * Time.deltaTime);
            }
            else if (isTurning)
            {
                // Check if the angle between current rotation and initial rotation is small
                float angleDifference = Quaternion.Angle(transform.rotation, initialRotation);
                if (angleDifference < 1f)
                {
                    // Reset the flag when rotation is complete
                    isTurning = false;
                }
                else
                {
                    // Continue rotating back to initial rotation
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, initialRotation, DrillCraftTurnSpeed * Time.deltaTime);
                }
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boost")
        {
            Messenger.Broadcast(GameEvent.BOOST_HIT);
            Destroy(other.gameObject);
        }

        if (other.tag == "Worm")
        {
            Messenger.Broadcast(GameEvent.SHIP_DAMAGE);
            Destroy(other.gameObject);
        }
    }


    // Method to pause the drill craft movement
    public void PauseObject()
    {
        isPaused = true;
    }

    // Method to resume the drill craft movement
    public void ResumeObject()
    {
        isPaused = false;
    }
}
