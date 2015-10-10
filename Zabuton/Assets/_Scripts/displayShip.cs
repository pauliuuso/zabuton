using UnityEngine;
using System.Collections;

public class displayShip : MonoBehaviour
{

    private Renderer render;
    public Material[] materials;
    public Mesh[] meshes;

    void Start ()
    {
        render = GetComponent<Renderer>();
        updateShip();
	}

    public void updateShip()
    {
        if (Settings.p_ship_level == 1) render.sharedMaterial = materials[0];
        else if (Settings.p_ship_level == 2) render.sharedMaterial = materials[1];
        else if (Settings.p_ship_level == 3)
        {
            GetComponent<MeshFilter>().sharedMesh = meshes[0];
            render.sharedMaterial = materials[2];
            gameObject.transform.position = new Vector3(-5f, 0f, -8f);
        }
    }
}
