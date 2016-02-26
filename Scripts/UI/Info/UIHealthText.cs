using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIHealthText : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.SetParent(GameObject.Find("Canvas").transform);
        //GetComponent<RectTransform>().offsetMax = new Vector2(-(Screen.height / 7f) / 8, Screen.height / 7f);
        //GetComponent<RectTransform>().offsetMin = new Vector2((Screen.height / 7f) * -1.45f, Screen.height / 9.5f);

        GetComponent<Text>().fontSize = (int) (Screen.height / 40f);

    }

    public void setup(string ty) {
        transform.SetParent(GameObject.Find("Canvas").transform);
        if (ty == "health") {
            GetComponent<RectTransform>().offsetMax = new Vector2(-(Screen.height / 7f) / 8, Screen.height / 7f);
            GetComponent<RectTransform>().offsetMin = new Vector2((Screen.height / 7f) * -1.45f, Screen.height / 9.5f);
        }
        if (ty == "house") {
            GetComponent<RectTransform>().offsetMax = new Vector2(-(Screen.height / 7f) / 8, Screen.height / 7f);
            GetComponent<RectTransform>().offsetMin = new Vector2((Screen.height / 7f) * -1.45f, Screen.height / 9.5f);
        }
    }

}
