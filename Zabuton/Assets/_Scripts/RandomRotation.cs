using UnityEngine;
using System.Collections;

public class RandomRotation : MonoBehaviour
{
    public float rotation = 100;

    void Start()
    {
        GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * rotation; // Sukuriamas random point tarp sferos pagal ji rotation sukuriama
    }

    

}
