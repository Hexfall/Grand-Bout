using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    private PlayerController player;
    public GameObject wand;
    public GameObject fireball;
    public float fireballCost;
    private bool Strengthening = false;
    public float SpeedMult = 2f;

    void Awake()
    {
        player = GetComponent<PlayerController>();
    }

    void Update()
    {
        
    }

    void OnFire()
    {
        if (!player.CanCast(fireballCost))
            return;
        player.SpendMana(fireballCost);
        var fire = Instantiate(fireball) as GameObject;
        fire.transform.position = wand.transform.position;
        fire.transform.Rotate(wand.transform.eulerAngles);
    }

    void OnStrengthen()
    {
        Strengthening = !Strengthening;
        if (Strengthening)
        {
            player.MulitplyMovement(SpeedMult);
        }
        else
        {
            player.MulitplyMovement(1/SpeedMult);
        }
    }
}
