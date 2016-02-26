using UnityEngine;
using System.Collections;
using Pathfinding;

public class Unit : MonoBehaviour {
    public float damage;
    public float attackSpeed;
    public float targetingRange;
    public bool engaged;
    public bool isEnemy;
    public ArrayList targets;
    public GameObject target;
    public GameObject approachingTarget;
    public GameObject oldTarget;
    public SpriteRenderer spriteRenderer;
    private bool newPath;
    public short houseCount;

    public bool wallAttacker;
    public ArrayList blacklist;

    public GameObject selectionRingPrefab;
    public GameObject selectionRing;

    public Vector3 rallyPoint;
    public bool targetRally;
    Animator anim;

    Damagable dm;

    public GameObject fortress;

    //public bool moving;

    //==============================================================
    //PATHFINDING VARS
    //The point to move to
    public Vector3 targetPosition;
    private Seeker seeker;
    //private CharacterController controller;
    //The calculated path
    public Path path;
    //The AI's speed per second
    public float speed;
    //The max distance from the AI to a waypoint for it to continue to the next waypoint
    public float nextWaypointDistance;
    //The waypoint we are currently moving towards
    private int currentWaypoint = 0;

    private bool turning = true;

    private Vector3 dir;
    //END PAtHFINDING VARS
    //==============================================================

    // Use this for initialization
    void Start() {
        //PATHFINDING
        seeker = GetComponent<Seeker>();
        //controller = GetComponent<CharacterController>();
        //Start a new path to the targetPosition, return the result to the OnPathComplete function
        repath();
        //nextWaypointDistance *= nextWaypointDistance;

        fortress = GameObject.Find("Fortress");
        houseCount = 0;
        //speed /= 40f;
        anim = GetComponent<Animator>();
        engaged = false;
        selectionRing = null;
        targets = new ArrayList();
        if (wallAttacker)
            blacklist = new ArrayList();

        dm = GetComponent<Damagable>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = (int)((200f - transform.position.y) * 50f);
        float rand = 0;// Random.Range(0, .3f);
        InvokeRepeating("expandCircle", .095f + rand, 1f);
        InvokeRepeating("findTarget", .1f + rand, 1f);
        InvokeRepeating("relayer", .7f, .7f);

        nextWaypointDistance += Random.Range(-.1f, .1f);

        if (isEnemy) {
            moveTo(fortress.transform.position);
        }
    }

    public void setup(float dmg) {
        damage = dmg;
    }

    public void FixedUpdate() {

        if (path == null) {
            //We have no path to move after yet
            return;
        }
        if (currentWaypoint >= path.vectorPath.Count) {
            //moving = false;
            dir = Vector3.zero;

            //anim.SetTrigger("Idle");
            if (target != null && newPath) {
                newPath = false;
                if (!engaged)
                    Invoke("fight", 0.1f);
            }
            turn();
            return;
        }
        //else moving = true;
        //Direction to the next waypoint
        dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        dir *= speed * Time.deltaTime;
        //print("Moving");
        transform.Translate(dir);
        if (turning) {
            turning = false;
            turn();
        }
        //Check if we are close enough to the next waypoint
        //If we are, proceed to follow the next waypoint
        if (Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]) < nextWaypointDistance) {
            //if (Vector3.SqrMagnitude(transform.position - path.vectorPath[currentWaypoint]) < nextWaypointDistance) {
            currentWaypoint++;
            turning = true;
            return;
        }
    }

    void relayer() {
        spriteRenderer.sortingOrder = (int)((200f - transform.position.y) * 50f);
    }

    void turn() {
        if (!newPath && target != null) {
            return;
        }
        if (dir.x == 0 && dir.y == 0) {
            anim.SetTrigger("Idle");
        }
        else if (dir.y > 0 && Mathf.Abs(dir.y / dir.x) > 0.84147f) {
            anim.SetTrigger("RunUp");
            if (dir.x > 0) {
                transform.localScale = new Vector3(-1, 1, 1);
                if (dm.hb != null)
                    dm.hb.flip();
            }
            else {
                transform.localScale = new Vector3(1, 1, 1);
                if (dm.hb != null)
                    dm.hb.flip();
            }

        }
        else {
            anim.SetTrigger("RunDown");
            if (dir.x < 0) {
                transform.localScale = new Vector3(-1, 1, 1);
                if (dm.hb != null)
                    dm.hb.flip();
            }
            else {
                transform.localScale = new Vector3(1, 1, 1);
                if (dm.hb != null)
                    dm.hb.flip();
            }
        }

    }

    public void OnPathComplete(Path p) {
        dir = Vector3.zero;
        if (!p.error) {
            path = p;
            //Reset the waypoint counter
            currentWaypoint = 0;
        }
        newPath = true;
        turning = true;
    }

    public void repath() {
        if (seeker == null)
            seeker = GetComponent<Seeker>();
        seeker.StartPath(transform.position, targetPosition, OnPathComplete);
    }

    void expandCircle() {
        GetComponent<CircleCollider2D>().radius = targetingRange;
    }

    public void findTarget() {
        while (targets != null && targets.Contains(null)) {
            targets.Remove(null);
        }
        GetComponent<CircleCollider2D>().radius = .2f;
        if (targets != null && (((target == null || target == fortress) && !wallAttacker && houseCount < 3) || (!isEnemy && engaged))) {
            ////////////////////////////////////////
            //targetSearch
            ////////////////////////////////////////
            oldTarget = target;
            GameObject engagedTarget = null;
            GameObject unengagedTarget = null;
            float closestDist = 1000f;  
            float unengagedClosestDist = 1000f;
            float dist = 0f;
            foreach (GameObject obj in targets) {
                if (obj != null) {
                    dist = Vector3.SqrMagnitude(transform.position - obj.transform.position);
                    if (obj.GetComponent<Unit>() && !obj.GetComponent<Unit>().engaged) {
                        if (dist < unengagedClosestDist) {
                            unengagedClosestDist = dist;
                            unengagedTarget = obj;
                        }
                    }
                    else {
                        if (dist < closestDist) {
                            closestDist = dist;
                            engagedTarget = obj;
                        }
                    }

                }
            }
            ////////////////////////////////////////
            //targetSearch
            ////////////////////////////////////////

            //allied mid fight check for unengaged targets
            if (!isEnemy && engaged) {
                if (unengagedTarget != null) {
                    target = unengagedTarget;
                    if (target.GetComponent<Unit>()) {
                        target.GetComponent<Unit>().engage(gameObject);
                    }
                    engage(target);
                }
            }
            if (!engaged && approachingTarget == null) {
                target = null;
                if (unengagedTarget != null)
                    target = unengagedTarget;
                else
                    target = engagedTarget;
                

                //target Found
                if (target != null) {
                    if (target.GetComponent<House>())
                        houseCount++;
                    if (target.GetComponent<Unit>()) {
                        target.GetComponent<Unit>().engage(gameObject);
                    }
                    engage(target);
                }
                else {
                    if (isEnemy) {
                        target = fortress;
                        engage(target);
                    }
                    else {
                        if (!targetRally) {
                            targetRally = true;
                            moveTo(rallyPoint);
                        }
                    }
                }
            }
        }
        //already attacked lots of houses, do this
        else if (houseCount >= 3 && !engaged && approachingTarget == null) {
            target = fortress;
            engage(target);
        }
        //do wallAttacker stuff
        if (wallAttacker) {
            float closestDist = 1000f;
            float dist = 0f;
            target = null;
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("WallTower")) {
                dist = Vector3.SqrMagnitude(transform.position - obj.transform.position);
                if (dist < closestDist && blacklist != null && !blacklist.Contains(obj)) {
                    closestDist = dist;
                    target = obj;
                }
            }
            if (target != null) {
                engage(target);
                if (path != null && path.vectorPath != null && (path.vectorPath[path.vectorPath.Count - 1] - target.transform.position).magnitude > 1f) {
                    blacklist.Add(target);
                    findTarget();
                }
            }
        }
        //check if approaching is fighting another
        if (approachingTarget != null) {
            if (approachingTarget.GetComponent<Unit>().engaged) {
                approachingTarget = null;
                //findTarget();
            }
        }
    }

    public void fight() {
        approachingTarget = null;
        if (GetComponent<Damagable>().healthBar == null) {
            GetComponent<Damagable>().spawnHealthBar();
        }
        if (target != null) {
            if (target.GetComponent<Unit>()) target.GetComponent<Unit>().approachingTarget = null;
            //check if the target is close enough to fight
            if (((target.transform.position - transform.position).magnitude < nextWaypointDistance + .3f) || (wallAttacker && (target.transform.position - transform.position).magnitude < nextWaypointDistance * 5f + 1f)) {
                if (/*(target.GetComponent<Unit>() == null || (target.GetComponent<Unit>() != null && target.GetComponent<Unit>().engaged)) && */currentWaypoint >= path.vectorPath.Count) {
                    engaged = true;
                    target.GetComponent<Damagable>().damagePhysical(damage, gameObject);
                    anim.SetTrigger("Fight");
                    //set direction to swing in
                    if (target.transform.position.x < transform.position.x) {
                        transform.localScale = new Vector3(-1, 1, 1);
                        if (dm.hb != null)
                            dm.hb.flip();
                    }
                    else {
                        transform.localScale = new Vector3(1, 1, 1);
                        if (dm.hb != null)
                            dm.hb.flip();
                    }
                    Invoke("reset", .417f);
                }
                Invoke("fight", attackSpeed);
            }
            else {
                engaged = false;
                engage(target);
                if (target.GetComponent<Unit>()) target.GetComponent<Unit>().engage(gameObject);
                //moveTo(target.transform.position);
            }
        }
        //if target has died
        else {
            engaged = false;
            expandCircle();
            Invoke("findTarget", .05f);
            Invoke("heal", 4f);
            
        }
    }

    public void reset() {
        anim.SetTrigger("Idle");
        if (wallAttacker)
            Destroy(gameObject);
    }

    public void moveTo(Vector3 pos) {
        if (pos != rallyPoint) {
            targetRally = false;
        }
        else {
            targetRally = true;
        }
        targetPosition = pos + new Vector3(Random.Range(-.15f, .15f), Random.Range(-.15f, .15f));
        repath();
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.GetComponent<Damagable>() != null && coll.gameObject.GetComponent<Damagable>().isEnemy != isEnemy) {
            if (!coll.gameObject.GetComponent<Wall>() || Random.Range(0, 100f) < 10f)
                if (!(isEnemy && coll.GetComponent<Unit>()))
                    targets.Add(coll.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D coll) {
        targets.Remove(coll.gameObject);
    }

    //called when this unit is targeted by another unit
    public void engage(GameObject otherUnit) {
        if (isEnemy) {
            //if otherUnit is a ally
            //if (otherUnit == null) findTarget();
            if (otherUnit != null && otherUnit.GetComponent<Unit>()) {
                if (target != null && target.GetComponent<Unit>() == null && engaged) {
                    //this unit is attacking a building
                    return;
                }
                //if this enemy unit is fighting
                else if (target != null && target.GetComponent<Unit>() && engaged) {
                    /*if (otherUnit.GetComponent<Unit>().engaged)
                        moveTo(otherUnit.transform.position);
                    else
                        moveTo(transform.position);*/
                }
                //if this unit is doing nothing
                else if (!wallAttacker && !engaged) {
                    approachingTarget = otherUnit;
                    target = null;
                    //engaged = true;
                    if (otherUnit.GetComponent<Unit>().engaged)
                        moveTo(otherUnit.transform.position);
                    else {
                        anim.SetTrigger("Idle");
                        moveTo(transform.position);
                    }
                }
            }
            //other is building
            else if (!engaged && approachingTarget == null) {
                moveTo(otherUnit.transform.position);
            }
        }
        //this unit is ally
        else {
            if (!engaged) {
                target = otherUnit;
                moveTo(otherUnit.transform.position);
            }
        }
    }

    //called when this object is attacked. attacks back if no target
    public void attack(GameObject obj) {
        if (!wallAttacker && obj != null && !engaged && obj.GetComponent<TowerBase>() == null && target == null) {
            target = obj;
            //approachingTarget = obj;
            if (!isEnemy) {
                moveTo(target.transform.position);
            }
            else {
                if (!engaged && target == null) {
                    fight();
                }
            }
        }
    }

    public void setRally(Vector3 pos, bool force) {
        if (!isEnemy) {
            rallyPoint = pos;
            if ((target == null && !engaged) || force) {
                targetPosition = pos;
                target = null;
                print("Moving to Rally point");
                moveTo(pos);
                engaged = false;
            }
        }
    }

    public void heal() {
        if (!isEnemy && !engaged) {
            if (dm.health < dm.maxHealth) {
                dm.health += dm.maxHealth / 10f;

                if (dm.health >= dm.maxHealth) {
                    dm.health = dm.maxHealth;
                    dm.hb.rb.setBar(dm.health, dm.maxHealth);
                    return;
                }
                if (dm.hb != null)
                    dm.hb.rb.setBar(dm.health, dm.maxHealth);
                Invoke("heal", 1f);
            }
        }
    }

    void OnDestroy() {
        if (isEnemy) {
            GetComponent<Selectable>().wm.kills++;
        }

        GetComponent<Selectable>().setSelect(false);

        if (oldTarget != null) {
            Unit u = oldTarget.GetComponent<Unit>();
            if (u && u.target == null && u.engaged) {
                u.findTarget();
            }
        }
        
        if (target != null) {
            Unit u = target.GetComponent<Unit>();
            if (u && u.target == null && u.engaged) {
                u.findTarget();
            }
        }
    }

}
