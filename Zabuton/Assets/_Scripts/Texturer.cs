using UnityEngine;
using System.Collections;

public class Texturer : MonoBehaviour
{

    public Material[] materials;
    public Mesh[] meshes;
    private Renderer render;

    void Start()
    {
        render = GetComponent<Renderer>();
        if (gameObject.tag == "Asteroid")
        {
            render.sharedMaterial = materials[(int)Mathf.Round(Random.Range(0, 4))];
            GetComponent<MeshFilter>().sharedMesh = meshes[(int)Mathf.Round(Random.Range(0, 4))];
        }
        else if (gameObject.tag == "Asteroid2")
        {
            render.sharedMaterial = materials[(int)Mathf.Round(Random.Range(0, 1))];
            GetComponent<MeshFilter>().sharedMesh = meshes[(int)Mathf.Round(Random.Range(0, 1))];
        }
    }
}
