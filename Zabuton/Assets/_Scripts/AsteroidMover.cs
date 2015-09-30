using UnityEngine;
using System.Collections;

public class AsteroidMover : MonoBehaviour
{
    private float asteroidSpeed;

    void Awake()
    {
        asteroidSpeed = Random.value * 8 + 5;
        GetComponent<Rigidbody>().velocity = transform.forward * -asteroidSpeed;
    }

}
