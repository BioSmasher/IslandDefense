using UnityEngine;
using System.Collections;

public class Tree : MonoBehaviour {
    public bool isRemovable;
	// Use this for initialization
	void Start () {
        GetComponent<SpriteRenderer>().sortingOrder = (int)((200f - transform.position.y) * 50f);
    }

    void OnTriggerEnter2D(Collider2D coll) {
        TowerGhost tg = coll.gameObject.GetComponent<TowerGhost>();
        if (tg && isRemovable) {
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .5f);
            tg.decor.Add(gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D coll) {
        TowerGhost tg = coll.gameObject.GetComponent<TowerGhost>();
        if (tg && isRemovable) {
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            tg.decor.Remove(gameObject);
        }
    }
}
