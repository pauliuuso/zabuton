using UnityEngine;
using System.Collections;

public class Enemy4 : MonoBehaviour 
{
    public GameObject turret;
    private GameObject turretClone;
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
    float currentTime;
    float fightingTime;
    Ray downRay;
    Ray leftRay;
    Ray upRay;
    Ray rightRay;
    RaycastHit hit;
    int sightDown = 10;
    int sightLeft = 10;
    int sightUp = 15;
    int sightRight = 10;
    bool dodging = false;
    bool retreating = false;
    bool leftThreat = false;
    bool upThreat = false;
    bool rightThreat = false;
    bool downThreat = false;
    Color rayColorLeft = Color.green;
    Color rayColorRight = Color.green;
    Color rayColorUp = Color.green;
    Color rayColorDown = Color.green;

    private float yRotation;

	void Start () 
    {
        tilt = gameObject.GetComponent<Soul>().tilt;
        setPosition();
        fightingTime = Random.Range(20, 50);
        fightingTime = 200;
        yRotation = gameObject.transform.eulerAngles.y;
        turretClone = Instantiate(turret, gameObject.transform.position, turret.transform.rotation) as GameObject;
        turretClone.transform.parent = gameObject.transform;
        turretClone.transform.position = new Vector3 (gameObject.transform.position.x , 0.66f, gameObject.transform.position.z);
        turretClone.GetComponent<Turret1>().owner = "enemy";
	}

	void FixedUpdate () 
    {
        Debug.DrawRay(gameObject.transform.position, Vector3.forward * -sightDown, rayColorDown);
        Debug.DrawRay(gameObject.transform.position, Vector3.left * sightLeft, rayColorLeft);
        Debug.DrawRay(gameObject.transform.position, Vector3.forward * sightUp, rayColorUp);
        Debug.DrawRay(gameObject.transform.position, Vector3.right * sightRight, rayColorRight);

        //leftRay = new Ray(gameObject.transform.position, Vector3.left * sightLeft);
        //rightRay = new Ray(gameObject.transform.position, Vector3.right * sightRight);

       /* if (Physics.Raycast(leftRay, out hit, sightLeft))
        {
            if (hit.collider.tag != "Untagged")
            {
                rayColorLeft = Color.red;
                leftThreat = true;
                movingLeft = false;
            }
        }
        else if (!Physics.Raycast(leftRay, out hit, sightLeft))
        {
            leftThreat = false;
            rayColorLeft = Color.green;
        }
        if (Physics.Raycast(rightRay, out hit, sightRight))
        {
            if (hit.collider.tag != "Untagged")
            {
                rayColorRight = Color.red;
                rightThreat = true;
                movingRight = false;
            }
        }
        else if (!Physics.Raycast(rightRay, out hit, sightRight))
        {
            rightThreat = false;
            rayColorRight = Color.green;
        }*/

        currentTime += Time.deltaTime;
        
        Vector3 movement = new Vector3(horizontalMovement, 0.0f, verticalMovement); // Vector3(x, y, z); Nustatoma kuria kryptimi juda
        GetComponent<Rigidbody>().velocity = movement * gameObject.GetComponent<Soul>().speed; // Cia vyksta pats judejimas

        if(currentTime > fightingTime)
        {
            retreating = true;
            fightingTime += currentTime;
            setPosition();
        }

        if (gameObject.transform.position.z < Settings.zMax && !initialized)
        {
            initialized = true;
            movingDown = false;
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

        gameObject.transform.rotation = Quaternion.Euler(gameObject.transform.rotation.x, yRotation, gameObject.transform.rotation.z + movingSideways * tilt); // Cia kai juda i sonus, kad pasisuktu laivas i sona

        if (initialized && !dodging)
        {
            if(!retreating)
            {
                GetComponent<Rigidbody>().position = new Vector3 // Cia yra nustatomos ribos, kad negaletu isskristi uz ekrano
                (
                    Mathf.Clamp(GetComponent<Rigidbody>().position.x, Settings.xMin, Settings.xMax), // Nustato kad laivas negaletu palikt ribu
                    0.0f,
                    Mathf.Clamp(GetComponent<Rigidbody>().position.z, Settings.zMin, Settings.zMax)
                );
            }


            if (newPosition.x > gameObject.transform.position.x) movingRight = true;
            else movingRight = false;
            if (newPosition.x < gameObject.transform.position.x - 0.5) movingLeft = true;
            else movingLeft = false;
            if (newPosition.z > gameObject.transform.position.z) movingUp = true;
            else movingUp = false;
            if (newPosition.z < gameObject.transform.position.z - 0.5) movingDown = true;
            else movingDown = false;
            if (!movingDown && !movingLeft) gameObject.GetComponent<Soul>().notMoving = true;
            else gameObject.GetComponent<Soul>().notMoving = false;

            if(Time.time > nextMove)
            {
                setPosition();
                nextMove = Time.time + randomas + 5;
            }
            if (gameObject.GetComponent<Soul>().collidingWithSame)
            {
                nextMove = Time.time + 1f;
            }
        }
        else if(initialized && (dodging || retreating))
        {

            if(!retreating)
            {
                GetComponent<Rigidbody>().position = new Vector3 // Cia yra nustatomos ribos, kad negaletu isskristi uz ekrano
                (
                    Mathf.Clamp(GetComponent<Rigidbody>().position.x, Settings.xMin, Settings.xMax), // Nustato kad laivas negaletu palikt ribu
                    0.0f,
                    Mathf.Clamp(GetComponent<Rigidbody>().position.z, Settings.zMin, Settings.zMax)
                );
            }
            else if (retreating)
            {
                GetComponent<Rigidbody>().position = new Vector3 // Cia yra nustatomos ribos, kad negaletu isskristi uz ekrano
                (
                    Mathf.Clamp(GetComponent<Rigidbody>().position.x, Settings.xMin, Settings.xMax), // Nustato kad laivas negaletu palikt ribu
                    0.0f,
                    Mathf.Clamp(GetComponent<Rigidbody>().position.z, -40, Settings.zMax)
                );
            }

            if (rightThreat && leftThreat && upThreat && !retreating)
            {
                if (!downThreat)
                {
                    movingRight = false;
                    movingUp = false;
                    movingLeft = false;
                    movingDown = true;
                }
            }
            else if (rightThreat && leftThreat && !retreating)
            {
                if (!upThreat)
                {
                    movingRight = false;
                    movingUp = true;
                    movingLeft = false;
                    movingDown = false;
                }
                else if (!downThreat)
                {
                    movingRight = false;
                    movingUp = false;
                    movingLeft = false;
                    movingDown = true;
                }
            }
            else if ((upThreat || downThreat) && (!leftThreat || !rightThreat) && !retreating)
            {
                if(Random.Range(0f, 1f) > 0.5f)
                {
                    if (!leftThreat)
                    {
                        movingRight = true;
                        movingUp = false;
                        movingLeft = false;
                        movingDown = false;
                    }
                    else if (!rightThreat)
                    {
                        movingRight = false;
                        movingUp = false;
                        movingLeft = true;
                        movingDown = false;
                    }
                }
                else if(Random.Range(0f, 1f) <= 0.5f)
                {
                    if (!rightThreat)
                    {
                        movingRight = false;
                        movingUp = false;
                        movingLeft = true;
                        movingDown = false;
                    }
                    else if (!leftThreat)
                    {
                        movingRight = true;
                        movingUp = false;
                        movingLeft = false;
                        movingDown = false;
                    }
                }

            }
            else if (leftThreat) 
            {
                if(!rightThreat) 
                {
                    movingRight = true;
                    movingUp = false;
                    movingLeft = false;
                    movingDown = false;
                }
            }
            else if (rightThreat)
            {
                if (!leftThreat)
                {
                    movingRight = false;
                    movingUp = false;
                    movingLeft = true;
                    movingDown = false;
                }
            }


        }


	}

    void setPosition()
    {
        if(!retreating) newPosition = new Vector3(Random.Range(Settings.xMin, Settings.xMax), 0.0f, Random.Range(0, Settings.zMax));
        else newPosition = new Vector3(Random.Range(Settings.xMin, Settings.xMax), 0.0f, Random.Range(-30f, -31f));
        randomas = Random.Range(1f, 4f);
    }

    void notMoving()
    {
        movingLeft = false;
        movingUp = false;
        movingRight = false;
        movingDown = false;
    }


}
