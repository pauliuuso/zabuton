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
        health.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 1.5f);
	}

    public void updateHealth()
    {
        healthBarReal.GetComponent<Image>().fillAmount = (float)gameObject.GetComponent<Soul>().health / (float)gameObject.GetComponent<Soul>().max_health;
        if (gameObject.GetComponent<Soul>().health <= 0) Destroy(health);
    }

}
