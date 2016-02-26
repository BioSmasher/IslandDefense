using UnityEngine;
using System.Collections;

public class TowerGhost : MonoBehaviour {
    public float segLength;
    public float segError;

    public Sprite archer;
    public Sprite mortar;
    public Sprite barracks;
    public Sprite temple;
    public Sprite wallTower;
    public Sprite farm;

    public Sprite house1;
    public Sprite house2;
    public Sprite house3;
    public Sprite house4;
    public int houseIndex;

    GameObject rangeRing;
    public GameObject rangeRingPrefab;
    public GameObject wallGuidePrefab;
    public GameObject wallSegmentHGhostPrefab;
    public GameObject wallSegmentVGhostPrefab;
    public GameObject wallSegmentHPrefab;
    public GameObject wallSegmentVPrefab;
    GameObject tempWallSegment;
    public ArrayList wallGuides;
    public ArrayList wallSegments;
    public ArrayList decor;

    public SpriteRenderer spriteRenderer;
    WorldManager wm;

    public float towerDistance;
    public float houseDistance;
    public float farmDistance;
    public float wallDistance;
    public float wallMaxDistance;
    public float fortressDistance;

    float distance;

    public bool valid;

    public int pop;
    float cost;
    
    int type;
    // Use this for initialization
    void Start() {
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = (int)((200f - transform.position.y) * 50f);
        for (int i = -10; i < 10; i++) {
            for (int j = -10; j < 10; j++) {
                //Grids.Add(Instantiate(GridPrefab, new Vector3(5f * i, 5f * j, 0), new Quaternion(0, 0, 0, 0)));
            }
        }
        wm = GameObject.Find("WorldManager").GetComponent<WorldManager>();

        decor = new ArrayList();

        valid = false;
        pop = 3;

    }

    public void setup(int ty, float cost) {
        wm = GameObject.Find("WorldManager").GetComponent<WorldManager>();
        type = ty;
        switch (ty) {
            case 0: GetComponent<SpriteRenderer>().sprite = farm;
                distance = farmDistance; break; //farm
            case 1: distance = houseDistance; setupHouse(); break; //house
            case 2: distance = wallDistance;
                
                wm.wallGuides.Clear();
                wm.wallValid = false;
                wallGuides = new ArrayList();
                wallSegments = new ArrayList();
                wallSetup();
                GetComponent<SpriteRenderer>().sprite = wallTower;
                InvokeRepeating("wallCheck", .2f, .2f);
                break; //wall
            case 3: GetComponent<SpriteRenderer>().sprite = temple;
                rangeRing = (GameObject) Instantiate(rangeRingPrefab, transform.position, Quaternion.identity);
                rangeRing.transform.localScale = new Vector3(Archer1.rangeStat * 2f, Archer1.rangeStat * 2f);
                distance = towerDistance;
                break; //temple
            case 4: GetComponent<SpriteRenderer>().sprite = barracks;
                rangeRing = (GameObject)Instantiate(rangeRingPrefab, transform.position, Quaternion.identity);
                rangeRing.transform.localScale = new Vector3(Archer1.rangeStat * 2f, Archer1.rangeStat * 2f);
                distance = towerDistance;
                break; //barracks
            case 5: GetComponent<SpriteRenderer>().sprite = mortar;
                rangeRing = (GameObject)Instantiate(rangeRingPrefab, transform.position, Quaternion.identity);
                rangeRing.transform.localScale = new Vector3(Mortar1.rangeStat * 2f, Mortar1.rangeStat * 2f);
                distance = towerDistance;
                break; //mortar
            case 6: GetComponent<SpriteRenderer>().sprite = archer;
                rangeRing = (GameObject)Instantiate(rangeRingPrefab, transform.position, Quaternion.identity);
                rangeRing.transform.localScale = new Vector3(Archer1.rangeStat * 2f, Archer1.rangeStat * 2f);
                distance = towerDistance;
                break; //archer
        }
    }
	
	// Update is called once per frame
	void Update () {
        spriteRenderer.sortingOrder = (int)((200f - transform.position.y) * 50f);
        if (rangeRing) {
            rangeRing.transform.position = transform.position;
            
        }
        valid = isValid();
        if (!valid) {
            GetComponent<SpriteRenderer>().color = new Color(1f, 0, 0, 0.3f);
        }
        else {
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.3f);
            if (type == 2) { ///if it is a wall
                //CHECK WALLS
            }
        }
        
	}

    public bool isValid() {
        if (wm.gold < cost) {
            wm.sendMessage("Not enough Gold!");
            return false;
        }
        if (pop + wm.pop > wm.maxPop && type != 1) {
            wm.sendMessage("Not enough Population. Build houses!");
            return false;
        }
        if (wm.towers != null)
            foreach (Vector3 pos in wm.towers) {
                if ((pos - transform.position).magnitude < distance + towerDistance) {
                    return false;
                }
            }
        if (wm.houses != null)
            foreach (Vector3 pos in wm.houses) {
                if ((pos - transform.position).magnitude < distance + houseDistance) {
                    return false;
                }
            }
        if (wm.farms != null)
            foreach (Vector3 pos in wm.farms) {
                if ((pos - transform.position).magnitude < distance + farmDistance) {
                    return false;
                }
            }
        if (wm.wallTowers != null)
            foreach (Vector3 pos in wm.wallTowers) {
                if ((pos - transform.position).magnitude < wallDistance) {
                    return false;
                }
            }
        if (wm.wallSegments != null)
            foreach (Vector3 pos in wm.wallTowers) {
                if ((pos - transform.position).magnitude < wallDistance) {
                    return false;
                }
            }
        if (Vector3.Magnitude(wm.fortress - transform.position) < fortressDistance) {
            return false;
        }
        if (!wm.valid)
            return false;
        return true;
    }

    public void wallCheck() {
        if (wm.wallSegments != null)
            foreach (GameObject obj in wallSegments) {
                Destroy(obj);
            }
        wallSegments.Clear();
        if (wm.wallGuides != null) {
            foreach (WallGuide wg in wm.wallGuides) {
                if (wg.dir == 1 || wg.dir == 3) {
                    tempWallSegment = (GameObject)Instantiate(wallSegmentHGhostPrefab, (transform.position + wg.towerPos) / 2f, Quaternion.identity);
                    tempWallSegment.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .5f);
                }
                else {
                    tempWallSegment = (GameObject)Instantiate(wallSegmentVGhostPrefab, (transform.position + wg.towerPos) / 2f, Quaternion.identity);
                }
                if (pop + wm.pop > wm.maxPop) {
                    tempWallSegment.GetComponent<SpriteRenderer>().color = new Color(1f, 0, 0, .5f);
                }
                wallSegments.Add(tempWallSegment);
            }
        }
    }

    public void wallSetup() {
        //if (wm.wallTowers != null) {
            
            GameObject wallGuideTemp;
            foreach (Vector3 pos in wm.wallTowers) {
                wallGuideTemp = (GameObject)Instantiate(wallGuidePrefab, pos + new Vector3(segLength, 0), Quaternion.identity);
                wallGuideTemp.GetComponent<WallGuide>().setup(pos);
                wallGuides.Add(wallGuideTemp);

                wallGuideTemp = (GameObject)Instantiate(wallGuidePrefab, pos + new Vector3(-segLength, 0), Quaternion.identity);
                wallGuideTemp.GetComponent<WallGuide>().setup(pos);
                wallGuides.Add(wallGuideTemp);

                wallGuideTemp = (GameObject)Instantiate(wallGuidePrefab, pos + new Vector3(0, segLength), Quaternion.identity);
                wallGuideTemp.GetComponent<WallGuide>().setup(pos);
                wallGuides.Add(wallGuideTemp);

                wallGuideTemp = (GameObject)Instantiate(wallGuidePrefab, pos + new Vector3(0, -segLength), Quaternion.identity);
                wallGuideTemp.GetComponent<WallGuide>().setup(pos);
                wallGuides.Add(wallGuideTemp);
            }
        //}
    }


    void setupHouse() {
        houseIndex = Random.Range(0, 8);
        spriteRenderer = GetComponent<SpriteRenderer>();
        switch (houseIndex) {
            case 0: spriteRenderer.sprite = house1; break;
            case 1: spriteRenderer.sprite = house2; break;
            case 2: spriteRenderer.sprite = house3; break;
            case 3: spriteRenderer.sprite = house4; break;
            case 4: spriteRenderer.sprite = house1; transform.localScale = new Vector3(-1, 1, 1); break;
            case 5: spriteRenderer.sprite = house2; transform.localScale = new Vector3(-1, 1, 1); break;
            case 6: spriteRenderer.sprite = house3; transform.localScale = new Vector3(-1, 1, 1); break;
            case 7: spriteRenderer.sprite = house4; transform.localScale = new Vector3(-1, 1, 1); break;
        }
    }

    void OnDestroy() {
        Destroy(rangeRing);
        if (wallGuides != null) {
            foreach (GameObject obj in wallGuides) {
                Destroy(obj);
            }
        }
        if (wallSegments != null) {
            foreach (GameObject obj in wallSegments) {
                Destroy(obj);
            }
        }
        foreach (GameObject obj in decor) {
            obj.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        }
    }
}
