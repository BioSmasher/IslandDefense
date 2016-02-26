using UnityEngine;
using System.Collections;

public class Mortar3B : MonoBehaviour {

    public TowerBase tb;
    Animator anim;

    public float damage;
    public float burnDamage;
    public float speed;
    public float explosionRadius;
    public float scale;
    public string title;
    public string desc;
    public static float rangeStat = 3f;
    public GameObject projectilePrefab;
    public GameObject projectile;
    
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
            Invoke("resetAnimation", 0.11f);
            Invoke("shoot", 0.1f);
        }
    }

    void shoot() {
        if (tb.target != null) {
            projectile = (GameObject)Instantiate(projectilePrefab, transform.position + new Vector3(0, 2.5f), Quaternion.identity);
            projectile.GetComponent<FireProjectile>().setup(tb.target, damage, explosionRadius, scale, burnDamage);
        }
        else {
            tb.findTarget();
            if (tb.target != null) {
                projectile = (GameObject)Instantiate(projectilePrefab, transform.position + new Vector3(0, 2.5f), Quaternion.identity);
                projectile.GetComponent<FireProjectile>().setup(tb.target, damage, explosionRadius, scale, burnDamage);
            }
        }
    }

    void resetAnimation() {
        anim.SetTrigger("Reset");
    }
}