using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    public AudioClip[] melodies;

    public new AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        if (Settings.current_level == 1) playMusic("level1", true);
    }

    public void playMusic(string title = "level1", bool looping = true)
    {
        if(title == "level1")
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
