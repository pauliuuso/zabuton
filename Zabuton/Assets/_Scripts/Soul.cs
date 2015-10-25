using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Soul : MonoBehaviour
{
    public int speed = 5;
    public int tilt = 5;
    public int health = 10;
    public int devast = 12;
    public int reward = 5;
    private string[] resistance = {"fire", "ice", "poison"};
    public int[] resistanceStrength = {0, 0, 0};
    public GameObject explosion;
    public bool addBoom = false;
    DamageCounter counter = new DamageCounter(); // Klase i kuria reikia nusiusti damage, damage type, dabartinio objekto resistance ir resitance strenght
    public string lastHitBy;

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

    public void damage(int damage, string type, List<string> effects = null)
    {
        if(effects != null)
        {
            for (int a = 0; a < effects.Count; a++)
            {
                if (effects[a] == "Poison1")
                {
                    if (Random.Range(0f, 1f) > 0.5f) gameObject.GetComponent<Effects>().poisoned1 = true;
                }
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
