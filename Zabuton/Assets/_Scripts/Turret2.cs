using UnityEngine;
using System.Collections;

public class Turret2 : MonoBehaviour 
{
    private int nextAngle;
    private float currentTime;
    private float nextFire;
    private bool readyToFire = false;
    public GameObject bolt;
    public Material boltMaterial;
    private int materialNumber;
    public GameObject canon;
    public GameObject turretShot;
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
    private bool retreating = false;


    void Start()
    {
        nextFire = Random.Range(2, 6);
        randomas = Random.Range(0f, 1f);
        updateAngle();

        bullet_type = "ice";


        if (owner == "enemy") angleFix = 0;
        else if (owner == "player") angleFix = 0;
    }

    void FixedUpdate()
    {
        if (gameObject.transform.position.z < -10) retreating = true;

        currentTime += Time.deltaTime;

        if(currentTime > nextFire && !retreating)
        {
            readyToFire = true;
            nextFire += 0.2f;
        }

        print(gameObject.transform.eulerAngles.z + " next angle: " + nextAngle);

        if (Mathf.Round(gameObject.transform.eulerAngles.z + 10) < nextAngle || Mathf.Round(gameObject.transform.eulerAngles.z - 10) > nextAngle)
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
            nextAngle = (int)Random.Range(0, 80);
            if (Random.Range(0f, 1f) > 0.5f)
            {
                turnSpeed *= -1;
                nextAngle = (int)Random.Range(260, 360);
            }
        }
        else if (owner == "player")
        {
            nextAngle = (int)Random.Range(0, 180);
            if (Random.Range(0f, 1f) > 0.5f) turnSpeed *= -1;
        }

    }

    void fire()
    {
        print("turret fire");

        bolt.GetComponent<Bullet>().effects.Clear(); // pirma isvalom effektu lista

        bolt.GetComponent<BulletMover>().speed = bullet_speed;
        bolt.GetComponent<Bullet>().devast = bullet_devast;
        bolt.GetComponent<Bullet>().owner = owner;
        bolt.GetComponent<Bullet>().type = bullet_type;
        bolt.GetComponent<Bullet>().fireLevel = fire_level;
        bolt.GetComponent<Bullet>().iceLevel = ice_level;
        bolt.GetComponent<Bullet>().poisonLevel = poison_level;
        bolt.GetComponent<Bullet>().effects.Add(effect);
        bolt.transform.GetChild(0).GetComponent<Renderer>().sharedMaterial = boltMaterial;

        bolt.transform.localScale = new Vector3(bullet_size[0], bullet_size[1], bullet_size[2]);

        Instantiate(bolt, new Vector3 (canon.transform.position.x, 0f, canon.transform.position.z), Quaternion.Euler(0f, gameObject.transform.eulerAngles.z + angleFix, 0f));
        Instantiate(turretShot, canon.transform.position, Quaternion.Euler(0f, gameObject.transform.eulerAngles.z + angleFix, 0f));

    }

}
