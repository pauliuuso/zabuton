using UnityEngine;
using System.Collections;

public class Enemy3 : MonoBehaviour
{
    float horizontalMovement = 0f;
    float verticalMovement = -1f;
    bool movingLeft = false;
    bool movingUp = false;
    bool movingRight = false;
    bool movingDown = true;
    int speed;
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
    private float nextFire = 0.5f;
    private float yRotation;

    void Start()
    {
        yRotation = gameObject.transform.eulerAngles.y;
    }

    void FixedUpdate()
    {
        Debug.DrawRay(gameObject.transform.position, Vector3.forward * -sightDown, rayColorDown);

        currentTime += Time.deltaTime;

        downRay = new Ray(gameObject.transform.position, Vector3.forward * -sightDown);

        Vector3 movement = new Vector3(horizontalMovement, 0.0f, verticalMovement); // Vector3(x, y, z); Nustatoma kuria kryptimi juda
        GetComponent<Rigidbody>().velocity = movement * gameObject.GetComponent<Soul>().speed; // Cia vyksta pats judejimas

        if (gameObject.transform.position.z < 7 && gameObject.transform.position.x < 0) movingRight = true;
        else if (gameObject.transform.position.z < 7 && gameObject.transform.position.x > 0) movingLeft = true;

        if(movingRight)
        {
            if (gameObject.transform.eulerAngles.y > 240)
            {
                gameObject.transform.Rotate(Vector3.right * Time.deltaTime * 30);
                gameObject.transform.Rotate(Vector3.down * Time.deltaTime * 30);
            }

        }
        if (movingLeft)
        {
            if (gameObject.transform.eulerAngles.y < 310)
            {
                gameObject.transform.Rotate(Vector3.right * Time.deltaTime * -30);
                gameObject.transform.Rotate(Vector3.down * Time.deltaTime * -30);
            }
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

        Instantiate(bolt, canon.transform.position, canon.transform.rotation);

    }

}
