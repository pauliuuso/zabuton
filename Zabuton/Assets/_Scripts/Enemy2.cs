using UnityEngine;
using System.Collections;

public class Enemy2 : MonoBehaviour
{
    float horizontalMovement = 0f;
    float verticalMovement = -1f;
    bool movingLeft = false;
    bool movingUp = false;
    bool movingRight = false;
    bool movingDown = true;
    int speed;
    int tilt;
    float movingSideways = 0f;
    Vector3 newPosition;
    float currentTime;
    float fightingTime;
    Ray downRay;
    RaycastHit hit;
    int sightDown = 15;
    Color rayColorDown = Color.green;
    public GameObject canon;
    public GameObject bolt;
    public Material boltMaterial;
    private float nextFire = 4;

    void Start()
    {
        tilt = gameObject.GetComponent<Soul>().tilt;
    }

    void FixedUpdate()
    {
        Debug.DrawRay(gameObject.transform.position, Vector3.forward * -sightDown, rayColorDown);

        currentTime += Time.deltaTime;

        downRay = new Ray(gameObject.transform.position, Vector3.forward * -sightDown);

        Vector3 movement = new Vector3(horizontalMovement, 0.0f, verticalMovement); // Vector3(x, y, z); Nustatoma kuria kryptimi juda
        GetComponent<Rigidbody>().velocity = movement * gameObject.GetComponent<Soul>().speed; // Cia vyksta pats judejimas


        if (Physics.Raycast(downRay, out hit, sightDown))
        {
            if (hit.collider.tag != "Untagged")
            {
                rayColorDown = Color.red;
                movingDown = false;
            }
        }
        else if (!Physics.Raycast(downRay, out hit, sightDown))
        {
            rayColorDown = Color.green;
            movingDown = true;
        }

        if(nextFire < currentTime)
        {
            nextFire += Random.Range(1, 5);
            fire();
        }
        

        if (movingLeft && horizontalMovement >= -1f) horizontalMovement -= 0.1f;
        if (movingUp && verticalMovement <= 1f) verticalMovement += 0.1f;
        if (movingRight && horizontalMovement <= 1f) horizontalMovement += 0.1f;
        if (movingDown && verticalMovement >= -1f) verticalMovement -= 0.1f;

        if (!movingLeft && !movingRight) horizontalMovement = 0f;
        if (!movingUp && !movingDown) verticalMovement = 0f;

        if (horizontalMovement < 0 && movingSideways > -10) movingSideways--; // Cia reikalinga del pasisukimo kai judama i kuria nors puse
        else if (horizontalMovement > 0 && movingSideways < 10) movingSideways++;
        else if (movingSideways < 0 && horizontalMovement == 0) movingSideways++;
        else if (movingSideways > 0 && horizontalMovement == 0) movingSideways--;

        GetComponent<Rigidbody>().rotation = Quaternion.Euler(270f + movingSideways * tilt, gameObject.transform.rotation.z + 90f, gameObject.transform.rotation.z); // Cia kai juda i sonus, kad pasisuktu laivas i sona


    }

    void fire()
    {
        bolt.GetComponent<Bullet>().effects.Clear(); // pirma isvalom effektu lista

        bolt.GetComponent<BulletMover>().speed = gameObject.GetComponent<Soul>().bullet_speed;
        bolt.GetComponent<Bullet>().devast = gameObject.GetComponent<Soul>().bullet_devast;
        bolt.GetComponent<Bullet>().owner = "enemy";
        bolt.GetComponent<Bullet>().type = gameObject.GetComponent<Soul>().bullet_type;
        bolt.GetComponent<Bullet>().fireLevel = gameObject.GetComponent<Soul>().fire_level;
        bolt.GetComponent<Bullet>().iceLevel = gameObject.GetComponent<Soul>().ice_level;
        bolt.GetComponent<Bullet>().poisonLevel = gameObject.GetComponent<Soul>().poison_level;
        bolt.GetComponent<Bullet>().effects.Add(GetComponent<Soul>().effect);
        bolt.transform.GetChild(0).GetComponent<Renderer>().sharedMaterial = boltMaterial;

        bolt.transform.localScale = new Vector3(gameObject.GetComponent<Soul>().bullet_size[0], gameObject.GetComponent<Soul>().bullet_size[1], gameObject.GetComponent<Soul>().bullet_size[2]);

        Instantiate(bolt, canon.transform.position, Quaternion.Euler(0f, 0f, 0f));

    }

}
