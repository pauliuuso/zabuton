using UnityEngine;
using System.Collections;

public class PlaySoundByTime : MonoBehaviour 
{
    public float waitTime;
    public new AudioSource audio;
    public AudioClip clipas;
    private bool played = false;
    private float timePassed;

    void Start()
    {
        //audio = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        timePassed += Time.deltaTime;
        if(waitTime < timePassed && !played)
        {
            audio.volume = Settings.sound_volume;
            audio.clip = clipas;
            audio.Play();
            played = true;
        }
    }


}
