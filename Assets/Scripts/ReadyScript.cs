using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyScript : MonoBehaviour
{
    public int players = 0;
    public int minimumPlayers = 2;

    private void OnEnable()
    {
        players = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player != null)
            players += 1;
        if (players >= minimumPlayers && players == GameManager.i.PlayerCount())
            GameManager.i.StartGame();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player != null)
            players -= 1;
    }
}
