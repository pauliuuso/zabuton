using UnityEngine;
using System.Collections;

public class Effects : MonoBehaviour 
{
    public bool poisoned1 = false;
    public bool frozen1 = false;

    public int poisoned1Steps;
    public int frozen1Steps;

    public GameObject poisonedEffect1;
    private GameObject poisonedEffectClone1;
    public GameObject frozenEffect1;
    private GameObject frozenEffectClone1;

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
    }


}
