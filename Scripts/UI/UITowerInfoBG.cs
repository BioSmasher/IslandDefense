using UnityEngine;
using System.Collections;

public class UITowerInfoBG : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    public void setup(int num) {

        transform.SetParent(GameObject.Find("Canvas").transform);

        float panelHeightRatio = 3f;
        float slideDist = (Screen.height - Screen.height / 6.5f - Screen.height / panelHeightRatio) / 5f;
        if (num > 0) {
            num--;
        }

        GetComponent<RectTransform>().offsetMax = new Vector2(Screen.height / 7f + Screen.height / 50f + Screen.height / 4.5f, Screen.height / 6.5f + Screen.height / panelHeightRatio + slideDist * num);
        GetComponent<RectTransform>().offsetMin = new Vector2(Screen.height / 7f + Screen.height / 50f, Screen.height / 6.5f + slideDist * num);
    }
}
