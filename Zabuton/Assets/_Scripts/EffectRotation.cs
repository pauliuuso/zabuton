using UnityEngine;
using System.Collections;

public class EffectRotation : MonoBehaviour 
{

    public float rotation;


	void Update () 
    {
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(+rotation, 0f, 0f));
	}
}
