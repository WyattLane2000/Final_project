using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeDGameManager : MonoBehaviour
{
    [SerializeField] OperateDoor rightDoor;
    [SerializeField] GameObject doorTrigger;
    [SerializeField] OperateDoor facDoor;
    [SerializeField] GameObject FacOneCyrstal;
    [SerializeField] GameObject FacOne;
    int cyrstalsCount = 0;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.AIRLOCK_SWITCH, AirlockSwitch);
        Messenger.AddListener(GameEvent.FAC_ONE_COLLECTED, FacOneCollected);
    }
    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.AIRLOCK_SWITCH, AirlockSwitch);
        Messenger.RemoveListener(GameEvent.FAC_ONE_COLLECTED, FacOneCollected);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //method to run airlock
    void AirlockSwitch()
    {
        rightDoor.Operate();
        facDoor.Operate();
        if(cyrstalsCount == 1)
        {
            FacOne.SetActive(false);
        }
    }

    //method to add cyrstal to ship hold
    void FacOneCollected()
    {
        FacOneCyrstal.SetActive(true);
        doorTrigger.SetActive(true);
        cyrstalsCount++;
    }
}
