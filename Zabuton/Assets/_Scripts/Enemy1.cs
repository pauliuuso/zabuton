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

	void Start () 
    {
        speed = gameObject.GetComponent<Soul>().speed;
        tilt = gameObject.GetComponent<Soul>().tilt;
        //movingDown = true;
	}

	void FixedUpdate () 
    {
        
        Vector3 movement = new Vector3(horizontalMovement, 0.0f, verticalMovement); // Vector3(x, y, z); Nustatoma kuria kryptimi juda
        GetComponent<Rigidbody>().velocity = movement * speed; // Cia vyksta pats judejimas

        if (gameObject.transform.position.z < Settings.zMax && !initialized)
        {
            movingRight = true;

            initialized = true;
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


        if (initialized)
        {
            GetComponent<Rigidbody>().position = new Vector3 // Cia yra nustatomos ribos, kad negaletu isskristi uz ekrano
            (
                Mathf.Clamp(GetComponent<Rigidbody>().position.x, Settings.xMin, Settings.xMax), // Nustato kad laivas negaletu palikt ribu
                0.0f,
                Mathf.Clamp(GetComponent<Rigidbody>().position.z, Settings.zMin, Settings.zMax)
            );
        }


	}

}
