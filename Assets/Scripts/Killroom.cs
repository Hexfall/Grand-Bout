using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killroom : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            player.Leave();
            if (GameManager.i.PlayerCount() == 0)
                Application.Quit();
        }
    }
}
