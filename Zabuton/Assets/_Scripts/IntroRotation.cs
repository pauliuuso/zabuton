using UnityEngine;
using System.Collections;

public class IntroRotation : MonoBehaviour 
{
    private float currentRotationY = 0f;
    private float currentRotationZ = 0f;

	void Update () 
    {
        currentRotationY += Time.deltaTime;
        currentRotationZ += Time.deltaTime * 2;

        gameObject.transform.rotation = Quaternion.Euler(gameObject.transform.rotation.x, currentRotationY * 2, currentRotationZ);

	}
}
