using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GoldNotification : MonoBehaviour {
    Text txt;
    float life;
	// Use this for initialization
	void Start () {
        life = 1f;
	}

    public void setup(string msg) {
        txt = GetComponent<Text>();
        txt.text = msg;
    }

    void Update() {
        life -= Time.deltaTime;
        
        transform.position = transform.position + new Vector3(0, 0.5f * Time.deltaTime, 0);
        if (life < 0) {
            Destroy(gameObject);
        }
        txt.color = new Color(1f, 1f, 1f, life);
        GetComponentInChildren<Image>().color = new Color(1f, 1f, 1f, life);
    }
}
