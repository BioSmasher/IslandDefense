using UnityEngine;
using System.Collections;

public class Ground : MonoBehaviour {
    WorldManager wm;
	// Use this for initialization
	void Start () {
        wm = GameObject.Find("WorldManager").GetComponent<WorldManager>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.GetComponent<TowerGhost>() != null) {
            wm.valid = true;
        }
        if (coll.gameObject.GetComponent<Ship>() != null) {
            coll.gameObject.GetComponent<Ship>().land();
        }
    }

    void OnTriggerExit2D(Collider2D coll) {
        if (coll.gameObject.GetComponent<TowerGhost>() != null) {
            wm.valid = false;
        }
		
    }
}
