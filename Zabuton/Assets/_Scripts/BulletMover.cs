using UnityEngine;
using System.Collections;

public class BulletMover : MonoBehaviour
{

    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * Settings.p_bullet_speed;
    }

}
