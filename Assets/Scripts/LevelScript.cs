using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour
{
    public Vector3[] spawnPoints = new Vector3[1];

    public Vector3 GetSpawnByIndex(int index)
    {
        if (index >= spawnPoints.Length)
            return GetRandomSpawn();
        return spawnPoints[index];
    }

    public Vector3 GetRandomSpawn()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Length - 1)];
    }

    void OnDrawGizmosSelected()
    {
        // Draw a small purple circle at the spawn points
        Gizmos.color = new Color(0.5f, 0, 0.5f, 1f);
        for (int i = 0; i < spawnPoints.Length; i++)
            Gizmos.DrawSphere(spawnPoints[i], .08f);
    }
}
