using UnityEngine;
using System.Collections;

public class Mortar2A : MonoBehaviour {

    public TowerBase tb;
    Animator anim;

    public float damage;
    public float speed;
    public float explosionRadius;
    public float scale;
    public string title;
    public string desc;
    public static float rangeStat = 3f;
    public GameObject projectilePrefab;
    public GameObject projectile;
    // Use this for initialization
    void Start() {
        tb = GetComponent<TowerBase>();
        GetComponent<Selectable>().setup(-1f, damage, speed, title, desc, null, null);
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
            anim.SetTrigger("Fire");
            Invoke("resetAnimation", 0.11f);
            Invoke("shoot", 0.1f);
            GetComponent<AudioSource>().Play();
        }


    }

    void shoot() {
        if (tb.target != null) {
            projectile = (GameObject)Instantiate(projectilePrefab, transform.position + new Vector3(0, 1.8f), Quaternion.identity);
            projectile.GetComponent<Bomb>().setup(tb.target, damage, explosionRadius, scale);
        }
        else {
            tb.findTarget();
            if (tb.target != null) {
                projectile = (GameObject)Instantiate(projectilePrefab, transform.position + new Vector3(0, 1.8f), Quaternion.identity);
                projectile.GetComponent<Bomb>().setup(tb.target, damage, explosionRadius, scale);
            }
        }
    }

    void resetAnimation() {
        anim.SetTrigger("Reset");
    }
}