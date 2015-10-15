using UnityEngine;
using System.Collections;

public class AsteroidMover : MonoBehaviour
{
    private float asteroidSpeed;
    public float fixedSpeed = 0;

    void Awake()
    {
        asteroidSpeed = Random.value * 8 + 5;
        GetComponent<Rigidbody>().velocity = transform.forward * -asteroidSpeed;
        if (gameObject.tag == "Saturn") GetComponent<Rigidbody>().velocity = transform.up * fixedSpeed;
    }

}
