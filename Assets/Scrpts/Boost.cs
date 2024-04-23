using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
    private float speed = 3f;
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
    void Update(){
        if (!isPaused)
        {
            //horizontal input/movement
            float horizontalInput = -Input.GetAxis("Horizontal");
            Vector3 horizontalMovement = new Vector3(horizontalInput, 0f, 0f) * (speed*0.75f) * Time.deltaTime;
            transform.Translate(horizontalMovement, Space.World);
            //vetiacal movement
            Vector3 verticalMovement = Vector3.up * speed * Time.deltaTime;
            transform.Translate(verticalMovement, Space.World);
            //off screen bye bye
            if (transform.position.y > 10)
            {
                Destroy(this.gameObject);
            }
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
