using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    // References
    public GameObject Asteroid1;
    public GameObject musicManager;
    public GameObject background;
    public GameObject boundary;
    public GameObject player;
    public Canvas mainCanvas;
    public Text goldText;
    public Text scoreText;
    public Image startImage;
    public Image title;
    public GameObject displayShip;

    private float startWait = 3f;
    private float nextWait = 2f;
    private float ObjectScale = 1;



    private string[] currentLevel;
    private string[] level1 = {"ast1", "ast1", "ast1", "ast1", "ast1","wait" ,"ast1", "wait", "ast1", "ast1", "ast1", "ast1", "ast1", "ast1", "ast1", "ast1", "wait", "wait", "wait", "ast1", "ast1", "ast1", "ast1", "ast1", "ast1", "ast1", "ast1", "wait", "wait", "wait", "ast1", "ast1", "ast1", "ast1", "ast1", "ast1", "ast1", "ast1", "wait", "ast1", "wait", "ast1", "ast1", "ast1"};

    // Game music



    void Start()
    {
        startImage.GetComponent<Button>().onClick.AddListener(() => { startMission(); });
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
        startImage.GetComponent<Button>().onClick.RemoveListener(() => { startMission(); });
        Destroy(startImage);
        Destroy(title);
        Destroy(displayShip);
        BuildLevel();
    }

    public void updateScore()
    {
        goldText.text = "Gold: " + Settings.p_gold;
        scoreText.text = "Score: " + Settings.p_score;
    }

    private void RemoveUI()
    {

    }

}
