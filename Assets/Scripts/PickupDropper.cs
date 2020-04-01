using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupDropper : MonoBehaviour
{
    public GameObject[] drops;
    public float[] MTTH;
    public Vector2 spawnArea;

    void FixedUpdate()
    {
        if (GameManager.i.IsFrozen() || GameManager.i.mainMenu)
            return;
        Check();
    }

    void Check()
    {
        for (int i = 0; i < MTTH.Length; i++)
        {
            if (Random.value <= (1f/MTTH[i])*Time.fixedDeltaTime)
                SpawnObject(drops[i]);
        }
    }

    void SpawnObject(GameObject toSpawn)
    {
        Instantiate(toSpawn, RandomPoint(), gameObject.transform.rotation);
    }

    Vector3 RandomPoint()
    {
        Vector3 retVec = Vector3.zero;
        retVec.x = Random.Range(-spawnArea.x, spawnArea.x);
        retVec.y = Random.Range(-spawnArea.y, spawnArea.y);
        return retVec;
    }

    void OnDrawGizmosSelected()
    {
        // Draw a semitransparent red cube at the transforms position
        Gizmos.color = new Color(1, 0, 0, 0.2f);
        Gizmos.DrawCube(transform.position, new Vector3(spawnArea.x*2, spawnArea.y*2, 1));
    }
}
