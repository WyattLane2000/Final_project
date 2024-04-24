using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirlockTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Messenger.Broadcast(GameEvent.AIRLOCK_SWITCH);
            this.gameObject.SetActive(false);
        }
    }
}
