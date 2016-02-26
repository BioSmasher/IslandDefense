using UnityEngine;
using System.Collections;

public class UIGoldIcon : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<RectTransform>().offsetMax = new Vector2((Screen.width / -2f) - 190f, -10f);
        GetComponent<RectTransform>().offsetMin = new Vector2((Screen.width / 2f) - 250f, Screen.height - 70f);
    }
	
}
