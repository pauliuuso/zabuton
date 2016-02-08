using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowHealth : MonoBehaviour 
{
    public GameObject enemyHealth;
    private GameObject health;
    private GameObject healthBarReal;


	void Start () 
    {
        health = Instantiate(enemyHealth, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), enemyHealth.transform.rotation) as GameObject;
        healthBarReal = health.transform.GetChild(1).gameObject;
        updateHealth();
    }

	void FixedUpdate () 
    {
        if (gameObject.GetComponent<Soul>().ship_name == "enemy1") health.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 1.5f);
        else if (gameObject.GetComponent<Soul>().ship_name == "boss1" || gameObject.GetComponent<Soul>().ship_name == "enemy11" || gameObject.GetComponent<Soul>().ship_name == "enemy2" || gameObject.GetComponent<Soul>().ship_name == "enemy3" || gameObject.GetComponent<Soul>().ship_name == "enemy4" || gameObject.GetComponent<Soul>().ship_name == "enemy12" || gameObject.GetComponent<Soul>().ship_name == "enemy5" || gameObject.GetComponent<Soul>().ship_name == "enemy6") health.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 4f);
        else if (gameObject.GetComponent<Soul>().ship_name == "enemy7" || gameObject.GetComponent<Soul>().ship_name == "enemy9" || gameObject.GetComponent<Soul>().ship_name == "enemy10") health.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 6f);
        else health.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 8f);
    }

    public void updateHealth()
    {
        healthBarReal.GetComponent<Image>().fillAmount = (float)gameObject.GetComponent<Soul>().health / (float)gameObject.GetComponent<Soul>().max_health;
        if (gameObject.GetComponent<Soul>().health <= 0) Destroy(health);
    }

}
