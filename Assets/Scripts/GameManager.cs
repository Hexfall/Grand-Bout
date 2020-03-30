using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager i;
    public bool invulnerable = true;
    public PlayerController[] players = new PlayerController[4];

    void Awake()
    {
        i = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            Destroy(players[0].gameObject);
    }

    public void AddPlayer(PlayerController player)
    {
        players[0] = player;
    }
}
