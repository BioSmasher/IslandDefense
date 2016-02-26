using UnityEngine;
using System.Collections;

public class UIRedX : MonoBehaviour {
    public float count = 0;
    public float scale;
	// Use this for initialization
	void Start () {
        scale = 1f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (count >= 40) {
            Destroy(gameObject);
        }
        else {
            scale = .7f + .3f * (count / 40f);
            transform.localScale = new Vector3(scale, scale, scale);
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f - (1f * (count / 40f)));
            count++;
            
        }
	}
}
