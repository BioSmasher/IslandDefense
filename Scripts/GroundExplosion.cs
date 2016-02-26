using UnityEngine;
using System.Collections;

public class GroundExplosion : MonoBehaviour {
    public float scale;

	// Use this for initialization
	void Start () {
        Invoke("delete", .417f);
        GetComponent<SpriteRenderer>().sortingOrder = (int)((200f - transform.position.y + .5f) * 50f);
    }

    // Update is called once per frame
    public void setup(float sc) {
        transform.localScale = new Vector3(sc, sc, sc);
    }

    void delete() {
        Destroy(gameObject);
    }
}
