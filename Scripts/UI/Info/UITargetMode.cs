using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UITargetMode : MonoBehaviour {
    Text txt;
    float buttonSize;
    float borderSize;
    // Use this for initialization
    void Start() {
        transform.SetParent(GameObject.Find("Canvas").transform);
        buttonSize = (Screen.height / 7f) - ((Screen.height / 7f) / 8);
        borderSize = ((Screen.height / 7f) / 16);
        GetComponent<RectTransform>().offsetMax = new Vector2((Screen.height / 7f) * -0.1f - borderSize, borderSize + buttonSize);
        GetComponent<RectTransform>().offsetMin = new Vector2((Screen.height / 7f) * -1.3f, borderSize);
    }


    public void setText(string msg) {
        txt = GetComponent<Text>();
        txt.text = msg;
    }
}
