using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIUpgradeInfoText : MonoBehaviour {

    Text txt;

    public void setup(string msg, float yStart, float height, float width) {
        txt = GetComponent<Text>();
        txt.text = msg;
        txt.fontSize = (int)(Screen.height * 0.03f);

        transform.SetParent(GameObject.Find("Canvas").transform);


        float borderRatio = 70f;

        GetComponent<RectTransform>().offsetMax = new Vector2(-Screen.height / borderRatio, Screen.height * yStart - Screen.height / borderRatio + Screen.height * height);
        GetComponent<RectTransform>().offsetMin = new Vector2(-Screen.height * width + Screen.height / borderRatio, Screen.height * yStart + Screen.height / borderRatio);
    }

}
