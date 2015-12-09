using UnityEngine;
using System.Collections;

public class AddAsteroidParts : MonoBehaviour 
{

    public int partCount = 5;
    public GameObject Part1;
    public GameObject Part2;

    private GameObject[] PartList = new GameObject[2];

	void Start () 
    {
        PartList[0] = Part1;
        PartList[1] = Part2;

	    for (int a = 0; a < partCount; a++)
        {
            GameObject currentPart = (GameObject)Instantiate(PartList[Random.Range(0, 1)], new Vector3(gameObject.transform.position.x + Random.Range(-1, 1), 0f, gameObject.transform.position.z + Random.Range(-1, 1)), gameObject.transform.rotation);
        }
	}

}
