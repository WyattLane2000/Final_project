using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharater : MonoBehaviour
{
    private int health;
    private int maxHealth = 100;

    private void Awake()
    {
        //Messenger<int>.AddListener(GameEvent.PICKUP_HEALTH, this.OnPickupHealth);
    }
    private void OnDestroy()
    {
        //Messenger<int>.RemoveListener(GameEvent.PICKUP_HEALTH, this.OnPickupHealth);
    }
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public void Hit()
    {
        health -= 10;
        Messenger<int>.Broadcast(GameEvent.PLAYER_HEALTH_CHANGED, health);
        if (health == 0)
        {
            //Debug.Break();
            Messenger.Broadcast(GameEvent.PLAYER_DEAD);
        }
    }

    public void OnPickupHealth(int healthAdded)
    {
        health += healthAdded;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        Messenger<int>.Broadcast(GameEvent.PLAYER_HEALTH_CHANGED, health);
    }
}
