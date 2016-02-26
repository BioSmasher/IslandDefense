using UnityEngine;
using System.Collections;

public class WallGuide : MonoBehaviour {
    WorldManager wm;
    public Vector3 towerPos;
    public short dir;
	// Use this for initialization
	void Start () {
        wm = GameObject.Find("WorldManager").GetComponent<WorldManager>();
    }

    public void setup(Vector3 pos) {
        wm = GameObject.Find("WorldManager").GetComponent<WorldManager>();
        foreach (Vector3 tPos in wm.wallTowers) {
            if ((transform.position - tPos).magnitude < .4f && tPos != pos) {
                Destroy(gameObject);
                return;
            }
        }
        towerPos = pos;
        if (pos.x == transform.position.x) {
            if (pos.y > transform.position.y) {
                dir = 0;
            }
            else {
                dir = 2;
            }
        }
        else if (pos.y == transform.position.y) {
            if (pos.x > transform.position.x) {
                dir = 1;
            }
            else {
                dir = 3;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.GetComponent<TowerGhost>()) {
            //wm.sendMessage("Added Guide");
            wm.wallValid = true;
            wm.wallGuides.Add(this);
            
        }
    }

    void OnTriggerStay2D(Collider2D coll) {
        /*if (coll.GetComponent<TowerGhost>()) {
            wm.wallStay = true;
        }*/
    }

    void OnTriggerExit2D(Collider2D coll) {
        if (coll.GetComponent<TowerGhost>()) {
            wm.wallGuides.Remove(this);
            if (wm.wallGuides.Count <= 0) {
                wm.wallValid = false;
            }
        }
    }
}
