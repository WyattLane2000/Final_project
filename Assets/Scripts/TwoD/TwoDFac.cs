using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDFac : MonoBehaviour
{
    private float speed = 5f;
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
    // Update is called once per frame
    void Update()
    {
        if (!isPaused)
        {
            Vector3 movement = Vector3.up * speed * Time.deltaTime;
            transform.Translate(movement);
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
