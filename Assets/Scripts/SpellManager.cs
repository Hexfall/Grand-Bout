using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    [Header("Player")]
    public GameObject wand;
    private PlayerController player;

    [Header("Fireball")]
    public GameObject fireball;
    public float fireballCost;
    
    [Header("Strengthening")]
    public float StrengthCPS = 2f;
    public float speedMult = 2f;
    public float resistMult = 2f;
    private bool Strengthening = false;
    private bool Strengthened = false;
    private bool strengthWaitForReset = false;
    
    [Header("Lightning")]
    public GameObject lightning;
    public float lightningCostPS;
    private bool castingLightning = false;
    private bool lightningWaitForReset = false;

    [Header("Invisibility")]
    public float invisCost;
    public float invisDuration;
    private bool invisible = false;

    void Awake()
    {
        player = GetComponent<PlayerController>();
        lightning.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (Strengthening && player.CanCast((-player.baseManaRegen + StrengthCPS) * Time.fixedDeltaTime) && !strengthWaitForReset)
            Strengthen();
        else if (Strengthening)
        {
            strengthWaitForReset = true;
            Unstregnthen();
        }
        else
            Unstregnthen();

        if (castingLightning && player.CanCast(lightningCostPS * Time.fixedDeltaTime) && !lightningWaitForReset)
        {
            Uninvisible();
            EnableLightning();
            player.SpendMana(lightningCostPS * Time.fixedDeltaTime);
        }
        else if (castingLightning)
        {
            lightningWaitForReset = true;
            DisableLightning();
        }
        else
            DisableLightning();
    }

    void OnFire()
    {
        CastFireball();
    }

    void OnLightning()
    {
        castingLightning = !castingLightning;
        lightningWaitForReset = false;
    }

    void EnableLightning()
    {
        lightning.SetActive(true);
    }

    void DisableLightning()
    {
        lightning.SetActive(false);
    }

    void CastFireball()
    {
        if (!player.CanCast(fireballCost))
            return;
        Uninvisible();
        player.SpendMana(fireballCost);
        var fire = Instantiate(fireball) as GameObject;
        fire.transform.GetChild(0).GetComponent<FireballScript>().owner = player;
        fire.transform.position = wand.transform.position;
        fire.transform.Rotate(wand.transform.eulerAngles);
    }

    void OnStrengthen()
    {
        Strengthening = !Strengthening;
        strengthWaitForReset = false;
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

    void OnInvisible()
    {
        if (invisible || !player.CanCast(invisCost))
            return;
        player.SpendMana(invisCost);
        player.Hide();
        invisible = true;
        StartCoroutine(Invisible());
    }

    public void Uninvisible()
    {
        invisible = false;
        player.Show();
    }

    IEnumerator Invisible()
    {
        yield return new WaitForSeconds(invisDuration);
        Uninvisible();
    }
}
