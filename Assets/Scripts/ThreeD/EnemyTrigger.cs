using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Messenger.Broadcast(GameEvent.FAC_ENEMY_ON);
            this.gameObject.SetActive(false);
        }
    }
}
