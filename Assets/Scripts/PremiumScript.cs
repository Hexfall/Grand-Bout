using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PremiumScript : MonoBehaviour
{
    public int players = 0;
    private SpriteRenderer text;

    private void Awake()
    {
        text = GetComponent<SpriteRenderer>();
        text.enabled = false;
    }

    private void OnEnable()
    {
        players = 0;
        text.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            players++;
            text.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player != null)
            players--;
        if (players == 0)
            text.enabled = false;
    }
}
