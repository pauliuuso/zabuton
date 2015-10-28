using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    // References
    public GameObject Asteroid1;
    public GameObject Asteroid2;
    public GameObject Enemy1;
    public GameObject Saturn;
    public GameObject musicManager;
    public GameObject background;
    public GameObject boundary;
    public GameObject player;
    public GameObject playerShip1;
    public GameObject playerShip2;
    public GameObject playerShip3;
    public GameObject playerShip4;
    public Canvas mainCanvas;
    public GameObject shop;
    public Text goldText;
    public Text scoreText;
    public Image startImage;
    public Image quitImage;
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
    private string[] level1 = {"en1", "sat", "en1", "en1", "ast1","wait" ,"ast1", "en1", "ast1", "en1", "speed3", "ast1", "ast2", "en1", "ast1", "ast1", "en1", "wait", "wait", "ast1", "endSpeed", "en1", "ast1", "ast1", "ast1", "ast1", "ast1", "en1", "wait", "wait", "ast1", "ast1", "ast1", "en1", "ast1", "ast2", "ast1", "ast1", "wait", "ast1", "wait", "en1", "ast1", "ast1", "ast1", "ast1", "ast2", "ast1", "ast1", "end"};

    // Game music



    void Start()
    {

        startImage.GetComponent<Button>().onClick.AddListener(() => { startMission(); });
        quitImage.GetComponent<Button>().onClick.AddListener(() => { quitGame(); });
        upgradeShipButton.onClick.AddListener(() => { upgradeShip(); });
        upgradeReloadButton.onClick.AddListener(() => { upgradeReload(); });
        upgradeFireButton.onClick.AddListener(() => { upgradeFire(); });
        upgradeIceButton.onClick.AddListener(() => { upgradeIce(); });
        upgradePoisonButton.onClick.AddListener(() => { upgradePoison(); });
        upgradeBulletSpeedButton.onClick.AddListener(() => { upgradeBulletSpeed(); });
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
            Vector3 spawnPosition = new Vector3(Random.Range(Settings.xMin, Settings.xMax), 0.0f, 12); //Pozicijos, x lokacija parenkama random
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
            else if(currentLevel[i] == "sat")
            {
                spawnPosition = new Vector3(Random.Range(Settings.xMin, Settings.xMax), -10, 16);
                Instantiate(Saturn, spawnPosition, Saturn.transform.rotation);
            }
            else if(currentLevel[i] == "wait")
            {
            }
            else if(currentLevel[i] == "speed3")
            {
                nextWait = 1f;
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

        Instantiate(background, background.transform.position, transform.rotation);
        Instantiate(boundary, new Vector3(0.0f, 0.0f, 0.0f), transform.rotation);
        GameObject playerShip = Instantiate(player, new Vector3(0.0f, 0.0f, -10f), transform.rotation) as GameObject;
        Instantiate(musicManager, new Vector3(0.0f, 20f, -3.4f), transform.rotation);
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
        healthBar.fillAmount = (float)Settings.p_health / (float)Settings.p_health_max;
        healthLeft.text = Settings.p_health + " / " + Settings.p_health_max;
    }

    private void RemoveUI()
    {
        Destroy(startImage);
        Destroy(quitImage);
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
    }

    IEnumerator finishLevel(float time)
    {
        yield return new WaitForSeconds(time);
        Application.LoadLevel(Application.loadedLevel);
        missionComplete.text = "";
        savePoints();
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
        //Application.Quit();
    }

    private void removeListeners()
    {
        startImage.GetComponent<Button>().onClick.RemoveListener(() => { startMission(); });
        quitImage.GetComponent<Button>().onClick.RemoveListener(() => { quitGame(); });
        upgradeShipButton.onClick.RemoveListener(() => { upgradeShip(); });
        upgradeReloadButton.onClick.RemoveListener(() => { upgradeReload(); });
        upgradeFireButton.onClick.RemoveListener(() => { upgradeFire(); });
        upgradeIceButton.onClick.RemoveListener(() => { upgradeIce(); });
        upgradePoisonButton.onClick.RemoveListener(() => { upgradePoison(); });
        upgradeBulletSpeedButton.onClick.RemoveListener(() => { upgradeBulletSpeed(); });
        RemoveUI();
    }

    private void updateCosts()
    {
        upgradeShipCost.text = Settings.shipCosts[Settings.p_ship_level - 1].ToString();
        upgradeShipCost.text += " gold";
        if (Settings.p_ship_level == Settings.shipHps.Length) upgradeShipCost.text = "Full";

        upgradeReloadCost.text = Settings.reloadCosts[Settings.p_cooldown_level - 1].ToString();
        upgradeReloadCost.text += " gold";
        if (Settings.p_cooldown_level == Settings.shipCooldowns.Length) upgradeReloadCost.text = "Full";
        
        upgradeFireCost.text = Settings.fireCosts[Settings.p_fire_level - 1].ToString();
        upgradeFireCost.text += " gold";
        if (Settings.p_fire_level == Settings.shipFireDamages.Length) upgradeFireCost.text = "Full";

        upgradeIceCost.text = Settings.iceCosts[Settings.p_ice_level - 1].ToString();
        upgradeIceCost.text += " gold";
        if (Settings.p_ice_level == Settings.shipIceDamages.Length) upgradeIceCost.text = "Full";

        upgradePoisonCost.text = Settings.poisonCosts[Settings.p_poison_level - 1].ToString();
        upgradePoisonCost.text += " gold";
        if (Settings.p_poison_level == Settings.shipPoisonDamages.Length) upgradePoisonCost.text = "Full";

        upgradeBulletSpeedCost.text = Settings.bulletSpeedCosts[Settings.p_bullet_speed_level - 1].ToString();
        upgradeBulletSpeedCost.text += " gold";
        if (Settings.p_bullet_speed_level == Settings.shipBulletSpeeds.Length) upgradeBulletSpeedCost.text = "Full";

        displayShipGraphic.GetComponent<displayShip>().updateShip();
        updateStatus();
    }

    private void updateStatus()
    {
        allStatus.text = "Ship level (<b><color=#FFDD00>" + Settings.p_ship_level + "</color></b>)\nReload time (<b><color=#FFDD00>" + Settings.p_cooldown + "s</color></b>)\nBullet speed (<b><color=#FFDD00>" + Settings.p_bullet_speed * Settings.p_bullet_speed * 3 + " km/h</color></b>)\nShip speed (<b><color=#FFDD00>" + Settings.p_speed * Settings.p_speed / 2 + " km/h</color></b>)\nBullet speed (<b><color=#FFDD00>" + Settings.p_bullet_speed * 15 + " km/h</color></b>)\nShip health (<b><color=#FFDD00>" + Settings.p_health + "</color></b>)\nFire damage (<b><color=#FFDD00>" + Settings.p_fire_devast + "</color></b>)\nIce damage (<b><color=#FFDD00>" + Settings.p_ice_devast + "</color></b>)\nPoison damage (<b><color=#FFDD00>" + Settings.p_poison_devast + "</color></b>)";
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
    }


}
