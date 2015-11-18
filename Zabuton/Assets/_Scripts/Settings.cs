using UnityEngine;
using System.Collections;


public class Settings
{
    // Player settings
    public static int p_health = 30;
    public static int p_health_max = 30;
    public static int p_speed = 12;
    public static int p_tilt = 4;
    public static int p_bullet_speed = 15;
    public static float p_cooldown = 1f;
    public static int p_devast = 5;
    public static int p_fire_devast = 5;
    public static int p_ice_devast = 5;
    public static int p_poison_devast = 5;
    public static string p_type = "fire";
    public static string p_fire = "fire";
    public static string p_ice = "ice";
    public static string p_poison = "poison";
    public static int p_fire_level = 1;
    public static int p_ice_level = 1;
    public static int p_poison_level = 1;
    public static int p_cooldown_level = 1;
    public static int p_bullet_speed_level = 1;
    public static int current_level = 1; // Sales levelis
    public static int p_gold = 9000;
    public static int p_score = 0;
    public static int p_previous_gold = 0;
    public static int p_previous_score = 0;
    public static bool p_alive = true;
    public static int p_ship_level = 1;
    public static string[] p_resistance = { "fire", "ice", "poison" };
    public static int[] p_resistanceStrength = { 0, 0, 0 };
    public static int p_fire_resistance_level = 1;
    public static int p_ice_resistance_level = 1;
    public static int p_poison_resistance_level = 1;
    public static int p_vampiric_regeneration_level = 1;
    public static int p_vampiric_regeneration_strength = 0;
    public static int p_critical_strike_level = 1;
    public static float p_critical_strike_strength = 0;

    public static float xMin = -20f, xMax = 20f, zMin = -14f, zMax = 6f;

    public static float music_volume = 0.1f;
    public static float sound_volume = 0.25f;


    // upgrade costs
    public static int[] shipCosts = new int[] {150, 450, 1150, 2350, 4250, 6780};
    public static int[] reloadCosts = new int[] { 100, 500, 1500, 3250, 6750, 10450, 14500 };
    public static int[] fireCosts = new int[] { 140, 540, 1200 };
    public static int[] iceCosts = new int[] { 200, 610, 1400 };
    public static int[] poisonCosts = new int[] { 130, 500, 1000 };
    public static int[] bulletSpeedCosts = new int[] { 80, 350, 850, 2200, 3400, 5200, 6400, 8000, 10000 };
    public static int[] fireResistanceCosts = new int[] { 100, 330, 680, 1900, 2400, 3500 };
    public static int[] iceResistanceCosts = new int[] { 100, 330, 680, 1900, 2400, 3500 };
    public static int[] poisonResistanceCosts = new int[] { 100, 330, 680, 1900, 2400, 3500 };
    public static int[] vampiricRegenerationCosts = new int[] { 220, 750, 1500, 3900, 10000};
    public static int[] criticalStrikeCosts = new int[] { 250, 800, 2100, 4050, 11500 };


    // upgrade parameters
    public static int[] shipHps = new int[] { 30, 45, 70, 100, 150, 220};
    public static int[] shipSpeeds = new int[] { 12, 13, 15, 17, 19, 20, 21};
    public static float[] shipCooldowns = new float[] { 1, 0.8f, 0.6f, 0.4f, 0.2f, 0.1f, 0.05f};
    public static int[] shipBulletSpeeds = new int[] { 15, 17, 20, 23, 26, 30, 33, 36, 40};
    public static int[] shipFireDamages = new int[] { 5, 17, 40};
    public static int[] shipIceDamages = new int[] { 5, 10, 25};
    public static int[] shipPoisonDamages = new int[] { 5, 12, 30};
    public static int[] shipFireResistance = new int[] { 0, 10, 20, 40, 60, 70};
    public static int[] shipIceResistance = new int[] { 0, 10, 20, 40, 60, 70};
    public static int[] shipPoisonResistance = new int[] { 0, 10, 20, 40, 60, 70};
    public static int[] shipVampiricRegeneration = new int[] { 0, 15, 30, 50, 80, 100};
    public static float[] shipCriticalStrike = new float[] { 0, 1.4f, 1.8f, 2.5f, 3f, 3.5f};
}

// Devast 0-15 alpha-force, 16-25 beta-force, 26-35 ceta-force