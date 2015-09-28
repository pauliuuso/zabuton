using UnityEngine;
using System.Collections;


public class Settings
{
    // Player settings
    public static int p_health = 30;
    public static int p_speed = 12;
    public static int p_tilt = 4;
    public static int p_bullet_speed = 15;
    public static float p_cooldown = 1f;
    public static int p_devast = 5;
    public static string p_type = "fire";
    public static string p_primary = "fire";
    public static string p_secondary = "none";
    public static int current_level = 1;
    public static int p_gold = 0;
    public static int p_score = 0;
    public static int p_previous_gold = 0;
    public static int p_previous_score = 0;
    public static bool p_alive = true;

    public static float xMin = -20f, xMax = 20f, zMin = -14f, zMax = 6f;

    public static float music_volume = 0.1f;
    public static float sound_volume = 0.25f;

}

// Devast 0-15 alpha-force, 16-25 beta-force, 26-35 ceta-force