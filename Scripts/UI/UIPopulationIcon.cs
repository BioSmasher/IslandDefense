using UnityEngine;
using System.Collections;

public class UIPopulationIcon : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<RectTransform>().offsetMax = new Vector2((Screen.width / -2f) + 60f, -10);
        GetComponent<RectTransform>().offsetMin = new Vector2((Screen.width / 2f) + 0f, Screen.height - 60f);
    }
}
