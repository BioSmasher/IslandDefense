using UnityEngine;
using System.Collections;

public class UITowerMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
        resize();
        GetComponent<CanvasRenderer>().SetAlpha(255f);
    }
	
    void resize() {
        float width = (Screen.height / 7f) * 1.25f;

        float height = (((width / 125f) * 1000f) - Screen.height) * -1;
        if (height > 0) {
            height = 0;
        }
        GetComponent<RectTransform>().offsetMax = new Vector2(-(Screen.width - width), 0);
        GetComponent<RectTransform>().offsetMin = new Vector2(0, height);
    }
}
