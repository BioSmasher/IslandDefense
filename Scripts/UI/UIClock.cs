using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIClock : MonoBehaviour {
    Text txt;
    // Use this for initialization
    void Start() {
        txt = GetComponent<Text>();
        GetComponent<RectTransform>().offsetMax = new Vector2(-100, -7f);
        GetComponent<RectTransform>().offsetMin = new Vector2(Screen.width - 250f, Screen.height - 90f);
    }

    public void setTime(float time) {
        string words = Mathf.Floor((int)time / 60f).ToString() + ":";
        if (Mathf.Floor((int)time % 60) < 10) {
            words = words + "0";
        }
        words = words + Mathf.Floor((int) time % 60);
        txt.text = words;
    }
}
