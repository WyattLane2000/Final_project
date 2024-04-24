using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShooter : ActiveDuringGameplay
{
    [SerializeField]
    private Camera cam;
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
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isPaused)
        {
            Vector3 point = new Vector3(cam.pixelWidth / 2, cam.pixelHeight / 2, 0);
            Ray ray = cam.ScreenPointToRay(point);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();
                // is this object our Enemy?
                if (target != null)
                {
                    target.ReactToHit();
                }
                else
                {
                    // visually indicate where there was a hit
                    StartCoroutine(CreateTempSphereIndicator(hit.point));
                }
            }
        }
    }
    
    private IEnumerator CreateTempSphereIndicator(Vector3 hitPosition)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        sphere.transform.position = hitPosition;
        yield return new WaitForSeconds(1);
        Destroy(sphere);
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
