﻿using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour
{
    public int health = 10;
    public string resistance = "none";
    public int resistanceStrength;
    DamageCounter counter = new DamageCounter(); // Klase i kuria reikia nusiusti damage, damage type, dabartinio objekto resistance ir resitance strenght

    public void damage(int damage, string type)
    {
        health -= counter.countDamage(damage, type, resistance, resistanceStrength); // Grazina apskaiciuota damage
    }

}
