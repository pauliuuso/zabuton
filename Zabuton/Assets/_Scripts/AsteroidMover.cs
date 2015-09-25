using UnityEngine;
using System.Collections;

public class AsteroidMover : MonoBehaviour
{
    private float asteroidSpeed = Random.value * 8 + 5;

    void Awake()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * -asteroidSpeed;
    }

}
