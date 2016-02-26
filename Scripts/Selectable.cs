using UnityEngine;
using System.Collections;

public class Selectable : MonoBehaviour {
    /*public GameObject selectionRingUnitPrefab;
    public GameObject selectionRingTowerPrefab;
    public GameObject selectionRingHousePrefab;
    public GameObject selectionRingFortressPrefab;*/

    public Sprite upgradeASprite;
    public Sprite upgradeBSprite;

    public GameObject selectionRingPrefab;
    public GameObject selectionRing;
    public WorldManager wm;
    public TowerBase tb;
    public bool selected;
    public bool isTower;
    public bool isFortress;
    public bool isWallTower;
    public bool isWallSegment;
    public bool isHouse;
    public bool isFarm;
    public bool isEnemyUnit;
    public bool isShip;
    public bool isFriendlyUnit;

    public float scale;

    public float health;
    public float damage;
    public float attackSpeed;
    public string title;
    public string desc;


    // Use this for initialization
    void Start () {
        selected = false;
        wm = GameObject.Find("WorldManager").GetComponent<WorldManager>();
        if (isTower) {
            tb = GetComponent<TowerBase>();
            select();
        }
	}

    public void setup(float health, float damage, float attackSpeed, string ti, string des, GameObject upgradeAButtonPrefab, GameObject upgradeBButtonPrefab) {
        this.health = health;
        this.damage = damage;
        this.attackSpeed = attackSpeed;
        title = ti;
        desc = des;
    }

    public void setSelect(bool b) {

        if (b && !selected) {
            selectionRing = (GameObject)Instantiate(selectionRingPrefab, transform.position, new Quaternion(0, 0, 0, 0));
            selectionRing.transform.SetParent(transform, true);
            selectionRing.transform.localScale = new Vector3(scale, scale);

            wm.gameObject.GetComponent<InfoManager>().resetPressedFlags();
            selected = true;
            if (isTower) {
                GetComponent<TowerBase>().showRange(true);
                if (GetComponent<Barracks>())
                    GetComponent<Barracks>().spawnRallyFlag();
            }
            
            if (isTower) {
                wm.gameObject.GetComponent<InfoManager>().showInfoTower(gameObject, health, damage, attackSpeed, title, desc, upgradeASprite, upgradeBSprite, isFortress);
            }
            else if (isEnemyUnit || isFriendlyUnit) {
                //show other type of info here!
                //if (GetComponent<Ship>() != null) print("SHIP SELECTED");
                wm.gameObject.GetComponent<InfoManager>().showInfoEnemy(gameObject, title);
            }
            else if (isShip) {

            }
            else if (isHouse) {
                wm.gameObject.GetComponent<InfoManager>().showInfoHouse(gameObject);
            }
            else if (isFarm) {
                wm.gameObject.GetComponent<InfoManager>().showInfoFarm();
            }
            else if (isWallTower) {
                wm.gameObject.GetComponent<InfoManager>().showInfoWallTower(gameObject);
            }
            else if (isWallSegment) {
                wm.gameObject.GetComponent<InfoManager>().showInfoWallSegment(gameObject);
            }
            else {
                wm.gameObject.GetComponent<InfoManager>().clean();
            }
        }
        else if (!b && selected) {
            if (!wm) wm = GameObject.Find("WorldManager").GetComponent<WorldManager>();
            wm.gameObject.GetComponent<InfoManager>().clean();
            Destroy(selectionRing);
            selectionRing = null;

            if (isTower) {
                GetComponent<TowerBase>().showRange(false);
                if (GetComponent<Barracks>())
                   Destroy(GetComponent<Barracks>().rallyFlag);
            }
            selected = false;
        }
        
    }

    public void select() {
        //print("Selected Obj");
        if (!wm) {
            wm = GameObject.Find("WorldManager").GetComponent<WorldManager>();
        }
        
        if (wm.postPan) {
            if (wm.spellActive) {
                if (wm.activeSpell.GetComponent<SpellButton1>()) {
                    wm.activeSpell.GetComponent<SpellButton1>().castSpell(Input.GetTouch(0).position);
                }
                /*else if (wm.activeSpell.GetComponent<SpellButton2>()) {
                    wm.activeSpell.GetComponent<SpellButton2>().castSpell(Input.GetTouch(0).position);
                }
                else if (wm.activeSpell.GetComponent<SpellButton3>()) {
                    wm.activeSpell.GetComponent<SpellButton3>().castSpell(Input.GetTouch(0).position);
                }*/
            }
            if (wm.settingRally) {
                if (wm.selected.gameObject.GetComponent<Barracks>().setRally(Input.GetTouch(0).position)) {
                    wm.settingRally = false;
                }
            }
            
            else {
                if (wm.selected) {

                    wm.selected.setSelect(false);
                }
                wm.selected = this;
                setSelect(true);
            }
        }
    }

    public void clean() {
        Destroy(selectionRing);
        selectionRing = null;

        if (isTower) {
            GetComponent<TowerBase>().showRange(false);
        }
        selected = false;
    }

    void OnDestroy() {
        setSelect(false);
    }

}
