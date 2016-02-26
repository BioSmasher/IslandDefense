using UnityEngine;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;

public class TowerBase : MonoBehaviour {
    public WorldManager wm;

    public float value;
    public float range;
    public ArrayList targets;
    public GameObject target;
    public short targetMode;
    public bool retarget;
    public bool isBarracks;

    public GameObject rangeRingPrefab;
    public GameObject rangeRing;

    //public GameObject upgradeAButtonPrefab;
    //public GameObject upgradeBButtonPrefab;

    public SpriteRenderer spriteRenderer;

    public GameObject upgradeAPrefab;
    public GameObject upgradeBPrefab;

    public float upgradeACost;
    public float upgradeBCost;

    public string upgradeAText;
    public string upgradeBText;

    GameObject towerObstacle;

    // Use this for initialization
    public void Start () {
        wm = GameObject.Find("WorldManager").GetComponent<WorldManager>();
        if (wm.towers == null) {
            wm.towers = new HashSet<Vector3>();
        }
        wm.towers.Add(transform.position);

        targetMode = 0;
        targets = new ArrayList();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = (int)((200f - transform.position.y) * 50f);
        range = GetComponent<CircleCollider2D>().radius;
        GetComponent<Selectable>().select();
        retarget = false;
        showRange(false);
        towerObstacle = (GameObject)Instantiate(wm.TowerObstaclePrefab, transform.position, Quaternion.identity);
        towerObstacle.transform.SetParent(transform);
        recalculate();

        upgradeAText = upgradeAText + "\nCost: " + upgradeACost;
        upgradeBText = upgradeBText + "\nCost: " + upgradeBCost;

        removeEscape();
    }

    public void findTarget() {
        if (targets == null) targets = new ArrayList(); ;
        if (target == null || (targets != null && !targets.Contains(target)) || retarget) {
            retarget = false;
            target = null;
            while (targets.Contains(null)) {
                targets.Remove(null);
            }

            switch (targetMode) {
                case 0: first(); break;
                case 1: last(); break;
                case 2: closest(); break;
                case 3: strongest(); break;
                case 4: weakest(); break;
                case 5: fighting(); break;
                default: first(); break;
            }
            //}
        }
        
    }

    public void closest() {
        float closestDist = 1000f;
        float dist = 500f;
        foreach (GameObject obj in targets) {
            if (obj) {
                dist = Vector3.SqrMagnitude(transform.position - obj.transform.position);
                if (dist < closestDist) {
                    closestDist = dist;
                    target = obj;
                }
            }
        }
    }

    public void first() {
        float closestDist = 1000f;
        float dist = 500f;
        foreach (GameObject obj in targets) {
            if (obj) {
                dist = Vector3.SqrMagnitude(wm.fortress - obj.transform.position);
                if (dist < closestDist) {
                    closestDist = dist;
                    target = obj;
                }
            }
           
        }
    }

    public void last() {
        float furthestDist = 0f;
        float dist = 0f;
        foreach (GameObject obj in targets) {
            if (obj) {
                dist = Vector3.SqrMagnitude(wm.fortress - obj.transform.position);
                if (dist > furthestDist) {
                    furthestDist = dist;
                    target = obj;
                }
            }
        }
    }

    public void strongest() {
        float health = 0;
        Damagable dmg;
        foreach (GameObject obj in targets) {
            if (obj) {
                dmg = obj.GetComponent<Damagable>();
                if (dmg != null) {
                    if (dmg.health > health) {
                        health = dmg.health;
                        target = obj;
                    }
                }
            }
        }
    }

    public void weakest() {
        float health = 1000000;
        Damagable dmg;
        foreach (GameObject obj in targets) {
            if (obj) {
                dmg = obj.GetComponent<Damagable>();
                if (dmg != null) {
                    if (dmg.health < health) {
                        health = dmg.health;
                        target = obj;
                    }
                }
            }
        }
    }

    public void fighting() {
        foreach (GameObject obj in targets) {
            if (obj) {
                if (obj.GetComponent<Unit>() && obj.GetComponent<Unit>().engaged) {
                    target = obj;
                }
            }
        }
        if (target == null) {
            first();
        }
    }

    public void showRange(bool state) {
        if (state && GetComponent<Selectable>().selected) {
            rangeRing = (GameObject) Instantiate(rangeRingPrefab, transform.position, new Quaternion(0, 0, 0, 0));
            rangeRing.transform.localScale = new Vector3(range * 2f, range * 2f);
        }
        else if (!state && GetComponent<Selectable>().selected) {
            Destroy(rangeRing);
        }
    }

    public void upgradeA() {
        if (wm.gold >= upgradeACost) {
            if (GetComponent<Barracks>() != null) {
                GetComponent<Barracks>().storeActiveUnits();
            }
            Instantiate(upgradeAPrefab, transform.position, Quaternion.identity);
            wm.gold -= upgradeACost;
            wm.updateGoldText();
            wm.sendMessage("Tower upgraded!");
            Destroy(gameObject);
        }
        else {
            wm.sendMessage("Not enough gold!");
        }
    }

    public void upgradeB() {
        if (wm.gold >= upgradeACost) {
            if (GetComponent<Barracks>() != null) {
                GetComponent<Barracks>().storeActiveUnits();
            }
            Instantiate(upgradeBPrefab, transform.position, Quaternion.identity);
            wm.gold -= upgradeBCost;
            wm.updateGoldText();
            wm.sendMessage("Tower upgraded!");
            Destroy(gameObject);
            
        }
        else {
            wm.sendMessage("Not enough gold!");
        }
    }

    public void sell() {
        wm.gold += value;
        wm.usePop(-3);
        wm.towers.Remove(transform.position);
        GetComponent<Selectable>().select();
        GetComponent<Selectable>().setSelect(false);
        Destroy(rangeRing);
        wm.gameObject.GetComponent<InfoManager>().clean();
        wm.updateGoldText();
        Destroy(gameObject);
        wm.sendMessage("Tower sold for " + ((int)value).ToString() + " gold");
    }

    public void toggleTargetMode() {
        targetMode++;
        if (targetMode > 5) {
            targetMode = 0;
        }
        retarget = true;
        target = null;
        InfoManager im = wm.gameObject.GetComponent<InfoManager>();
        im.targetMode.GetComponent<UITargetMode>().setText(getTargetMode());
    }

    public string getTargetMode() {
        switch (targetMode) {
            case 0: return "First";
            case 1: return "Last";
            case 2: return "Closest";
            case 3: return "Strongest";
            case 4: return "Weakest";
            case 5: return "Fighting";
            default: return "";
        }
    }

    void recalculate() {
        Bounds bound = GetComponent<BoxCollider2D>().bounds;
        bound.Expand(3.5f);
        var guo = new GraphUpdateObject(bound);
        if (guo != null && AstarPath.active != null)
            AstarPath.active.UpdateGraphs(guo);
    }

    void removeEscape() {
        int result = upgradeAText.IndexOf("\\");
        while (result != -1) {
            upgradeAText = upgradeAText.Substring(0, result) + "\n" + upgradeAText.Substring(result + 1);
            result = upgradeAText.IndexOf("\\");
        }

        result = upgradeBText.IndexOf("\\");
        while (result != -1) {
            upgradeBText = upgradeBText.Substring(0, result) + "\n" + upgradeBText.Substring(result + 1);
            result = upgradeBText.IndexOf("\\");
        }
    }

    public void OnTriggerEnter2D(Collider2D coll) {
        if (!isBarracks && coll.gameObject.GetComponent<Damagable>() && coll.gameObject.GetComponent<Damagable>().isEnemy) {
            if (targets == null) {
                targets = new ArrayList();
            }
            targets.Add(coll.gameObject);
        }
    }

    public void OnTriggerExit2D(Collider2D coll) {
        if (!isBarracks) {
            if (targets == null) {
                targets = new ArrayList();
            }
            targets.Remove(coll.gameObject);
        }
    }
}
