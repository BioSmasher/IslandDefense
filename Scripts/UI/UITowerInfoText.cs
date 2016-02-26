using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UITowerInfoText: MonoBehaviour {
    Text txt;
	// Use this for initialization
	void Start () {

    }

    public void setup(string msg, int num) {
        txt = GetComponent<Text>();
        txt.text = msg;
        txt.fontSize = (int)(Screen.height * 0.03f);

        transform.SetParent(GameObject.Find("Canvas").transform);

        float panelHeightRatio = 3f;
        float borderRatio = 70f;
        float slideDist = (Screen.height - Screen.height / 6.5f - Screen.height / panelHeightRatio) / 5f;
        if (num > 0) {
            num--;
        }

        GetComponent<RectTransform>().offsetMax = new Vector2(Screen.height / 7f + Screen.height / 50f + Screen.height / 4.5f - Screen.height / borderRatio, Screen.height / 6.5f + Screen.height / panelHeightRatio + slideDist * num - Screen.height / borderRatio);
        GetComponent<RectTransform>().offsetMin = new Vector2(Screen.height / 7f + Screen.height / 50f + Screen.height / borderRatio, Screen.height / 6.5f + slideDist * num);
    }

}
