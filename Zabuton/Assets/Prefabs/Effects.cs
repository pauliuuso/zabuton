using UnityEngine;
using System.Collections;

public class Effects : MonoBehaviour 
{
    public GameObject effectsLink;

    public bool fired1 = false;
    public bool fired2 = false;
    public bool poisoned1 = false;
    public bool poisoned2 = false;
    public bool frozen1 = false;
    public bool fireResisting1 = false;
    public bool iceResisting1 = false;
    public bool poisonResisting1 = false;
    public bool vampiricRegenerating = false;

    public int fired1Steps;
    public int fired2Steps;
    public int poisoned1Steps;
    public int poisoned2Steps;
    public int frozen1Steps;
    public int fireResistance1Steps;
    public int iceResistance1Steps;
    public int poisonResistance1Steps;
    public int vampiricRegenerationSteps;

    public GameObject bolt;
    private GameObject boltClone;

    private GameObject firedEffect1;
    private GameObject firedEffectClone1;
    private GameObject firedEffect2;
    private GameObject firedEffectClone2;
    private GameObject poisonedEffect1;
    private GameObject poisonedEffectClone1;
    private GameObject poisonedEffect2;
    private GameObject poisonedEffectClone2;
    private GameObject frozenEffect1;
    private GameObject frozenEffectClone1;
    private GameObject fireResistance1;
    private GameObject fireResistanceClone1;
    private GameObject iceResistance1;
    private GameObject iceResistanceClone1;
    private GameObject poisonResistance1;
    private GameObject poisonResistanceClone1;
    private GameObject poisonArrow;
    private GameObject poisonArrowClone;
    private GameObject vampiricRegeneration;
    private GameObject vampiricRegenerationClone;

    public Material emptyMaterial;

    private Transform childEffect;
    private GameObject currentEffect;
    private int currentStep = 0;
    private float timePassed;

    private bool delayedRemoveFire1 = false;
    private int previousSpeed;

    public GameController gameController;


    void Start()
    {
        firedEffect1 = effectsLink.GetComponent<EffectsLink>().firedEffect1;
        firedEffect2 = effectsLink.GetComponent<EffectsLink>().firedEffect2;
        poisonedEffect1 = effectsLink.GetComponent<EffectsLink>().poisonedEffect1;
        poisonedEffect2 = effectsLink.GetComponent<EffectsLink>().poisonedEffect2;
        frozenEffect1 = effectsLink.GetComponent<EffectsLink>().frozenEffect1;
        fireResistance1 = effectsLink.GetComponent<EffectsLink>().fireResistance1;
        iceResistance1 = effectsLink.GetComponent<EffectsLink>().iceResistance1;
        poisonResistance1 = effectsLink.GetComponent<EffectsLink>().poisonResistance1;
        poisonArrow = effectsLink.GetComponent<EffectsLink>().poisonArrow;
        vampiricRegeneration = effectsLink.GetComponent<EffectsLink>().vampiricRegeneration;


        previousSpeed = gameObject.GetComponent<Soul>().speed;
        if (gameObject.tag == "Player_ship") previousSpeed = Settings.p_speed;

        GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
        if (gameControllerObject != null) gameController = gameControllerObject.GetComponent<GameController>();
        else Debug.Log("Effects can't find GameController script");


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


        if (fired1)
        {
            childEffect = gameObject.transform.Find("Fired1(Clone)");
            if (childEffect == null)
            {
                firedEffectClone1 = Instantiate(firedEffect1, gameObject.transform.position, firedEffect1.transform.rotation) as GameObject;
                firedEffectClone1.transform.parent = gameObject.transform;
                fired1Steps = currentStep + 2;
            }

        }

        if (fired2)
        {
            childEffect = gameObject.transform.Find("Fired2(Clone)");
            if (childEffect == null)
            {
                firedEffectClone2 = Instantiate(firedEffect2, gameObject.transform.position, firedEffect2.transform.rotation) as GameObject;
                firedEffectClone2.transform.parent = gameObject.transform;
                fired2Steps = currentStep;
            }

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

        if (poisoned2)
        {
            childEffect = gameObject.transform.Find("Poisoned2(Clone)");
            if (childEffect == null)
            {
                poisonedEffectClone2 = Instantiate(poisonedEffect2, gameObject.transform.position, poisonedEffect2.transform.rotation) as GameObject;
                poisonedEffectClone2.transform.parent = gameObject.transform;
                poisoned2Steps = currentStep + 4;
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
                iceResistanceClone1.transform.localScale = new Vector3(gameObject.GetComponent<Soul>().resistanceSize, gameObject.GetComponent<Soul>().resistanceSize, gameObject.GetComponent<Soul>().resistanceSize);
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
                fireResistanceClone1.transform.localScale = new Vector3(gameObject.GetComponent<Soul>().resistanceSize, gameObject.GetComponent<Soul>().resistanceSize, gameObject.GetComponent<Soul>().resistanceSize);
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
                poisonResistanceClone1.transform.localScale = new Vector3(gameObject.GetComponent<Soul>().resistanceSize, gameObject.GetComponent<Soul>().resistanceSize, gameObject.GetComponent<Soul>().resistanceSize);
                poisonResistanceClone1.transform.parent = gameObject.transform;
                poisonResistance1Steps = currentStep + 1;
            }
        }

        if (vampiricRegenerating)
        {
            childEffect = gameObject.transform.Find("VampiricRegeneration(Clone)");
            if(childEffect == null)
            {
                vampiricRegenerationClone = Instantiate(vampiricRegeneration, gameObject.transform.position, vampiricRegeneration.transform.rotation) as GameObject;
                vampiricRegenerationClone.transform.parent = gameObject.transform;
                vampiricRegenerationSteps = currentStep + 3;
            }
        }



    }

    void applyEffects()
    {
        if(poisoned1)
        {
            if(gameObject.tag == "Player_ship")
            {
                Settings.p_health -= 6;
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
                gameObject.GetComponent<Soul>().health -= 6;
                gameObject.GetComponent<Colisions>().checkLife();
                if (gameObject.GetComponent<ShowHealth>()) gameObject.GetComponent<ShowHealth>().updateHealth();
                if (poisoned1Steps < currentStep)
                {
                    poisoned1 = false;
                    Destroy(poisonedEffectClone1);
                }
            }

        }

        if (poisoned2)
        {
            if (gameObject.tag == "Player_ship")
            {
                Settings.p_health -= 12;
                gameObject.GetComponent<PlayerShip>().checkLife();
                gameController.updateHealth();
                if (poisoned2Steps < currentStep)
                {
                    poisoned2 = false;
                    Destroy(poisonedEffectClone2);
                }
            }
            else
            {
                gameObject.GetComponent<Soul>().health -= 12;
                gameObject.GetComponent<Colisions>().checkLife();
                if (gameObject.GetComponent<ShowHealth>()) gameObject.GetComponent<ShowHealth>().updateHealth();
                if (poisoned2Steps < currentStep)
                {
                    poisoned2 = false;
                    Destroy(poisonedEffectClone2);
                }

                fire("poisonArrow");
                fire("poisonArrow");

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
        if (fired1)
        {
            if (gameObject.tag == "Player_ship")
            {

                if (fired1Steps < currentStep)
                {
                    Settings.p_health -= 40;
                    gameObject.GetComponent<PlayerShip>().checkLife();
                    gameController.updateHealth();
                    fired1 = false;
                    delayedRemoveFire1 = true;
                    fired1Steps = currentStep + 2;
                }
            }
            else
            {
                if (fired1Steps < currentStep)
                {
                    gameObject.GetComponent<Soul>().health -= 40;
                    gameObject.GetComponent<Colisions>().checkLife();
                    if (gameObject.GetComponent<ShowHealth>()) gameObject.GetComponent<ShowHealth>().updateHealth();
                    fired1 = false;
                    delayedRemoveFire1 = true;
                    fired1Steps = currentStep + 2;
                }
            }
        }
        if (fired2)
        {
            if (gameObject.tag == "Player_ship")
            {
                if(gameObject)
                {
                    fired2 = false;
                    Settings.p_health -= 80;
                    gameObject.GetComponent<PlayerShip>().checkLife();
                    gameController.updateHealth();
                }
            }
            else
            {
                if (gameObject)
                {
                    fired2 = false;
                    gameObject.GetComponent<Soul>().health -= 80;
                    gameObject.GetComponent<Colisions>().checkLife();
                    if (gameObject.GetComponent<ShowHealth>()) gameObject.GetComponent<ShowHealth>().updateHealth();
                }
            }
        }
        if(vampiricRegenerating)
        {
            if(vampiricRegenerationSteps < currentStep)
            {
                vampiricRegenerating = false;
                Destroy(vampiricRegenerationClone);
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



        if (delayedRemoveFire1 && fired1Steps < currentStep)
        {
            Destroy(firedEffectClone1);
            delayedRemoveFire1 = false;
        }

    }

    void fire(string type)
    {
        if(type == "poisonArrow")
        {
            boltClone = Instantiate(bolt, gameObject.transform.position, Quaternion.Euler(0f, Random.Range(-90f, 90f), 0f)) as GameObject;
            poisonArrowClone = Instantiate(poisonArrow, boltClone.transform.position, poisonArrow.transform.rotation) as GameObject;
            poisonArrowClone.transform.parent = boltClone.transform;
            boltClone.transform.GetChild(0).GetComponent<Renderer>().sharedMaterial = emptyMaterial;
            boltClone.tag = "Untagged";
            boltClone.GetComponent<Bullet>().effects.Clear();
            boltClone.GetComponent<Bullet>().fromEffects = true;
            boltClone.GetComponent<Bullet>().devast = 5; // Soviniui suteikiama damage
            boltClone.GetComponent<Bullet>().type = "poison"; // Sovinio tipas
            boltClone.GetComponent<Bullet>().owner = "player";
            boltClone.GetComponent<Bullet>().poisonLevel = 2;
            boltClone.GetComponent<Bullet>().effects.Clear();
            boltClone.GetComponent<Bullet>().effects.Add("Poison1");
            boltClone.GetComponent<BulletMover>().speed = 10;
            boltClone.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }


}
