using UnityEngine;
using System.Collections;

public class AbyssBall : MonoBehaviour {
    float ballOffset;
    Vector3 targetPos;
    Animator anim;
    Vector3 pos;
    Vector3 towerPos;
    bool firing;
    float damage;

    public GameObject beamPrefab;
    GameObject beam;
    AbyssBeam abyssBeam;

    public GameObject explosionPrefab;
    GameObject explosion;
    AbyssExplosion abyssExplosion;

    public bool grown;
    // Use this for initialization
    void Start () {
        GetComponent<SpriteRenderer>().sortingOrder = (int)((200f - transform.position.y + 1.1f * ballOffset) * 50f);
        anim = GetComponent<Animator>();
        grow();
        grown = false;
        firing = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (grown && firing) {
            if (beam == null) {
                beam = (GameObject)Instantiate(beamPrefab, (transform.position + pos) / 2f, Quaternion.identity);
                abyssBeam = beam.GetComponent<AbyssBeam>();
                abyssBeam.setup(transform.position, towerPos);
            }
            if (explosion == null) {
                explosion = (GameObject)Instantiate(explosionPrefab, pos, Quaternion.identity);
                abyssExplosion = explosion.GetComponent<AbyssExplosion>();
                abyssExplosion.setup(damage);
            }

            pos += (targetPos - pos) * Time.deltaTime;

            abyssBeam.updatePosition(pos);
            abyssExplosion.updatePosition(pos);
        }
	}

    public void setup(float ballOffset, Vector3 initTargPos, Vector3 towerP, float dm) {
        this.ballOffset = ballOffset;
        targetPos = initTargPos;
        pos = targetPos;
        firing = true;
        towerPos = towerP;
        damage = dm;
    }

    public void newTarget(Vector3 pos) {
        firing = true;
        targetPos = pos;
    }

    public void stop() {
        firing = false;
        Destroy(beam);
        abyssBeam = null;
        if (abyssExplosion) {
            abyssExplosion.end();
            abyssExplosion = null;
        }
    }

    void grow() {
        
        anim.SetTrigger("Grow");
        Invoke("setIdle", 0.30f);
    }

    void setIdle() {
        grown = true;
        anim.SetTrigger("Idle");
    }

    public void end() {
        grown = false;
        anim.SetTrigger("Shrink");
        Invoke("destroy", 0.30f);
    }

    public void destroy() {
        Destroy(gameObject);
    }
}
