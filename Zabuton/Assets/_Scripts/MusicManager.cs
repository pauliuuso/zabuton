using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    public AudioClip[] melodies;

    public new AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void playMusic(string title = "firstLevel", bool looping = true)
    {
        if(title == "firstLevel")
        {
            audio.clip = melodies[0];
        }
        
        if(looping)
        {
            audio.loop = true;
        }
        else
        {
            audio.loop = false;
        }

        audio.volume = Settings.music_volume;
        audio.Play();

    }


}
