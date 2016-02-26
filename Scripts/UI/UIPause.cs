using UnityEngine;
using System.Collections;

public class UIPause : MonoBehaviour {

    // Use this for initialization
    void Start() {
        GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
        GetComponent<RectTransform>().offsetMin = new Vector2(Screen.width - 90f, Screen.height - 90f);
        //GameObject.Find("PauseLarge").GetComponent<CanvasRenderer>().SetAlpha(0f);
    }
}