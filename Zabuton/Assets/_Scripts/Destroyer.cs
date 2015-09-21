using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour
{

    void OnTriggerExit(Collider other) // Kai bet koks objektas iseina uz priskirto sitam scriptui triggerio ribu
    {
        Destroy(other.gameObject); //jis sunaikinamas
    }
}
