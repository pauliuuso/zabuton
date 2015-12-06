using UnityEngine;
using System.Collections;

public class RandomScale : MonoBehaviour 
{

    public float randomScale1 = 1f;
    public float randomScale2 = 2f;

	void Start () 
    {
        float realScale = Random.Range(randomScale1, randomScale2);
        gameObject.transform.localScale = new Vector3 (realScale, realScale, realScale);
	}

}
