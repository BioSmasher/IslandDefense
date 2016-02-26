using UnityEngine;
using System.Collections;

public class AbyssTemple : MonoBehaviour {
    public TowerBase tb;
    Animator anim;

    public float damage;
    public float speed;
    public float endDelay;
    public float ballOffset;
    public string title;
    public string desc;
    public static float rangeStat = 3.5f;
    public GameObject ballPrefab;
    public GameObject ball;

    float timeNoTarget;

    private WorldManager wm;
    // Use this for initialization
    void Start() {
        tb = GetComponent<TowerBase>();
        GetComponent<Selectable>().setup(-1f, damage, speed, title, desc, null, null);
        InvokeRepeating("fire", speed, speed);
        timeNoTarget = 0;
        //ball = (GameObject)Instantiate(ballPrefab, transform.position + new Vector3(0, ballOffset), Quaternion.identity);
        //ball.GetComponent<AbyssBall>().setup(ballOffset, new Vector3(0, 0, 0));
        wm = GameObject.Find("WorldManager").GetComponent<WorldManager>();
    }

    public void fire() {

        tb.findTarget();
        if (tb.target != null) {
            timeNoTarget = 0;
            if (ball == null) {
                ball = (GameObject)Instantiate(ballPrefab, transform.position + new Vector3(0, ballOffset), Quaternion.identity);
                ball.GetComponent<AbyssBall>().setup(ballOffset, tb.target.transform.position, transform.position, damage);
            }
            ball.GetComponent<AbyssBall>().newTarget(tb.target.transform.position);
        }
        else {
            timeNoTarget += speed;
            if (ball) {
                ball.GetComponent<AbyssBall>().stop();
                if (timeNoTarget >= endDelay) {
                    ball.GetComponent<AbyssBall>().end();
                }
            }
            
        }
    }
}
