using UnityEngine;
using System.Collections;

public class Enemy1 : MonoBehaviour 
{
    bool initialized = false;
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
    float randomas = 2;
    float nextMove = 2;
    Ray downRay;
    Ray leftRay;
    Ray upRay;
    Ray rightRay;
    RaycastHit hit;
    int sightDown = -15;
    int sightLeft = -10;
    int sightUp = 10;
    int sightRight = 10;
    bool dodging = false;
    int threatNumber; // 0 - bottom, 1 -left, 2 -up, 3 - right;

	void Start () 
    {
        speed = gameObject.GetComponent<Soul>().speed;
        tilt = gameObject.GetComponent<Soul>().tilt;
        setPosition();
        downRay = new Ray(gameObject.transform.position, Vector3.down * 15);
        leftRay = new Ray(gameObject.transform.position, Vector3.left * 10);
        upRay = new Ray(gameObject.transform.position, Vector3.up * 10);
        rightRay = new Ray(gameObject.transform.position, Vector3.right * 10);
        //movingDown = true;
	}

	void FixedUpdate () 
    {
        //Debug.DrawRay(gameObject.transform.position, new Vector3(0.0f, 0.0f, sightDown), Color.green);
        Debug.DrawRay(gameObject.transform.position, Vector3.left * 10, Color.green);
        //Debug.DrawRay(gameObject.transform.position, new Vector3(0.0f, 0.0f, sightUp), Color.green);
        //Debug.DrawRay(gameObject.transform.position, new Vector3(sightRight, 0.0f, 0.0f), Color.green);
        
        Vector3 movement = new Vector3(horizontalMovement, 0.0f, verticalMovement); // Vector3(x, y, z); Nustatoma kuria kryptimi juda
        GetComponent<Rigidbody>().velocity = movement * speed; // Cia vyksta pats judejimas

        if (gameObject.transform.position.z < Settings.zMax && !initialized)
        {
            initialized = true;
            movingDown = false;
        }

        /*if (Physics.Raycast(downRay, out hit, 10))
        {
            if(hit.collider.tag != "Untagged")
            {
                dodging = true;
                threatNumber = 0;
                //print("down");
            }
        }*/
        if (Physics.Raycast(leftRay, out hit, 10))
        {
            if(hit.collider.tag != "Untagged")
            {
                dodging = true;
                threatNumber = 1;
                print("left");
            }
        }
        /*if (Physics.Raycast(upRay, out hit, 10))
        {
            if (hit.collider.tag != "Untagged")
            {
                dodging = true;
                threatNumber = 2;
                //print("up");
            }
        }*/
        /*if (Physics.Raycast(rightRay, out hit, 10))
        {
            if (hit.collider.tag != "Untagged")
            {
                dodging = true;
                threatNumber = 3;
                print("right");
            }
        }*/
        else if(dodging)
        {
            dodging = false;
            threatNumber = 4;
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

        GetComponent<Rigidbody>().rotation = Quaternion.Euler(270f + movingSideways * tilt, 90f, gameObject.transform.rotation.z); // Cia kai juda i sonus, kad pasisuktu laivas i sona


        if (initialized && !dodging)
        {
            GetComponent<Rigidbody>().position = new Vector3 // Cia yra nustatomos ribos, kad negaletu isskristi uz ekrano
            (
                Mathf.Clamp(GetComponent<Rigidbody>().position.x, Settings.xMin, Settings.xMax), // Nustato kad laivas negaletu palikt ribu
                0.0f,
                Mathf.Clamp(GetComponent<Rigidbody>().position.z, Settings.zMin, Settings.zMax)
            );

            if (newPosition.x > gameObject.transform.position.x) movingRight = true;
            else movingRight = false;
            if (newPosition.x < gameObject.transform.position.x - 0.5) movingLeft = true;
            else movingLeft = false;
            if (newPosition.z > gameObject.transform.position.z) movingUp = true;
            else movingUp = false;
            if (newPosition.z < gameObject.transform.position.z - 0.5) movingDown = true;
            else movingDown = false;

            if(Time.time > nextMove)
            {
                setPosition();
                nextMove = Time.time + randomas;
            }
        }
        else if(dodging)
        {
            if (threatNumber == 0) movingRight = true;
            else movingRight = false;
            if (threatNumber == 1) movingDown = true;
            else movingDown = false;
            if (threatNumber == 2) movingLeft = true;
            else movingLeft = false;
            if (threatNumber == 3) movingUp = true;
            else movingUp = false;
        }


	}

    void setPosition()
    {
        newPosition = new Vector3(Random.Range(Settings.xMin, Settings.zMax), 0.0f, Random.Range(-4, Settings.zMax));
        randomas = Random.Range(1f, 4f);
    }

}
