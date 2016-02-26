using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIHealthBar : MonoBehaviour {

    public GameObject redBarPrefab;
    public GameObject redBar;

    public GameObject txtPrefab;
    public GameObject txt;
    public float x;
    public float y;
    public float length;
    public float height;
    // Use this for initialization
    void Start() {
        transform.SetParent(GameObject.Find("Canvas").transform);
        //GetComponent<RectTransform>().offsetMax = new Vector2(-(Screen.height / 7f) / 8, Screen.height / 7f);
        //GetComponent<RectTransform>().offsetMin = new Vector2((Screen.height / 7f) * -1.5f, Screen.height / 9.5f);
        //redBar = Instantiate(redBarPrefab);
        //txt = Instantiate(txtPrefab);
    }

    public void setBar(float health, float maxHealth) {
        if (redBar == null) {
            redBar = Instantiate(redBarPrefab);
            redBar.GetComponent<UIRedBar>().setup(health, maxHealth, gameObject);
            redBar.transform.SetParent(transform);
            redBar.GetComponent<RectTransform>().offsetMax = new Vector2(0, Screen.height * height);
            redBar.transform.SetAsLastSibling();
            //redBar.GetComponent<RectTransform>().offsetMin = new Vector2((health / maxHealth) * Screen.height * length - Screen.height * x, Screen.height * y - Screen.height * height);
            txt = Instantiate(txtPrefab);
            txt.GetComponent<RectTransform>().offsetMax = new Vector2(Screen.width - Screen.height * x + Screen.height * length, Screen.height * y);
            txt.GetComponent<RectTransform>().offsetMin = new Vector2(Screen.width - Screen.height * x + Screen.height * length * 0.05f, Screen.height * y - Screen.height * height);
            //txt.transform.SetAsLastSibling();
        }
        redBar.GetComponent<RectTransform>().offsetMin = new Vector2(-((maxHealth - health) / maxHealth) * Screen.height * length, 0);
        txt.GetComponent<Text>().text = ((int)health).ToString() + "/" + ((int)maxHealth).ToString();
    }

    public void setup(string ty, float h, float mh) {
        setBar(h, mh);
        //print(health + " " + maxHealth);
        transform.SetParent(GameObject.Find("Canvas").transform);
        if (ty == "health") {
            GetComponent<RectTransform>().offsetMax = new Vector2(-(Screen.height / 7f) / 8, Screen.height / 7f);
            GetComponent<RectTransform>().offsetMin = new Vector2((Screen.height / 7f) * -1.5f, Screen.height / 9.5f);
        }
        if (ty == "house") {
            GetComponent<RectTransform>().offsetMax = new Vector2(-(Screen.height / 7f) / 8, Screen.height / 7f);
            GetComponent<RectTransform>().offsetMin = new Vector2((Screen.height / 7f) * -1.5f, Screen.height / 9.5f);
        }
    }

    public void setup(float xx, float yy, float len, float heig, float h, float mh) {
        x = xx;
        y = yy;
        length = len;
        height = heig;

        setBar(h, mh);
        
        transform.SetParent(GameObject.Find("Canvas").transform);
        GetComponent<RectTransform>().offsetMax = new Vector2(-Screen.height * x + Screen.height * length, Screen.height * y);
        GetComponent<RectTransform>().offsetMin = new Vector2(-Screen.height * x, Screen.height * y - Screen.height * height);
    }

    public void OnDestroy() {
        Destroy(redBar);
        Destroy(txt);
    }
}
