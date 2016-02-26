using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {
    public GameObject target;
    public float arc;
    public int speed; //number of frames it takes to hit
    public int count;
    public Vector3 offset;
    public float yBuffer;
    public float yMove;
    public float damage;
    private short halfSpeed;

    public bool trueDamage;

    public Vector3 pos;

	// Use this for initialization
	void Start() {
        count = 0;
        yBuffer = 0;
        GetComponent<SpriteRenderer>().sortingOrder = 999999;
        halfSpeed = (short) (speed / 2);
        //transform.localScale = new Vector3(-1, 1, 1);
        yMove = (1f - Mathf.Sin(Mathf.PI * ((float)count / speed))) * arc;
        transform.eulerAngles = new Vector3(0, 0, Mathf.Rad2Deg * Mathf.Atan((yMove + offset.y) / offset.x));
    }

    public void setup(GameObject targ, float dm) {
        target = targ;
        damage = dm;
        recalculate();
    }

    public void setTarget(GameObject targ) {
        target = targ;
        recalculate();
    }

    public void recalculate() {
        offset = ((transform.position - new Vector3(0, yBuffer)) - target.transform.position) / (speed - count) * -1f;
        pos = target.transform.position;
        if (pos.x < transform.position.x) transform.localScale = new Vector3(1, 1, 1);
        else transform.localScale = new Vector3(-1, 1, 1);
    }

    void FixedUpdate() {
        if (target == null) {
            if (count >= speed + 40) {
                Destroy(gameObject);
                return;
            }

            if (count <= speed) {

                transform.Translate(offset, Space.World);

                yMove = (1f - Mathf.Sin(Mathf.PI * ((float)count / speed))) * arc;
                if (count > halfSpeed) {
                    yMove *= -1;
                }
                yBuffer += yMove;
                transform.Translate(new Vector3(0, yMove), Space.World);

                transform.eulerAngles = new Vector3(0, 0, Mathf.Rad2Deg * Mathf.Atan((yMove + offset.y) / offset.x));
            }
            count++;
        }
        else {
            if (count >= speed) {
                if (!trueDamage) {
                    target.GetComponent<Damagable>().damagePhysical(damage, null);
                }
                else {
                    target.GetComponent<Damagable>().damageTrue(damage);
                }
                Destroy(gameObject);
                return;
            }
            if (count % 5 == 0) recalculate();
            transform.Translate(offset, Space.World);

            yMove = (1f - Mathf.Sin(Mathf.PI * ((float)count / speed))) * arc;
            if (count > halfSpeed) {
                yMove *= -1;
            }
            yBuffer += yMove;
            transform.Translate(new Vector3(0, yMove), Space.World);

            //rotate in dir of travel;

            transform.eulerAngles = new Vector3(0, 0, Mathf.Rad2Deg * Mathf.Atan((yMove + offset.y) / offset.x));
            count++;
        }
    }
}
