using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningScript : MonoBehaviour
{
    private PlayerController[] players = new PlayerController[4];
    private PlayerController owner;
    public float DPS;

    void Start()
    {
        owner = GetComponentInParent(typeof(PlayerController)) as PlayerController;
    }

    void FixedUpdate()
    {
        for (int i = 0; i < players.Length; i++)
            if (!(players[i] == owner || players[i] == null))
                players[i].Damage(DPS * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            foreach (var p in players)
                if (p == player)
                    return;
            for (int i = 0; i < players.Length; i++)
                if (players[i] == null)
                {
                    players[i] = player;
                    return;
                }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        
        if (player != null)
        {
            for (int i = 0; i < players.Length; i++)
                if (players[i] == player)
                    players[i] = null;
        }
    }
}
