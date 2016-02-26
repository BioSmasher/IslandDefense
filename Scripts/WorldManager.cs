using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
public class WorldManager : MonoBehaviour {

    public Spawner spawner;

    public float border;

    public float gold;
    public float goldIncome; //per 3 seconds
    public int maxPop;
    public int pop;
    public int houseCount;

    public float time;

    public bool overpopulated;

    public int wave;
    public float waveTime;
    public float healthMultiplier;
    public float difficultyMultiplier;

    public bool began;
    public bool paused;

    public bool postPan;
    public short prePostPanCount;

    public bool settingRally;
    public Vector2 lastTouchPos;

    public float width;
    public bool panning;
    Vector3 panPos;

    public bool wallValid;
    public ArrayList wallGuides;
    //public bool isValid; //set by ground collider if towerghost is on land

    public Selectable selected;

    public UIGoldText uiGoldText;
    public UIPopulationText uiPopulationText;
    public UIClock uiClock;

    EventSystem eventSystem;

    public GameObject towerGhostPrefab;
    public GameObject ArcherTowerPrefab;
    public GameObject MortarTowerPrefab;
    public GameObject BarracksPrefab;
    public GameObject TemplePrefab;
    public GameObject WallTowerPrefab;
    public GameObject HousePrefab;
    public GameObject FarmPrefab;
    public GameObject TowerObstaclePrefab;

    public GameObject pauseLargePrefab;
    public GameObject pauseLarge;

    public GameObject messagePrefab;
    public GameObject message;

    public GameObject spellButton1Prefab;
    public GameObject spellButton1;

    public GameObject spellButton2Prefab;
    public GameObject spellButton2;

    public GameObject spellButton3Prefab;
    public GameObject spellButton3;

    public bool spellActive;
    public GameObject activeSpell;

    public HashSet<Vector3> towers;
    public ArrayList houses;
    public ArrayList farms;
    public Vector3 fortress;
    public ArrayList wallTowers;
    public ArrayList wallSegments;
    public ArrayList spawnPoints;

    public ArrayList tempBarracksUnits;

    public AudioClip buildSound;
    AudioSource buildSoundSource;

    //tower info vars
    public GameObject InfoArrowPrefab;
    public GameObject InfoBGPrefab;
    public GameObject InfoTextPrefab;
    GameObject InfoArrow;
    GameObject InfoBG;
    GameObject InfoText;

    public GameObject CloudPrefab;
    ArrayList clouds;
    public int cloudLimit;

    public GameObject GoldNotificationPrefab;

    float cost;
    int selectedTower;
    GameObject towerGhost;
    public bool valid; //set by walls and farms if there is a towerghost in proximity

    int selectedBuilding;

    ///////////////////////////////////////////////////////////////////////////
    //////STATISTICS//////
    ///////////////////////////////////////////////////////////////////////////
    public int kills;

    ///////////////////////////////////////////////////////////////////////////
    //////STATISTICS//////
    ///////////////////////////////////////////////////////////////////////////

    // Use this for initialization
    void Start () {
        Application.targetFrameRate = 40;

        postPan = true;
        paused = false;
        settingRally = false;
        houseCount = 0;
        time = 0;
        wave = 1;
        began = false;
        spawner = gameObject.GetComponent<Spawner>();
        eventSystem = EventSystem.current;
        panning = false;
        valid = true;
        overpopulated = false;
        healthMultiplier = 1f;
        difficultyMultiplier = 1f;

        if (towers == null) towers = new HashSet<Vector3>();
        houses = new ArrayList();
        if (farms == null) farms = new ArrayList();
        wallTowers = new ArrayList();
        wallSegments = new ArrayList();
        wallGuides = new ArrayList();
        spawnPoints = new ArrayList();

        //GetComponent<AudioSource>().PlayOneShot(buildSound, 1);
        buildSoundSource = gameObject.AddComponent<AudioSource>();
        buildSoundSource.clip = buildSound;
        buildSoundSource.loop = false;

        width = (Screen.height / 7f) * 1.25f;

        uiGoldText = GameObject.Find("GoldText").GetComponent<UIGoldText>();
        uiGoldText.updateGold((int)gold);

        uiPopulationText = GameObject.Find("PopulationText").GetComponent<UIPopulationText>();
        uiPopulationText.updatePopulation(pop, maxPop);

        uiClock = GameObject.Find("Clock").GetComponent<UIClock>();
        uiClock.setTime(time);

        begin();

        clouds = new ArrayList();
        InvokeRepeating("spawnCloud", 0f, 22f);
        //selectedTower = 5;
        //showInfo(selectedTower);
    }
	
	// Update is called once per frame
	void Update () {
        if (began) {
            time += Time.deltaTime;
        }

        if (Application.isMobilePlatform) {
            //HANDLE SINGLE TOUCH
            if (Input.touchCount == 1) {
                //Touch begin
                if (Input.GetTouch(0).phase == TouchPhase.Began) {
                    //IF ON SIDE PANEL
                    if (Input.GetTouch(0).position.x < width) {
                        selectedTower = (short) ((Input.GetTouch(0).position.y) / (Screen.height / 7f));
                        switch (selectedTower) {
                            case 0: cost = 100f;  break; //farm
                            case 1: cost = 50f; break; //house
                            case 2: cost = 50f; break; //wall
                            case 3: cost = 100f; break; //temple
                            case 4: cost = 100f; break; //barracks
                            case 5: cost = 100f; break; //mortar
                            case 6: cost = 100f; break; //archer
                        }
                        //GameObject.Find("Button" + selectedTower.ToString()).GetComponent<Image>().color = new Color(100f, 100f, 100f, 255f);
                    }
                    //Not side panel
                    else if (!eventSystem.IsPointerOverGameObject()) {
                        panning = true;
                        panPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                    }
                    else {
                        
                    }
                }
                //Touch end
                else if (Input.GetTouch(0).phase == TouchPhase.Ended) {
                    lastTouchPos = Input.GetTouch(0).position;
                    if (selectedTower < 7 && selectedTower >= 0) {
                        //GameObject.Find("Button" + selectedTower.ToString()).GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
                    }
                    if (panning) {
                        panning = false;
                    }
                    //IF ON SIDE PANEL
                    else if (Input.GetTouch(0).position.x < width) {
                        if (selectedBuilding != -1) {
                            //fix icon
                        }
                        selectedBuilding = (int)((Input.GetTouch(0).position.y) / (Screen.height / 7f));
                        
                        if (selectedBuilding == selectedTower) {
                            showInfoPopCheck();
                            //show info about tower
                            showInfo(selectedTower);
                        }
                    }
                    else if (selectedTower != -1) {
                        Vector3 temp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                        if (towerGhost.GetComponent<TowerGhost>().isValid()) {
                            if (gold >= cost) {
                                if ((maxPop - pop >= 3 && selectedTower >= 3) || (maxPop - pop >= 1 && selectedTower == 2) || (maxPop - pop >= 5 && selectedTower == 0) || (maxPop - pop >= 1 && selectedTower == 2) || selectedTower == 1) {
                                    //spawn tower!
                                    buildSoundSource.Play();
                                    gold -= cost;
                                    uiGoldText.updateGold((int)gold);
                                    GetComponent<AudioSource>().PlayOneShot(buildSound, 1);
                                    foreach (GameObject obj in towerGhost.GetComponent<TowerGhost>().decor) {
                                        Destroy(obj);
                                    }
                                    switch (selectedTower) {
                                        case 0: Instantiate(FarmPrefab, new Vector3(temp.x, temp.y, 0), new Quaternion(0, 0, 0, 0));
                                            //farms.Add(new Vector3(temp.x, temp.y, 0));
                                            //usePop(5); //pop used in Farm.cs
                                            break; //farm
                                        case 1: GameObject houseTemp = (GameObject)Instantiate(HousePrefab, new Vector3(temp.x, temp.y, 0), new Quaternion(0, 0, 0, 0));
                                            //houses.Add(new Vector3(temp.x, temp.y, 0));
                                            houseTemp.GetComponent<House>().setup(towerGhost.GetComponent<TowerGhost>().houseIndex);
                                            break; //house
                                        case 2: wallTowers.Add(new Vector3(temp.x, temp.y, 0));
                                            /*GameObject wallTemp = (GameObject)*/Instantiate(WallTowerPrefab, new Vector3(temp.x, temp.y, 0), new Quaternion(0, 0, 0, 0));
                                            //sendMessage("started wall");
                                            //wallTemp.GetComponent<Wall>().respawnWallSegments();
                                            sendMessage("made walltower successfully");
                                            //towerGhost.GetComponent<TowerGhost>().spawnWallSegments();
                                            //recalculate A*
                                            break; //wall
                                        case 3: Instantiate(TemplePrefab, new Vector3(temp.x, temp.y, 0), new Quaternion(0, 0, 0, 0));
                                            //towers.Add(new Vector3(temp.x, temp.y, 0));
                                            usePop(3);
                                            break; //temple
                                        case 4: Instantiate(BarracksPrefab, new Vector3(temp.x, temp.y, 0), new Quaternion(0, 0, 0, 0));
                                            //towers.Add(new Vector3(temp.x, temp.y, 0));
                                            //barracks use pop in barracks script
                                            break; //barracks
                                        case 5: Instantiate(MortarTowerPrefab, new Vector3(temp.x, temp.y, 0), new Quaternion(0, 0, 0, 0));
                                            //towers.Add(new Vector3(temp.x, temp.y, 0));
                                            usePop(3);
                                            break; //mortar
                                        case 6: Instantiate(ArcherTowerPrefab, new Vector3(temp.x, temp.y, 0), new Quaternion(0, 0, 0, 0));
                                            //towers.Add(new Vector3(temp.x, temp.y, 0));
                                            usePop(3);
                                            break; //archer
                                    }
                                }
                                else sendMessage("Not Enough Population. Build houses!");
                            }
                            else sendMessage("Not Enough Gold!");
                        }
                        else sendMessage("Can't build here!");
                        
                    }
                    Destroy(towerGhost);
                    selectedTower = -1;
                    invokeResetPostPan();
                }
                //Touch move
                else if (Input.GetTouch(0).phase == TouchPhase.Moved) {
                    //PAN!
                    if (panning) {
                        if (prePostPanCount >= 5) {
                            postPan = false;
                        }
                        prePostPanCount++;
                        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                        Camera.main.transform.Translate(panPos - worldPoint);
                        int count = 0;
                        foreach (GameObject obj in clouds) {
                            if (obj != null) {
                                obj.transform.Translate((panPos - worldPoint) * (-.50f - .1f * count));
                            }
                            count++;
                        }
                        //check border
                        if (Mathf.Abs(Camera.main.transform.position.x) > border) {
                            if (Camera.main.transform.position.x > 0)
                                Camera.main.transform.position = new Vector3(border, Camera.main.transform.position.y, -10f);
                            else
                                Camera.main.transform.position = new Vector3(-border, Camera.main.transform.position.y, -10f);
                        }
                        if (Mathf.Abs(Camera.main.transform.position.y) > border) {
                            if (Camera.main.transform.position.y > 0)
                                Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, border, -10f);
                            else
                                Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, -border, -10f);
                        }
                    }
                    else if (selectedTower != -1) {
                        if (Input.GetTouch(0).position.x > width) {
                            if (towerGhost == null) {
                                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                                towerGhost = (GameObject)Instantiate(towerGhostPrefab, new Vector3(pos.x, pos.y, 0), Quaternion.identity);
                                towerGhost.GetComponent<TowerGhost>().setup(selectedTower, cost);
                            }
                            Vector3 temp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                            towerGhost.transform.position = new Vector3(temp.x, temp.y, 0);
                        }
                        else {
                            Destroy(towerGhost);
                        }
                    }
                }
                //Touch stationary
                else if (Input.GetTouch(0).phase == TouchPhase.Stationary) {

                }
            }
            //HANDLE MULTI-TOUCH
            else if (Input.touchCount > 1) {
                if (Input.GetTouch(0).phase == TouchPhase.Began) {

                }
            }
        }
        //HANDLE MOUSE
        else {
            //Touch begin
            if (Input.GetMouseButtonDown(0)) {
                //IF ON SIDE PANEL
                /*if (Input.GetTouch(0).position.x > Screen.width - width) {

                }*/
                //Otherwise
                if (!eventSystem.IsPointerOverGameObject()) {
                    panning = true;
                    panPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }
                else {

                }
            }
            //Touch end
            else if (Input.GetMouseButtonUp(0)) {
                lastTouchPos = Input.mousePosition;
                if (panning) {
                    panning = false;
                }
                //IF ON SIDE PANEL
            }
            if (panning) {
                Camera.main.transform.Translate(panPos - Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }
        }
	}

    public void regold() {
        if (overpopulated) {
            //gold += (int)(goldIncome / 2f);
        }
        else {
            gold += (int)goldIncome;
            GameObject obj;
            obj = (GameObject)Instantiate(GoldNotificationPrefab, fortress + new Vector3(0, 1f, 0), Quaternion.identity);
            obj.GetComponent<GoldNotification>().setup("+5");
            foreach (Vector3 pos in farms) {
                obj = (GameObject)Instantiate(GoldNotificationPrefab, pos, Quaternion.identity);
                obj.GetComponent<GoldNotification>().setup("+1");
            }
        }
        uiGoldText.updateGold((int)gold);
    }

    public void updateGoldText() {
        uiGoldText.updateGold((int)gold);
    }

    public void retime() {
        uiClock.setTime(time);
    }

    public void addMaxPop(int num) {
        maxPop += num;
        updatePopText();
        checkPop();
    }

    public void usePop(int num) {
        pop += num;
        updatePopText();
        checkPop();
    }

    public void updatePopText() {
        if (uiPopulationText != null)
            uiPopulationText.updatePopulation(pop, maxPop);
    }

    public void checkPop() {
        if (pop > maxPop) {
            overpopulated = true;
            popMsg1();
        }
        else {
            overpopulated = false;
        }
        if (maxPop - pop < 5) {
            GameObject.Find("Button0").GetComponent<Image>().color = new Color(1f, 0, 0);
        }
        else {
            GameObject.Find("Button0").GetComponent<Image>().color = new Color(1f, 1f, 1f);
        }
        if (maxPop - pop < 3) {
            GameObject.Find("Button6").GetComponent<Image>().color = new Color(1f, 0, 0);
            GameObject.Find("Button5").GetComponent<Image>().color = new Color(1f, 0, 0);
            GameObject.Find("Button4").GetComponent<Image>().color = new Color(1f, 0, 0);
            GameObject.Find("Button3").GetComponent<Image>().color = new Color(1f, 0, 0);
            //GameObject.Find("Button0").GetComponent<Image>().color = new Color(1f, 0, 0);
        }
        else {
            GameObject.Find("Button6").GetComponent<Image>().color = new Color(1f, 1f, 1f);
            GameObject.Find("Button5").GetComponent<Image>().color = new Color(1f, 1f, 1f);
            GameObject.Find("Button4").GetComponent<Image>().color = new Color(1f, 1f, 1f);
            GameObject.Find("Button3").GetComponent<Image>().color = new Color(1f, 1f, 1f);
            //GameObject.Find("Button0").GetComponent<Image>().color = new Color(1f, 1f, 1f);
        }
    }

    void popMsg1() {
        sendMessage("NOT ENOUGH POPULATION!", true);
        if (overpopulated) Invoke("popMsg2", 3f);
    }

    void popMsg2() {
        sendMessage("ALL TOWERS DEAL HALF DAMAGE!", true);
        if (overpopulated) Invoke("popMsg3", 3f);
    }

    void popMsg3() {
        sendMessage("BUILD MORE HOUSES!", true);
        if (overpopulated) Invoke("popMsg1", 3f);
    }

    public void sendMessage(string msg) {
        sendMessage(msg, false);
    }

    public void sendMessage(string msg, bool isWarning) {
        if (message != null) {
            Destroy(message);
        }
        message = Instantiate(messagePrefab);
        message.GetComponent<UIMessage>().setup(msg);
        if (isWarning) {
            message.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 1f);
        }
    }

    public void begin() {
        InvokeRepeating("regold", 5f, 5f);
        InvokeRepeating("retime", 1f, 1f);
        began = true;
        Invoke("spawn", 10f);
    }

    public void spawn() {
        spawner.spawn(wave);
        wave++;
        Invoke("spawn", waveTime);

        print("spawning wave " + (wave - 1f).ToString());

    }

    public void pause() {
        if (paused) {
            Time.timeScale = 1f;
            paused = false;
            Destroy(pauseLarge);
            //GameObject.Find("PauseLarge").GetComponent<CanvasRenderer>().SetAlpha(0f);
        }
        else {
            Time.timeScale = 0;
            paused = true;
            pauseLarge = (GameObject)Instantiate(pauseLargePrefab);
            pauseLarge.transform.SetParent(GameObject.Find("Canvas").transform);
            pauseLarge.GetComponent<RectTransform>().offsetMax = new Vector2(100f, 100f);
            pauseLarge.GetComponent<RectTransform>().offsetMin = new Vector2(-100f, -100f);
            //GameObject.Find("PauseLarge").GetComponent<CanvasRenderer>().SetAlpha(50f);
        }
    }

    void resetPostPan() {
        postPan = true;
        prePostPanCount = 0;
    }

    void invokeResetPostPan() {
        Invoke("resetPostPan", 0.05f);
    }

    void showInfo(int selectedTower) {
        
        removeInfo();

        
        InfoBG = Instantiate(InfoBGPrefab);
        InfoArrow = Instantiate(InfoArrowPrefab);
        InfoText = Instantiate(InfoTextPrefab);

        InfoArrow.GetComponent<UITowerInfoArrow>().setup(selectedTower);
        InfoBG.GetComponent<UITowerInfoBG>().setup(selectedTower);

        switch (selectedTower) {
            case 0:
                InfoText.GetComponent<UITowerInfoText>().setup("FARM\nGenerates 1 Gold every 5 seconds.\nCost: 100\nPop: 5", selectedTower); break; //farm
            case 1:
                InfoText.GetComponent<UITowerInfoText>().setup("HOUSE\n+5 population limit.\nCost: 50\nPop: +5", selectedTower); break; //house
            case 2:
                InfoText.GetComponent<UITowerInfoText>().setup("WALL\nConnect walls to guide enemies and protect buildings.\nCost: 50\nPop: 1", selectedTower); break; //wall
            case 3:
                InfoText.GetComponent<UITowerInfoText>().setup("TEMPLE\nDeals magic damage. Medium Range.\nCost: 100\nPop: 3", selectedTower); break; //temple
            case 4:
                InfoText.GetComponent<UITowerInfoText>().setup("BARRACKS\nTrains soldiers to fight.\nCost: 100\nPop: 3", selectedTower); break; //barracks
            case 5:
                InfoText.GetComponent<UITowerInfoText>().setup("MORTAR\nDeals AOE damage. Low Range.\nCost: 100\nPop: 3", selectedTower); break; //mortar
            case 6:
                InfoText.GetComponent<UITowerInfoText>().setup("ARCHER\nAttacks individual units. Massive range.\nCost: 100\nPop: 3", selectedTower); break; //archer
        }
    }

    void showInfoPopCheck() {
        if (selectedTower >= 3) {
            if (pop + 3 > maxPop) {
                sendMessage("Low population. Build houses.", true);
            }
        }
        else if (selectedTower == 0) {
            if (pop + 5 > maxPop) {
                sendMessage("Low population. Build houses.", true);
            }
        }
        else {
            sendMessage("Drag towers from side bar to build");
        }
    }

    void spawnCloud() {
        while (clouds.Contains(null)) {
            clouds.Remove(null);
        }
        if (clouds.Count < cloudLimit) {
            clouds.Add(Instantiate(CloudPrefab));
        }
    }

    public void removeInfo() {
        Destroy(InfoArrow);
        Destroy(InfoBG);
        Destroy(InfoText);
    }

    public void toggleSettingRallyPoint() {
        settingRally = !settingRally;
    }

    public void GAMEOVER() {
		
    }

}
