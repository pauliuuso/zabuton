using UnityEngine;
using System.Collections;

public class Effects : MonoBehaviour 
{
    public bool poisoned1 = false;

    public int poisoned1Steps;

    public GameObject poisonedEffect1;

    private Transform childEffect;
    private GameObject currentEffect;
    private int currentStep = 0;
    private float timePassed;

    void Start()
    {

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
                currentEffect = Instantiate(poisonedEffect1, gameObject.transform.position, poisonedEffect1.transform.rotation) as GameObject;
                currentEffect.transform.parent = gameObject.transform;
                poisoned1Steps = currentStep + 5;
            }


        }
        else
        {
            if (childEffect != null) Destroy(gameObject.transform.Find("Poisoned1(Clone)").gameObject);
        }
    }

    void applyEffects()
    {
        if(poisoned1)
        {
            gameObject.GetComponent<Soul>().health -= 5;
            gameObject.GetComponent<Colisions>().checkLife();
            if (gameObject.GetComponent<ShowHealth>()) gameObject.GetComponent<ShowHealth>().updateHealth();
            if (poisoned1Steps < currentStep) poisoned1 = false;
        }
    }

}
