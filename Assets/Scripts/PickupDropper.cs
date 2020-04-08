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
        if (Input.GetButtonDown("Fire2"))
            ClearDrops();
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
        var drop = Instantiate(toSpawn, RandomPoint(), gameObject.transform.rotation) as GameObject;
        drop.transform.parent = transform;
    }

    Vector3 RandomPoint()
    {
        Vector3 retVec = Vector3.zero;
        retVec.x = Random.Range(-spawnArea.x, spawnArea.x);
        retVec.y = Random.Range(-spawnArea.y, spawnArea.y);
        if (ValidSpot(retVec))
            return retVec;
        return RandomPoint();
    }

    private bool ValidSpot(Vector3 point)
    {
        return !GameManager.i.InNoDropZone(point);
    }

    public void ClearDrops()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
            Destroy(transform.GetChild(i).gameObject);
    }

    void OnDrawGizmosSelected()
    {
        // Draw a semitransparent red cube at the transforms position
        Gizmos.color = new Color(1, 0, 0, 0.2f);
        Gizmos.DrawCube(transform.position, new Vector3(spawnArea.x*2, spawnArea.y*2, 1));
    }
}
