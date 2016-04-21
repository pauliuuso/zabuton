using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    public AudioClip[] melodies;

    public new AudioSource audio;

    void Start()
    {
        if (Settings.current_level < 8) playMusic("main", true);
        else playMusic("main2", true);
    }

    public void playMusic(string title = "main", bool looping = true)
    {
        gameObject.SetActive(true);
        audio = GetComponent<AudioSource>();
        if(title == "main")
        {
            audio.clip = melodies[0];
            audio.volume = Settings.music_volume;
        }
        else if(title == "main2")
        {
            audio.clip = melodies[1];
            audio.volume = Settings.music_volume;
        }
        else if (title == "boss")
        {
            audio.clip = melodies[2];
            audio.volume = Settings.music_volume;
        }
        else if (title == "boss2")
        {
            audio.clip = melodies[3];
            if (Settings.music_volume > 0) audio.volume = 0.3f;
        }
        
        if(looping)
        {
            audio.loop = true;
        }
        else
        {
            audio.loop = false;
        }

        audio.Play();

    }


}
