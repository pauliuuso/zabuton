using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SkipIntro : MonoBehaviour 
{
    public GameObject splashScreen;

	void Update () 
    {

	    if(Input.anyKey)
        {
            gameObject.GetComponent<AudioSource>().enabled = false;
            splashScreen.SetActive(true);
            Application.LoadLevel(1);
        }
	}

}
