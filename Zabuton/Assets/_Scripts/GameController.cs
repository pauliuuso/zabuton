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
    public GameObject Enemy7;
    public GameObject Enemy8;
    public GameObject Enemy9;
    public GameObject Enemy10;
    public GameObject Enemy11;
    public GameObject Enemy12;
    public GameObject EnemyRocket1;
    public GameObject Boss1;
    public GameObject Boss2;
    public GameObject Boss3;
    public GameObject Boss4;
    public GameObject Saturn;
    public GameObject ShipSwarm1;
    public GameObject Moon;
    public GameObject Island1;
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
    public Text yourScore;
    public Button submitButton;
    public InputField userInput;
    public Text enterName;
    public Text topPlayers;
    public GameObject finished;
    public Button topPlayersButton;
    public GameObject topPlayersScreen;
    public Text topPlayersText;
    public Button restartButton;
    public GameObject Controls;
    public GameObject KongregateAPI;

    public Button muteMusicButton;
    public Text muteMusicText;

    public Text allStatus;
    public GameObject statusPanel;
    public Image healthBar;
    public Text healthLeft;
    public Text missionComplete;
    public GameObject smoke;
    private GameObject smokeClone;
    public GameObject fastSmoke;
    private GameObject fastSmokeClone;

    private bool gamePlaying = false;

    private float startWait = 3f;
    private float nextWait = 2f;
    private float defaultWait = 2f;
    private float ObjectScale = 1;



    private string[] currentLevel;
    private static string[] level1 = { "Assault 1/13", "ast1", "ast1", "ast2", "ast1", "ast1", "speed2", "ast1", "ast2", "ast1", "wait", "ast1", "ast2", "ast1", "ast2", "ast1", "ast1", "wait", "ast2", "endSpeed", "ast2", "ast1", "ast1", "ast1", "ast2", "ast1", "wait", "ast2", "ast1", "ast1", "end"};
    private static string[] level2 = { "Assault 2/13", "moon", "ast2", "ast2", "ast1", "wait", "speed1", "ast1", "ast1", "ast1", "ast1", "ast1", "speed3", "ast1", "ast1", "ast1", "ast1", "ast2", "ast2", "ast1", "ast2", "ast1", "wait", "ast2", "ast2", "ast2", "ast1", "endSpeed", "ast2", "ast1", "ast1", "ast2", "ast1", "en1", "ast1", "ast2", "ast1", "ast2", "ast1", "wait", "ast2", "end" };
    private static string[] level3 = { "Assault 3/13", "sat", "en1", "en1", "ast1", "wait", "wait", "ast1", "ast2", "ast2", "wait", "speed1", "ast1", "ast1", "ast1", "ast1", "ast1", "ast2", "endSpeed", "en1", "ast1", "ast1", "speed3", "ast1", "ast2", "ast1", "wait", "en1", "ast2", "ast2", "endSpeed", "ast1", "wait", "ast2", "ast1", "ast1", "ast2", "ast1", "wait", "ast2", "mus_boss", "Here comes the boss!", "wait", "boss1", "wait", "wait", "wait", "wait", "wait" };
    private static string[] level4 = { "Assault 4/13", "ast2", "ast1", "ast2", "wait", "speed1", "ast1", "ast1", "ast2", "speed3", "ast1", "wait", "en1", "ast1", "wait", "ast2", "ast1", "en1", "en1", "endSpeed", "ast2", "ast1", "wait", "ast1", "wait", "en2", "wait", "wait", "ast2", "ast1", "ast1", "ast2", "wait", "ast2", "ast1", "en1", "ast1", "wait", "speed0", "ast1", "ast1", "ast1", "ast1", "ast2", "ast1", "ast1", "endSpeed", "wait", "ast2", "ast2", "ast1", "end" };
    private static string[] level5 = { "Assault 5/13", "moon", "ast1", "ast1", "ast1", "wait", "ast2", "ast1", "en1", "ast1", "wait", "en2", "ast1", "wait", "en1", "ast1", "ast1", "wait", "ast2", "wait", "speed0", "en1", "en1", "en1", "wait", "ast1", "endSpeed", "ast1", "ast2", "wait", "ast2", "speed3", "ast2", "ast1", "en3", "ast1", "en2", "ast1", "ast2", "wait", "ast1", "en1", "endSpeed", "ast1", "ast2", "wait", "en1", "ast1", "ast2", "ast1", "speed0", "ast1", "ast1", "ast2", "ast2", "ast1", "ast1", "endSpeed", "wait", "ast2", "end" };
    private static string[] level6 = { "Assault 6/13", "isl1", "en1", "en1", "ast1", "speed3", "ast2", "ast2", "en1", "wait", "ast2", "en4", "wait", "wait", "ast2", "ast1", "ast1", "wait", "en2", "isl1", "speed3", "en1", "en1", "en1", "en1", "wait", "ast1", "wait", "ast1", "wait", "en1", "en4", "ast2", "ast2", "ast1", "en1", "wait", "speed0", "ast2", "ast1", "ast1", "ast1", "ast2", "ast1", "speed3", "wait", "ast1", "en3", "ast2", "wait", "wait", "en4", "wait", "ast1", "ast2", "ast2", "wait", "isl1", "wait", "en1", "en1", "en1", "endSpeed", "ast1", "en2", "wait", "ast2", "ast2", "rock1", "wait", "ast2", "ast1", "en2", "en1", "ast1", "ast2", "rock1", "ast1", "en4", "wait", "ast2", "ast2", "ast1", "ast1", "wait", "en1", "rock1", "ast1", "ast2", "ast2", "wait", "ast2", "wait", "end" };
    private static string[] level7 = { "Assault 7/13", "sat", "en6", "ast2", "ast2", "speed3", "en6", "ast1", "en6", "wait", "ast1", "wait", "en1", "ast1", "ast2", "en2", "ast2", "ast2", "wait", "en6", "ast2", "wait", "wait", "wait", "en4", "ast1", "ast1", "en3", "ast2", "en1", "wait", "speed0", "ast1", "ast2", "ast2", "ast1", "ast2", "ast1", "ast2", "speed3", "ast1", "wait", "ast2", "en2", "ast1", "wait", "en1", "ast1", "ast1", "wait", "ast2", "en6", "ast2", "wait", "wait", "en4", "ast1", "ast2", "wait", "wait", "en2", "wait", "ast2", "rock1", "rock1", "rock1", "ast1", "en1", "ast2", "ast2", "en3", "rock1", "wait", "ast2", "ast1", "ast1", "wait", "endSpeed", "mus_boss", "add_fast_smoke", "Here comes the boss!", "del_slow_smoke", "boss2", "ast1", "ast2", "ast2", "ast1", "ast2", "ast1", "wait", "wait", "wait", "ast2"};
    private static string[] level8 = { "Assault 8/13", "ship_swarm1", "ast1", "ast2", "speed3", "en2", "ast1", "wait", "ast2", "en1", "ast2", "ast1", "ast1", "en7", "ast1", "wait", "wait", "ast2", "ast2", "wait", "en4", "ast1", "wait", "wait", "en1", "en1", "ast2", "ast1", "en6", "wait", "en6", "speed0", "ast1", "ast2", "ast2", "ast1", "ast2", "en1", "moon", "ast2", "speed3", "wait", "en2", "ast1", "wait", "ast1", "ast1", "ast1", "en1", "en4", "ast1", "wait", "ast1", "wait", "en7", "wait", "wait", "en3", "ast1", "ast2", "ast2", "en1", "wait", "rock1", "rock1", "wait", "en1", "ast1", "ast2", "ast2", "en2", "wait", "ast1", "wait", "en1", "ast2", "en7", "wait", "wait", "ast2", "ast2", "wait", "en2", "wait", "wait", "ast2", "rock1", "ast2", "ast1", "ast1", "end" };
    private static string[] level9 = { "Assault 9/13", "moon", "ast2", "ast1", "speed3", "en1", "en1", "en1", "en1", "en1", "ast1", "wait", "ast2", "wait", "en4", "wait", "ast2", "wait", "ast2", "en8", "wait", "wait", "ast1", "ast2", "ast1", "ast1", "moon", "wait", "en1", "en3", "wait", "ast2", "wait", "en7", "wait", "ast2", "ast2", "ast2", "wait", "en2", "wait", "en6", "ast2", "ast2", "wait", "ast2", "en1", "wait", "en8", "ast2", "ast1", "ast2", "ast2", "wait", "wait", "ast2", "en4", "en1", "en1", "wait", "ast1", "ast2", "ast2", "wait", "speed0", "ast1", "ast2", "ast2", "ast1", "ast2", "ast1", "rock1", "rock1", "speed3", "ast1", "ast2", "wait", "ast2", "wait", "ast2", "wait", "en2", "ast2", "rock1", "ast2", "rock1", "en1", "wait", "ast2", "mus_boss", "add_fast_smoke", "Here comes the boss!", "del_slow_smoke", "boss3", "wait", "wait", "ast2", "ast1", "ast1", "wait", "wait", "ast2", "wait", "wait", "ast1", "ast1", "ast1", "wait", "ast2", "wait", "ast1", "wait", "ast2" };
    private static string[] level10 = { "Assault 10/13", "ship_swarm1", "ast2", "speed3", "wait", "en9", "wait", "ast2", "wait", "ast1", "ast2", "en1", "en11", "en1", "ast2", "ast1", "wait", "wait", "ast2", "ast1", "en6", "ast2", "ast2", "en2", "wait", "ast2", "moon", "ast2", "en1", "en6", "wait", "ast2", "ast2", "en11", "en11", "wait", "ast2", "en7", "rock1", "rock1", "rock1", "ast2", "ast1", "ast2", "ast1", "wait", "en9", "ast1", "ast2", "rock1", "rock1", "en9", "wait", "speed0", "ast2", "ast1", "ast1", "ast1", "ast2", "speed3", "wait", "wait", "ast2", "wait", "en6", "en6", "en6", "wait", "ast2", "ast1", "rock1", "rock1", "en11", "ast1", "ast2", "wait", "wait", "en7", "ast2", "ast1", "wait", "en1", "en1", "wait", "ast2", "rock1", "rock1", "en3", "ast2", "wait", "ast2", "ast2", "wait", "en1", "ast2", "wait", "wait", "ast2", "end" };
    private static string[] level11 = { "Assault 11/13", "isl1", "ast2", "ast2", "speed4", "en10", "ast1", "ast1", "ast2", "wait", "en11", "en6", "en2", "ast2", "ast2", "ship_swarm1", "ast1", "ast2", "wait", "en1", "wait", "en7", "ast2", "en3", "ast2", "ast2", "wait", "ast1", "rock1", "en4", "ast2", "speed0", "ast1", "ast2", "ast1", "ast2", "ast1", "ast1", "speed3", "wait", "en6", "en6", "wait", "ast2", "rock1", "rock1", "rock1", "ast2", "en8", "wait", "wait", "ast2", "ast1", "en4", "ast2", "en9", "ast2", "wait", "wait", "en1", "en11", "wait", "rock1", "rock1", "ast2", "ast2", "ast1", "en1", "ast2", "ast1", "ast1", "wait", "en10", "ast2", "wait", "ast1", "ast1", "wait", "en2", "wait", "wait", "en6", "ast2", "ast2", "en7", "ast2", "ast2", "wait", "wait", "rock1", "rock1", "wait", "ast2", "ast2", "wait", "ast1", "end" };
    private static string[] level12 = { "Assault 12/13", "ship_swarm1", "ast2", "ast2", "en12", "ast1", "wait", "speed3", "en1", "en11", "ast2", "ast2", "wait", "wait", "speed0", "rock1", "rock1", "rock1", "rock1", "rock1", "rock1", "rock1", "speed3", "en7", "ast2", "ast1", "wait", "wait", "en6", "en6", "en6", "wait", "ast2", "ast1", "ast2", "en2", "wait", "ast2", "ast1", "en4", "ast2", "en3", "ast2", "ast2", "wait", "wait", "en2", "rock1", "rock1", "ast2", "en1", "wait", "wait", "en9", "ast2", "ast2", "wait", "ast1", "rock1", "ast2", "en10", "wait", "en2", "ast2", "ast2", "wait", "ast1", "ast1", "en12", "ast2", "en1", "en1", "ast2", "wait", "wait", "rock1", "rock1", "ast2", "en4", "wait", "wait", "ast1", "ast2", "en6", "en6", "ast2", "en7", "ast2", "wait", "rock1", "wait", "ast2", "en11", "en11", "wait", "ast2", "ast2", "ast2", "en8", "wait", "wait", "wait", "en1", "ast1", "wait", "en12", "ast2", "ast2", "wait", "ast2", "ast1", "wait", "rock1", "ast2", "ast2", "wait", "end" };
    private static string[] level13 = { "Final assault!", "ship_swarm1", "ast2", "ast2", "wait", "en7", "wait", "en6", "ast2", "ast2", "speed3", "wait", "ast1", "en11", "en11", "en11", "ast2", "ast2", "ast1", "wait", "en6", "en6", "en6", "en6", "wait", "ast2", "ast2", "ast2", "ast2", "rock1", "rock1", "wait", "speed0", "en1", "en1", "en1", "en1", "en1", "en1", "speed3", "en2", "ast2", "ast1", "ast2", "wait", "ast1", "ast2", "en2", "ast1", "ast2", "wait", "en3", "wait", "ast2", "en4", "en4", "en4", "rock1", "rock1", "ast2", "wait", "ast2", "wait", "en7", "en7", "ast2", "rock1", "rock1", "rock1", "ast2", "wait", "ast2", "wait", "en8", "ast2", "ast1", "ast2", "ast2", "en9", "ast2", "wait", "rock1", "ast2", "rock1", "ast2", "wait", "en10", "en2", "wait", "rock1" , "en10", "wait", "ast2", "ast1", "wait", "en1", "ast2", "ast2", "wait", "en11", "ast2", "wait", "en11", "rock1", "rock1", "wait", "ast2", "ast1", "ast1", "en12", "en12", "ast2", "ast2", "wait", "ast2", "rock1", "rock1", "ast2", "ast1", "ast2", "ast1", "wait", "ast2", "ast2", "ast2", "rock1", "rock1", "rock1", "rock1", "rock1", "wait", "mus_boss", "add_fast_smoke", "Here comes the boss!", "del_slow_smoke", "boss4", "wait", "wait", "ast2", "ast1", "ast1", "wait", "wait", "ast2", "wait", "wait", "ast1", "ast1", "ast1", "wait", "ast2", "wait", "ast1", "wait", "ast2" };
    private string[][] allLevels = new string[][] { level1, level1, level2, level3, level4, level5, level6, level7, level8, level9, level10, level11, level12, level13 };

    void Start()
    {
        if(Settings.current_level <= 13)
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
            topPlayersButton.onClick.AddListener(() => { showTopPlayers(); });
            muteMusicButton.onClick.AddListener(() => { muteMusic(false); });
            if(!Settings.opened)
            {
                Settings.opened = true;
                sendViewInfo();
            }
        }
        else
        {
            finished.SetActive(true);
            yourScore.text = "Your score: " + Settings.p_score;
            submitButton.onClick.AddListener(() => { submitScore(); });
            restartButton.onClick.AddListener(() => { restartGame(); });
            getPlayers();
            KongregateAPI.GetComponent<KongregateAPI>().submitScore(Settings.p_score);
        }

        updateScore();
        updateCosts();
        updateStatus();
        muteMusic(true);
        healthBar.gameObject.SetActive(false);
        gamePlaying = false;
    }


    void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            Screen.fullScreen = !Screen.fullScreen;
            updateHealth();
        }
        if (!Settings.p_alive)
        {
            StartCoroutine(reloadLevel(2.0f));
            Settings.p_alive = true;
        }
    }

    IEnumerator spawnEnemies()
    {
        yield return new WaitForSeconds(startWait);

        for (int i = 0; i < currentLevel.Length; i++)
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
                    Asteroid1.GetComponent<Soul>().reward = (int)Random.Range(12,20);
                }
                else
                {
                    Asteroid1.GetComponent<Soul>().health = 10;
                    Asteroid1.GetComponent<Soul>().devast = 12;
                    Asteroid1.GetComponent<Soul>().reward = (int)Random.Range(20, 30);
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
                    Asteroid2.GetComponent<Soul>().reward = (int)Random.Range(25, 35);
                }
                else
                {
                    Asteroid2.GetComponent<Soul>().health = 20;
                    Asteroid2.GetComponent<Soul>().devast = 26;
                    Asteroid2.GetComponent<Soul>().reward = (int)Random.Range(25, 55);
                }
                randomScale();
            }
            else if (currentLevel[i] == "en1")
            {
                Instantiate(Enemy1, spawnPosition, Enemy1.transform.rotation);

                Enemy1.GetComponent<Soul>().health = 30;
                Enemy1.GetComponent<Soul>().devast = 25;
                Enemy1.GetComponent<Soul>().reward = (int)Random.Range(70, 95);
            }
            else if (currentLevel[i] == "en2")
            {
                Instantiate(Enemy2, spawnPosition, Enemy2.transform.rotation);

                Enemy2.GetComponent<Soul>().devast = 60;
                Enemy2.GetComponent<Soul>().reward = (int)Random.Range(150, 200);
            }
            else if (currentLevel[i] == "en3")
            {
                Instantiate(Enemy3, spawnPosition, Enemy3.transform.rotation);
                Enemy2.GetComponent<Soul>().reward = (int)Random.Range(190, 250);
            }
            else if (currentLevel[i] == "en4")
            {
                Instantiate(Enemy4, spawnPosition, Enemy4.transform.rotation);
                Enemy4.GetComponent<Soul>().reward = (int)Random.Range(200, 300);
            }
            else if (currentLevel[i] == "en6")
            {
                Instantiate(Enemy6, spawnPosition, Enemy6.transform.rotation);
                Enemy6.GetComponent<Soul>().reward = (int)Random.Range(200, 300);
            }
            else if (currentLevel[i] == "en7")
            {
                spawnPosition = new Vector3(Random.Range(Settings.xMin, Settings.xMax), Enemy7.transform.position.y, 20f);
                Instantiate(Enemy7, spawnPosition, Enemy7.transform.rotation);
                Enemy7.GetComponent<Soul>().reward = (int)Random.Range(350, 450);
            }
            else if (currentLevel[i] == "en8")
            {
                Instantiate(Enemy8, new Vector3(spawnPosition.x, Enemy8.transform.position.y, 20f), Enemy8.transform.rotation);
                Enemy8.GetComponent<Soul>().reward = (int)Random.Range(450, 600);
            }
            else if (currentLevel[i] == "en9")
            {
                Instantiate(Enemy9, new Vector3(spawnPosition.x, Enemy9.transform.position.y, 20f), Enemy9.transform.rotation);
                Enemy9.GetComponent<Soul>().reward = (int)Random.Range(240, 360);
            }
            else if (currentLevel[i] == "en10")
            {
                Instantiate(Enemy10, new Vector3(spawnPosition.x, Enemy10.transform.position.y, 20f), Enemy10.transform.rotation);
                Enemy10.GetComponent<Soul>().reward = (int)Random.Range(400, 600);
            }
            else if (currentLevel[i] == "en11")
            {
                Instantiate(Enemy11, new Vector3(spawnPosition.x, Enemy11.transform.position.y, 20f), Enemy11.transform.rotation);
                Enemy11.GetComponent<Soul>().reward = (int)Random.Range(160, 240);
            }
            else if (currentLevel[i] == "en12")
            {
                Instantiate(Enemy12, new Vector3(spawnPosition.x, Enemy12.transform.position.y, 20f), Enemy12.transform.rotation);
                Enemy12.GetComponent<Soul>().reward = (int)Random.Range(400, 550);
            }
            else if (currentLevel[i] == "rock1")
            {
                Instantiate(EnemyRocket1, spawnPosition, EnemyRocket1.transform.rotation);
                EnemyRocket1.GetComponent<Soul>().reward = (int)Random.Range(0, 0);
            }
            else if (currentLevel[i] == "boss1")
            {
                Instantiate(Boss1, spawnPosition, Boss1.transform.rotation);
                Boss1.GetComponent<Soul>().reward = (int)Random.Range(300, 350);
            }
            else if (currentLevel[i] == "boss2")
            {
                Instantiate(Boss2, spawnPosition, Boss2.transform.rotation);
                Boss2.GetComponent<Soul>().reward = (int)Random.Range(500, 700);
            }
            else if (currentLevel[i] == "boss3")
            {
                Instantiate(Boss3, new Vector3(spawnPosition.x, Boss3.transform.position.y, spawnPosition.z), Boss3.transform.rotation);
                Boss3.GetComponent<Soul>().reward = (int)Random.Range(700, 900);
            }
            else if (currentLevel[i] == "boss4")
            {
                Instantiate(Boss4, new Vector3(spawnPosition.x, Boss4.transform.position.y, spawnPosition.z), Boss4.transform.rotation);
                Boss4.GetComponent<Soul>().reward = (int)Random.Range(1000, 1200);
            }
            else if(currentLevel[i] == "sat")
            {
                spawnPosition = new Vector3(Random.Range(Settings.xMin, Settings.xMax), -22, 16);
                Instantiate(Saturn, spawnPosition, Saturn.transform.rotation);
            }
            else if (currentLevel[i] == "moon")
            {
                spawnPosition = new Vector3(Random.Range(Settings.xMin, Settings.xMax), -20, 16);
                Instantiate(Moon, spawnPosition, Moon.transform.rotation);
            }
            else if (currentLevel[i] == "isl1")
            {
                spawnPosition = new Vector3(Random.Range(Settings.xMin, Settings.xMax), -20, 45f);
                Instantiate(Island1, spawnPosition, Island1.transform.rotation);
            }
            else if (currentLevel[i] == "ship_swarm1")
            {
                spawnPosition = new Vector3(Random.Range(Settings.xMin, Settings.xMax), -12, 45f);
                Instantiate(ShipSwarm1, spawnPosition, ShipSwarm1.transform.rotation);
            }
            else if(currentLevel[i] == "wait")
            {
            }
            else if (currentLevel[i] == "speed0")
            {
                nextWait = 0.1f;
            }
            else if(currentLevel[i] == "speed1")
            {
                nextWait = 0.3f;
            }
            else if (currentLevel[i] == "speed2")
            {
                nextWait = 0.6f;
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
            else if(currentLevel[i] == "mus_boss")
            {
                musicManagerClone.GetComponent<MusicManager>().playMusic("boss", true);
            }
            else if(currentLevel[i] == "mus_boss1")
            {
                musicManagerClone.GetComponent<MusicManager>().playMusic("boss1", true);
            }
            else if (currentLevel[i] == "mus_boss2")
            {
                musicManagerClone.GetComponent<MusicManager>().playMusic("boss2", true);
            }


            //Efektai
            else if (currentLevel[i] == "add_fast_smoke")
            {
                fastSmokeClone = (GameObject)Instantiate(fastSmoke, fastSmoke.transform.position, fastSmoke.transform.rotation);
            }
            else if (currentLevel[i] == "del_slow_smoke")
            {
                Destroy(smokeClone);
            }

            else
            {
                StartCoroutine(clearWarning(3f)); // Jei yra arrayjuje koks nors neatpazintas tekstas tai ji parodo ekrane ir uz 3 s panaikina
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
        currentLevel = allLevels[Settings.current_level];
        randomScale();
        updateScore();
        healthBar.gameObject.SetActive(true);
        updateHealth();
        updateShipSettings();

        Instantiate(background, background.transform.position, transform.rotation);
        Instantiate(boundary, new Vector3(0.0f, 0.0f, 0.0f), transform.rotation);
        GameObject playerShip = Instantiate(player, new Vector3(0.0f, 0.0f, -10f), transform.rotation) as GameObject;
        musicManagerClone = Instantiate(musicManager, new Vector3(0.0f, 20f, -3.4f), transform.rotation) as GameObject;
        smokeClone = (GameObject)Instantiate(smoke, smoke.transform.position, smoke.transform.rotation);
        smokeClone.SetActive(true);

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
        gamePlaying = true;
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
        healthBar.transform.FindChild("Health").GetComponent<Image>().fillAmount = (float)Settings.p_health / (float)Settings.p_health_max;
        healthLeft.text = Settings.p_health + " / " + Settings.p_health_max;
    }

    private void restartGame()
    {
        Settings.current_level = 1;
        Settings.p_gold = 500;
        Settings.p_score = 0;
        Settings.p_fire_level = 1;
        Settings.p_ice_level = 1;
        Settings.p_poison_level = 1;
        Settings.p_cooldown_level = 1;
        Settings.p_bullet_speed_level = 1;
        Settings.p_previous_gold = 500;
        Settings.p_previous_score = 0;
        Settings.p_ship_level = 1;
        Settings.p_fire_resistance_level = 1;
        Settings.p_ice_resistance_level = 1;
        Settings.p_poison_resistance_level = 1;
        Settings.p_vampiric_regeneration_level = 1;
        Settings.p_critical_strike_level = 1;
        Application.LoadLevel(Application.loadedLevel);
    }

    private void submitScore()
    {
        submitButton.enabled = false;
        string username = userInput.text;
        string url = "http://games.teroute.com/zabuton/submitScore.php";
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("score", Settings.p_score);
        WWW www = new WWW(url, form);
        StartCoroutine(sendScore(www));
    }

    private void getPlayers()
    {
        string url = "http://games.teroute.com/zabuton/getPlayers.php";
        WWW www = new WWW(url);
        StartCoroutine(getPlayers(www));
    }

    private void sendViewInfo()
    {
        string url = "http://games.teroute.com/zabuton/setView.php";
        print(Application.absoluteURL);
        WWWForm form = new WWWForm();
        form.AddField("url", Application.absoluteURL);
        WWW www = new WWW(url, form);
        StartCoroutine(setView(www));
    }

    private void showTopPlayers()
    {
        topPlayersScreen.SetActive(true);
        string url = "http://games.teroute.com/zabuton/getPlayers.php";
        WWW www = new WWW(url);
        StartCoroutine(getTopPlayers(www));
    }

    IEnumerator setView(WWW www)
    {
        yield return www;
        if(www.error == null)
        {
            print("View sent!");
        }
        else
        {
            print("Failed to send view " + www.error);
        }
    }

    IEnumerator sendScore(WWW www)
    {
        yield return www;
        if(www.error == null)
        {
            print("success!");
            Destroy(userInput.gameObject);
            Destroy(submitButton.gameObject);
            Destroy(enterName.gameObject);
            getPlayers();
        }
        else
        {
            print("fail: " + www.error);
            submitButton.enabled = true;
        }
    }

    IEnumerator getPlayers(WWW www)
    {
        yield return www;
        if(www.error == null)
        {
            topPlayers.text = www.text;
        }
        else
        {
            topPlayers.text = "Failed to connect to server, try again!";
            print("Couldn't connect to server " + www.error);
        }
    }

    IEnumerator getTopPlayers(WWW www)
    {
        yield return www;
        if (www.error == null)
        {
            topPlayersText.text = www.text;
        }
        else
        {
            topPlayersText.text = "Failed to connect to server, try again!";
            print("Couldn't connect to server " + www.error);
        }
    }

    private void RemoveUI()
    {
        Destroy(Controls);
        Destroy(startButton.gameObject);
        Destroy(topPlayersButton.gameObject);
        Destroy(quitButton.gameObject);
        Destroy(title);
        Destroy(displayShip);
        Destroy(shop);
        Destroy(statusPanel);
        Destroy(muteMusicButton.gameObject);
        BuildLevel();
    }

    public void bossDestroyed()
    {
        missionComplete.text = "Mission complete!";
        StartCoroutine(finishLevel(5f));
    }

    IEnumerator reloadLevel(float time)
    {
        yield return new WaitForSeconds(time);
        Application.LoadLevel(Application.loadedLevel);
        reloadPoints();
        updateShipSettings();
    }

    public IEnumerator finishLevel(float time)
    {
        yield return new WaitForSeconds(time);
        Application.LoadLevel(Application.loadedLevel);
        missionComplete.text = "";
        Settings.current_level++;
        savePoints();
        updateShipSettings();
        if(Settings.current_level == 7)
        {
            KongregateAPI.GetComponent<KongregateAPI>().bossDefeated();
        }
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
        muteMusicButton.onClick.RemoveListener(() => { muteMusic(false); });
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

    private void muteMusic(bool onlyUpdating = false)
    {
        if(Settings.music_volume > 0 && !onlyUpdating)
        {
            Settings.music_volume = 0;
            muteMusicText.text = "Muted";
            muteMusicButton.image.color = Color.gray;
        }
        else if (Settings.music_volume == 0 && !onlyUpdating)
        {
            Settings.music_volume = 0.3f;
            muteMusicText.text = "Mute music";
            muteMusicButton.image.color = Color.green;
        }

        if(onlyUpdating && Settings.music_volume == 0)
        {
            muteMusicText.text = "Muted";
            muteMusicButton.image.color = Color.gray;
        }
        else if(onlyUpdating && Settings.music_volume > 0)
        {
            Settings.music_volume = 0.3f;
            muteMusicText.text = "Mute music";
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
