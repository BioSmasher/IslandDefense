using UnityEngine;
using System.Collections;

public class AbyssBeam : MonoBehaviour {
    public Vector3 ballPosition;

    public Vector3 pos;
    Vector3 diff;
    Vector3 towerPos;
	// Use this for initialization
	void Start () {
	    
	}

    public void setup(Vector3 p, Vector3 t) {
        ballPosition = p;
        towerPos = t;
    }

    // Update is called once per frame
    public void updatePosition(Vector3 p) {
        pos = p;
        diff = (ballPosition - pos);
        if (diff.y > 0) {
            transform.eulerAngles = new Vector3(0, 0, Mathf.Rad2Deg * Mathf.Atan(diff.y / diff.x));
        }
        else {
            transform.eulerAngles = new Vector3(0, 0, Mathf.Rad2Deg * Mathf.Atan(diff.y / diff.x));
        }
        transform.localScale = new Vector3(diff.magnitude, 1f, 1f);
        transform.position = (ballPosition + pos) / 2f;
        if (pos.y < towerPos.y) {
            GetComponent<SpriteRenderer>().sortingOrder = (int)((200f - towerPos.y + 3f) * 50f) + 1;
        }
        else {
            GetComponent<SpriteRenderer>().sortingOrder = (int)((200f - towerPos.y) * 50f) - 1;
        }
        
    }
}
