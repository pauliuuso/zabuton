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
    public GameObject playerShip;
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
    public Material[] playerShipMaterials;


    private float startWait = 3f;
    private float nextWait = 2f;
    private float ObjectScale = 1;



    private string[] currentLevel;
    private string[] level1 = {"ast1", "ast2", "ast1", "ast1", "ast2","wait" ,"ast1", "wait", "ast1", "ast2", "ast1", "ast1", "ast1", "ast1", "ast1", "ast2", "wait", "wait", "wait", "ast2", "ast1", "ast1", "ast1", "ast2", "ast1", "ast1", "ast1", "wait", "wait", "wait", "ast1", "ast1", "ast1", "ast1", "ast1", "ast2", "ast2", "ast1", "wait", "ast1", "wait", "ast2", "ast1", "ast1"};

    // Game music



    void Start()
    {
        startImage.GetComponent<Button>().onClick.AddListener(() => { startMission(); });
        quitImage.GetComponent<Button>().onClick.AddListener(() => { quitGame(); });
        upgradeShipButton.onClick.AddListener(() => { upgradeShip(); });
        updateScore();
        updateCosts();
        updatePlayerShip();
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

        for(int i = 0; i < level1.Length; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(Settings.xMin, Settings.xMax), 0.0f, 20); //Pozicijos, x lokacija parenkama random
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
            if (currentLevel[i] == "ast2")
            {
                Instantiate(Asteroid2, spawnPosition, spawnRotation);
                Asteroid2.transform.localScale = new Vector3(ObjectScale, ObjectScale, ObjectScale);
                if (ObjectScale < 0.9f)
                {
                    Asteroid2.GetComponent<Soul>().health = 9;
                    Asteroid2.GetComponent<Soul>().devast = 10;
                    Asteroid2.GetComponent<Soul>().reward = (int)Random.Range(8, 12);
                }
                else
                {
                    Asteroid2.GetComponent<Soul>().health = 16;
                    Asteroid2.GetComponent<Soul>().devast = 15;
                    Asteroid2.GetComponent<Soul>().reward = (int)Random.Range(15, 21);
                }
                randomScale();
            }
            else if(currentLevel[i] == "wait")
            {
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
        StartCoroutine(spawnEnemies());
        if (Settings.current_level == 1) currentLevel = level1;
        randomScale();
        updateScore();

        Instantiate(background, new Vector3(0.0f, -12f, 0.0f), transform.rotation);
        Instantiate(boundary, new Vector3(0.0f, 0.0f, 0.0f), transform.rotation);
        Instantiate(player, new Vector3(0.0f, 0.0f, -10f), transform.rotation);
        Instantiate(musicManager, new Vector3(0.0f, 20f, -3.4f), transform.rotation);
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

    private void RemoveUI()
    {
        Destroy(startImage);
        Destroy(quitImage);
        Destroy(title);
        Destroy(displayShip);
        Destroy(shop);
        BuildLevel();
    }

    IEnumerator reloadLevel(float time)
    {
        yield return new WaitForSeconds(time);
        Application.LoadLevel(Application.loadedLevel);
        reloadPoints();
    }

    private void reloadPoints()
    {
        Settings.p_gold = Settings.p_previous_gold;
        Settings.p_score = Settings.p_previous_score;
        Settings.p_health = Settings.p_health_max;
    }

    private void quitGame()
    {
        Application.Quit();
    }

    private void removeListeners()
    {
        startImage.GetComponent<Button>().onClick.RemoveListener(() => { startMission(); });
        quitImage.GetComponent<Button>().onClick.RemoveListener(() => { quitGame(); });
        upgradeShipButton.onClick.RemoveListener(() => { upgradeShip(); });
        RemoveUI();
    }

    private void updateCosts()
    {
        if (Settings.p_ship_level == 1) upgradeShipCost.text = "150 gold";
        else if (Settings.p_ship_level == 2) upgradeShipCost.text = "450 gold";

        displayShipGraphic.GetComponent<displayShip>().updateShip();
    }

    private void upgradeShip()
    {
        if(Settings.p_ship_level == 1 && Settings.p_gold >= 150)
        {
            Settings.p_gold -= 150;
            Settings.p_ship_level++;
            Settings.p_health_max += 5;
            Settings.p_health += 5;
            updateCosts();
        }
    }

    private void updatePlayerShip()
    {
        playerShip.GetComponent<Renderer>().sharedMaterial = playerShipMaterials[0];
    }

}
