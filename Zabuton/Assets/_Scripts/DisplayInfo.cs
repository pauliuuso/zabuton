using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DisplayInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject infoBar;
    public string infoText = "";

	void Start () 
    {
        transform.GetComponent<Image>().color = new Color(1f, 0.9f, 0f);
	}

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.GetComponent<Image>().color = new Color(0.9f, 0.5f, 0f);
        infoBar.transform.position = new Vector2(infoBar.transform.position.x, transform.position.y - 55);
        infoBar.transform.FindChild("Text").GetComponent<Text>().text = infoText;
        infoBar.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        infoBar.SetActive(false);
        transform.GetComponent<Image>().color = new Color(1f, 0.9f, 0f);
    }
}
