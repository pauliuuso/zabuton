using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowHealth : MonoBehaviour 
{
    public GameObject enemyHealth;
    public Image healthBar;
    private GameObject gyvybes;
    private Image healthImage;

	void Start () 
    {
        gyvybes = Instantiate(enemyHealth, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), enemyHealth.transform.rotation) as GameObject;
        healthImage = Instantiate(healthBar, healthBar.transform.position, healthBar.transform.rotation) as Image;
        healthImage.transform.parent = gyvybes.transform;
    }

	void FixedUpdate () 
    {
        updateHealth();
        if(gameObject.GetComponent<Soul>().name == "enemy1") gyvybes.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 1.5f);
	}

    public void updateHealth()
    {
        healthImage.fillAmount = (float)Settings.p_health / (float)Settings.p_health_max;
    }

}
