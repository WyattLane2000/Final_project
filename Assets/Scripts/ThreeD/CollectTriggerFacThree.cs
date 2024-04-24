using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectTriggerFacThree : MonoBehaviour
{
    [SerializeField] GameObject Cyrstal;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Messenger.Broadcast(GameEvent.FAC_THREE_COLLECTED);
            Cyrstal.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }
}
