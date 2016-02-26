using UnityEngine;
using System.Collections;

public class Mortar2B : MonoBehaviour {

    public TowerBase tb;
    Animator anim;

    public float damage;
    public float speed;
    public float explosionRadius;
    public float spread;
    public float scale;
    public string title;
    public string desc;
    public static float rangeStat = 3f;
    public GameObject projectilePrefab;
    public GameObject projectile;

    private short targetMode;
    private int count;
    // Use this for initialization
    void Start() {
        tb = GetComponent<TowerBase>();
        GetComponent<Selectable>().setup(-1f, damage, speed, title, desc, null, null);
        anim = GetComponent<Animator>();
        InvokeRepeating("fire", speed, speed);
        count = 0;
    }

    // Update is called once per frame
    void Update() {

    }

    public void fire() {
        if (count == 4) {
            count = 0;
            targetMode = tb.targetMode;
            tb.targetMode = (short)Random.Range(0, 5.999f);
            tb.findTarget();
        }
        else {
            count++;
            tb.findTarget();
        }
        

        
        Invoke("resetAnimation", 0.10f);
        
        if (tb.target != null) {
            anim.SetTrigger("Fire");
            
            resetAnimation();
            Invoke("shoot", 0.1f);
            GetComponent<AudioSource>().Play();
        }


    }

    void shoot() {
        if (tb.target != null) {
            spawnBullets();
        }
        else {
            tb.findTarget();
            if (tb.target != null) {
                spawnBullets();
            }
        }
    }

    void resetAnimation() {
        anim.SetTrigger("Reset");
    }

    void spawnBullets() {
        projectile = (GameObject)Instantiate(projectilePrefab, transform.position + new Vector3(0, 1.8f), Quaternion.identity);
        projectile.GetComponent<Bomb>().setup(tb.target.transform.position + new Vector3(0, spread), damage, explosionRadius, scale);

        projectile = (GameObject)Instantiate(projectilePrefab, transform.position + new Vector3(0, 1.8f), Quaternion.identity);
        projectile.GetComponent<Bomb>().setup(tb.target.transform.position + new Vector3(Mathf.Sqrt((spread * spread) / 2), -Mathf.Sqrt((spread * spread) / 2)), damage, explosionRadius, scale);

        projectile = (GameObject)Instantiate(projectilePrefab, transform.position + new Vector3(0, 1.8f), Quaternion.identity);
        projectile.GetComponent<Bomb>().setup(tb.target.transform.position + new Vector3(-Mathf.Sqrt((spread * spread) / 2), -Mathf.Sqrt((spread * spread) / 2)), damage, explosionRadius, scale);
    }
}