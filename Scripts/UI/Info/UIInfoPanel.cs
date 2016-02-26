using UnityEngine;
using System.Collections;

public class UIInfoPanel : MonoBehaviour {
    //float buttonSize;
	// Use this for initialization
	void Start () {
        
    }

    public void setup(string ty) {
        transform.SetParent(GameObject.Find("Canvas").transform);
        //buttonSize = (Screen.height / 7f) - ((Screen.height / 7f) / 8);
        if (ty == "tower") {
            GetComponent<RectTransform>().offsetMax = new Vector2(0, (Screen.height / 7f) * 2.33f);
            GetComponent<RectTransform>().offsetMin = new Vector2((Screen.height / 7f) * -6f, 0);
        }
        if (ty == "health") {
            GetComponent<RectTransform>().offsetMax = new Vector2(0, Screen.height / 6f);
            GetComponent<RectTransform>().offsetMin = new Vector2((Screen.height / 7f) * -4f, 0);
        }
        if (ty == "info") {
            GetComponent<RectTransform>().offsetMax = new Vector2(0, Screen.height / 8f);
            GetComponent<RectTransform>().offsetMin = new Vector2((Screen.height / 7f) * -3f, 0);
        }
        if (ty == "house") {
            GetComponent<RectTransform>().offsetMax = new Vector2(0, (Screen.height / 7f) * 1.5f);
            GetComponent<RectTransform>().offsetMin = new Vector2((Screen.height / 7f) * -4f, 0);
        }
    }

    public void setup(float x, float y) {
        transform.SetParent(GameObject.Find("Canvas").transform);
        //buttonSize = (Screen.height / 7f) - ((Screen.height / 7f) / 8);

        GetComponent<RectTransform>().offsetMax = new Vector2((1000f * (Screen.height / 950f)) - Screen.height * (x + 0.01f), Screen.height * y);
        GetComponent<RectTransform>().offsetMin = new Vector2(Screen.height * (-x - 0.01f), (-1000f * (Screen.height / 950f)) + Screen.height * y);
    }

}
