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
    public GameObject canon1;
    public GameObject canon2;
    public GameObject bolt;
    public Material boltMaterial;
    private float yRotation;

	void Start () 
    {
        tilt = gameObject.GetComponent<Soul>().tilt;
        setPosition();
        fightingTime = Random.Range(10, 30);
        yRotation = gameObject.transform.eulerAngles.y;
	}

	void FixedUpdate () 
    {
        Debug.DrawRay(gameObject.transform.position, Vector3.forward * -sightDown, rayColorDown);
        Debug.DrawRay(gameObject.transform.position, Vector3.left * sightLeft, rayColorLeft);
        Debug.DrawRay(gameObject.transform.position, Vector3.forward * sightUp, rayColorUp);
        Debug.DrawRay(gameObject.transform.position, Vector3.right * sightRight, rayColorRight);

        currentTime += Time.deltaTime;

        //downRay = new Ray(gameObject.transform.position, Vector3.forward * -sightDown);
        //leftRay = new Ray(gameObject.transform.position, Vector3.left * sightLeft);
        //upRay = new Ray(gameObject.transform.position, Vector3.forward * sightUp);
        //rightRay = new Ray(gameObject.transform.position, Vector3.right * sightRight);
        
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

        /*if (Physics.Raycast(leftRay, out hit, sightLeft))
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


        /*if (Physics.Raycast(downRay, out hit, sightDown))
        {
            if(hit.collider.tag != "Untagged")
            {
                rayColorDown = Color.red;
                dodging = true;
                downThreat = true;
            }
        }
        else if (!Physics.Raycast(downRay, out hit, sightDown))
        {
            downThreat = false;
            rayColorDown = Color.green;
        }
        if (Physics.Raycast(upRay, out hit, sightUp))
        {
            if (hit.collider.tag != "Untagged")
            {
                rayColorUp = Color.red;
                dodging = true;
                upThreat = true;
            }
        }
        if (!Physics.Raycast(upRay, out hit, sightUp))
        {
            upThreat = false;
            rayColorUp = Color.green;
        }

        if (!Physics.Raycast(leftRay, out hit, sightLeft) && !Physics.Raycast(rightRay, out hit, sightRight) && !Physics.Raycast(upRay, out hit, sightUp) && !Physics.Raycast(downRay, out hit, sightDown) && dodging)
        {
            dodging = false;
        }*/

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

        if(gameObject.GetComponent<Soul>().ship_name != "enemy7") gameObject.transform.rotation = Quaternion.Euler(270f + movingSideways * tilt, yRotation, gameObject.transform.rotation.z); // Cia kai juda i sonus, kad pasisuktu laivas i sona
        else if (gameObject.GetComponent<Soul>().ship_name == "enemy7")
        {
            gameObject.transform.rotation = Quaternion.Euler(gameObject.transform.localEulerAngles.x, yRotation, 90f + movingSideways * tilt); 
        }

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



            if(Time.time > nextMove)
            {
                setPosition();
                nextMove = Time.time + randomas;
                fire();
            }
            if(gameObject.GetComponent<Soul>().collidingWithSame)
            {
                setPosition();
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

    void fire()
    {
        if(!gameObject.GetComponent<Soul>().particle_bolt)
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

            if (Random.Range(0f, 1f) > 0.5f)
            {

                Instantiate(bolt, canon1.transform.position, Quaternion.Euler(0f, 0f, 0f));
            }
            else
            {
                Instantiate(bolt, canon2.transform.position, Quaternion.Euler(0f, 0f, 0f));
            }
        }
        else
        {
            bolt.GetComponent<Bullet>().effects.Clear(); // pirma isvalom effektu lista

            bolt.GetComponent<Bullet>().particleBolt = true;
            bolt.GetComponent<Bullet>().devast = gameObject.GetComponent<Soul>().bullet_devast;
            bolt.GetComponent<Bullet>().owner = "enemy";
            bolt.GetComponent<Bullet>().type = gameObject.GetComponent<Soul>().bullet_type;
            bolt.GetComponent<Bullet>().fireLevel = gameObject.GetComponent<Soul>().fire_level;
            bolt.GetComponent<Bullet>().iceLevel = gameObject.GetComponent<Soul>().ice_level;
            bolt.GetComponent<Bullet>().poisonLevel = gameObject.GetComponent<Soul>().poison_level;
            bolt.GetComponent<Bullet>().effects.Add(GetComponent<Soul>().effect);
            GameObject boltClone = (GameObject)Instantiate(bolt, new Vector3(canon1.transform.position.x, canon1.transform.position.y, canon1.transform.position.z - 15) , bolt.transform.rotation);
            boltClone.transform.parent = gameObject.transform;
        }


    }

}
