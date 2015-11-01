using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour 
{
    public float xRotate = 0f;
    public float yRotate = 0f;
    public float zRotate = 0f;
    public float rotationSpeed = 1;
    public bool startRotation;

    void Start()
    {
        if(startRotation) gameObject.transform.rotation = Quaternion.Euler(new Vector3(Random.Range(0f, 180f), Random.Range(0f, 180f), Random.Range(0f, 180f)));
    }

    void Update()
    {
        gameObject.transform.Rotate(new Vector3(xRotate, yRotate, zRotate), Time.deltaTime * rotationSpeed);
    }

}
