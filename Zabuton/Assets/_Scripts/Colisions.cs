using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Colisions : MonoBehaviour
{
    public TextMesh stageText;

    private GameController gameController;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
        if (gameControllerObject != null) gameController = gameControllerObject.GetComponent<GameController>();
        else Debug.Log("Collisions can't find GameController script");
    }


    void OnTriggerEnter(Collider other) // Jei susiduria objektas kuriam yra priskirtas sitas scriptas ir koks nors kitas objektas
    {
        if (other.tag == "Bolt") // Jei kitas objektas yra zaidejo suvis
        {
            if (gameObject.tag != "Player_ship" && !(gameObject.tag == "Enemy" && other.GetComponent<Bullet>().owner == "enemy"))
            {
                gameObject.GetComponent<Soul>().damage(other.gameObject.GetComponent<Bullet>().devast, other.gameObject.GetComponent<Bullet>().type, other.gameObject.GetComponent<Bullet>().effects); // Asteroidas turi savo klase kurioje yra jo gyvybes ir t.t tai i sita klase siunciama sovinio damage ir atakos tipas
                gameObject.GetComponent<Soul>().lastHitBy = other.GetComponent<Bullet>().owner;
                if(gameObject.GetComponent<ShowHealth>()) gameObject.GetComponent<ShowHealth>().updateHealth();
                other.gameObject.GetComponent<Bullet>().addBoom = true;
                Destroy(other.gameObject); // Sunaikinamas sovinys
                checkLife();
            }
            else if(gameObject.tag == "Player_ship" && other.GetComponent<Bullet>().owner == "enemy")
            {
                stageText.text = "-" + other.GetComponent<Bullet>().devast;
                stageText.color = Color.red;
                Instantiate(stageText, gameObject.transform.position, Quaternion.Euler(90, 0, 0));
                gameObject.GetComponent<Soul>().damage(other.gameObject.GetComponent<Bullet>().devast, other.gameObject.GetComponent<Bullet>().type, other.gameObject.GetComponent<Bullet>().effects); // Zaidejui nuimama tiek kiek gali nuimt objektas kai susiduria
                gameObject.GetComponent<PlayerShip>().checkLife(); // Sita funkcija yra playership scripte ir tikrina kiek gyvybiu liko zaidejui
                gameController.updateHealth();
                other.gameObject.GetComponent<Bullet>().addBoom = true;
                Destroy(other.gameObject); // Sunaikinamas sovinys
            }


        }
        else if(other.tag == "Player_ship") // jei kitas objektas yra zaidejo laivas
        {

            stageText.text = "-" + gameObject.GetComponent<Soul>().devast;
            stageText.color = Color.red;
            Instantiate(stageText, other.transform.position, Quaternion.Euler(90, 0, 0));

            gameObject.GetComponent<Soul>().damage(Settings.p_health, "none"); //Nuimama objektui tiek kiek zaidejas turi gyvybiu
            if (gameObject.GetComponent<ShowHealth>()) gameObject.GetComponent<ShowHealth>().updateHealth();
            Settings.p_health -= gameObject.GetComponent<Soul>().devast; // Zaidejui nuimama tiek kiek gali nuimt objektas kai susiduria
            checkLife();
            other.GetComponent<PlayerShip>().checkLife(); // Sita funkcija yra playership scripte ir tikrina kiek gyvybiu liko zaidejui
            gameController.updateHealth();
        }

    }

    public void checkLife()
    {
        if (gameObject.GetComponent<Soul>().health <= 0)
        {
            if (gameObject.GetComponent<Soul>().lastHitBy == "player")
            {
                stageText.text = "+" + gameObject.GetComponent<Soul>().reward + " gold";
                stageText.color = new Color(1.0f, 0.8f, 0.0f);
                Instantiate(stageText, gameObject.transform.position, Quaternion.Euler(90, 0, 0));
                Settings.p_gold += gameObject.GetComponent<Soul>().reward;
                Settings.p_score += gameObject.GetComponent<Soul>().reward * 2;
                gameController.updateScore();
            }

            gameObject.GetComponent<Soul>().addBoom = true;

            Destroy(gameObject); // Kai baigiasi gyvybes asteroidas sunaikinamas
        }
 
    }
}
