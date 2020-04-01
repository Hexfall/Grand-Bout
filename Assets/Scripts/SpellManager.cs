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
    private bool Strengthened = false;
    public float StrengthCPS = 2f;
    public float speedMult = 2f;
    public float resistMult = 2f;

    void Awake()
    {
        player = GetComponent<PlayerController>();
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (Strengthening && player.CanCast((-player.baseManaRegen + StrengthCPS) * Time.fixedDeltaTime))
            Strengthen();
        else
            Unstregnthen();
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
    }

    void Strengthen()
    {
        if (Strengthened)
            return;
        player.MulitplyMovement(speedMult);
        player.MultiplyDamage(1 / resistMult);
        player.PassiveSpendMana(StrengthCPS);
        
        Strengthened = true;
    }

    void Unstregnthen()
    {
        if (!Strengthened)
            return;
        player.MulitplyMovement(1 / speedMult);
        player.MultiplyDamage(resistMult);
        player.PassiveRegainMana(StrengthCPS);

        Strengthened = false;
    }
}
