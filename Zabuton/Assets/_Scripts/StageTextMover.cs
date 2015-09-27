using UnityEngine;
using System.Collections;

public class StageTextMover : MonoBehaviour
{
    public float showTime;
    void Awake()
    {
        gameObject.GetComponent<Rigidbody>().velocity = transform.up;
    }
    void Update()
    {
        showTime -= Time.deltaTime;
        if (showTime < 0) Destroy(gameObject);
    }
}
