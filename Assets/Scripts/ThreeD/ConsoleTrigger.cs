using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Messenger.Broadcast(GameEvent.SWAP_GAME);
            this.gameObject.SetActive(false);
        }
    }
}
