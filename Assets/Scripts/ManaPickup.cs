using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPickup : Pickup
{
    public float manaAmount;

    public override void Use(PlayerController player)
    {
        player.RegainMana(manaAmount);
    }
}
