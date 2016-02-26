using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIInfoTitle : MonoBehaviour {
    Text txt;
    public float buttonSize;
	// Use this for initialization
	void Start () {

    }

    public void setup(string msg, string ty) {
        txt = GetComponent<Text>();
        txt.text = msg;
        txt.fontSize = (int)(Screen.height / 14f);

        transform.SetParent(GameObject.Find("Canvas").transform);
        buttonSize = (Screen.height / 7f) - ((Screen.height / 7f) / 8);
        if (ty == "tower") {
            GetComponent<RectTransform>().offsetMax = new Vector2(0, Screen.height / 3.1f);
            GetComponent<RectTransform>().offsetMin = new Vector2((Screen.height / 7f) * -6f + 20f, Screen.height / 3f - 50f);
        }
        if (ty == "health") {
            GetComponent<RectTransform>().offsetMax = new Vector2(0, Screen.height / 6.2f);
            GetComponent<RectTransform>().offsetMin = new Vector2((Screen.height / 7f) * -4f + 20f, Screen.height / 6f - 50f);
        }
        if (ty == "house") {
            GetComponent<RectTransform>().offsetMax = new Vector2(0, (Screen.height / 7f) * 1.5f - 20f);
            GetComponent<RectTransform>().offsetMin = new Vector2((Screen.height / 7f) * -3f + 20f, Screen.height / 8f - 50f);
        }
    }

    public void setup(string msg, float x, float y, float scale) {
        txt = GetComponent<Text>();
        txt.text = msg;
        txt.fontSize = (int)(Screen.height * scale);

        transform.SetParent(GameObject.Find("Canvas").transform);
        buttonSize = (Screen.height / 7f) - ((Screen.height / 7f) / 8);

        GetComponent<RectTransform>().offsetMax = new Vector2(0, Screen.height * y - Screen.height * 0.01f);
        GetComponent<RectTransform>().offsetMin = new Vector2(Screen.height * -x + Screen.height * 0.02f, Screen.height * y / 2f);
    }

    public void setup(string msg, float x, float y) {
        setup(msg, x, y, 0.06f);
    }
}
