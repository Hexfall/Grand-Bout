using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public float damage;
    private bool canDamageSelf = false;
    public float NoDamageTime = 0.1f;
    private float spawnTime = 0f;
    public GameObject casing;
    public GameObject explosion;
    public PlayerController owner;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void SelfDestruct()
    {
        Destroy(casing);
        var expl = Instantiate(explosion);
        expl.transform.position = new Vector3(transform.position.x, transform.position.y + .5f, 0);
    }

    void Update()
    {
        transform.localPosition += Vector3.down * speed * Time.deltaTime;
        if (canDamageSelf)
            return;
        spawnTime += Time.deltaTime;
        if (spawnTime >= NoDamageTime)
            canDamageSelf = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name.ToLower().Contains("pass"))
            return;
        if (other.name.Contains("Fireball"))
        {
            other.GetComponent<FireballScript>().SelfDestruct();
            SelfDestruct();
            return;
        }

        var playerController = other.GetComponent<PlayerController>();
        if (playerController != null)
        {
            if (!(canDamageSelf && playerController == owner))
            {
                playerController.Damage(damage);
                SelfDestruct();
            }
            return;
        }
        Pickup Pickup = other.transform.GetComponent<Pickup>();
        if (Pickup != null)
            return;

        if (other.gameObject.GetComponent<LightningScript>() != null)
            return;

        if (other.gameObject.GetComponent<CrownScript>() != null)
            return;
        
        SelfDestruct();
    }
}
