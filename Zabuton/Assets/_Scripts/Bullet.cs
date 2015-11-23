﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bullet : MonoBehaviour
{
    public int devast;
    public string type;
    public string owner;
    public bool addBoom = false;
    public int fireLevel;
    public int iceLevel;
    public int poisonLevel;
    public List<string> effects;
    public GameObject[] fireBooms;
    public GameObject[] iceBooms;
    public GameObject[] poisonBooms;
    public bool fromEffects = false;
    public bool particleBolt = false;
    public bool damageDone = false;

    void Start()
    {
        /*if(owner == "player" && !fromEffects)
        {
            fireLevel = Settings.p_fire_level;
            iceLevel = Settings.p_ice_level;
            poisonLevel = Settings.p_poison_level;
        }*/

    }

    void OnTriggerExit(Collider other)
    {
        if (gameObject.tag == "Untagged")
        {
            gameObject.tag = "Bolt";
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

    public void particleBoltHit(Vector3 location)
    {
        if (type == "fire") Instantiate(fireBooms[fireLevel], location, fireBooms[fireLevel].transform.rotation);
        else if (type == "ice") Instantiate(iceBooms[iceLevel], location, iceBooms[iceLevel].transform.rotation);
        else if (type == "poison") Instantiate(poisonBooms[poisonLevel], location, poisonBooms[poisonLevel].transform.rotation);
    }

    void OnApplicationQuit()
    {
        addBoom = false;
    }

}
