using UnityEngine;
using System.Collections;

public class UIUpgradeBButton : MonoBehaviour {

    public float buttonSize;
    float borderSize;
    public bool invalid;
    // Use this for initialization
    void Start() {
        transform.SetParent(GameObject.Find("Canvas").transform);
        buttonSize = (Screen.height / 7f) - ((Screen.height / 7f) / 8);
        borderSize = ((Screen.height / 7f) / 16);
        GetComponent<RectTransform>().offsetMax = new Vector2((Screen.height / 7f) * -4f - borderSize, borderSize + buttonSize);
        GetComponent<RectTransform>().offsetMin = new Vector2((Screen.height / 7f) * -5f + borderSize, borderSize);
    }

    public void upgradeB() {
        if (!invalid)
            GameObject.Find("WorldManager").GetComponent<InfoManager>().upgradeB();
        else
            GameObject.Find("WorldManager").GetComponent<InfoManager>().cancelSelect();
    }
}

