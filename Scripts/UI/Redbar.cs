using UnityEngine;
using System.Collections;

public class Redbar : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //transform.localScale = new Vector3(0, 1f, 1f);
        GetComponent<SpriteRenderer>().sortingOrder = 1000001;
    }

    public void setBar(float health, float maxHealth) {
        if (GetComponentInParent<Wall>()) print(health + " " + maxHealth);
        transform.localScale = new Vector3((maxHealth - health) / maxHealth, 1f, 1f);
    }
}
