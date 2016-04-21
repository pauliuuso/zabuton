using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CloseControls : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
        transform.GetComponent<Button>().onClick.AddListener(() => { closeInfo(); });
        if(Settings.current_level != 1)
        {
            transform.parent.gameObject.SetActive(false);
        }
	}
	
    void closeInfo()
    {
        transform.parent.gameObject.SetActive(false);
    }
}
