using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour
{
    public GameObject spawnPoints;
    private Vector3[] spawnPointArray = new Vector3[1];

    void Start()
    {
        int childCount = spawnPoints.transform.childCount;
        if (childCount == 0)
        {
            spawnPointArray[0] = new Vector3(0, 0, 0);
            return;
        }

        spawnPointArray = new Vector3[childCount];
        for (int i = 0; i < childCount; i++)
            spawnPointArray[i] = spawnPoints.transform.GetChild(i).position;
    }

    public Vector3 GetSpawnByIndex(int index)
    {
        if (index > spawnPointArray.Length)
            return spawnPointArray[0];
        return spawnPointArray[index];
    }

    public Vector3 GetRandomSpawn()
    {
        return spawnPointArray[Random.Range(0, spawnPointArray.Length)];
    }
}
