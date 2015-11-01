using UnityEngine;
using System.Collections;

public class DamageCounter
{
    private int returnDamage;

    public int countDamage(int damage, string type, string[] resistance, int[] resistanceStrength) // Cia gaunama damage ir kiti parametrai is kliento, tada apskaiciuojama damage ir siunciama atgal
    {
        returnDamage = damage;

        if (resistanceStrength[0] > 0 && type == "fire") returnDamage = returnDamage - returnDamage * resistanceStrength[0] / 100;
        else if (resistanceStrength[1] > 0 && type == "ice") returnDamage = returnDamage - returnDamage * resistanceStrength[1] / 100;
        else if (resistanceStrength[2] > 0 && type == "poison") returnDamage = returnDamage - returnDamage * resistanceStrength[2] / 100;

        return returnDamage;

    }

}
