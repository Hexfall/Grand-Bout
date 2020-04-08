using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableLevelScript : LevelScript
{
    public int crownHolderIndex = -1;
    public GameObject crownPrefab;
    public Vector3 crownLocation;
    public float[] progress;
    public float progressRate = 0.1f;
    public GameObject[] progressItems;
    public ProgressScript[] progressScripts;
    public Vector4[] noDropAreas;

    void Start()
    {
        progress = new float[GameManager.i.players.Length];
    }

    private void OnEnable()
    {
        crownHolderIndex = -1;
        SpawnCrown();
        progressScripts = new ProgressScript[progressItems.Length];
        for (int i = 0; i < progressItems.Length; i++)
            progressScripts[i] = progressItems[i].GetComponent<ProgressScript>();
        SetColors();
    }

    private void FixedUpdate()
    {
        if (crownHolderIndex == -1)
            return;
        UpdateProgress();
        CheckWin();
    }

    private void SpawnCrown()
    {
        var crown = Instantiate(crownPrefab) as GameObject;
        crown.transform.position = crownLocation;
    }

    public void SetCrownHolder(int index)
    {
        crownHolderIndex = index;
    }

    private void SetColors()
    {
        for (int i = 0; i < progressItems.Length; i++)
            if (GameManager.i.players[i] != null)
                progressScripts[i].SetColor(GameManager.i.players[i].GetPlayerColor());
    }

    public void UpdateProgress()
    {
        progress[crownHolderIndex] += progressRate * Time.fixedDeltaTime;
        progressScripts[crownHolderIndex].SetProgress(progress[crownHolderIndex]);
    }

    void ResetProgess()
    {
        for (int i = 0; i < progress.Length; i++)
            progress[i] = 0f;
        foreach (var ps in progressScripts)
            ps.SetProgress(0);
    }

    public void Reset()
    {
        ResetProgess();
    }

    public void CheckWin()
    {
        if (progress[crownHolderIndex] < 1)
            return;
        Reset();
        GameManager.i.MainMenu();
        GameManager.i.players[crownHolderIndex].Crown();
    }

    public bool InNoDrop(Vector3 point)
    {
        foreach (Vector4 area in noDropAreas)
            if (point.x >= Mathf.Min(area.x, area.y) &&
                point.x <= Mathf.Max(area.x, area.y) &&
                point.y >= Mathf.Min(area.z, area.w) &&
                point.y <= Mathf.Max(area.z, area.w)
            ) {return true;}
        return false;
    }

    public override void OnDrawGizmosSelected()
    {
        // Draw a small purple circle at the spawn points
        Gizmos.color = new Color(0.5f, 0, 0.5f, 1f);
        for (int i = 0; i < spawnPoints.Length; i++)
            Gizmos.DrawSphere(spawnPoints[i], .08f);
        Gizmos.color = new Color(1f, 1f, 0f, 1f);
        Gizmos.DrawSphere(crownLocation, .08f);
        Gizmos.color = new Color(1f, 0, 0f, 1f);
        foreach (Vector4 area in noDropAreas)
            Gizmos.DrawCube(new Vector3((area.x + area.y) / 2, (area.z + area.w) / 2, 1), new Vector3(Mathf.Abs(area.x - area.y), Mathf.Abs(area.z - area.w), 1));
    }
}
