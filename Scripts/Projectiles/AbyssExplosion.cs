using UnityEngine;
using System.Collections;

public class AbyssExplosion : MonoBehaviour {
    public ArrayList targets;
    public float life;
    public float damage;
    public float fade;

    bool ending;

    // Use this for initialization
    void Start() {
        GetComponent<SpriteRenderer>().sortingOrder = (int)((200f - transform.position.y - 1f) * 50f);
        targets = new ArrayList();
        InvokeRepeating("damageTick", 0.5f, 0.5f);
        InvokeRepeating("relayer", 0.2f, 0.2f);
        //Invoke("end", life - 1f);
        fade = 1f;
        ending = false;
    }

    public void setup(float d) {
        damage = d;
    }

    void damageTick() {
        foreach (GameObject targ in targets) {
            if (targ && targ.GetComponent<Damagable>() && targ.GetComponent<Damagable>().isEnemy) {
                targ.GetComponent<Damagable>().damageMagic(damage, null);
            }
        }
    }

    public void updatePosition(Vector3 pos) {
        if (ending) {
            ending = false;
            fade = 1f;
            CancelInvoke("end");
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, fade);
        }
        transform.position = pos;
        
    }

    void relayer() {
        GetComponent<SpriteRenderer>().sortingOrder = (int)((200f - transform.position.y - 1f) * 50f);
    }

    public void end() {
        Invoke("end", 0.1f);
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, fade);
        ending = true;
        fade -= 0.1f;
        if (fade <= 0) {
            CancelInvoke("end");
            Destroy(gameObject);
        }
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
