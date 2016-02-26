using UnityEngine;
using System.Collections;

public class TempleStrike : MonoBehaviour {

    public GameObject target;
    public float radius;
    public float damage;

    // Use this for initialization
    void Start() {
        Invoke("strike", 0.2f);
        Invoke("delete", 0.583f);
        GetComponent<SpriteRenderer>().sortingOrder = (int)((200f - transform.position.y) * 50f) + 6;
    }

    public void setup(GameObject targ, float dm) {
        target = targ;
        transform.SetParent(target.transform);
        damage = dm;
        radius = target.GetComponent<Selectable>().scale * 3.5f;
        transform.localScale = new Vector3(radius, radius, radius);
    }

    public void setTarget(GameObject targ) {
        target = targ;
    }

    void strike() {
        target.GetComponent<Damagable>().damageMagic(damage, null);
    }

    void delete() {
        Destroy(gameObject);
    }
}
