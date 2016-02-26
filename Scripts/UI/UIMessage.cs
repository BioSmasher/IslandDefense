using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIMessage : MonoBehaviour {
    Text txt;
    public int life;
    public int fadeIn;

    SpriteRenderer sr;

	// Use this for initialization
	void Start () {
        txt = GetComponent<Text>();
        life = 130;
        fadeIn = 20;
        transform.SetParent(GameObject.Find("Canvas").transform);
        GetComponent<RectTransform>().offsetMax = new Vector2((Screen.width / 2f) + 450, Screen.height - 80f);
        GetComponent<RectTransform>().offsetMin = new Vector2((Screen.width / 2f) - 450f, Screen.height - 150f);
        
    }

    void FixedUpdate() {
        if (fadeIn > 0) {
            txt.color = new Color(255f, 255f, 255f, ((20f - fadeIn) / 20f) * (255f / 20f));
        }
        if (life < 85) {
            txt.color = new Color(255f, 255f, 255f, (life / 85f) * (255f / 85f));
        }
        else {
            txt.color = new Color(255f, 255f, 255f, 255f);
        }
        if (life <= 0) {
            Destroy(gameObject);
        }
        life--;
        fadeIn--;
    }

    public void setup(string msg) {
        txt = GetComponent<Text>();
        txt.text = msg;
    }
}
