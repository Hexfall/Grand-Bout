using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSound : MonoBehaviour
{
    AudioSource sound;
    public Vector2 pitchRange = new Vector2(.9f, 1.1f);
    private float SinceSpawn = 0f;

    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
        sound.pitch = Random.Range(pitchRange.x, pitchRange.y);
        sound.Play();
    }

    void FixedUpdate()
    {
        SinceSpawn += Time.fixedDeltaTime;
    }

    public float TimeLeft()
    {
        if (SinceSpawn >= sound.clip.length)
            return 0f;
        return sound.clip.length - SinceSpawn;
    }
}
