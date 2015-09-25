using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{

    // References
    public GameObject Asteroid1;
    public GameObject musicManager;

    private float startWait = 3f;
    private float nextWait = 2f;
    private float ObjectScale = 1;

    private string[] currentLevel;
    private string[] level1 = {"ast1", "ast1", "ast1", "ast1", "ast1","wait" ,"ast1", "wait", "ast1", "ast1", "ast1", "ast1", "ast1", "ast1", "ast1", "ast1", "wait", "wait", "wait", "ast1", "ast1", "ast1", "ast1", "ast1", "ast1", "ast1", "ast1", "wait", "wait", "wait", "ast1", "ast1", "ast1", "ast1", "ast1", "ast1", "ast1", "ast1", "wait", "ast1", "wait", "ast1", "ast1", "ast1"};

    // Game music



    void Start()
    {
        StartCoroutine (spawnEnemies());
        if (Settings.current_level == 1) currentLevel = level1;
        randomScale();
        BuildLevel();
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
        if (Settings.current_level == 1) musicManager.GetComponent<MusicManager>().playMusic();
    }

}
