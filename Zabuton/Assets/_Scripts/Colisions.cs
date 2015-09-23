using UnityEngine;
using System.Collections;

public class Colisions : MonoBehaviour
{

    public GameObject explosion1;

    private GameObject explosion;

    void OnTriggerEnter(Collider other) // Jei susiduria objektas kuriam yra priskirtas sitas scriptas ir koks nors kitas objektas
    {

        if(gameObject.tag == "Asteroid")
        {
            explosion = explosion1;
        }

        if (other.tag == "Bolt") // Jei kitas objektas yra zaidejo suvis
        {
            gameObject.GetComponent<Soul>().damage(other.gameObject.GetComponent<Bullet>().devast, other.gameObject.GetComponent<Bullet>().type); // Asteroidas turi savo klase kurioje yra jo gyvybes ir t.t tai i sita klase siunciama sovinio damage ir atakos tipas
            Destroy(other.gameObject); // Sunaikinamas sovinys
            checkLife();
        }
        else if(other.tag == "Player_ship") // jei kitas objektas yra zaidejo laivas
        {
            gameObject.GetComponent<Soul>().damage(Settings.p_health, "none"); //Nuimama objektui tiek kiek zaidejas turi gyvybiu
            Settings.p_health -= gameObject.GetComponent<Soul>().devast; // Zaidejui nuimama tiek kiek gali nuimt objektas kai susiduria
            checkLife();
            other.GetComponent<PlayerShip>().checkLife(); // Sita funkcija yra playership scripte ir tikrina kiek gyvybiu liko zaidejui
        }

    }

    void checkLife()
    {
        if (gameObject.GetComponent<Soul>().health <= 0)
        {
            Destroy(gameObject); // Kai baigiasi gyvybes asteroidas sunaikinamas
            Instantiate(explosion, transform.position, transform.rotation);
        }
 
    }
}
