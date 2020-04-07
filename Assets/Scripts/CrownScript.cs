using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrownScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            player.Crown();
            GameManager.i.SetCrownHolder(player);
            Destroy(gameObject);
        }
    }
}
