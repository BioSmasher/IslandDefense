using UnityEngine;
using System.Collections;

public class Archer3B : MonoBehaviour {

    public TowerBase tb;
    Animator anim;

    public float damage;
    public float speed;
    public string title;
    public string desc;
    public static float rangeStat = 8f;
    public GameObject projectilePrefab;
    public GameObject projectile;
    // Use this for initialization
    void Start() {
        tb = GetComponent<TowerBase>();
        anim = GetComponent<Animator>();
        InvokeRepeating("fire", speed, speed);
    }

    // Update is called once per frame
    void Update() {

    }

    public void fire() {
        //anim.SetTrigger("FireLeft");

        tb.findTarget();
        if (tb.target != null) {
            rotate(tb.target);
            Invoke("shoot", 0.1f);
        }


    }

    void shoot() {
        if (tb.target != null) {
            projectile = (GameObject)Instantiate(projectilePrefab, transform.position + new Vector3(0, 1.8f), Quaternion.identity);
            projectile.GetComponent<Arrow>().setup(tb.target, damage);
        }
    }

    void rotate(GameObject targ) {
        Vector3 dir = transform.position + new Vector3(0, 1.8f, 0) - targ.transform.position;
        float val = Mathf.Abs(Mathf.Atan(dir.y / dir.x) * Mathf.Rad2Deg);
        if (val > 22.5f && val < 67.5f) {
            //diagonal
            if (dir.x > 0) {
                //right side
                if (dir.y > 0) {
                    //up
                    anim.SetTrigger("LD");
                }
                else {
                    //down
                    anim.SetTrigger("LU");
                }
            }
            else {
                //left side
                if (dir.y > 0) {
                    //up
                    anim.SetTrigger("RD");
                }
                else {
                    //down
                    anim.SetTrigger("RU");
                }
            }
        }
        else {
            //straight
            if (val < 22.5f) {
                //side shot
                if (dir.x < 0) {
                    anim.SetTrigger("R");
                }
                else {
                    anim.SetTrigger("L");
                }
            }
            else {
                //vertical
                if (dir.y < 0) {
                    anim.SetTrigger("U");
                }
                else {
                    anim.SetTrigger("D");
                }
            }
        }
    }
}
