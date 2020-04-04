using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float duration = 1.2f;
    public AudioSource sound;
    public float shakeDuration = 0.1f;
    public float shakeIntensity = 1f;

    // Start is called before the first frame update
    void Start()
    {
        sound.pitch = Random.Range(.3f, .4f);
		sound.Play();
        GameManager.i.ScreenShake(shakeIntensity, shakeDuration);
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
