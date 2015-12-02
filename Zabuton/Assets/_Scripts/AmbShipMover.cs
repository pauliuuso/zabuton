using UnityEngine;
using System.Collections;

public class AmbShipMover : MonoBehaviour
{
    public float fixedSpeed = 0;
    public float fixedScale = 0;
    public float randomSpeed1;
    public float randomSpeed2;
    public float randomScale1;
    public float randomScale2;
    private float scale;


    void Awake()
    {
        if (fixedScale == 0) scale = Random.Range(randomScale1, randomScale2);
        else scale = fixedScale;
        gameObject.transform.localScale = new Vector3(scale, scale, scale);

        if (fixedSpeed == 0) fixedSpeed = Random.Range(randomSpeed1, randomSpeed2);
        if(fixedSpeed != 0) GetComponent<Rigidbody>().velocity = transform.right * -fixedSpeed;
    }

}
