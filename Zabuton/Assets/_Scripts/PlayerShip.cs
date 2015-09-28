using UnityEngine;
using System.Collections;

public class PlayerShip : MonoBehaviour
{

    float movingSideways = 0f;
    float nextFire = 0.0f;

    public Transform BulletSpawn; // reference i bulletspawn objekta, pagal jo koordinates ikelsim bullet
    public GameObject Bolt; // reference i bullet objekta
    public GameObject playerExplosion; // reference i player explosion

    public new AudioSource audio;
    public AudioClip[] shots;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    void Update() // Naudojama viskam kas nesusiije su fizika, pvz soviniai kurie juda ne fizikos pagalba
    {
        if(Input.GetButton("Fire1") && Time.time > nextFire) // Jei paspaustas sovimo mygtukas ir cooldown baiges
        {
            Bolt.GetComponent<Bullet>().devast = Settings.p_devast; // Soviniui suteikiama damage
            Bolt.GetComponent<Bullet>().type = Settings.p_type; // Sovinio tipas
            nextFire = Time.time + Settings.p_cooldown;
            Instantiate(Bolt, BulletSpawn.position, BulletSpawn.rotation); // Instantiate ikelia objekta, antras parametras pozicija, trecias rotation
            PlaySound();
            //GameObject clone = Instantiate(Bolt, BulletSpawn.position, BulletSpawn.rotation) as GameObject; - cia jei reiktu tureti reference i naujai ideta obekta
        }
    }


    void FixedUpdate() // Naudojama viskam kas susiije su fizika
    {

        float horizontalMovement = Input.GetAxis("Horizontal"); // Gaunama reiksme, jei spaudzia i kaire nueina iki -1 jei i desine iki 1
        float verticalMovement = Input.GetAxis("Vertical");


        if (horizontalMovement < 0 && movingSideways > -10) movingSideways--; // Cia reikalinga del pasisukimo kai judama i kuria nors puse
        else if (horizontalMovement > 0 && movingSideways < 10) movingSideways++;
        else if (movingSideways < 0 && horizontalMovement == 0) movingSideways++;
        else if (movingSideways > 0 && horizontalMovement == 0) movingSideways--;


        Vector3 movement = new Vector3 (horizontalMovement, 0.0f, verticalMovement); // Vector3(x, y, z); Nustatoma kuria kryptimi juda

        GetComponent<Rigidbody>().velocity = movement * Settings.p_speed; // Cia vyksta pats judejimas

        GetComponent<Rigidbody>().position = new Vector3 // Cia yra nustatomos ribos, kad negaletu isskristi uz ekrano
        (
            Mathf.Clamp(GetComponent<Rigidbody>().position.x, Settings.xMin, Settings.xMax), // Nustato kad laivas negaletu palikt ribu
            0.0f,
            Mathf.Clamp(GetComponent<Rigidbody>().position.z, Settings.zMin, Settings.zMax)
        );

        GetComponent<Rigidbody>().rotation = Quaternion.Euler(0.0f, 0.0f, movingSideways * -Settings.p_tilt); // Cia kai juda i sonus, kad pasisuktu laivas i sona


    }

    public void checkLife()
    {
        if (Settings.p_health <= 0)
        {
            Settings.p_alive = false;
            Settings.p_gold = Settings.p_previous_gold;
            Settings.p_score = Settings.p_previous_score;
            Destroy(gameObject);
            Instantiate(playerExplosion, transform.position, transform.rotation);
        }
    }

    private void PlaySound()
    {
        if (Settings.p_type == "fire" && Settings.p_devast <= 15) audio.clip = shots[0];

        audio.volume = Settings.sound_volume;
        audio.Play(); // Garso efektas
    }


}

