using UnityEngine;
using System.Collections;

public class Turret1 : MonoBehaviour 
{
    public bool hasRadar = false;

    private int nextAngle;
    private float currentTime;
    private float nextFire = 4;
    private bool readyToFire = false;
    public GameObject bolt;
    public Material boltMaterial;
    public GameObject canon;
    public GameObject turretShot;
    private int turnSpeed = 300;


    void Start()
    {
        updateAngle();

    }

    void FixedUpdate()
    {
        currentTime += Time.deltaTime;

        if(currentTime > nextFire)
        {
            readyToFire = true;
            nextFire += 0.2f;
        }

        if(!hasRadar)
        {
            if (Mathf.Round(gameObject.transform.localEulerAngles.y + 10) < nextAngle || Mathf.Round(gameObject.transform.localEulerAngles.y - 10) > nextAngle)
            {
                gameObject.transform.Rotate(Vector3.forward * (int)(Time.deltaTime * turnSpeed));
            }
            else
            {
                if (GetComponentInParent<Soul>().notMoving && readyToFire)
                {
                    fire();
                    readyToFire = false;
                    updateAngle();
                }
            }
        }
    }

    private void updateAngle()
    {
        if (Random.Range(0f, 1f) > 0.5f) nextAngle = (int)Random.Range(0, 90);
        else
        {
            turnSpeed *= -1;
            nextAngle = (int)Random.Range(280, 360);
        }
    }

    void fire()
    {
        bolt.GetComponent<Bullet>().effects.Clear(); // pirma isvalom effektu lista

        bolt.GetComponent<BulletMover>().speed = gameObject.GetComponentInParent<Soul>().bullet_speed;
        bolt.GetComponent<Bullet>().devast = gameObject.GetComponentInParent<Soul>().bullet_devast;
        bolt.GetComponent<Bullet>().owner = "enemy";
        bolt.GetComponent<Bullet>().type = gameObject.GetComponentInParent<Soul>().bullet_type;
        bolt.GetComponent<Bullet>().fireLevel = gameObject.GetComponentInParent<Soul>().fire_level;
        bolt.GetComponent<Bullet>().iceLevel = gameObject.GetComponentInParent<Soul>().ice_level;
        bolt.GetComponent<Bullet>().poisonLevel = gameObject.GetComponentInParent<Soul>().poison_level;
        bolt.GetComponent<Bullet>().effects.Add(gameObject.GetComponentInParent<Soul>().effect);
        bolt.transform.GetChild(0).GetComponent<Renderer>().sharedMaterial = boltMaterial;

        bolt.transform.localScale = new Vector3(gameObject.GetComponentInParent<Soul>().bullet_size[0], gameObject.GetComponentInParent<Soul>().bullet_size[1], gameObject.GetComponentInParent<Soul>().bullet_size[2]);

        Instantiate(bolt, canon.transform.position, Quaternion.Euler(0f, gameObject.transform.localEulerAngles.y, 0f));
        Instantiate(turretShot, canon.transform.position, Quaternion.Euler(0f, gameObject.transform.localEulerAngles.y, 0f));

    }

}
