﻿using UnityEngine;
using System.Collections;

public class Archer2B : MonoBehaviour {
    public TowerBase tb;
    Animator anim;

    public float damage;
    public float speed;
    public string title;
    public string desc;
    public static float rangeStat = 6f;
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
            if (tb.target.transform.position.x < transform.position.x) {
                anim.SetTrigger("FireLeft");

            }
            else {
                anim.SetTrigger("FireRight");
            }
            Invoke("resetAnimation", 0.333f);
            Invoke("shoot", 0.2f);
        }


    }

    void shoot() {
        if (tb.target != null) {
            projectile = (GameObject)Instantiate(projectilePrefab, transform.position + new Vector3(0, 1.6f), Quaternion.identity);
            projectile.GetComponent<Arrow>().setup(tb.target, damage);
        }
        else {
            tb.findTarget();
            if (tb.target != null) {
                projectile = (GameObject)Instantiate(projectilePrefab, transform.position + new Vector3(0, 1.6f), Quaternion.identity);
                projectile.GetComponent<Arrow>().setup(tb.target, damage);
            }
        }
    }

    void resetAnimation() {
        anim.SetTrigger("Reset");
    }
}