using UnityEngine;
using System.Collections;

public class AddMetalParts : MonoBehaviour 
{

    public int partCount = 5;
    public GameObject Part1;
    public GameObject Part2;
    public GameObject Part3;
    public GameObject Part4;

    private GameObject[] PartList = new GameObject[4];

	void Start () 
    {
        PartList[0] = Part1;
        PartList[1] = Part2;
        PartList[2] = Part3;
        PartList[3] = Part4;

	    for (int a = 0; a < partCount; a++)
        {
            GameObject currentPart = (GameObject)Instantiate(PartList[Random.Range(0,3)], new Vector3 (gameObject.transform.position.x, 0f, gameObject.transform.position.z), gameObject.transform.rotation);
        }
	}

}
