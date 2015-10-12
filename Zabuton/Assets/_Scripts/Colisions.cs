using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Colisions : MonoBehaviour
{

    public GameObject explosion1;
    public GameObject[] fireBooms;
    public GameObject[] iceBooms;
    public GameObject[] poisonBooms;
    public TextMesh stageText;

    private GameObject explosion;
    private GameController gameController;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
        if (gameControllerObject != null) gameController = gameControllerObject.GetComponent<GameController>();
        else Debug.Log("Collisions can't find GameController script");
    }


    void OnTriggerEnter(Collider other) // Jei susiduria objektas kuriam yra priskirtas sitas scriptas ir koks nors kitas objektas
    {

        if(gameObject.tag == "Asteroid" || gameObject.tag == "Asteroid2")
        {
            explosion = explosion1;
        }

        if (other.tag == "Bolt") // Jei kitas objektas yra zaidejo suvis
        {
            gameObject.GetComponent<Soul>().damage(other.gameObject.GetComponent<Bullet>().devast, other.gameObject.GetComponent<Bullet>().type); // Asteroidas turi savo klase kurioje yra jo gyvybes ir t.t tai i sita klase siunciama sovinio damage ir atakos tipas
            if (other.gameObject.GetComponent<Bullet>().type == "fire") Instantiate(fireBooms[Settings.p_fire_level], new Vector3(other.transform.position.x, other.transform.position.y + 5, other.transform.position.z + 0.2f), other.transform.rotation);
            else if (other.gameObject.GetComponent<Bullet>().type == "ice") Instantiate(iceBooms[Settings.p_ice_level], new Vector3(other.transform.position.x, other.transform.position.y + 5, other.transform.position.z + 0.2f), iceBooms[Settings.p_ice_level].transform.rotation);
            else if (other.gameObject.GetComponent<Bullet>().type == "poison") Instantiate(poisonBooms[Settings.p_poison_level], new Vector3(other.transform.position.x, other.transform.position.y + 5, other.transform.position.z + 0.2f), other.transform.rotation);

            Destroy(other.gameObject); // Sunaikinamas sovinys
            checkLife();
        }
        else if(other.tag == "Player_ship") // jei kitas objektas yra zaidejo laivas
        {

            stageText.text = "-" + gameObject.GetComponent<Soul>().devast;
            stageText.color = Color.red;
            Instantiate(stageText, other.transform.position, Quaternion.Euler(90, 0, 0));

            gameObject.GetComponent<Soul>().damage(Settings.p_health, "none"); //Nuimama objektui tiek kiek zaidejas turi gyvybiu
            Settings.p_health -= gameObject.GetComponent<Soul>().devast; // Zaidejui nuimama tiek kiek gali nuimt objektas kai susiduria
            checkLife();
            other.GetComponent<PlayerShip>().checkLife(); // Sita funkcija yra playership scripte ir tikrina kiek gyvybiu liko zaidejui
            gameController.updateHealth();
        }

    }

    void checkLife()
    {
        if (gameObject.GetComponent<Soul>().health <= 0)
        {
            stageText.text = "+" + gameObject.GetComponent<Soul>().reward + " gold";
            stageText.color = new Color (1.0f, 0.8f, 0.0f);
            Instantiate(stageText, gameObject.transform.position, Quaternion.Euler(90, 0, 0));
            Settings.p_gold += gameObject.GetComponent<Soul>().reward;
            Settings.p_score += gameObject.GetComponent<Soul>().reward * 2;
            gameController.updateScore();

            Destroy(gameObject); // Kai baigiasi gyvybes asteroidas sunaikinamas
            Instantiate(explosion, transform.position, transform.rotation);
        }
 
    }
}
