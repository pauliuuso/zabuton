using UnityEngine;
using System.Collections;

public class Texturer : MonoBehaviour
{

    public Material[] materials;
    public Mesh[] meshes;
    public GameObject[] asteroids;
    private Renderer render;
    private int randomas = 0;

    void Start()
    {
        updateRandom();
        render = GetComponent<Renderer>();
        if (gameObject.tag == "Asteroid")
        {
            render.sharedMaterial = materials[(int)Mathf.Round(Random.Range(0, 4))];
            GetComponent<MeshFilter>().sharedMesh = meshes[(int)Mathf.Round(Random.Range(0, 4))];
        }
        else if (gameObject.tag == "Asteroid2")
        {
            render.sharedMaterial = materials[randomas];
            GetComponent<MeshFilter>().sharedMesh = meshes[randomas];
            updateRandom();
        }
    }

    void updateRandom()
    {
        randomas = (int)Mathf.Round(Random.Range(0, materials.Length));
    }

}
