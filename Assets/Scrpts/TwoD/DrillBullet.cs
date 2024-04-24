using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillBullet : MonoBehaviour
{
    private bool isPaused = false;
    public float speed = 14f;
    public float destroyDelay = 5f; // Delay before destruction(so there aren't a bunch of theses flying off)
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
    // Start is called before the first frame update
    void Start()
    {
        // Destroy after the delay
        Destroy(gameObject, destroyDelay);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isPaused)
        {
            transform.Translate(-Vector3.up * speed * Time.deltaTime);
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

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Worm")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
