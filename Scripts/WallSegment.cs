using UnityEngine;
using System.Collections;

public class WallSegment : MonoBehaviour {
    public bool vert;
    public bool hor;
    Wall parentTower;

    WorldManager wm;

    public float offset;
	// Use this for initialization
	void Start () {
        wm = GameObject.Find("WorldManager").GetComponent<WorldManager>();
        GetComponent<SpriteRenderer>().sortingOrder = (int)((200f - transform.position.y - offset) * 50f);
        wm.wallSegments.Add(transform.position);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setup(Wall parent) {
        parentTower = parent;
    }

    public void remove() {
        Destroy(gameObject);
    }

    void OnDestroy() {
        if (wm.wallSegments != null)
            wm.wallSegments.Remove(transform.position);
        if (parentTower != null) parentTower.recalculate();
    }

}
