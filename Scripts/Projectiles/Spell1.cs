using UnityEngine;
using System.Collections;

public class Spell1 : MonoBehaviour {
    public ArrayList targets;
    public int speed; //number of frames it takes to hit
    public float damage;
    public float damagePercentage;
    private int halfSpeed;

    public Vector3 pos;

    // Use this for initialization
    void Start() {
        halfSpeed = (int) (speed / 2f);
        GetComponent<SpriteRenderer>().sortingOrder = (int)((200f - transform.position.y) * 50f);
        targets = new ArrayList();
    }

    void FixedUpdate() {
        if (speed == halfSpeed) {
            foreach (GameObject obj in targets) {
                if (obj != null)
                    obj.GetComponent<Damagable>().damageMagicPercent(damagePercentage, null);
            }
        }
        if (speed <= 0) {
            Destroy(gameObject);
        }
        speed--;
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.GetComponent<Damagable>() && coll.gameObject.GetComponent<Damagable>().isEnemy) {
            targets.Add(coll.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D coll) {
        targets.Remove(coll.gameObject);
    }
}
