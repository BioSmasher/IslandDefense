using UnityEngine;
using System.Collections;

public class FireProjectile : MonoBehaviour {
    public GameObject target;
    public ArrayList targets;
    public float arc;
    public int speed; //number of frames it takes to hit
    public float radius;
    public GameObject explosionPrefab;
    public GameObject firePrefab;
    public int count;
    public Vector3 offset;
    public float yBuffer;
    public float yMove;
    public float damage;
    private short halfSpeed;
    public float scale;
    public float burnDamage;

    public Vector3 pos;

    // Use this for initialization
    void Start() {
        count = 0;
        yBuffer = 0;
        GetComponent<SpriteRenderer>().sortingOrder = 999999;
        halfSpeed = (short)(speed / 2);
        targets = new ArrayList();
        transform.localScale = new Vector3(scale, scale, scale);
        transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 180));
    }

    public void setup(GameObject targ, float dm, float rad, float sc, float burnD) {
        target = targ;
        damage = dm;
        radius = rad;
        scale = sc;
        burnDamage = burnD;
        GetComponent<CircleCollider2D>().radius = radius;
        recalculate();
    }

    public void setTarget(GameObject targ) {
        target = targ;
        recalculate();
    }

    public void recalculate() {
        offset = ((transform.position - new Vector3(0, yBuffer)) - target.transform.position) / (speed - count) * -1f;
        pos = target.transform.position;
    }

    void FixedUpdate() {
        if (count > speed) {
            foreach (GameObject obj in targets) {
                if (obj != null) {
                    obj.GetComponent<Damagable>().damagePhysical(damage, null);
                    obj.GetComponent<Damagable>().burn(burnDamage, firePrefab);
                }
            }
            GameObject explosion = (GameObject)Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            explosion.GetComponent<GroundExplosion>().setup(radius);
            Destroy(gameObject);
            return;
        }
        transform.Translate(offset, Space.World);

        yMove = (1f - Mathf.Sin(Mathf.PI * ((float)count / speed))) * arc;
        if (count > halfSpeed) {
            yMove *= -1;
        }
        yBuffer += yMove;
        transform.Translate(new Vector3(0, yMove), Space.World);
        transform.eulerAngles = transform.eulerAngles + new Vector3(0, 0, 13);
        count++;

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