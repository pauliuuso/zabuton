using UnityEngine;
using System.Collections;

public class SkipIntro : MonoBehaviour 
{

	void Update () 
    {

	    if(Input.anyKey)
        {
            Application.LoadLevel(1);
        }
	}

}
