using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : ActiveDuringGameplay
{
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

    // enum to set values by name instead of number.
    // makes code more readable!
    public enum RotationAxes
    {
        MouseXAndY,
        MouseX,
        MouseY
    }
    // public class-scope variable so it shows up in Inspector
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityHoriz = 9.0f;
    public float sensitivityVert = 9.0f;

    public float minVert = -45.0f;
    public float maxVert = 45.0f;
    
    private float rotationX = 0.0f;
    // Update is called once per frame
    void Update()
    {
        //to Check 3D pause state
        if (!isPaused)
        {
            if (axes == RotationAxes.MouseX)
            {
                // horizontal rotation here
                float deltaHoriz = Input.GetAxis("Mouse X") * sensitivityHoriz;
                transform.Rotate(Vector3.up * deltaHoriz);
            }
            else if (axes == RotationAxes.MouseY)
            {
                // vertical rotation here
                rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
                rotationX = Mathf.Clamp(rotationX, minVert, maxVert);
                transform.localEulerAngles = new Vector3(rotationX, 0, 0);
            }
            else
            {
                // both horizontal and vertical rotation here
                rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
                rotationX = Mathf.Clamp(rotationX, minVert, maxVert);

                float deltaHoriz = Input.GetAxis("Mouse X") * sensitivityHoriz;
                float rotationY = transform.localEulerAngles.y + deltaHoriz;

                transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);
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
