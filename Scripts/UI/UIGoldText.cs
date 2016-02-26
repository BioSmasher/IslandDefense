using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIGoldText : MonoBehaviour {
    Text txt;
	// Use this for initialization
	void Start () {
        txt = GetComponent<Text>();
        GetComponent<RectTransform>().offsetMax = new Vector2((Screen.width / -2f) + 60f, -20);
        GetComponent<RectTransform>().offsetMin = new Vector2((Screen.width / 2f) - 180f, Screen.height - 70f);
    }

    public void updateGold(int gold) {
        txt.text = gold.ToString();
    }
}
