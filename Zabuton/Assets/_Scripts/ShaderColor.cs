using UnityEngine;
using System.Collections;

public class ShaderColor : MonoBehaviour 
{
    public float red;
    public float green;
    public float blue;
    public float brightness;

	void Start () 
    {
        Renderer renderer = gameObject.GetComponent<Renderer>();
        renderer.material.color = new Color(red, green, blue, brightness);
	}
	

}
