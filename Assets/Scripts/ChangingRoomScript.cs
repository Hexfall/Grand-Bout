using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangingRoomScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            player.colorChanging = true;
            player.ColorChange();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player != null)
            player.colorChanging = false;
    }
}
