﻿using UnityEngine;
using System.Collections;

public class Effects : MonoBehaviour 
{
    public bool poisoned1 = false;
    public bool frozen1 = false;
    public bool fireResisting1 = false;
    public bool iceResisting1 = false;
    public bool poisonResisting1 = false;

    public int poisoned1Steps;
    public int frozen1Steps;
    public int fireResistance1Steps;
    public int iceResistance1Steps;
    public int poisonResistance1Steps;

    public GameObject poisonedEffect1;
    private GameObject poisonedEffectClone1;
    public GameObject frozenEffect1;
    private GameObject frozenEffectClone1;
    public GameObject fireResistance1;
    private GameObject fireResistanceClone1;
    public GameObject iceResistance1;
    private GameObject iceResistanceClone1;
    public GameObject poisonResistance1;
    private GameObject poisonResistanceClone1;

    private Transform childEffect;
    private GameObject currentEffect;
    private int currentStep = 0;
    private float timePassed;

    private int previousSpeed;

    private GameController gameController;


    void Start()
    {
        previousSpeed = gameObject.GetComponent<Soul>().speed;
        if (gameObject.tag == "Player_ship") previousSpeed = Settings.p_speed;

        GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
        if (gameControllerObject != null) gameController = gameControllerObject.GetComponent<GameController>();
        else Debug.Log("Collisions can't find GameController script");


    }

    void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed > 1f)
        {
            timePassed = 0;
            currentStep++;
            applyEffects();
        }


        if(poisoned1)
        {
            childEffect = gameObject.transform.Find("Poisoned1(Clone)"); 
            if(childEffect == null)
            {
                poisonedEffectClone1 = Instantiate(poisonedEffect1, gameObject.transform.position, poisonedEffect1.transform.rotation) as GameObject;
                poisonedEffectClone1.transform.parent = gameObject.transform;
                poisoned1Steps = currentStep + 4;
            }

        }


        if (frozen1)
        {
            childEffect = gameObject.transform.Find("Frozen1(Clone)");
            if (childEffect == null)
            {
                frozenEffectClone1 = Instantiate(frozenEffect1, gameObject.transform.position, frozenEffect1.transform.rotation) as GameObject;
                frozenEffectClone1.transform.parent = gameObject.transform;
                frozen1Steps = currentStep + 6;
            }
        }

        if (iceResisting1)
        {
            childEffect = gameObject.transform.Find("IceResistance(Clone)");
            if (childEffect == null)
            {
                iceResistanceClone1 = Instantiate(iceResistance1, gameObject.transform.position, iceResistance1.transform.rotation) as GameObject;
                if (gameObject.tag == "Player_ship" || gameObject.GetComponent<Soul>().ship_name == "enemy1") iceResistanceClone1.transform.localScale = new Vector3(2f, 2f, 2f);
                iceResistanceClone1.transform.parent = gameObject.transform;
                iceResistance1Steps = currentStep + 1;
            }
        }

        if (fireResisting1)
        {
            childEffect = gameObject.transform.Find("FireResistance(Clone)");
            if (childEffect == null)
            {
                fireResistanceClone1 = Instantiate(fireResistance1, gameObject.transform.position, fireResistance1.transform.rotation) as GameObject;
                if (gameObject.tag == "Player_ship" || gameObject.GetComponent<Soul>().ship_name == "enemy1") fireResistanceClone1.transform.localScale = new Vector3(2f, 2f, 2f);
                fireResistanceClone1.transform.parent = gameObject.transform;
                fireResistance1Steps = currentStep + 1;
                
            }
        }

        if (poisonResisting1)
        {
            childEffect = gameObject.transform.Find("PoisonResistance(Clone)");
            if (childEffect == null)
            {
                poisonResistanceClone1 = Instantiate(poisonResistance1, gameObject.transform.position, poisonResistance1.transform.rotation) as GameObject;
                if (gameObject.tag == "Player_ship" || gameObject.GetComponent<Soul>().ship_name == "enemy1") poisonResistanceClone1.transform.localScale = new Vector3(2f, 2f, 2f);
                poisonResistanceClone1.transform.parent = gameObject.transform;
                poisonResistance1Steps = currentStep + 1;
            }
        }



    }

    void applyEffects()
    {
        if(poisoned1)
        {
            if(gameObject.tag == "Player_ship")
            {
                Settings.p_health -= 5;
                gameObject.GetComponent<PlayerShip>().checkLife();
                gameController.updateHealth();
                if (poisoned1Steps < currentStep)
                {
                    poisoned1 = false;
                    Destroy(poisonedEffectClone1);
                }
            }
            else
            {
                gameObject.GetComponent<Soul>().health -= 5;
                gameObject.GetComponent<Colisions>().checkLife();
                if (gameObject.GetComponent<ShowHealth>()) gameObject.GetComponent<ShowHealth>().updateHealth();
                if (poisoned1Steps < currentStep)
                {
                    poisoned1 = false;
                    Destroy(poisonedEffectClone1);
                }
            }

        }
        if (frozen1)
        {
            if (gameObject.tag == "Player_ship")
            {
                if (Settings.p_speed > 2) Settings.p_speed -= 1;
                if (frozen1Steps < currentStep)
                {
                    Settings.p_speed = previousSpeed;
                    frozen1 = false;
                    Destroy(frozenEffectClone1);
                }
            }
            else
            {
                if (gameObject.GetComponent<Soul>().speed > 2) gameObject.GetComponent<Soul>().speed -= 1;
                if (frozen1Steps < currentStep)
                {
                    gameObject.GetComponent<Soul>().speed = previousSpeed;
                    frozen1 = false;
                    Destroy(frozenEffectClone1);
                }
            }
        }

        if (iceResisting1)
        {
            if (iceResistance1Steps < currentStep)
            {
                iceResisting1 = false;
                Destroy(iceResistanceClone1);
            }

        }

        if (fireResisting1)
        {
            if (fireResistance1Steps < currentStep)
            {
                fireResisting1 = false;
                Destroy(fireResistanceClone1);
            }

        }

        if (poisonResisting1)
        {
            if (poisonResistance1Steps < currentStep)
            {
                poisonResisting1 = false;
                Destroy(poisonResistanceClone1);
            }

        }

    }


}
