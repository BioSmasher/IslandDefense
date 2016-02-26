using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("destroy", .5f);
	}

    public void setup(float rad) {
        transform.localScale = new Vector3(rad, rad, rad);
    }

    void destroy() {
        Destroy(gameObject);
    }
}
