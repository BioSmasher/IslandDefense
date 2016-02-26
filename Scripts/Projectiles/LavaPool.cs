using UnityEngine;
using System.Collections;

public class LavaPool : MonoBehaviour {
    public ArrayList targets;
    public float life;
    public float damage;
    public float fade;
    public GameObject firePrefab;
    public float burnDamage;

    LavaTemple tower;

    // Use this for initialization
    void Start() {
        GetComponent<SpriteRenderer>().sortingOrder = (int)((200f - transform.position.y - 1f) * 50f);
        targets = new ArrayList();
        InvokeRepeating("damageTick", 0.5f, 0.5f);
        Invoke("end", life - 1f);
        fade = 1f;
    }

    public void setup(float d, float l, float bd, LavaTemple t) {
        damage = d;
        life = l;
        burnDamage = bd;
        tower = t;
    }

    void damageTick() {
        foreach (GameObject targ in targets) {
            if (targ && targ.GetComponent<Damagable>() && targ.GetComponent<Damagable>().isEnemy) {
                targ.GetComponent<Damagable>().damageMagic(damage, null);
            }
        }
    }

    void end() {
        Invoke("end", 0.1f);
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, fade);
        //GetComponent<Animator>().
        fade -= 0.1f;
        if (fade <= 0) {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.GetComponent<Damagable>() && coll.gameObject.GetComponent<Damagable>().isEnemy && !tower.onPool.Contains(coll.gameObject)) {
            if (targets == null) targets = new ArrayList();
            targets.Add(coll.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D coll) {
        if (targets.Contains(coll.gameObject)) {
            tower.onPool.Remove(coll.gameObject);
            targets.Remove(coll.gameObject);
        }
        
    }
}
