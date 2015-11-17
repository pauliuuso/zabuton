using UnityEngine;
using System.Collections;

public class Enemy3 : MonoBehaviour
{
    private float horizontalMovement = 0f;
    private float verticalMovement = -1f;
    private bool movingLeft = false;
    private bool movingUp = false;
    private bool movingRight = false;
    private bool movingDown = true;
    private int speed;
    private float currentTime;



    void Start()
    {
        movingDown = true;
    }

    void FixedUpdate()
    {

        currentTime += Time.deltaTime;


        Vector3 movement = new Vector3(horizontalMovement, 0.0f, verticalMovement); // Vector3(x, y, z); Nustatoma kuria kryptimi juda
        GetComponent<Rigidbody>().velocity = movement * gameObject.GetComponent<Soul>().speed; // Cia vyksta pats judejimas


        if (movingLeft && horizontalMovement >= -1f) horizontalMovement -= 0.1f;
        if (movingUp && verticalMovement <= 1f) verticalMovement += 0.1f;
        if (movingRight && horizontalMovement <= 1f) horizontalMovement += 0.1f;
        if (movingDown && verticalMovement >= -1f) verticalMovement -= 0.1f;

        if (!movingLeft && !movingRight) horizontalMovement = 0f;
        if (!movingUp && !movingDown) verticalMovement = 0f;
    }

}
