using UnityEngine;
using System.Collections;

public class showUpgrade : MonoBehaviour 
{
    public int showFromLevel = 0;

	void Start () 
    {
        if (showFromLevel > Settings.current_level) gameObject.SetActive(false);
	}
	
}
