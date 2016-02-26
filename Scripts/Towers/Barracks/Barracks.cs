using UnityEngine;
using System.Collections;

public class Barracks : MonoBehaviour {
    public WorldManager wm;

    public GameObject unitPrefab;

    public Vector3 rallyPoint;
    public Vector3 tempRallyPoint;

    public GameObject rallyFlagPrefab;
    public GameObject rallyFlag;

    public GameObject redXPrefab;

    public short maxUnits;

    public short popCost;

    public ArrayList children;
	// Use this for initialization
	void Start () {
        wm = GameObject.Find("WorldManager").GetComponent<WorldManager>();
        wm.usePop(popCost);
        if (wm.tempBarracksUnits != null) {
            children = wm.tempBarracksUnits;
            wm.tempBarracksUnits = null;
        }
        else {
            children = new ArrayList();
        }

        rallyPoint = transform.position + new Vector3(0, -.8f);

        spawn();

        InvokeRepeating("spawn", 15f, 15f);
    }

    public void spawn() {
        while (children.Contains(null)) {
            children.Remove(null);
        }

        while (children.Count < maxUnits) {
            children.Add((GameObject)Instantiate(unitPrefab, transform.position + new Vector3(0 + Random.Range(-.6f, .6f), Random.Range(-.2f, .4f), 0), Quaternion.identity));
        }

        foreach (GameObject obj in children) {
            if (obj !=  null)
                obj.GetComponent<Unit>().setRally(rallyPoint, false);
        }
    }

    public bool setRally(Vector2 pos) {
        tempRallyPoint = Camera.main.ScreenToWorldPoint(pos);
        tempRallyPoint = tempRallyPoint - new Vector3(0, 0, tempRallyPoint.z);

        if ((tempRallyPoint - transform.position).magnitude <= GetComponent<CircleCollider2D>().radius) {
            Debug.LogError("SettingRallyPoint");
            rallyPoint = tempRallyPoint;
            foreach (GameObject obj in children) {
                if (obj != null)
                    obj.GetComponent<Unit>().setRally(rallyPoint, true);
            }
            spawnRallyFlag();
            wm.gameObject.GetComponent<InfoManager>().rallyButton.GetComponent<UIRallyButton>().reset();
            return true;
        }
        else {
            Instantiate(redXPrefab, tempRallyPoint, Quaternion.identity);
            wm.gameObject.GetComponent<InfoManager>().rallyButton.GetComponent<UIRallyButton>().reset();
        }
        return true;
        
    }

    public void spawnRallyFlag() {
        Destroy(rallyFlag);
        rallyFlag = (GameObject)Instantiate(rallyFlagPrefab, rallyPoint, Quaternion.identity);
    }

    public void storeActiveUnits() {
        wm.tempBarracksUnits = children;
    }

}
