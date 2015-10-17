using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public int devast;
    public string type;
    public bool addBoom = true;
    public GameObject[] fireBooms;
    public GameObject[] iceBooms;
    public GameObject[] poisonBooms;

    void OnDestroy()
    {
        if(addBoom)
        {
            if (type == "fire") Instantiate(fireBooms[Settings.p_fire_level], new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 5, gameObject.transform.position.z + 0.2f), fireBooms[Settings.p_fire_level].transform.rotation);
            else if (type == "ice") Instantiate(iceBooms[Settings.p_ice_level], new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 5, gameObject.transform.position.z + 0.2f), iceBooms[Settings.p_ice_level].transform.rotation);
            else if (type == "poison") Instantiate(poisonBooms[Settings.p_poison_level], new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 5, gameObject.transform.position.z + 0.2f), poisonBooms[Settings.p_poison_level].transform.rotation);
        }
    }

}
