using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float baseMod = 0.025f;
    public float timeLeft = 5f;
    private float intense = 1f;

    public void ScreenShake(float intensity, float duration)
    {
        if (intensity < intense)
            return;
        timeLeft = duration;
        intense = intensity;
    }

    void Update()
    {
        if (timeLeft <= 0)
            return;

        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
            intense = 0f;

        Vector3 point = Random.insideUnitCircle * intense * baseMod;
        point.z = -10f;
        transform.position = point;
    }
}
