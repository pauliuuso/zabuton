using UnityEngine;
using System.Collections;

public class Turret1 : MonoBehaviour 
{
    private int nextAngle;
    private float currentTime;
    private float nextFire = 4;
    private bool readyToFire = false;
    public GameObject bolt;
    public Material[] boltMaterial;
    private int materialNumber;
    public GameObject canon;
    public GameObject[] turretShot;
    private int turnSpeed = 300;
    private float randomas;

    public int bullet_speed;
    public int bullet_devast;
    public string owner;
    public string bullet_type;
    public int fire_level;
    public int ice_level;
    public int poison_level;
    public string effect;
    public float[] bullet_size;
    private int angleFix;


    void Start()
    {
        randomas = Random.Range(0f, 1f);
        updateAngle();
        if(bullet_type == "random")
        {
            if (randomas > 0 && randomas < 0.33f)
            {
                bullet_type = "fire";
                bullet_devast = 35;
            }
            else if (randomas >= 0.33f && randomas < 0.66f)
            {
                bullet_type = "ice";
                bullet_devast = 30;
            }
            else
            {
                bullet_type = "poison";
                bullet_devast = 20;
                effect = "Poison1";
            }
        }
        if (bullet_type == "fire") materialNumber = 0;
        else if (bullet_type == "ice") materialNumber = 1;
        else materialNumber = 2;

        if (owner == "enemy") angleFix = 90;
        else if (owner == "player") angleFix = 90;
    }

    void FixedUpdate()
    {
        currentTime += Time.deltaTime;

        if(currentTime > nextFire)
        {
            readyToFire = true;
            nextFire += 0.2f;
        }


        if (Mathf.Round(gameObject.transform.eulerAngles.y + 10) < nextAngle || Mathf.Round(gameObject.transform.eulerAngles.y - 10) > nextAngle)
        {
            gameObject.transform.Rotate(Vector3.forward * (int)(Time.deltaTime * turnSpeed));
        }
        else
        {
            if (owner == "enemy" && GetComponentInParent<Soul>().notMoving && readyToFire)
            {
                fire();
                readyToFire = false;
                updateAngle();
            }
            else if (owner == "player" && readyToFire)
            {
                fire();
                readyToFire = false;
                updateAngle();
            }

        }

    }

    private void updateAngle()
    {
        if(owner == "enemy")
        {
            nextAngle = (int)Random.Range(180, 360);
            if (Random.Range(0f, 1f) > 0.5f) turnSpeed *= -1;
        }
        else if (owner == "player")
        {
            nextAngle = (int)Random.Range(0, 180);
            if (Random.Range(0f, 1f) > 0.5f) turnSpeed *= -1;
        }

    }

    void fire()
    {
        bolt.GetComponent<Bullet>().effects.Clear(); // pirma isvalom effektu lista

        bolt.GetComponent<BulletMover>().speed = bullet_speed;
        bolt.GetComponent<Bullet>().devast = bullet_devast;
        bolt.GetComponent<Bullet>().owner = owner;
        bolt.GetComponent<Bullet>().type = bullet_type;
        bolt.GetComponent<Bullet>().fireLevel = fire_level;
        bolt.GetComponent<Bullet>().iceLevel = ice_level;
        bolt.GetComponent<Bullet>().poisonLevel = poison_level;
        bolt.GetComponent<Bullet>().effects.Add(effect);
        bolt.transform.GetChild(0).GetComponent<Renderer>().sharedMaterial = boltMaterial[materialNumber];

        bolt.transform.localScale = new Vector3(bullet_size[0], bullet_size[1], bullet_size[2]);

        Instantiate(bolt, new Vector3 (canon.transform.position.x, 0f, canon.transform.position.z), Quaternion.Euler(0f, gameObject.transform.eulerAngles.y + angleFix, 0f));
        Instantiate(turretShot[materialNumber], canon.transform.position, Quaternion.Euler(0f, gameObject.transform.localEulerAngles.y + angleFix, 0f));

    }

}
