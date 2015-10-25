using UnityEngine;
using System.Collections;

public class DamageCounter
{
    private int returnDamage;

    public int countDamage(int damage, string type, string[] resistance, int[] resistanceStrength) // Cia gaunama damage ir kiti parametrai is kliento, tada apskaiciuojama damage ir siunciama atgal
    {
        if (resistanceStrength[0] == 0 && resistanceStrength[1] == 0 && resistanceStrength[2] == 0) returnDamage = damage;


        return returnDamage;

    }

}
