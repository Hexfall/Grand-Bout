using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public float damage;
    private bool canDamage = false;
    public float NoDamageTime = 0.1f;
    private float spawnTime = 0f;
    public GameObject casing;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void SelfDestruct()
    {
        Destroy(casing);
    }

    void Update()
    {
        transform.localPosition += Vector3.down * speed * Time.deltaTime;
        if (canDamage)
            return;
        spawnTime += Time.deltaTime;
        if (spawnTime >= NoDamageTime)
            canDamage = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var playerController = other.GetComponent<PlayerController>();
        if (playerController != null)
        {
            if (canDamage)
            {
                playerController.Damage(damage);
                SelfDestruct();
            }
            return;
        }
        Pickup Pickup = other.transform.GetComponent<Pickup>();
        if (Pickup != null)
            return;
        SelfDestruct();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        var playerController = other.GetComponent<PlayerController>();
        if (playerController != null)
            return;
        Pickup Pickup = other.transform.GetComponent<Pickup>();
        if (Pickup != null)
            return;
        SelfDestruct();
    }
}
