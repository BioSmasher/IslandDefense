using UnityEngine;
using System.Collections;

public class UITowerInfoArrow : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    public void setup(int num) {
        transform.SetParent(GameObject.Find("Canvas").transform);

        GetComponent<RectTransform>().offsetMax = new Vector2(Screen.height / 8f + Screen.height / 25f, Screen.height / 14f + Screen.height / 7f * num + Screen.height / 50f);
        GetComponent<RectTransform>().offsetMin = new Vector2(Screen.height / 8f, Screen.height / 14f + Screen.height / 7f * num - Screen.height / 50f);
    }
}
