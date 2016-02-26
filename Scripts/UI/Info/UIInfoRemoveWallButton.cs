using UnityEngine;
using System.Collections;

public class UIInfoRemoveWallButton : MonoBehaviour {
    float buttonSize;
    float borderSize;
    // Use this for initialization
    void Start() {
        transform.SetParent(GameObject.Find("Canvas").transform);
        if (buttonSize == 0) {
            buttonSize = (Screen.height / 7f) - ((Screen.height / 7f) / 8);
            borderSize = ((Screen.height / 7f) / 16);
            GetComponent<RectTransform>().offsetMax = new Vector2((Screen.height / 7f) * -2.8f + borderSize, borderSize + buttonSize);
            GetComponent<RectTransform>().offsetMin = new Vector2((Screen.height / 7f) * -3.8f + 3f * borderSize, borderSize);
        }
    }

    public void remove() {
        GameObject.Find("WorldManager").GetComponent<InfoManager>().remove();
    }

    public void setup(float x) {
        transform.SetParent(GameObject.Find("Canvas").transform);
        buttonSize = (Screen.height / 7f) - ((Screen.height / 7f) / 8);
        borderSize = ((Screen.height / 7f) / 16);
        GetComponent<RectTransform>().offsetMax = new Vector2(Screen.height * -x + buttonSize / 2, borderSize + buttonSize);
        GetComponent<RectTransform>().offsetMin = new Vector2(Screen.height * -x - buttonSize / 2, borderSize);
    }

    public void setup(float x, bool edgeFollow) {
        transform.SetParent(GameObject.Find("Canvas").transform);
        if (edgeFollow) {
            buttonSize = (Screen.height / 7f) - ((Screen.height / 7f) / 8);
            borderSize = ((Screen.height / 7f) / 16);
            GetComponent<RectTransform>().offsetMax = new Vector2(Screen.height * -x + buttonSize + borderSize * 2f, borderSize + buttonSize);
            GetComponent<RectTransform>().offsetMin = new Vector2(Screen.height * -x + borderSize * 2f, borderSize);
        }
        else setup(x);
    }
}