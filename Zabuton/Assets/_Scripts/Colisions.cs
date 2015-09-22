using UnityEngine;
using System.Collections;

public class Colisions : MonoBehaviour
{
    void OnTriggerEnter(Collider other) // Jei susiduria objektas kuriam yra priskirtas sitas scriptas ir koks nors kitas objektas
    {
        if (other.gameObject.tag == "Bolt") // Jei kitas objektas yra zaidejo suvis
        {
            if (gameObject.tag == "Asteroid")
            {
                gameObject.GetComponent<Asteroid>().damage(other.gameObject.GetComponent<Bullet>().devast, other.gameObject.GetComponent<Bullet>().type); // Asteroidas turi savo klase kurioje yra jo gyvybes ir t.t tai i sita klase siunciama sovinio damage ir atakos tipas
                if (gameObject.GetComponent<Asteroid>().health <= 0) Destroy(gameObject); // Kai baigiasi gyvybes asteroidas sunaikinamas
            }

            Destroy(other.gameObject); // Sunaikinamas sovinys
        }
    }
}
