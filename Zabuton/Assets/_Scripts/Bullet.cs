﻿using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public int devast;
    public string type;
    public string owner;
    public bool addBoom = false;
    public int fireLevel;
    public int iceLevel;
    public int poisonLevel;
    public GameObject[] fireBooms;
    public GameObject[] iceBooms;
    public GameObject[] poisonBooms;

    void Start()
    {
        if(owner == "player")
        {
            fireLevel = Settings.p_fire_level;
            iceLevel = Settings.p_ice_level;
            poisonLevel = Settings.p_poison_level;
        }

    }

    void OnDestroy()
    {
        if(addBoom)
        {
            if (type == "fire") Instantiate(fireBooms[fireLevel], new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 5, gameObject.transform.position.z + 0.2f), fireBooms[fireLevel].transform.rotation);
            else if (type == "ice") Instantiate(iceBooms[iceLevel], new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 5, gameObject.transform.position.z + 0.2f), iceBooms[iceLevel].transform.rotation);
            else if (type == "poison") Instantiate(poisonBooms[poisonLevel], new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 5, gameObject.transform.position.z + 0.2f), poisonBooms[poisonLevel].transform.rotation);
        }
    }

    void OnApplicationQuit()
    {
        addBoom = false;
    }

}
