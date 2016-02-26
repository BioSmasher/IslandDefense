using UnityEngine;
using System.Collections;

public class Archer3A : TowerBase {
    public TowerBase tb;
    Animator anim;

    public float damage;
    public float speed;
    public string title;
    public string desc;
    public static float rangeStat = 6f;
    public GameObject projectilePrefab;
    public GameObject projectile;

    public GameObject target1;
    public GameObject target2;

    public GameObject activeTarget;

    public bool firemode = false;
    // Use this for initialization
    new void Start() {
        base.Start();
        
        
        //GetComponent<Selectable>().setup(-1f, damage, speed, title, desc, null, null);
        anim = GetComponent<Animator>();
        InvokeRepeating("fire", speed, speed);
        GetComponent<CircleCollider2D>().radius = rangeStat;
    }

    public void fire() {
        //anim.SetTrigger("FireLeft");
        firemode = !firemode;
        findTarget();
        if (firemode) {
            activeTarget = target1;
        }
        else {
            activeTarget = target2;
        }
        if (!targets.Contains(activeTarget)) {
            activeTarget = null;
        }
        if (activeTarget != null) {
            if (activeTarget.transform.position.x < transform.position.x) {
                anim.SetTrigger("FireLeft");

            }
            else {
                anim.SetTrigger("FireRight");
            }
            Invoke("resetAnimation", 0.333f);
            Invoke("shoot", 0.1f);
        }
    }

    void shoot() {
        if (activeTarget != null) {
            GetComponent<AudioSource>().Play();
            projectile = (GameObject)Instantiate(projectilePrefab, transform.position + new Vector3(0, 1.75f), Quaternion.identity);
            projectile.GetComponent<Arrow>().setup(activeTarget, damage);
        }
    }

    void resetAnimation() {
        anim.SetTrigger("Reset");
    }
    

    public new void findTarget() {
        //print("IN OVERRIDEN FINDTARGET");
        if (targets == null) targets = new ArrayList(); ;
        if (target1 == null || target2 == null || !targets.Contains(target1) || !targets.Contains(target2) || retarget) {
            retarget = false;
            target1 = null;
            target2 = null;
            while (targets.Contains(null)) {
                targets.Remove(null);
            }

            switch (GetComponent<TowerBase>().targetMode) {
                case 0: first(); break;
                case 1: last(); break;
                case 2: closest(); break;
                case 3: strongest(); break;
                case 4: weakest(); break;
                case 5: fighting(); break;
                default: first(); break;
            }
        }
    }

    public new void closest() {
        float closestDist = 1000f;
        float dist = 500f;
        foreach (GameObject obj in targets) {
            if (obj) {
                dist = Vector3.SqrMagnitude(transform.position - obj.transform.position);
                if (dist < closestDist && obj != target1 && obj != target2) {
                    closestDist = dist;
                    target2 = target1;
                    target1 = obj;
                    
                }
            }
        }
    }

    public new void first() {
        float closestDist = 1000f;
        float dist = 500f;
        foreach (GameObject obj in targets) {
            if (obj) {
                dist = Vector3.SqrMagnitude(wm.fortress - obj.transform.position);
                if (dist < closestDist && obj != target1 && obj != target2) {
                    closestDist = dist;
                    target2 = target1;
                    target1 = obj;
                }
            }
        }
    }

    public new void last() {
        float furthestDist = 0f;
        float dist = 0f;
        foreach (GameObject obj in targets) {
            if (obj) {
                dist = Vector3.SqrMagnitude(wm.fortress - obj.transform.position);
                if (dist > furthestDist && obj != target1 && obj != target2) {
                    furthestDist = dist;
                    target2 = target1;
                    target1 = obj;
                }
            }
        }
    }

    public new void strongest() {
        float health = 0;
        Damagable dmg;
        foreach (GameObject obj in targets) {
            if (obj) {
                dmg = obj.GetComponent<Damagable>();
                if (dmg != null) {
                    if (dmg.health > health && obj != target1 && obj != target2) {
                        health = dmg.health;
                        target2 = target1;
                        target1 = obj;
                    }
                }
            }
        }
    }

    public new void weakest() {
        float health = 1000000;
        Damagable dmg;
        foreach (GameObject obj in targets) {
            if (obj) {
                dmg = obj.GetComponent<Damagable>();
                if (dmg != null) {
                    if (dmg.health < health && obj != target1 && obj != target2) {
                        health = dmg.health;
                        target2 = target1;
                        target1 = obj;
                    }
                }
            }
        }
    }

    public new void fighting() {
        foreach (GameObject obj in targets) {
            if (obj) {
                if (obj.GetComponent<Unit>() && obj.GetComponent<Unit>().engaged) {
                    target2 = target1;
                    target1 = obj;
                }
            }
        }
        if (target1 == null && targets.Count != 0) {
            target1 = (GameObject) targets[0];
        }
        if (target2 == null && targets.Count != 0) {
            target2 = (GameObject) targets[0];
        }
    }
}
