using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource source;
    public AudioClip menuMusic;
    public AudioClip battleMusic;

    void Start()
    {
        
    }

    public void Volume(float volume)
    {
        source.volume = volume;
    }

    public void MenuMusic()
    {
        source.clip = menuMusic;
        source.Play();
    }

    public void BattleMusic()
    {
        source.clip = battleMusic;
        source.Play();
    }
}
