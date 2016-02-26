using UnityEngine;
using System.Collections;

public class Mortar1 : MonoBehaviour {

    public TowerBase tb;
    Animator anim;

    public float damage;
    public float speed;
    public float explosionRadius;
    public float scale;
    public string title;
    public string desc;
    public static float rangeStat = 2.5f;
    public GameObject projectilePrefab;
    public GameObject projectile;

    // Use this for initialization
    void Start() {
        tb = GetComponent<TowerBase>();
        GetComponent<Selectable>().setup(-1f, damage, speed, title, desc, null, null);
        anim = GetComponent<Animator>();
        InvokeRepeating("fire", speed, speed);
    }

    public void fire() {
        tb.findTarget();
        if (tb.target != null) {
            anim.SetTrigger("Fire");
            Invoke("resetAnimation", 0.12f);
            Invoke("shoot", 0.1f);
            GetComponent<AudioSource>().Play();
        }


    }

    void shoot() {
        if (tb.target != null) {
            projectile = (GameObject)Instantiate(projectilePrefab, transform.position + new Vector3(0, 1.6f), Quaternion.identity);
            projectile.GetComponent<Bomb>().setup(tb.target, damage, explosionRadius, scale);
        }
        else {
            tb.findTarget();
            if (tb.target != null) {
                projectile = (GameObject)Instantiate(projectilePrefab, transform.position + new Vector3(0, 1.6f), Quaternion.identity);
                projectile.GetComponent<Bomb>().setup(tb.target, damage, explosionRadius, scale);
            }
        }
    }

    void resetAnimation() {
        anim.SetTrigger("Reset");
    }
}

