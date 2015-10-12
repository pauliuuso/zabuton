using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    // References
    public GameObject Asteroid1;
    public GameObject Asteroid2;
    public GameObject musicManager;
    public GameObject background;
    public GameObject boundary;
    public GameObject player;
    public GameObject playerShip1;
    public GameObject playerShip2;
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
    public Button upgradeFireButton;
    public Text upgradeFireCost;
    public Button upgradeIceButton;
    public Text upgradeIceCost;
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
    private string[] level1 = {"ast1", "ast1", "ast1", "ast1", "ast1","wait" ,"ast1", "wait", "ast1", "ast1", "speed3", "ast1", "ast2", "ast1", "ast1", "ast1", "wait", "wait", "wait", "ast1", "endSpeed", "ast1", "ast1", "ast1", "ast1", "ast1", "ast1", "wait", "wait", "wait", "ast1", "ast1", "ast1", "ast1", "ast1", "ast2", "ast1", "ast1", "wait", "ast1", "wait", "ast1", "ast1", "ast1", "ast1", "ast1", "ast2", "ast1", "ast1", "end"};

    // Game music



    void Start()
    {

        startImage.GetComponent<Button>().onClick.AddListener(() => { startMission(); });
        quitImage.GetComponent<Button>().onClick.AddListener(() => { quitGame(); });
        upgradeShipButton.onClick.AddListener(() => { upgradeShip(); });
        upgradeReloadButton.onClick.AddListener(() => { upgradeReload(); });
        upgradeFireButton.onClick.AddListener(() => { upgradeFire(); });
        upgradeIceButton.onClick.AddListener(() => { upgradeIce(); });
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

        if (Settings.p_ship_level < 3) player = playerShip1;
        else if (Settings.p_ship_level == 3) player = playerShip2;

        Instantiate(background, new Vector3(0.0f, -12f, 0.0f), transform.rotation);
        Instantiate(boundary, new Vector3(0.0f, 0.0f, 0.0f), transform.rotation);
        Instantiate(player, new Vector3(0.0f, 0.0f, -10f), transform.rotation);
        Instantiate(musicManager, new Vector3(0.0f, 20f, -3.4f), transform.rotation);
        Instantiate(smoke, smoke.transform.position, smoke.transform.rotation);
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
        RemoveUI();
    }

    private void updateCosts()
    {
        if (Settings.p_ship_level == 1) upgradeShipCost.text = "150 gold";
        else if (Settings.p_ship_level == 2) upgradeShipCost.text = "450 gold";
        else if (Settings.p_ship_level == 3) upgradeShipCost.text = "1150 gold";

        if (Settings.p_cooldown == 1f) upgradeReloadCost.text = "100 gold";
        else if (Settings.p_cooldown == 0.8f) upgradeReloadCost.text = "500 gold";
        else if (Settings.p_cooldown == 0.6f) upgradeReloadCost.text = "1500 gold";

        if (Settings.p_fire_level == 1) upgradeFireCost.text = "120 gold";
        else if (Settings.p_fire_level == 2) upgradeFireCost.text = "440 gold";
        else if (Settings.p_fire_level == 3) upgradeFireCost.text = "1200 gold";

        if (Settings.p_ice_level == 1) upgradeIceCost.text = "120 gold";
        else if (Settings.p_ice_level == 2) upgradeIceCost.text = "400 gold";
        else if (Settings.p_ice_level == 3) upgradeIceCost.text = "1000 gold";

        displayShipGraphic.GetComponent<displayShip>().updateShip();
        updateStatus();
    }

    private void updateStatus()
    {
        allStatus.text = "Ship level (<b><color=#FFDD00>" + Settings.p_ship_level + "</color></b>)\nReload time (<b><color=#FFDD00>" + Settings.p_cooldown + "s</color></b>)\nShip speed (<b><color=#FFDD00>" + Settings.p_speed * 2 + " km/h</color></b>)\nBullet speed (<b><color=#FFDD00>" + Settings.p_bullet_speed * 15 + " km/h</color></b>)\nShip health (<b><color=#FFDD00>" + Settings.p_health + "</color></b>)\nFire damage (<b><color=#FFDD00>" + Settings.p_fire_devast + "</color></b>)\nIce damage (<b><color=#FFDD00>" + Settings.p_ice_devast + "</color></b>)\nPoison damage (<b><color=#FFDD00>" + Settings.p_poison_devast + "</color></b>)";
    }

    private void upgradeShip()
    {
        if(Settings.p_ship_level == 1 && Settings.p_gold >= 150)
        {
            Settings.p_gold -= 150;
            Settings.p_ship_level++;
            Settings.p_health_max += 15;
            Settings.p_health += 15;
            updateCosts();
            updateScore();
        }
        else if (Settings.p_ship_level == 2 && Settings.p_gold >= 450)
        {
            Settings.p_gold -= 450;
            Settings.p_ship_level++;
            Settings.p_health_max += 30;
            Settings.p_health += 30;
            updateCosts();
            updateScore();
        }
    }

    private void upgradeReload()
    {
        if(Settings.p_cooldown == 1f && Settings.p_gold >= 100)
        {
            Settings.p_gold -= 100;
            Settings.p_cooldown = 0.8f;
            updateCosts();
            updateScore();
        }
        else if (Settings.p_cooldown == 0.8f && Settings.p_gold >= 500)
        {
            Settings.p_gold -= 500;
            Settings.p_cooldown = 0.6f;
            updateCosts();
            updateScore();
        }
    }

    private void upgradeFire()
    {
        if (Settings.p_fire_level == 1 && Settings.p_gold >= 120)
        {
            Settings.p_gold -= 120;
            Settings.p_fire_level++;
            Settings.p_fire_devast += 10;
            updateCosts();
            updateScore();
        }
        else if (Settings.p_fire_level == 2 && Settings.p_gold >= 440)
        {
            Settings.p_gold -= 440;
            Settings.p_fire_level++;
            Settings.p_fire_devast += 22;
            updateCosts();
            updateScore();
        }
    }

    private void upgradeIce()
    {
        if (Settings.p_ice_level == 1 && Settings.p_gold >= 120)
        {
            Settings.p_gold -= 120;
            Settings.p_ice_level++;
            Settings.p_ice_devast += 5;
            updateCosts();
            updateScore();
        }
        else if (Settings.p_ice_level == 2 && Settings.p_gold >= 400)
        {
            Settings.p_gold -= 400;
            Settings.p_ice_level++;
            Settings.p_ice_devast += 12;
            updateCosts();
            updateScore();
        }
    }


}
