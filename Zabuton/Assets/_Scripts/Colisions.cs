﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Colisions : MonoBehaviour
{
    public TextMesh stageText;

    private GameController gameController;
    DamageCounter counter = new DamageCounter();

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
                
                if (!other.GetComponent<Bullet>().particleBolt)
                {
                    gameObject.GetComponent<Soul>().damage(other.gameObject.GetComponent<Bullet>().devast, other.gameObject.GetComponent<Bullet>().type, other.gameObject.GetComponent<Bullet>().effects); // Asteroidas turi savo klase kurioje yra jo gyvybes ir t.t tai i sita klase siunciama sovinio damage ir atakos tipas
                    other.gameObject.GetComponent<Bullet>().addBoom = true;
                    Destroy(other.gameObject); // Sunaikinamas sovinys
                }
                else if (!other.GetComponent<Bullet>().damageDone)
                {
                    gameObject.GetComponent<Soul>().damage(other.gameObject.GetComponent<Bullet>().devast, other.gameObject.GetComponent<Bullet>().type, other.gameObject.GetComponent<Bullet>().effects); // Asteroidas turi savo klase kurioje yra jo gyvybes ir t.t tai i sita klase siunciama sovinio damage ir atakos tipas
                    other.GetComponent<Bullet>().particleBoltHit(gameObject.transform.position);
                    other.GetComponent<Bullet>().damageDone = true;
                    other.GetComponent<CapsuleCollider>().enabled = false;
                }

                gameObject.GetComponent<Soul>().lastHitBy = other.GetComponent<Bullet>().owner;
                if (gameObject.GetComponent<ShowHealth>()) gameObject.GetComponent<ShowHealth>().updateHealth();
                other.gameObject.GetComponent<Bullet>().addBoom = true;
                checkLife();
            }
            else if(gameObject.tag == "Player_ship" && other.GetComponent<Bullet>().owner == "enemy")
            {
                showDamage("-", counter.countDamage(other.GetComponent<Bullet>().devast, other.GetComponent<Bullet>().type, Settings.p_resistance, Settings.p_resistanceStrength));

                if (!other.GetComponent<Bullet>().particleBolt)
                {
                    gameObject.GetComponent<Soul>().damage(other.gameObject.GetComponent<Bullet>().devast, other.gameObject.GetComponent<Bullet>().type, other.gameObject.GetComponent<Bullet>().effects); 
                    other.gameObject.GetComponent<Bullet>().addBoom = true;
                    Destroy(other.gameObject); // Sunaikinamas sovinys
                }
                else if(!other.GetComponent<Bullet>().damageDone)
                {
                    gameObject.GetComponent<Soul>().damage(other.gameObject.GetComponent<Bullet>().devast, other.gameObject.GetComponent<Bullet>().type, other.gameObject.GetComponent<Bullet>().effects); 
                    other.GetComponent<Bullet>().particleBoltHit(gameObject.transform.position);
                    other.GetComponent<Bullet>().damageDone = true;
                    other.GetComponent<CapsuleCollider>().enabled = false;
                }

                gameObject.GetComponent<PlayerShip>().checkLife(); // Sita funkcija yra playership scripte ir tikrina kiek gyvybiu liko zaidejui
                gameController.updateHealth();

            }


        }
        else if(other.tag == "Player_ship" && gameObject.tag != "Bolt") // jei kitas objektas yra zaidejo laivas
        {
            gameObject.GetComponent<Soul>().lastHitBy = "player";
            showDamage("-", gameObject.GetComponent<Soul>().devast);
            gameObject.GetComponent<Soul>().damage(Settings.p_health, "none"); //Nuimama objektui tiek kiek zaidejas turi gyvybiu
            if (gameObject.GetComponent<ShowHealth>()) gameObject.GetComponent<ShowHealth>().updateHealth();
            Settings.p_health -= gameObject.GetComponent<Soul>().devast; // Zaidejui nuimama tiek kiek gali nuimt objektas kai susiduria
            checkLife();
            other.GetComponent<PlayerShip>().checkLife(); // Sita funkcija yra playership scripte ir tikrina kiek gyvybiu liko zaidejui
            gameController.updateHealth();
        }
        if(other.tag == "Enemy" && gameObject.tag == "Enemy")
        {
            gameObject.GetComponent<Soul>().collidingWithSame = true;
            other.GetComponent<Soul>().collidingWithSame = true;
        }


    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy" && gameObject.tag == "Enemy")
        {
            gameObject.GetComponent<Soul>().collidingWithSame = false;
            other.GetComponent<Soul>().collidingWithSame = false;
        }
    }

    public void checkLife()
    {
        if (gameObject.GetComponent<Soul>().health <= 0)
        {
            if (gameObject.GetComponent<Soul>().lastHitBy == "player" && gameObject.GetComponent<Soul>().reward > 0)
            {
                //stageText.text = "+" + gameObject.GetComponent<Soul>().reward + " gold";
                //stageText.color = new Color(1.0f, 0.8f, 0.0f);
                showDamage("+", gameObject.GetComponent<Soul>().reward, "yellow", " gold", 2f);
                Settings.p_gold += gameObject.GetComponent<Soul>().reward;
                Settings.p_score += gameObject.GetComponent<Soul>().reward * 2;
                gameController.updateScore();
            }

            gameObject.GetComponent<Soul>().addBoom = true;

            Destroy(gameObject); // Kai baigiasi gyvybes asteroidas sunaikinamas
        }
 
    }

    public void showDamage(string prefix, int damage, string color = "red", string ending = null, float time = 1f)
    {
        stageText.text = prefix + damage + ending;
        stageText.GetComponent<StageTextMover>().showTime = time;
        if (color == "red") stageText.color = Color.red;
        else if (color == "green") stageText.color = Color.green;
        else if (color == "yellow") stageText.color = Color.yellow;
        Instantiate(stageText, gameObject.transform.position, Quaternion.Euler(90, 0, 0));
    }

}
