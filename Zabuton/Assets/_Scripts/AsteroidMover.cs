using UnityEngine;
using System.Collections;

public class AsteroidMover : MonoBehaviour
{
    private float asteroidSpeed = Random.value * 10 + 5;

    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * -asteroidSpeed;
    }

}
