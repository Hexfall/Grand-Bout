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
    public Color[] playerColors;
    private PlayerInputManager pim;
    public GameObject[] levels;

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
        player.transform.position = transform.position;
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] == null)
            {
                players[i] = player;
                player.SetPlayerColor(playerColors[i % playerColors.Length]);
                return;
            }
        }
    }

    public int PlayerCount()
    {
        int retInt = 0;
        foreach (var player in players)
            retInt += (player != null ? 1 : 0);
        return retInt;
    }

    public bool IsFrozen()
    {
        return false;
    }

    public void StartGame()
    {
        
    }
}
