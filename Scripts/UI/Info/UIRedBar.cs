using UnityEngine;
using System.Collections;

public class UIRedBar : MonoBehaviour {
    private float health;
    private float maxHealth;
	// Use this for initialization
	void Start () {
        //transform.SetParent(GameObject.Find("Canvas").transform);
        //GetComponent<RectTransform>().offsetMax = new Vector2(-1f - (Screen.height / 7f) / 8, Screen.height / 7f);
        //GetComponent<RectTransform>().offsetMin = new Vector2(-(Screen.height / 7f) / 8, Screen.height / 9.5f);

        //GetComponent<RectTransform>().offsetMax = new Vector2(-1f - (Screen.height / 7f) / 8, Screen.height / 7f);
        //GetComponent<RectTransform>().offsetMin = new Vector2((((maxHealth - health) / maxHealth) * (Screen.height / 7f) * -1.5f), Screen.height / 9.5f);
    }

    public void setup(float h, float mh, GameObject healthBar) {
        //transform.SetParent(GameObject.Find("Canvas").transform);
        //health = h;
        //maxHealth = mh;
        //GetComponent<RectTransform>().offsetMax = new Vector2(Screen.height * healthBar.GetComponent<UIHealthBar>().x + Screen.height * healthBar.GetComponent<UIHealthBar>().length, Screen.height * healthBar.GetComponent<UIHealthBar>().y);
        //GetComponent<RectTransform>().offsetMin = new Vector2(Screen.height * healthBar.GetComponent<UIHealthBar>().x + Screen.height * healthBar.GetComponent<UIHealthBar>().length, Screen.height * healthBar.GetComponent<UIHealthBar>().y - Screen.height * healthBar.GetComponent<UIHealthBar>().height);
    }

    
}
