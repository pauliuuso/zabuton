using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    // References
    public GameObject Asteroid1;
    public GameObject Asteroid2;
    public GameObject Enemy1;
    public GameObject Enemy2;
    public GameObject Enemy3;
    public GameObject Enemy4;
    public GameObject Enemy6;
    public GameObject EnemyRocket1;
    public GameObject Saturn;
    public GameObject musicManager;
    private GameObject musicManagerClone;
    public GameObject background;
    public GameObject boundary;
    public GameObject player;
    public GameObject playerShip1;
    public GameObject playerShip2;
    public GameObject playerShip3;
    public GameObject playerShip4;
    public GameObject playerShip5;
    public GameObject playerShip6;
    public Canvas mainCanvas;
    public GameObject shop;
    public Text goldText;
    public Text scoreText;
    public Button startButton;
    public Button quitButton;
    public Image title;
    public GameObject displayShip;
    public GameObject displayShipGraphic;
    public Button upgradeShipButton;
    public Text upgradeShipCost;
    public Button upgradeReloadButton;
    public Text upgradeReloadCost;
    public Button upgradeBulletSpeedButton;
    public Text upgradeBulletSpeedCost;
    public Button upgradeFireButton;
    public Text upgradeFireCost;
    public Button upgradeIceButton;
    public Text upgradeIceCost;
    public Button upgradePoisonButton;
    public Text upgradePoisonCost;
    public Button upgradeFireResistanceButton;
    public Text upgradeFireResistanceCost;
    public Button upgradeIceResistanceButton;
    public Text upgradeIceResistanceCost;
    public Button upgradePoisonResistanceButton;
    public Text upgradePoisonResistanceCost;
    public Button upgradeVampiricRegenerationButton;
    public Text upgradeVampiricRegenerationCost;
    public Button upgradeCriticalStrikeButton;
    public Text upgradeCriticalStrikeCost;

    public Text allStatus;
    public GameObject statusPanel;
    public Image healthBar;
    public GameObject playerHealth;
    public Text healthLeft;
    public Text missionComplete;
    public GameObject smoke;



    private float startWait = 3f;
    private float nextWait = 2f;
    private float defaultWait = 2f;
    private float ObjectScale = 1;



    private string[] currentLevel;
    private string[] level1 = {"sat", "en6", "ast2", "ast2", "ast1", "en4", "ast1", "en1", "ast2", "ast2", "ast2", "ast2", "ast1", "en2", "wait", "ast1", "en1", "wait", "ast1", "ast2", "speed3", "ast1", "ast2", "ast1", "ast1", "ast1", "rock1", "ast2", "wait", "wait", "en6", "wait", "ast2", "ast1", "wait", "wait", "rock1", "wait", "en6", "wait", "ast2", "en1", "wait", "ast1", "ast2", "wait", "wait", "en6", "ast2", "ast2", "en2", "ast1", "wait", "endSpeed", "en1", "en1", "en1", "wait", "wait", "ast1", "en2", "wait", "en2", "ast1", "ast1", "ast1", "ast2", "ast1", "ast2", "ast2", "ast1", "wait", "ast1", "wait", "wait", "wait", "ast2", "wait", "en1", "en1", "en6", "ast1", "ast1", "ast1", "ast1", "ast2", "incoming attack!", "mus_attack1", "speed3", "rock1", "rock1", "rock1", "rock1", "rock1", "ast2", "en2", "ast2", "wait", "en1", "wait", "wait", "wait", "ast2", "en1", "wait", "ast2", "en1", "en1", "ast2", "rock1", "rock1", "ast2", "wait", "wait", "en1", "wait", "ast2", "ast2", "en1", "wait", "wait", "en3", "wait", "wait", "en1", "ast2", "ast2", "ast2", "wait", "wait", "wait", "en2", "en1", "endSpeed", "wait", "ast2", "wait", "en6", "en6", "wait", "en6", "en6", "wait", "wait", "ast2", "ast1", "en3", "wait", "ast2", "wait", "rock1", "ast1", "en6", "wait", "en2", "wait", "speed3", "ast1", "ast1", "ast2", "ast2", "rock1", "ast1", "ast2", "endSpeed", "wait", "ast2", "wait", "rock1", "wait", "end" };

    // Game music



    void Start()
    {

        startButton.onClick.AddListener(() => { startMission(); });
        quitButton.onClick.AddListener(() => { quitGame(); });
        upgradeShipButton.onClick.AddListener(() => { upgradeShip(); });
        upgradeReloadButton.onClick.AddListener(() => { upgradeReload(); });
        upgradeFireButton.onClick.AddListener(() => { upgradeFire(); });
        upgradeIceButton.onClick.AddListener(() => { upgradeIce(); });
        upgradePoisonButton.onClick.AddListener(() => { upgradePoison(); });
        upgradeBulletSpeedButton.onClick.AddListener(() => { upgradeBulletSpeed(); });
        upgradeFireResistanceButton.onClick.AddListener(() => { upgradeFireResistance(); });
        upgradeIceResistanceButton.onClick.AddListener(() => { upgradeIceResistance(); });
        upgradePoisonResistanceButton.onClick.AddListener(() => { upgradePoisonResistance(); });
        upgradeVampiricRegenerationButton.onClick.AddListener(() => { upgradeVampiricRegeneration(); });
        upgradeCriticalStrikeButton.onClick.AddListener(() => { upgradeCriticalStrike(); });
        updateScore();
        updateCosts();
        updateStatus();
        playerHealth.SetActive(false);
    }


    void Update()
    {
        if (!Settings.p_alive)
        {
            StartCoroutine(reloadLevel(2.0f));
            Settings.p_alive = true;
        }
    }

    IEnumerator spawnEnemies()
    {
        yield return new WaitForSeconds(startWait);

        for (int i = 0; i < level1.Length; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(Settings.xMin, Settings.xMax), 0.0f, 13); //Pozicijos, x lokacija parenkama random
            Quaternion spawnRotation = Quaternion.identity; // Rotation bus 0, identity reiskia kad nebus jokios rotacijos

            if(currentLevel[i] == "ast1")
            {
                Instantiate(Asteroid1, spawnPosition, spawnRotation);
                Asteroid1.transform.localScale = new Vector3(ObjectScale, ObjectScale, ObjectScale);
                if (ObjectScale < 0.9f)
                {
                    Asteroid1.GetComponent<Soul>().health = 5;
                    Asteroid1.GetComponent<Soul>().devast = 6;
                    Asteroid1.GetComponent<Soul>().reward = (int)Random.Range(2,6);
                }
                else
                {
                    Asteroid1.GetComponent<Soul>().health = 10;
                    Asteroid1.GetComponent<Soul>().devast = 12;
                    Asteroid1.GetComponent<Soul>().reward = (int)Random.Range(8, 12);
                }
                randomScale();
            }
            else if (currentLevel[i] == "ast2")
            {
                Instantiate(Asteroid2, spawnPosition, spawnRotation);
                Asteroid2.transform.localScale = new Vector3(ObjectScale, ObjectScale, ObjectScale);
                if (ObjectScale < 0.9f)
                {
                    Asteroid2.GetComponent<Soul>().health = 10;
                    Asteroid2.GetComponent<Soul>().devast = 15;
                    Asteroid2.GetComponent<Soul>().reward = (int)Random.Range(10, 15);
                }
                else
                {
                    Asteroid2.GetComponent<Soul>().health = 20;
                    Asteroid2.GetComponent<Soul>().devast = 26;
                    Asteroid2.GetComponent<Soul>().reward = (int)Random.Range(20, 26);
                }
                randomScale();
            }
            else if (currentLevel[i] == "en1")
            {
                Instantiate(Enemy1, spawnPosition, Enemy1.transform.rotation);

                Enemy1.GetComponent<Soul>().health = 30;
                Enemy1.GetComponent<Soul>().devast = 25;
                Enemy1.GetComponent<Soul>().reward = (int)Random.Range(35, 50);
            }
            else if (currentLevel[i] == "en2")
            {
                Instantiate(Enemy2, spawnPosition, Enemy2.transform.rotation);

                Enemy2.GetComponent<Soul>().devast = 60;
                Enemy2.GetComponent<Soul>().reward = (int)Random.Range(100, 125);
            }
            else if (currentLevel[i] == "en3")
            {
                Instantiate(Enemy3, spawnPosition, Enemy3.transform.rotation);
                Enemy2.GetComponent<Soul>().reward = (int)Random.Range(110, 145);
            }
            else if (currentLevel[i] == "en4")
            {
                Instantiate(Enemy4, spawnPosition, Enemy4.transform.rotation);
                Enemy4.GetComponent<Soul>().reward = (int)Random.Range(110, 130);
            }
            else if (currentLevel[i] == "en6")
            {
                Instantiate(Enemy6, spawnPosition, Enemy6.transform.rotation);
                Enemy6.GetComponent<Soul>().reward = (int)Random.Range(90, 150);
            }
            else if (currentLevel[i] == "rock1")
            {
                Instantiate(EnemyRocket1, spawnPosition, EnemyRocket1.transform.rotation);
                EnemyRocket1.GetComponent<Soul>().reward = (int)Random.Range(0, 0);
            }
            else if(currentLevel[i] == "sat")
            {
                spawnPosition = new Vector3(Random.Range(Settings.xMin, Settings.xMax), -10, 16);
                Instantiate(Saturn, spawnPosition, Saturn.transform.rotation);
            }
            else if(currentLevel[i] == "wait")
            {
            }
            else if(currentLevel[i] == "speed1")
            {
                nextWait = 0.4f;
            }
            else if (currentLevel[i] == "speed2")
            {
                nextWait = 0.7f;
            }
            else if(currentLevel[i] == "speed3")
            {
                nextWait = 1f;
            }
            else if (currentLevel[i] == "speed4")
            {
                nextWait = 1.5f;
            }
            else if(currentLevel[i] == "endSpeed")
            {
                nextWait = defaultWait;
            }
            else if(currentLevel[i] == "end")
            {
                StartCoroutine(finishLevel(5.0f));
                missionComplete.text = "Mission complete!";
            }

            //Music
            else if(currentLevel[i] == "mus_attack1")
            {
                musicManagerClone.GetComponent<MusicManager>().playMusic("attack1", true);
            }

            else
            {
                StartCoroutine(clearWarning(3f));
                missionComplete.text = currentLevel[i];
            }

            yield return new WaitForSeconds(nextWait);
        }
    }

    private void randomScale()
    {
        ObjectScale = Random.Range(0.6f, 1.2f);
    }

    private void BuildLevel()
    {
        savePoints();
        StartCoroutine(spawnEnemies());
        if (Settings.current_level == 1) currentLevel = level1;
        randomScale();
        updateScore();
        playerHealth.SetActive(true);
        updateHealth();
        updateShipSettings();

        Instantiate(background, background.transform.position, transform.rotation);
        Instantiate(boundary, new Vector3(0.0f, 0.0f, 0.0f), transform.rotation);
        GameObject playerShip = Instantiate(player, new Vector3(0.0f, 0.0f, -10f), transform.rotation) as GameObject;
        musicManagerClone = Instantiate(musicManager, new Vector3(0.0f, 20f, -3.4f), transform.rotation) as GameObject;
        Instantiate(smoke, smoke.transform.position, smoke.transform.rotation);

        if (Settings.p_ship_level == 1)
        {
            var currentShip = Instantiate(playerShip1, playerShip.transform.position, Quaternion.Euler(-90f, 0.0f, 180f)) as GameObject;
            currentShip.transform.parent = playerShip.transform;
        }

		if (Settings.p_ship_level == 2)
		{
			var currentShip = Instantiate(playerShip2, playerShip.transform.position + new Vector3(0.5f, -1.5f, -1.5f), Quaternion.Euler(-90f, 0.0f, 180f)) as GameObject;
			currentShip.transform.parent = playerShip.transform;
		}

        else if (Settings.p_ship_level == 3)
        {
            var currentShip = Instantiate(playerShip3, playerShip.transform.position, Quaternion.Euler(-90f, 0.0f, 180f)) as GameObject;
            currentShip.transform.parent = playerShip.transform;
        }

        else if (Settings.p_ship_level == 4)
        {
            var currentShip = Instantiate(playerShip4, playerShip.transform.position, Quaternion.Euler(-90f, 0.0f, 180f)) as GameObject;
            currentShip.transform.parent = playerShip.transform;
        }

        else if (Settings.p_ship_level == 5)
        {
            var currentShip = Instantiate(playerShip5, playerShip.transform.position, Quaternion.Euler( -90f, -90f, 180f)) as GameObject;
            currentShip.transform.parent = playerShip.transform;
        }

        else if (Settings.p_ship_level == 6)
        {
            var currentShip = Instantiate(playerShip6, playerShip.transform.position, Quaternion.Euler(-90f, -90f, 180f)) as GameObject;
            currentShip.transform.parent = playerShip.transform;
        }

    }

    private void startMission()
    {
        removeListeners();
    }

    public void updateScore()
    {
        goldText.text = "Gold: " + Settings.p_gold;
        scoreText.text = "Score: " + Settings.p_score;
    }

    public void updateHealth()
    {
        if (Settings.p_health > Settings.p_health_max) Settings.p_health = Settings.p_health_max;
        if (Settings.p_health < 0) Settings.p_health = 0;
        healthBar.fillAmount = (float)Settings.p_health / (float)Settings.p_health_max;
        healthLeft.text = Settings.p_health + " / " + Settings.p_health_max;
    }

    private void RemoveUI()
    {
        Destroy(startButton.gameObject);
        Destroy(quitButton.gameObject);
        Destroy(title);
        Destroy(displayShip);
        Destroy(shop);
        Destroy(statusPanel);
        BuildLevel();
    }

    IEnumerator reloadLevel(float time)
    {
        yield return new WaitForSeconds(time);
        Application.LoadLevel(Application.loadedLevel);
        reloadPoints();
        updateShipSettings();
    }

    IEnumerator finishLevel(float time)
    {
        yield return new WaitForSeconds(time);
        Application.LoadLevel(Application.loadedLevel);
        missionComplete.text = "";
        savePoints();
        updateShipSettings();
    }

    IEnumerator clearWarning(float time)
    {
        yield return new WaitForSeconds(time);
        missionComplete.text = "";
    }

    private void reloadPoints()
    {
        Settings.p_gold = Settings.p_previous_gold;
        Settings.p_score = Settings.p_previous_score;
        Settings.p_health = Settings.p_health_max;
    }

    private void savePoints()
    {
        Settings.p_previous_gold = Settings.p_gold;
        Settings.p_previous_score = Settings.p_score;
        Settings.p_health = Settings.p_health_max;
    }

    private void quitGame()
    {
        Application.Quit();
    }

    private void removeListeners()
    {
        startButton.onClick.RemoveListener(() => { startMission(); });
        quitButton.onClick.RemoveListener(() => { quitGame(); });
        upgradeShipButton.onClick.RemoveListener(() => { upgradeShip(); });
        upgradeReloadButton.onClick.RemoveListener(() => { upgradeReload(); });
        upgradeFireButton.onClick.RemoveListener(() => { upgradeFire(); });
        upgradeIceButton.onClick.RemoveListener(() => { upgradeIce(); });
        upgradePoisonButton.onClick.RemoveListener(() => { upgradePoison(); });
        upgradeBulletSpeedButton.onClick.RemoveListener(() => { upgradeBulletSpeed(); });
        upgradeFireResistanceButton.onClick.RemoveListener(() => { upgradeFireResistance(); });
        upgradeIceResistanceButton.onClick.RemoveListener(() => { upgradeIceResistance(); });
        upgradePoisonResistanceButton.onClick.RemoveListener(() => { upgradePoisonResistance(); });
        upgradeVampiricRegenerationButton.onClick.RemoveListener(() => { upgradeVampiricRegeneration(); });
        upgradeCriticalStrikeButton.onClick.RemoveListener(() => { upgradeCriticalStrike(); });
        RemoveUI();
    }

    private void updateCosts()
    {
        upgradeShipCost.text = Settings.shipCosts[Settings.p_ship_level - 1].ToString();
        upgradeShipCost.text += " gold";
        if (Settings.p_ship_level == Settings.shipHps.Length) upgradeShipCost.text = "Full";
        if (Settings.p_gold < Settings.shipCosts[Settings.p_ship_level - 1] || upgradeShipCost.text == "Full") upgradeShipButton.interactable = false;

        upgradeReloadCost.text = Settings.reloadCosts[Settings.p_cooldown_level - 1].ToString();
        upgradeReloadCost.text += " gold";
        if (Settings.p_cooldown_level == Settings.shipCooldowns.Length) upgradeReloadCost.text = "Full";
        if (Settings.p_gold < Settings.reloadCosts[Settings.p_cooldown_level - 1] || upgradeReloadCost.text == "Full") upgradeReloadButton.interactable = false;
        
        upgradeFireCost.text = Settings.fireCosts[Settings.p_fire_level - 1].ToString();
        upgradeFireCost.text += " gold";
        if (Settings.p_fire_level == Settings.shipFireDamages.Length) upgradeFireCost.text = "Full";
        if (Settings.p_gold < Settings.fireCosts[Settings.p_fire_level - 1] || upgradeFireCost.text == "Full") upgradeFireButton.interactable = false;

        upgradeIceCost.text = Settings.iceCosts[Settings.p_ice_level - 1].ToString();
        upgradeIceCost.text += " gold";
        if (Settings.p_ice_level == Settings.shipIceDamages.Length) upgradeIceCost.text = "Full";
        if (Settings.p_gold < Settings.iceCosts[Settings.p_ice_level - 1] || upgradeIceCost.text == "Full") upgradeIceButton.interactable = false;

        upgradePoisonCost.text = Settings.poisonCosts[Settings.p_poison_level - 1].ToString();
        upgradePoisonCost.text += " gold";
        if (Settings.p_poison_level == Settings.shipPoisonDamages.Length) upgradePoisonCost.text = "Full";
        if (Settings.p_gold < Settings.poisonCosts[Settings.p_poison_level - 1] || upgradePoisonCost.text == "Full") upgradePoisonButton.interactable = false;

        upgradeBulletSpeedCost.text = Settings.bulletSpeedCosts[Settings.p_bullet_speed_level - 1].ToString();
        upgradeBulletSpeedCost.text += " gold";
        if (Settings.p_bullet_speed_level == Settings.shipBulletSpeeds.Length) upgradeBulletSpeedCost.text = "Full";
        if (Settings.p_gold < Settings.bulletSpeedCosts[Settings.p_bullet_speed_level - 1] || upgradeBulletSpeedCost.text == "Full") upgradeBulletSpeedButton.interactable = false;

        upgradeFireResistanceCost.text = Settings.fireResistanceCosts[Settings.p_fire_resistance_level - 1].ToString();
        upgradeFireResistanceCost.text += " gold";
        if (Settings.p_fire_resistance_level == Settings.shipFireResistance.Length) upgradeFireResistanceCost.text = "Full";
        if (Settings.p_gold < Settings.fireResistanceCosts[Settings.p_fire_resistance_level - 1] || upgradeFireResistanceCost.text == "Full") upgradeFireResistanceButton.interactable = false;

        upgradeIceResistanceCost.text = Settings.iceResistanceCosts[Settings.p_ice_resistance_level - 1].ToString();
        upgradeIceResistanceCost.text += " gold";
        if (Settings.p_ice_resistance_level == Settings.shipIceResistance.Length) upgradeIceResistanceCost.text = "Full";
        if (Settings.p_gold < Settings.iceResistanceCosts[Settings.p_ice_resistance_level - 1] || upgradeIceResistanceCost.text == "Full") upgradeIceResistanceButton.interactable = false;

        upgradePoisonResistanceCost.text = Settings.poisonResistanceCosts[Settings.p_poison_resistance_level - 1].ToString();
        upgradePoisonResistanceCost.text += " gold";
        if (Settings.p_poison_resistance_level == Settings.shipPoisonResistance.Length) upgradePoisonResistanceCost.text = "Full";
        if (Settings.p_gold < Settings.poisonResistanceCosts[Settings.p_poison_resistance_level - 1] || upgradePoisonResistanceCost.text == "Full") upgradePoisonResistanceButton.interactable = false;

        upgradeVampiricRegenerationCost.text = Settings.vampiricRegenerationCosts[Settings.p_vampiric_regeneration_level - 1].ToString();
        upgradeVampiricRegenerationCost.text += " gold";
        if (Settings.p_vampiric_regeneration_level == Settings.shipVampiricRegeneration.Length) upgradeVampiricRegenerationCost.text = "Full";
        if (Settings.p_gold < Settings.vampiricRegenerationCosts[Settings.p_vampiric_regeneration_level - 1] || upgradeVampiricRegenerationCost.text == "Full") upgradeVampiricRegenerationButton.interactable = false;

        upgradeCriticalStrikeCost.text = Settings.criticalStrikeCosts[Settings.p_critical_strike_level - 1].ToString();
        upgradeCriticalStrikeCost.text += " gold";
        if (Settings.p_critical_strike_level == Settings.shipCriticalStrike.Length) upgradeCriticalStrikeCost.text = "Full";
        if (Settings.p_gold < Settings.criticalStrikeCosts[Settings.p_critical_strike_level - 1] || upgradeCriticalStrikeCost.text == "Full") upgradeCriticalStrikeButton.interactable = false;


        displayShipGraphic.GetComponent<displayShip>().updateShip();
        updateStatus();
    }

    private void updateStatus()
    {
        allStatus.text = "Ship level (<b><color=#FFDD00>" + Settings.p_ship_level + "</color></b>)\nReload time (<b><color=#FFDD00>" + Settings.p_cooldown + "s</color></b>)\nBullet speed (<b><color=#FFDD00>" + Settings.p_bullet_speed * Settings.p_bullet_speed * 3 + " km/h</color></b>)\nShip speed (<b><color=#FFDD00>" + Settings.p_speed * Settings.p_speed / 2 + " km/h</color></b>)\nBullet speed (<b><color=#FFDD00>" + Settings.p_bullet_speed * 15 + " km/h</color></b>)\nShip health (<b><color=#FFDD00>" + Settings.p_health + "</color></b>)\nFire damage (<b><color=#FFDD00>" + Settings.p_fire_devast + "</color></b>)\nIce damage (<b><color=#FFDD00>" + Settings.p_ice_devast + "</color></b>)\nPoison damage (<b><color=#FFDD00>" + Settings.p_poison_devast + "</color></b>)\nFire resistance (<b><color=#FFDD00>" + Settings.p_resistanceStrength[0] + "%</color></b>)\nIce resistance (<b><color=#FFDD00>" + Settings.p_resistanceStrength[1] + "%</color></b>)\nPoison resistance (<b><color=#FFDD00>" + Settings.p_resistanceStrength[2] + "%</color></b>)\nVampiric regeneration (<b><color=#FFDD00>" + Settings.p_vampiric_regeneration_strength + "%</color></b>)\nCritical strike (<b><color=#FFDD00>~" + Settings.p_critical_strike_strength + "x</color></b>)";
    }

    private void upgradeShip()
    {
        if (Settings.p_gold >= Settings.shipCosts[Settings.p_ship_level - 1] && Settings.p_ship_level < Settings.shipHps.Length)
        {
            Settings.p_gold -= Settings.shipCosts[Settings.p_ship_level - 1];
            Settings.p_ship_level++;
            updateShipSettings();
            updateCosts();
            updateScore();
        }

    }

    private void upgradeReload()
    {
        if (Settings.p_gold >= Settings.reloadCosts[Settings.p_cooldown_level - 1] && Settings.p_cooldown_level < Settings.shipCooldowns.Length)
        {
            Settings.p_gold -= Settings.reloadCosts[Settings.p_cooldown_level - 1];
            Settings.p_cooldown_level++;
            updateShipSettings();
            updateCosts();
            updateScore();
        }
    }

    private void upgradeFire()
    {
        if (Settings.p_gold >= Settings.fireCosts[Settings.p_fire_level - 1] && Settings.p_fire_level < Settings.shipFireDamages.Length)
        {
            Settings.p_gold -= Settings.fireCosts[Settings.p_fire_level - 1];
            Settings.p_fire_level++;
            updateShipSettings();
            updateCosts();
            updateScore();
        }
    }

    private void upgradeIce()
    {
        if (Settings.p_gold >= Settings.iceCosts[Settings.p_ice_level - 1] && Settings.p_ice_level < Settings.shipIceDamages.Length)
        {
            Settings.p_gold -= Settings.iceCosts[Settings.p_ice_level - 1];
            Settings.p_ice_level++;
            updateShipSettings();
            updateCosts();
            updateScore();
        }

    }

    private void upgradePoison()
    {
        if (Settings.p_gold >= Settings.poisonCosts[Settings.p_poison_level - 1] && Settings.p_poison_level < Settings.shipPoisonDamages.Length)
        {
            Settings.p_gold -= Settings.poisonCosts[Settings.p_poison_level - 1];
            Settings.p_poison_level++;
            updateShipSettings();
            updateCosts();
            updateScore();
        }

    }

    private void upgradeBulletSpeed()
    {
        if (Settings.p_gold >= Settings.bulletSpeedCosts[Settings.p_bullet_speed_level - 1] && Settings.p_bullet_speed_level < Settings.shipBulletSpeeds.Length)
        {
            Settings.p_gold -= Settings.bulletSpeedCosts[Settings.p_bullet_speed_level - 1];
            Settings.p_bullet_speed_level++;
            updateShipSettings();
            updateCosts();
            updateScore();
        }
    }

    private void upgradeFireResistance()
    {
        if (Settings.p_gold >= Settings.fireResistanceCosts[Settings.p_fire_resistance_level - 1] && Settings.p_fire_resistance_level < Settings.shipFireResistance.Length)
        {
            Settings.p_gold -= Settings.fireResistanceCosts[Settings.p_fire_resistance_level - 1];
            Settings.p_fire_resistance_level++;
            updateShipSettings();
            updateCosts();
            updateScore();
        }
    }

    private void upgradeIceResistance()
    {
        if (Settings.p_gold >= Settings.iceResistanceCosts[Settings.p_ice_resistance_level - 1] && Settings.p_ice_resistance_level < Settings.shipIceResistance.Length)
        {
            Settings.p_gold -= Settings.iceResistanceCosts[Settings.p_ice_resistance_level - 1];
            Settings.p_ice_resistance_level++;
            updateShipSettings();
            updateCosts();
            updateScore();
        }
    }

    private void upgradePoisonResistance()
    {
        if (Settings.p_gold >= Settings.poisonResistanceCosts[Settings.p_poison_resistance_level - 1] && Settings.p_poison_resistance_level < Settings.shipPoisonResistance.Length)
        {
            Settings.p_gold -= Settings.poisonResistanceCosts[Settings.p_poison_resistance_level - 1];
            Settings.p_poison_resistance_level++;
            updateShipSettings();
            updateCosts();
            updateScore();
        }
    }

    private void upgradeVampiricRegeneration()
    {
        if (Settings.p_gold >= Settings.vampiricRegenerationCosts[Settings.p_vampiric_regeneration_level - 1] && Settings.p_vampiric_regeneration_level < Settings.shipVampiricRegeneration.Length)
        {
            Settings.p_gold -= Settings.vampiricRegenerationCosts[Settings.p_vampiric_regeneration_level - 1];
            Settings.p_vampiric_regeneration_level++;
            updateShipSettings();
            updateCosts();
            updateScore();
        }
    }

    private void upgradeCriticalStrike()
    {
        if (Settings.p_gold >= Settings.criticalStrikeCosts[Settings.p_critical_strike_level - 1] && Settings.p_critical_strike_level < Settings.shipCriticalStrike.Length)
        {
            Settings.p_gold -= Settings.criticalStrikeCosts[Settings.p_critical_strike_level - 1];
            Settings.p_critical_strike_level++;
            updateShipSettings();
            updateCosts();
            updateScore();
        }
    }

    public void updateShipSettings()
    {
        Settings.p_bullet_speed = Settings.shipBulletSpeeds[Settings.p_bullet_speed_level - 1];
        Settings.p_ice_devast = Settings.shipIceDamages[Settings.p_ice_level - 1];
        Settings.p_fire_devast = Settings.shipFireDamages[Settings.p_fire_level - 1];
        Settings.p_poison_devast = Settings.shipPoisonDamages[Settings.p_poison_level - 1];
        Settings.p_cooldown = Settings.shipCooldowns[Settings.p_cooldown_level - 1];
        Settings.p_health_max = Settings.shipHps[Settings.p_ship_level - 1];
        Settings.p_health = Settings.shipHps[Settings.p_ship_level - 1];
        Settings.p_speed = Settings.shipSpeeds[Settings.p_ship_level - 1];
        Settings.p_resistanceStrength[0] = Settings.shipFireResistance[Settings.p_fire_resistance_level - 1];
        Settings.p_resistanceStrength[1] = Settings.shipIceResistance[Settings.p_ice_resistance_level - 1];
        Settings.p_resistanceStrength[2] = Settings.shipPoisonResistance[Settings.p_poison_resistance_level - 1];
        Settings.p_vampiric_regeneration_strength = Settings.shipVampiricRegeneration[Settings.p_vampiric_regeneration_level - 1];
        Settings.p_critical_strike_strength = Settings.shipCriticalStrike[Settings.p_critical_strike_level - 1];
    }


}
