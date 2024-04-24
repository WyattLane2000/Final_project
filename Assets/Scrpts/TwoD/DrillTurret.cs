using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillTurret : MonoBehaviour
{
    bool isPaused = false;//to keep track of twoD pause state
    float rotationSpeed = 500f;

    [SerializeField] private GameObject bulletPrefab;
    private GameObject bullet;
    public float fireRate = 2.0f;
    private float nextFire = 0.0f;
    private void Awake()
    {
        //to keep track of twoD pause state
        Messenger.AddListener(GameEvent.TWOD_PAUSED, PauseObject);
        Messenger.AddListener(GameEvent.TWOD_RESUMED, ResumeObject);
    }
    private void OnDestroy()
    {
        //to keep track of twoD pause state
        Messenger.RemoveListener(GameEvent.TWOD_PAUSED, PauseObject);
        Messenger.RemoveListener(GameEvent.TWOD_RESUMED, ResumeObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //to Check twoD pause state
        if (!isPaused)
        {
            // Get horizontal movement of the mouse
            float mouseX = Input.GetAxis("Mouse X");
            // Rotate along the Z-axis based on mouse movement
            transform.Rotate(Vector3.forward, mouseX * rotationSpeed * Time.deltaTime);
            
            //spawn bullet on left mouse click
            if (Input.GetMouseButtonDown(0) && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                bullet = Instantiate(bulletPrefab) as GameObject;
                bullet.transform.position = transform.TransformPoint(0, -2.8f, -1f);
                bullet.transform.rotation = transform.rotation;
            }
        }
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
