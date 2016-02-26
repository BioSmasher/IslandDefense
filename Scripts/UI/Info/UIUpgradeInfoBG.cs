using UnityEngine;
using System.Collections;

public class UIUpgradeInfoBG : MonoBehaviour {

    public void setup(float yStart, float height, float width) {

        transform.SetParent(GameObject.Find("Canvas").transform);

        GetComponent<RectTransform>().offsetMax = new Vector2(0, Screen.height * yStart + Screen.height * height);
        GetComponent<RectTransform>().offsetMin = new Vector2(-Screen.height * width, Screen.height * yStart);
    }
}
