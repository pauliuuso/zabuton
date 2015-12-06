using UnityEngine;
using System.Collections;

public class RandomMover : MonoBehaviour 
{

    public float randomSpeed1 = 5f;
    public float randomSpeed2 = 10f;
    public float fixedSpeed = 0f;


	void Start () 
    {
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);

        float realSpeed = Random.Range(randomSpeed1, randomSpeed2);
        if (fixedSpeed == 0) gameObject.GetComponent<Rigidbody>().velocity = new Vector3(randomX, 0f, randomY) * realSpeed;
        else gameObject.GetComponent<Rigidbody>().velocity = new Vector3(randomX, 0f, randomY) * fixedSpeed;


	}
	
}
