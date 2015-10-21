using UnityEngine;
using System.Collections;

public class Soul : MonoBehaviour
{
    public int speed = 5;
    public int tilt = 5;
    public int health = 10;
    public int devast = 12;
    public int reward = 5;
    public string resistance = "none";
    public int resistanceStrength;
    public GameObject explosion;
    public bool addBoom = false;
    DamageCounter counter = new DamageCounter(); // Klase i kuria reikia nusiusti damage, damage type, dabartinio objekto resistance ir resitance strenght
    public string lastHitBy;

    public void damage(int damage, string type)
    {
        health -= counter.countDamage(damage, type, resistance, resistanceStrength); // Grazina apskaiciuota damage
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
