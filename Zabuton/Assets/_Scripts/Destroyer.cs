using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour
{

    void OnTriggerExit(Collider other) // Kai bet koks objektas iseina uz priskirto sitam scriptui triggerio ribu
    {
        if (gameObject.tag == "Boundary") Destroy(other.gameObject); //jis sunaikinamas jei iseina is boundary objekto
        if (other.tag == "Bolt") other.GetComponent<Bullet>().addBoom = false;
        if (other.tag == "Asteroid" || other.tag == "Asteroid2" || other.tag == "Enemy") other.GetComponent<Soul>().addBoom = false;
    }
}
