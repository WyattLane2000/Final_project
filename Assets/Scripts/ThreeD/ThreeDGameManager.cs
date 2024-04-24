using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeDGameManager : MonoBehaviour
{
    [SerializeField] OperateDoor rightDoor;
    [SerializeField] GameObject doorTrigger;
    [SerializeField] GameObject consoleTrigger;
    [SerializeField] OperateDoor facDoor;
    [SerializeField] GameObject facOneCyrstal;
    [SerializeField] GameObject facTwoCyrstal;
    [SerializeField] GameObject facThreeCyrstal;
    [SerializeField] GameObject facOne;
    [SerializeField] GameObject facTwo;
    [SerializeField] GameObject facThree;
    int cyrstalsCount = 0;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.AIRLOCK_SWITCH, AirlockSwitch);//swap doors close/open
        Messenger.AddListener(GameEvent.FAC_ONE_COLLECTED, FacOneCollected);//FacOne crystal collect
        Messenger.AddListener(GameEvent.FAC_TWO_COLLECTED, FacTwoCollected);//Fac2 crystal collect
        Messenger.AddListener(GameEvent.FAC_THREE_COLLECTED, FacThreeCollected);//Fac3 crystal collect
        Messenger.AddListener(GameEvent.FAC_ONE_HIT, SpawnFacOne);//for setting facOne to active and opening door
        Messenger.AddListener(GameEvent.FAC_TWO_HIT, SpawnFacTwo);//for setting fac2 to active and opening door
        Messenger.AddListener(GameEvent.FAC_THREE_HIT, SpawnFacThree);//for setting fac3 to active and opening door
    }
    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.AIRLOCK_SWITCH, AirlockSwitch);//swap doors close/open
        Messenger.RemoveListener(GameEvent.FAC_ONE_COLLECTED, FacOneCollected);//FacOne crystal collect
        Messenger.RemoveListener(GameEvent.FAC_TWO_COLLECTED, FacTwoCollected);//Fac2 crystal collect
        Messenger.RemoveListener(GameEvent.FAC_THREE_COLLECTED, FacThreeCollected);//Fac3 crystal collect
        Messenger.RemoveListener(GameEvent.FAC_ONE_HIT, SpawnFacOne);//for setting facOne to active and opening door
        Messenger.RemoveListener(GameEvent.FAC_TWO_HIT, SpawnFacTwo);//for setting fac2 to active and opening door
        Messenger.RemoveListener(GameEvent.FAC_THREE_HIT, SpawnFacThree);//for setting fac3 to active and opening door
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
            facOne.SetActive(false);
        }else if (cyrstalsCount == 2)
        {
            facTwo.SetActive(false);
        }
        else if (cyrstalsCount == 3)
        {
            facThree.SetActive(false);
        }
    }
//method to add cyrstal to ship hold
    void FacOneCollected()
    {
        facOneCyrstal.SetActive(true);
        doorTrigger.SetActive(true);
        consoleTrigger.SetActive(true);
        cyrstalsCount++;
    }
    //method to add cyrstal to ship hold
    void FacTwoCollected()
    {
        facTwoCyrstal.SetActive(true);
        doorTrigger.SetActive(true);
        consoleTrigger.SetActive(true);
        cyrstalsCount++;
    }
    //method to add cyrstal to ship hold
    void FacThreeCollected()
    {
        facThreeCyrstal.SetActive(true);
        doorTrigger.SetActive(true);
        consoleTrigger.SetActive(true);
        cyrstalsCount++;
    }
    //for setting facOne to active and opening door
    void SpawnFacOne()
    {
        facOne.SetActive(true );
        rightDoor.Operate();
    }
    //for setting fac2 to active and opening door
    void SpawnFacTwo()
    {
        facTwo.SetActive(true);
        doorTrigger.SetActive(true);
    }
    //for setting fac3 to active and opening door
    void SpawnFacThree()
    {
        facThree.SetActive(true);
        doorTrigger.SetActive(true);
    }
}
