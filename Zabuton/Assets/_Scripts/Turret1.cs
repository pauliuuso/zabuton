using UnityEngine;
using System.Collections;

public class Turret1 : MonoBehaviour 
{
    public bool hasRadar = false;

    private int nextAngle;
    private float currentTime;
    private float nextFire = 4;
    private bool readyToFire = false;

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
            nextFire += 4;
        }

        if(!hasRadar)
        {
            if (Mathf.Round(gameObject.transform.localEulerAngles.y) < nextAngle)
            {
                gameObject.transform.Rotate(Vector3.forward * (int)(Time.deltaTime * 100));
            }
            else if (Mathf.Round(gameObject.transform.localEulerAngles.y) > nextAngle)
            {
                gameObject.transform.Rotate(Vector3.forward * (int)(Time.deltaTime * -100));
            }
            if (Mathf.Round(gameObject.transform.localEulerAngles.y) == nextAngle)
            {
                if (GetComponentInParent<Soul>().notMoving && readyToFire)
                {
                    print("fire");
                    readyToFire = false;
                    updateAngle();
                }
            }
        }
    }

    private void updateAngle()
    {
        nextAngle = (int)Random.Range(0, 360);
    }

}
