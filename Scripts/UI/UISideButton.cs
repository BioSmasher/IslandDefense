using UnityEngine;
using System.Collections;

public class UISideButton : MonoBehaviour {
    public int buttonNum;
	// Use this for initialization
	void Start () {
        float buttonSize = (Screen.height / 7f) - ((Screen.height / 7f) / 8);
        float borderSize = ((Screen.height / 7f) / 16);
        GetComponent<RectTransform>().offsetMax = new Vector2(borderSize + buttonSize, -1f * (buttonNum * (borderSize + borderSize + buttonSize) + borderSize));
        GetComponent<RectTransform>().offsetMin = new Vector2(borderSize, -1f * (buttonNum * (borderSize + borderSize + buttonSize) + borderSize + buttonSize));
    }
}
