using UnityEngine;
using Pathfinding;
using System.Collections;

public class Wall : MonoBehaviour {
    WorldManager wm;
    public ArrayList wallSegments;

    public float value;

    public GameObject wallSegmentHPrefab;
    public GameObject wallSegmentVPrefab;

    public float segLength;
    public float segError;

	// Use this for initialization
	void Start () {
        wm = GameObject.Find("WorldManager").GetComponent<WorldManager>();
        GetComponent<SpriteRenderer>().sortingOrder = (int)((200f - transform.position.y) * 50f);

        respawnWallSegments();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void respawnWallSegments() {
        if (wallSegments == null) {
            wallSegments = new ArrayList();
        }
        while (wallSegments.Contains(null)) {
            wallSegments.Remove(null);
        }
        foreach (Vector3 pos in wm.wallTowers) {
            if (Mathf.Abs(pos.x - transform.position.x) > segLength - segError && Mathf.Abs(pos.x - transform.position.x) < segLength + segError && Mathf.Abs(pos.y - transform.position.y) < segError && !wm.wallSegments.Contains((pos + transform.position) / 2f)) {
                GameObject tempWall = (GameObject)Instantiate(wallSegmentHPrefab, (transform.position + pos) / 2f, Quaternion.identity);
                tempWall.GetComponent<WallSegment>().setup(this);
                wallSegments.Add(tempWall);
            }
            else if (Mathf.Abs(pos.y - transform.position.y) > segLength - segError && Mathf.Abs(pos.y - transform.position.y) < segLength + segError && Mathf.Abs(pos.x - transform.position.x) < segError && !wm.wallSegments.Contains((pos + transform.position) / 2f)) {
                GameObject tempWall = (GameObject)Instantiate(wallSegmentVPrefab, (transform.position + pos) / 2f, Quaternion.identity);
                tempWall.GetComponent<WallSegment>().setup(this);
                wallSegments.Add(tempWall);
            }
        }
        recalculate();
    }

    public void recalculate() {
        Bounds bound = GetComponent<BoxCollider2D>().bounds;
        bound.Expand(3.5f);
        var guo = new GraphUpdateObject(bound);
        if (guo != null && AstarPath.active != null)
            AstarPath.active.UpdateGraphs(guo);
    }

    public void sell() {
        wm.gold += value;
        wm.usePop(-3);
        wm.towers.Remove(transform.position);
        GetComponent<Selectable>().select();
        GetComponent<Selectable>().setSelect(false);
        wm.gameObject.GetComponent<InfoManager>().clean();
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("WallSegment")) {
            if ((obj.transform.position - transform.position).magnitude < segLength + segError * 1.1) {
                Destroy(obj);
            }
        }
        wm.updateGoldText();
        Destroy(gameObject);
        wm.sendMessage("Wall Tower sold for " + ((int)value).ToString() + " gold");
        AstarPath.active.Scan();
    }

    public void removeSegments() {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("WallSegment")) {
            if ((obj.transform.position - transform.position).magnitude < segLength + segError * 1.1) {
                Destroy(obj);
            }
        }
        recalculate();
    }

    void OnDestroy() {
        wm.wallTowers.Remove(transform.position);
        removeSegments();
    }
}
