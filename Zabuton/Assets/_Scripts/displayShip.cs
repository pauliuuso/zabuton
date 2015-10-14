using UnityEngine;
using System.Collections;

public class displayShip : MonoBehaviour
{

    public GameObject[] ships;

    void Start ()
    {
       // updateShip();
	}

    public void updateShip()
    {
        /*if (Settings.p_ship_level == 1) render.sharedMaterial = materials[0];
        else if (Settings.p_ship_level == 2) render.sharedMaterial = materials[1];
        else if (Settings.p_ship_level == 3)
        {
            GetComponent<MeshFilter>().sharedMesh = meshes[0];
            render.sharedMaterial = materials[2];
            gameObject.transform.position = new Vector3(-5f, 0f, -8f);
        }
        */
        GameObject currentShip = Instantiate(ships[Settings.p_ship_level - 1], gameObject.transform.position, Quaternion.Euler(330f, 12f, 40f)) as GameObject;
        currentShip.transform.localScale = new Vector3(2f, 2f, 2f);
        currentShip.transform.parent = gameObject.transform;
        if(transform.childCount > 1) Destroy(transform.GetChild(0).gameObject);
    }
}
