using UnityEngine;
using System.Collections;

public class LavaTemple : MonoBehaviour {
    public TowerBase tb;
    Animator anim;

    public float damage;
    public float burnDamage;
    public float poolLife;
    public float speed;
    public string title;
    public string desc;
    public static float rangeStat = 3.5f;
    public GameObject projectilePrefab;
    public GameObject projectile;

    public ArrayList onPool;

    private WorldManager wm;
    // Use this for initialization
    void Start() {
        tb = GetComponent<TowerBase>();
        GetComponent<Selectable>().setup(-1f, damage, speed, title, desc, null, null);
        anim = GetComponent<Animator>();
        InvokeRepeating("fire", speed, speed);

        wm = GameObject.Find("WorldManager").GetComponent<WorldManager>();
        onPool = new ArrayList();
    }

    // Update is called once per frame
    void Update() {

    }

    public void fire() {
        //anim.SetTrigger("FireLeft");

        tb.findTarget();
        if (tb.target != null) {
            anim.SetTrigger("Fire");
            Invoke("resetAnimation", 0.333f);
            Invoke("shoot", 0.1f);
        }


    }

    void shoot() {
        if (tb.target != null) {
            spawnProjectile();
        }
        else {
            tb.findTarget();
            if (tb.target != null) {
                spawnProjectile();
            }
        }
    }

    void spawnProjectile() {
        projectile = (GameObject)Instantiate(projectilePrefab, tb.target.transform.position, Quaternion.identity);
        projectile.GetComponent<LavaPool>().setup(damage, poolLife, burnDamage, this);
    }

    void resetAnimation() {
        anim.SetTrigger("Reset");
    }
}
