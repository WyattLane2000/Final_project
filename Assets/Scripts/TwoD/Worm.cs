using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : MonoBehaviour
{
    Vector3 targetPoint = new Vector3(0f,5.5f,-3f);
    float moveSpeed = 3f;
    float rotationSpeed = 5f;
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
        Messenger.Broadcast(GameEvent.WORM_KILL);//for end game stat
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused)
        {
            // find direction
            Vector3 directionToTarget = targetPoint - transform.position;

            // Move towards
            transform.position = Vector3.MoveTowards(transform.position, targetPoint, moveSpeed * Time.deltaTime);

            // Rotate to look at 
            if (directionToTarget != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
                targetRotation *= Quaternion.Euler(0, -90, -90);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

            // Handle input movement
            float horizontalInput = -Input.GetAxis("Horizontal");
            Vector3 horizontalMovement = new Vector3(horizontalInput, 0f, 0f) * (moveSpeed*0.50f) * Time.deltaTime;
            transform.Translate(horizontalMovement, Space.World);
        }
    }


    // Method to pause 
    public void PauseObject()
    {
        isPaused = true;
    }

    // Method to resume
    public void ResumeObject()
    {
        isPaused = false;
    }
}
