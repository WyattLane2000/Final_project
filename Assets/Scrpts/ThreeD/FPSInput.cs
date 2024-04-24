using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
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
    float gravity = -9.8f;
    float speed = 10f;
    [SerializeField]
    private CharacterController charController;

    private float pushForce = 5.0f;

    // Update is called once per frame
    void Update()
    {
        if(!isPaused) {
            float horizInput = Input.GetAxis("Horizontal");
            float vertInput = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(horizInput, 0, vertInput);

            // Clamp magnitude to limit diagonal movement
            movement = Vector3.ClampMagnitude(movement, 1.0f);
            // take speed into account
            movement *= speed;
            //add gravity
            movement.y = gravity;
            // make movement processor independent
            movement *= Time.deltaTime;
            // Convert local to global coordinates
            movement = transform.TransformDirection(movement);

            charController.Move(movement);
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        // does it have a rigidbody and is Physics enabled?
        if (body != null && !body.isKinematic)
        {
            body.velocity = hit.moveDirection * pushForce;
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
