using UnityEngine;
using Pathfinding;
using System.Collections;

public class ShipMotion : MonoBehaviour {

    public SpriteRenderer spriteRenderer;
    //private bool newPath;
    public short houseCount;

    Animator anim;

    Damagable dm;

    public GameObject fortress;

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
    //private int currentWaypoint = 0;

    private bool turning;
	
	public bool moving = true;

    public Vector3 dir;
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
        speed /= 40f;
		moving = true;
        anim = GetComponent<Animator>();

        dm = GetComponent<Damagable>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = (int)((200f - transform.position.y) * 50f);
        InvokeRepeating("relayer", .7f, .7f);

        nextWaypointDistance += Random.Range(-.1f, .1f);

        moveTo(fortress.transform.position);

    }

    public void FixedUpdate() {
		//simple version
		if (moving) {
			dir = (fortress.transform.position - transform.position).normalized * speed;
			transform.Translate(dir);
			turn();
		}
        //GetComponent<Selectable>().select();


        //pathfinding version
        /*if (path == null) {
            //We have no path to move after yet
            return;
        }
        if (currentWaypoint >= path.vectorPath.Count) {

            dir = Vector3.zero;
            turn();
            return;
        }
        //Direction to the next waypoint
        dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        dir *= speed;// * Time.deltaTime;
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
        }*/
    }

    void relayer() {
        spriteRenderer.sortingOrder = (int)((200f - transform.position.y) * 50f);
    }

    void turn() {
        if (dir.x == 0 && dir.y == 0) {
            //anim.SetTrigger("Idle");
        }
        else if (dir.y > 0 && Mathf.Abs(dir.y / dir.x) > 0.84147f) {
            //transform.eulerAngles = new Vector3(0, 0, -Mathf.Atan(dir.y / dir.x) * Mathf.Rad2Deg);
            anim.SetTrigger("Up");
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
        else if (dir.y < 0 && Mathf.Abs(dir.y / dir.x) > 0.84147f) {
            //transform.eulerAngles = new Vector3(0, 0, -Mathf.Atan(dir.y / dir.x) * Mathf.Rad2Deg);
            anim.SetTrigger("Down");
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
        else {
            //transform.eulerAngles = new Vector3(0, 0, 0);
            anim.SetTrigger("Side");
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
            //currentWaypoint = 0;
        }
        //newPath = true;
        //turning = true;
    }

    public void repath() {
        if (seeker == null)
            seeker = GetComponent<Seeker>();
        seeker.StartPath(transform.position, targetPosition, OnPathComplete);
    }

    public void reset() {
        anim.SetTrigger("Idle");
    }

    public void moveTo(Vector3 pos) {
        targetPosition = pos + new Vector3(Random.Range(-.15f, .15f), Random.Range(-.15f, .15f));
        repath();
    }
}
