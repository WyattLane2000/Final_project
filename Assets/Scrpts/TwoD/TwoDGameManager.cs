using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDGameManager : MonoBehaviour
{
    bool isPaused = false;
    int SHealth = 300;//ship health
    private void Awake()
    {
        Messenger.AddListener(GameEvent.TWOD_PAUSED, PauseObject);
        Messenger.AddListener(GameEvent.TWOD_RESUMED, ResumeObject);
        Messenger.AddListener(GameEvent.SHIP_DAMAGE, shipReactToHit);
        Messenger.AddListener(GameEvent.REPAIR, shipRepair);
    }
    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.TWOD_PAUSED, PauseObject);
        Messenger.RemoveListener(GameEvent.TWOD_RESUMED, ResumeObject);
        Messenger.RemoveListener(GameEvent.SHIP_DAMAGE, shipReactToHit);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused)
        {

        }
    }

    //for update ship health when worm becomes mush
    void shipReactToHit()
    {
        SHealth -= 10;
        Messenger<int>.Broadcast(GameEvent.SHIP_HEALTH_CHANGED, SHealth);
    }

    //method to restore ship health when repaired
    void shipRepair()
    {
        SHealth += 30;
        if(SHealth > 300)
        {
            SHealth = 300;
        }
        Messenger<int>.Broadcast(GameEvent.SHIP_HEALTH_CHANGED, SHealth);
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
