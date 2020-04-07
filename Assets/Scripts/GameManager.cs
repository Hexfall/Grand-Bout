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
    private LevelScript[] levelScripts;
    private GameObject enabledLevel;
    private LevelScript enabledLevelScript;
    public GameObject PickupDropper;
    public MusicManager music;

    void Awake()
    {
        i = this;

        // Init Players.
        pim = GetComponent<PlayerInputManager>();
        players = new PlayerController[pim.maxPlayerCount];

        // Init Levels.
        levelScripts = new LevelScript[levels.Length];
        for (int i = 0; i < levels.Length; i++)
            levelScripts[i] = levels[i].GetComponent<LevelScript>();
        enabledLevel = levels[0];
        enabledLevelScript = levelScripts[0];
        MainMenu();
    }

    public void AddPlayer(PlayerController player)
    {
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

    public void RemovePlayer(PlayerController player)
    {
        for (int i = 0; i < players.Length; i++)
            if (players[i] == player)
                players[i] = null;
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

    private void EnableLevel(int index)
    {
        if (index >= levels.Length)
            index = 0;
        // Clean up from previous level.
        enabledLevel.SetActive(false);
        PickupDropper.GetComponent<PickupDropper>().ClearDrops();

        enabledLevel = levels[index];
        enabledLevelScript = levelScripts[index];
        enabledLevel.SetActive(true);
        foreach (var player in players)
            if (player != null)
                player.Reset();
    }

    public Vector3 GetSpawn(int index)
    {
        return enabledLevelScript.GetSpawnByIndex(index);
    }

    public Vector3 GetSpawnRandom()
    {
        return enabledLevelScript.GetRandomSpawn();
    }

    public void StartGame()
    {
        mainMenu = false;
        invulnerable = false;
        pim.DisableJoining();
        LoadLevel(Random.Range(1, levels.Length));
        music.BattleMusic();
    }

    public void MainMenu()
    {
        mainMenu = true;
        invulnerable = true;
        pim.EnableJoining();
        LoadLevel(0);
        music.MenuMusic();
    }

    private void SetPlayerLocation(PlayerController player, Vector3 location)
    {
        player.SetLocation(location);
    }

    private void LoadLevel(int index)
    {
        EnableLevel(index);

        for (int i = 0; i < players.Length; i++)
            if (players[i] != null)
                SetPlayerLocation(players[i], GetSpawn(i));
    }

    public int AlivePlayers()
    {
        int retInt = 0;
        foreach (var player in players)
            if (player != null)
                if (player.Alive())
                    retInt++;
        return retInt;
    }

    public PlayerController GetWinner()
    {
        foreach (var player in players)
            if (player != null && player.alive)
                return player;
        return null;
    }

    public void CheckWin()
    {
        if (AlivePlayers() == 1)
        {
            var winner = GetWinner();
            MainMenu();
            foreach (var player in players)
                if (player != null)
                    player.Reset();
            winner.Win();
        }
    }

    public void ScreenShake(float intensity, float duration = 0.1f)
    {
        Camera.main.gameObject.GetComponent<CameraManager>().ScreenShake(intensity, duration);
    }
}
