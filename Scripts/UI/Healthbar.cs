using UnityEngine;
using System.Collections;

public class Healthbar : MonoBehaviour {
    public GameObject redPrefab;
    public GameObject red;

    public GameObject par;

    public Redbar rb;

    public float scale;
    public float offset;
	// Use this for initialization
	void Start () {
        flip();
        GetComponent<SpriteRenderer>().sortingOrder = 1000000;
    }

    public void setup(GameObject obj, float hbScale, float off) {
        offset = off;
        scale = hbScale;
        par = obj;
        red = (GameObject) Instantiate(redPrefab, transform.position, Quaternion.identity);
        red.transform.SetParent(transform);
        red.transform.Translate(new Vector3(.5f, 0), 0);

        rb = red.GetComponent<Redbar>();
        transform.position = obj.transform.position + new Vector3(0, offset);
        transform.SetParent(obj.transform);
    }

    public void flip() {
        transform.localScale = new Vector3(scale * par.transform.localScale.x, scale, scale);
    }
}
