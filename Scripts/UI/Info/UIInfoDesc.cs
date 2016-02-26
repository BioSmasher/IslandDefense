using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIInfoDesc : MonoBehaviour {
    Text txt;
    //float buttonSize;
    // Use this for initialization
    void Start() {
        transform.SetParent(GameObject.Find("Canvas").transform);
    }

    public void setup(string msg, float y, float scale) {
        transform.SetParent(GameObject.Find("Canvas").transform);
        txt = GetComponent<Text>();
        txt.text = msg;
        txt.fontSize = (int)(Screen.height * scale);

        transform.SetParent(GameObject.Find("Canvas").transform);
        //buttonSize = (Screen.height / 7f) - ((Screen.height / 7f) / 8);

        GetComponent<RectTransform>().offsetMax = new Vector2(Screen.height * -0.02f, Screen.height * y - Screen.height * 0.025f);
        GetComponent<RectTransform>().offsetMin = new Vector2(-Screen.height * 0.3f, Screen.height * y / 2f);
    }

    public void setup(string msg, float y) {
        setup(msg, y, 0.03f);
    }
}
