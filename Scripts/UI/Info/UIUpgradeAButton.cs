using UnityEngine;
using System.Collections;

public class UIUpgradeAButton : MonoBehaviour {
    public float buttonSize;
    float borderSize;
    public bool invalid;
    // Use this for initialization
    void Start () {
        transform.SetParent(GameObject.Find("Canvas").transform);
        buttonSize = (Screen.height / 7f) - ((Screen.height / 7f) / 8);
        borderSize = ((Screen.height / 7f) / 16);
        GetComponent<RectTransform>().offsetMax = new Vector2((Screen.height / 7f) * -5f, borderSize + buttonSize);
        GetComponent<RectTransform>().offsetMin = new Vector2((Screen.height / 7f) * -6f + 2f * borderSize, borderSize);
    }

    public void upgradeA() {
        if (!invalid)
            GameObject.Find("WorldManager").GetComponent<InfoManager>().upgradeA();
        else
            GameObject.Find("WorldManager").GetComponent<InfoManager>().cancelSelect();
    }
	
}
