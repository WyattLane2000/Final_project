using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeDGameManager : MonoBehaviour
{
    [SerializeField] OperateDoor rightDoor;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("manager run");
        rightDoor.Operate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
