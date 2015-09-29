using UnityEngine;
using System.Collections;

public class displayShip : MonoBehaviour
{

    private Renderer render;
    public Material[] materials;

    void Start ()
    {
        render = GetComponent<Renderer>();
        updateShip();
	}
	
    public void updateShip()
    {
        if(Settings.p_ship_level == 2) render.sharedMaterial = materials[0];
    }
}
