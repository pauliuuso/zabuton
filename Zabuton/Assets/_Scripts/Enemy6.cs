using UnityEngine;
using System.Collections;

public class Enemy6 : MonoBehaviour
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
    Ray downRay;
    RaycastHit hit;
    int sightDown = 15;
    Color rayColorDown = Color.green;
    private float yRotation;
    private bool readyToChange = true;
    private float nextFire = 4;

    void Start()
    {
        tilt = gameObject.GetComponent<Soul>().tilt;
        yRotation = gameObject.transform.eulerAngles.y;
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
            if (hit.collider.tag == "Player_ship")
            {
                rayColorDown = Color.red;
                movingDown = false;
                if (Random.Range(0f, 1f) > 0.5f) movingLeft = true;
                else movingRight = true;
                readyToChange = false;
            }
        }
        else if (!Physics.Raycast(downRay, out hit, sightDown) && readyToChange)
        {
            rayColorDown = Color.green;
            movingDown = true;
            movingLeft = false;
            movingRight = false;
        }

        if (nextFire < currentTime)
        {
            nextFire += Random.Range(3f, 5f);
            readyToChange = true;
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

        //GetComponent<Rigidbody>().rotation = Quaternion.Euler(270f + movingSideways * tilt, gameObject.transform.rotation.z + 90f, gameObject.transform.rotation.z); // Cia kai juda i sonus, kad pasisuktu laivas i sona
        gameObject.transform.rotation = Quaternion.Euler(270f + movingSideways * tilt, yRotation, gameObject.transform.rotation.z); // Cia kai juda i sonus, kad pasisuktu laivas i sona


    }



}
