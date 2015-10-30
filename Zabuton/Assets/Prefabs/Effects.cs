using UnityEngine;
using System.Collections;

public class Effects : MonoBehaviour 
{
    public bool poisoned1 = false;
    public bool frozen1 = false;

    public int poisoned1Steps;
    public int frozen1Steps;

    public GameObject poisonedEffect1;
    public GameObject frozenEffect1;

    private Transform childEffect;
    private GameObject currentEffect;
    private int currentStep = 0;
    private float timePassed;

    private int previousSpeed;


    void Start()
    {
        previousSpeed = gameObject.GetComponent<Soul>().speed;
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
                poisonedEffect1 = Instantiate(poisonedEffect1, gameObject.transform.position, poisonedEffect1.transform.rotation) as GameObject;
                poisonedEffect1.transform.parent = gameObject.transform;
                poisoned1Steps = currentStep + 4;
            }

        }


        if (frozen1)
        {
            childEffect = gameObject.transform.Find("Frozen1(Clone)");
            if (childEffect == null)
            {
                frozenEffect1 = Instantiate(frozenEffect1, gameObject.transform.position, frozenEffect1.transform.rotation) as GameObject;
                frozenEffect1.transform.parent = gameObject.transform;
                frozen1Steps = currentStep + 6;
            }
        }





    }

    void applyEffects()
    {
        if(poisoned1)
        {
            gameObject.GetComponent<Soul>().health -= 5;
            gameObject.GetComponent<Colisions>().checkLife();
            if (gameObject.GetComponent<ShowHealth>()) gameObject.GetComponent<ShowHealth>().updateHealth();
            if (poisoned1Steps < currentStep)
            {
                poisoned1 = false;
                Destroy(poisonedEffect1);
            }
        }
        if (frozen1)
        {
            if(gameObject.GetComponent<Soul>().speed > 2) gameObject.GetComponent<Soul>().speed -= 1;
            if (frozen1Steps < currentStep)
            {
                gameObject.GetComponent<Soul>().speed = previousSpeed;
                frozen1 = false;
                Destroy(frozenEffect1);
            }
        }
    }


}
