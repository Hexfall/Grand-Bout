using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningScript : MonoBehaviour
{
    public List<PlayerController> players = new List<PlayerController>();
    private PlayerController owner;
    public float DPS;

    void Start()
    {
        owner = GetComponentInParent(typeof(PlayerController)) as PlayerController;
    }

    void FixedUpdate()
    {
        foreach (PlayerController player in players)
            if (player != owner)
                player.Damage(DPS * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null)
            players.Add(other.gameObject.GetComponent<PlayerController>());
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null)
            players.Remove(other.gameObject.GetComponent<PlayerController>());
    }
}
