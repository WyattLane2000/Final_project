using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectTrigger : MonoBehaviour
{
    [SerializeField] GameObject Cyrstal;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Messenger.Broadcast(GameEvent.FAC_ONE_COLLECTED);
            Cyrstal.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }
}
