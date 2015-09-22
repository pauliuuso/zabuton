using UnityEngine;
using System.Collections;

public class DamageCounter
{
    private int returnDamage;

    public int countDamage(int damage, string type, string resistance, int resistanceStrength) // Cia gaunama damage ir kiti parametrai is kliento, tada apskaiciuojama damage ir siunciama atgal
    {
        if (resistance == "none") returnDamage = damage;
        else if (resistance == "fire" && type == "fire")
        {
            returnDamage = damage - (damage * resistanceStrength) / 100;
        }
        else returnDamage = damage;

        return returnDamage;

    }

}
