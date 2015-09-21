using UnityEngine;
using System.Collections;

public class BulletMover : MonoBehaviour
{

    GameController settings = new GameController();

    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * settings.p_bullet_speed;
    }

}
