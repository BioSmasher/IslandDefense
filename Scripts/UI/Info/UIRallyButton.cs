using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIRallyButton : MonoBehaviour {

    float buttonSize;
    float borderSize;

    bool settingRally;

    public Sprite flag;
    public Sprite redX;
    // Use this for initialization
    void Start() {
        settingRally = false;
        transform.SetParent(GameObject.Find("Canvas").transform);
        buttonSize = (Screen.height / 7f) - ((Screen.height / 7f) / 8);
        borderSize = ((Screen.height / 7f) / 16);
        GetComponent<RectTransform>().offsetMax = new Vector2((Screen.height / 7f) * -1.3f - borderSize, borderSize + buttonSize);
        GetComponent<RectTransform>().offsetMin = new Vector2((Screen.height / 7f) * -2.3f + borderSize, borderSize);
    }

    public void toggle() {
        GameObject.Find("WorldManager").GetComponent<WorldManager>().toggleSettingRallyPoint();
        settingRally = !settingRally;
        if (settingRally)
            GetComponent<Image>().sprite = redX;
        else
            GetComponent<Image>().sprite = flag;
    }

    public void reset() {
        Debug.LogError("RESETING BUTTON");
        GetComponent<Image>().sprite = flag;
        settingRally = false;
    }
}
