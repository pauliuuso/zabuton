using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Soul : MonoBehaviour
{
    public int speed = 5;
    public int tilt = 5;
    public int health = 10;
    public int max_health;
    public int devast = 12;
    public int reward = 5;
    public float bullet_speed = 10f;
    public int bullet_devast = 20;
    public string bullet_type = "fire";
    public int fire_level = 1;
    public int ice_level = 1;
    public int poison_level = 1;
    public string effect;
    public float[] bullet_size = { 2f, 2f, 2f };
    private string[] resistance = {"fire", "ice", "poison"};
    public int[] resistanceStrength = {0, 0, 0};
    public GameObject explosion;
    public bool addBoom = false;
    DamageCounter counter = new DamageCounter(); // Klase i kuria reikia nusiusti damage, damage type, dabartinio objekto resistance ir resitance strenght
    public string lastHitBy;
    public string ship_name;
    private GameObject playerShip;

    // Effects //////
    /*
     * Poison1 - silpnas poison
     * Poison2 - vidutinis poison
     * Poison3 - strong poison
     * 
     * 
     * 
     * 
     */

    void Start()
    {
        max_health = health;
        if (gameObject.tag == "Player_ship") resistanceStrength = Settings.p_resistanceStrength;

        GameObject playerShipObject = GameObject.FindGameObjectWithTag("Player_ship");
        if (playerShipObject != null) playerShip = playerShipObject;
    }

    public void damage(int damage, string type, List<string> effects = null, bool returnDamage = false)
    {
        if(effects != null)
        {
            for (int a = 0; a < effects.Count; a++)
            {
                if (effects[a] == "Fired1" && resistanceStrength[0] < 50)
                {
                    if (Random.Range(0f, 1f) < 0.5f) gameObject.GetComponent<Effects>().fired1 = true;
                }
                if (effects[a] == "Poison1" && resistanceStrength[2] < 50)
                {
                    if (Random.Range(0f, 1f) < 0.5f) gameObject.GetComponent<Effects>().poisoned1 = true;
                }
                if (effects[a] == "Poison2" && resistanceStrength[2] < 50)
                {
                    if (Random.Range(0f, 1f) < 0.7f) gameObject.GetComponent<Effects>().poisoned2 = true;
                }
                if (effects[a] == "Frozen1" && resistanceStrength[1] < 50)
                {
                    if (Random.Range(0f, 1f) < 0.5f) gameObject.GetComponent<Effects>().frozen1 = true;
                }
                if (effects[a] == "Vampiric")
                {
                    if (Random.Range(0f, 1f) < 0.12f)
                    {
                        playerShip.GetComponent<Effects>().vampiricRegenerating = true;
                        int hpGain = (int)((float)counter.countDamage(damage, type, Settings.p_resistance, Settings.p_resistanceStrength) * ((float)Settings.p_vampiric_regeneration_strength / 100));
                        Settings.p_health += hpGain;
                        playerShip.GetComponent<PlayerShip>().checkLife();
                        playerShip.GetComponent<Effects>().gameController.updateHealth();
                        playerShip.GetComponent<Colisions>().showDamage("+", hpGain, "green");
                    }
                }
            }
        }

        //resistance
        if(gameObject.tag != "Player_ship")
        {
            if (resistanceStrength[0] > 0 && type == "fire")
            {
                gameObject.GetComponent<Effects>().fireResisting1 = true;
            }
            if (resistanceStrength[1] > 0 && type == "ice")
            {
                gameObject.GetComponent<Effects>().iceResisting1 = true;
            }
            if (resistanceStrength[2] > 0 && type == "poison")
            {
                gameObject.GetComponent<Effects>().poisonResisting1 = true;
            }
        }
        else
        {
            if (Settings.p_resistanceStrength[0] > 0 && type == "fire")
            {
                gameObject.GetComponent<Effects>().fireResisting1 = true;
            }
            if (Settings.p_resistanceStrength[1] > 0 && type == "ice")
            {
                gameObject.GetComponent<Effects>().iceResisting1 = true;
            }
            if (Settings.p_resistanceStrength[2] > 0 && type == "poison")
            {
                gameObject.GetComponent<Effects>().poisonResisting1 = true;
            }
        }


        if(gameObject.tag != "Player_ship") health -= counter.countDamage(damage, type, resistance, resistanceStrength); // Grazina apskaiciuota damage
        else Settings.p_health -= counter.countDamage(damage, type, Settings.p_resistance, Settings.p_resistanceStrength);


    }

    void OnApplicationQuit()
    {
        addBoom = false;
    }

    void OnDestroy()
    {
        if(addBoom)
        {
            Instantiate(explosion, gameObject.transform.position, explosion.transform.rotation);
        }
    }

}
