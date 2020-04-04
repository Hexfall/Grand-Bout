using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public bool DestroyOnPickup = true;

    public virtual void Use(PlayerController player)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player != null && player.alive)
        {
            Use(player);
            if (DestroyOnPickup)
                Destroy(gameObject);
        }
    }
}
