using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup
{
    public float healAmount;

    public override void Use(PlayerController player)
    {
        player.Heal(healAmount);
    }
}
