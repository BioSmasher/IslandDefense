using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIPopulationText : MonoBehaviour {
    Text txt;
    // Use this for initialization
    void Start() {
        txt = GetComponent<Text>();
        GetComponent<RectTransform>().offsetMax = new Vector2((Screen.width / -2f) + 370f, -20f);
        GetComponent<RectTransform>().offsetMin = new Vector2((Screen.width / 2f) + 70f, Screen.height - 70f);
    }

    public void updatePopulation(int pop, int maxPop) {
        txt.text = pop.ToString() + " / " + maxPop.ToString();
    }
}
