using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    private float speed = 16f;
    private Vector3 startPos;
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
    private void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (!isPaused)
        {
            Vector3 movement = Vector3.up * speed * Time.deltaTime;
            transform.Translate(movement);

            if (transform.position.y > 12)
            {
                transform.position += new Vector3(0, -22, 0);
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
