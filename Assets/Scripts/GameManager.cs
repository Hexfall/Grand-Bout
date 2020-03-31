using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager i;
    public bool mainMenu = true;
    public bool invulnerable = true;
    public PlayerController[] players;
    private PlayerInputManager pim;

    void Awake()
    {
        i = this;
        pim = GetComponent<PlayerInputManager>();
        players = new PlayerController[pim.maxPlayerCount];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (pim.joiningEnabled)
                pim.DisableJoining();
            else
                pim.EnableJoining();
        }
    }

    public void AddPlayer(PlayerController player)
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] == null)
            {
                players[i] = player;
                return;
            }
        }
    }

    public bool IsFrozen()
    {
        return false;
    }
}
