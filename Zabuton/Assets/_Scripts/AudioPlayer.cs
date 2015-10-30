using UnityEngine;
using System.Collections;

public class AudioPlayer : MonoBehaviour 
{

    public AudioClip[] fireShots;
    public AudioClip[] iceShots;
    public AudioClip[] poisonShots;
    public new AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void PlaySound(string type)
    {
        if (type == "fire")
        {
            audio.clip = fireShots[Settings.p_fire_level];
        }
        else if (type == "ice")
        {
            audio.clip = iceShots[Settings.p_ice_level];
        }
        else if (type == "poison")
        {
            audio.clip = poisonShots[Settings.p_poison_level];
        }

        audio.volume = Settings.sound_volume;
        audio.Play(); // Garso efektas
    }

}
