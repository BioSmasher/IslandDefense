using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InfoManager : MonoBehaviour {

    public Sprite checkButton;
    public Sprite oldButton;
    public GameObject upgradeAButtonPrefab;
    public GameObject upgradeBButtonPrefab;
    GameObject upgradeAButton;
    GameObject upgradeBButton;

    public Sprite upgradeASprite;
    public Sprite upgradeBSprite;
    public Sprite sellSprite;
    public Sprite rebuildSprite;
    public Sprite removeSprite;
    public Sprite lockedSprite;

    public GameObject sellButtonPrefab;
    GameObject sellButton;

    public GameObject rebuildButtonPrefab;
    GameObject rebuildButton;

    public GameObject removeButtonPrefab;
    GameObject removeButton;

    public GameObject targetButtonPrefab;
    GameObject targetButton;

    public GameObject infoPanelPrefab;
    GameObject infoPanel;

    public GameObject titlePrefab;
    GameObject title;

    public GameObject healthBarPrefab;
    GameObject healthBar;

    public GameObject descPrefab;
    GameObject desc;

    public GameObject targetModePrefab;
    public GameObject targetMode;

    public GameObject rallyButtonPrefab;
    public GameObject rallyButton;

    public GameObject upgradeInfoBGPrefab;
    GameObject upgradeInfoBG;
    public GameObject upgradeInfoTextPrefab;
    GameObject upgradeInfoText;

   // float buttonSize = (Screen.height / 7f) - ((Screen.height / 7f) / 8);
    //float borderSize = ((Screen.height / 7f) / 16);


    public bool APressed;
    public bool BPressed;
    public bool sellPressed;
    public bool rebuildPressed;
    public bool removePressed;

    private WorldManager wm;

    // Use this for initialization
    void Start () {
        APressed = false;
        BPressed = false;
        sellPressed = false;
        rebuildPressed = false;
        removePressed = false;

        wm = GetComponent<WorldManager>();
	}


    public void showInfoEnemy(GameObject enemy, string ti) {
        clean();

        infoPanel = Instantiate(infoPanelPrefab);
        infoPanel.GetComponent<UIInfoPanel>().setup(0.4f, 0.2f);

        title = Instantiate(titlePrefab);
        title.GetComponent<UIInfoTitle>().setup(ti, 0.4f, 0.2f);

        healthBar = Instantiate(healthBarPrefab);
        healthBar.GetComponent<UIHealthBar>().setup(0.375f, 0.1f, 0.35f, 0.06f, enemy.GetComponent<Damagable>().health, enemy.GetComponent<Damagable>().maxHealth);

        updateHealth(enemy.GetComponent<Damagable>().health, enemy.GetComponent<Damagable>().maxHealth);
    }

    public void showInfoHouse(GameObject house) {
        clean();

        infoPanel = Instantiate(infoPanelPrefab);
        infoPanel.GetComponent<UIInfoPanel>().setup(0.45f, 0.315f);

        sellButton = Instantiate(sellButtonPrefab);
        sellButton.GetComponent<UISellButton>().setup(0.45f, true);
        

        title = Instantiate(titlePrefab);
        title.GetComponent<UIInfoTitle>().setup("House", 0.45f, 0.315f);

        healthBar = Instantiate(healthBarPrefab);
        healthBar.GetComponent<UIHealthBar>().setup(0.425f, 0.21f, 0.4f, 0.06f, house.GetComponent<Damagable>().health, house.GetComponent<Damagable>().maxHealth);

        desc = Instantiate(descPrefab);
        desc.GetComponent<UIInfoDesc>().setup("+5 Population", 0.315f);

        updateHealth(house.GetComponent<Damagable>().health, house.GetComponent<Damagable>().maxHealth);
    }

    public void showInfoCity(string ti) {
        clean();
        infoPanel = Instantiate(infoPanelPrefab);
        infoPanel.GetComponent<UIInfoPanel>().setup("info");

        title = Instantiate(titlePrefab);
        title.GetComponent<UIInfoTitle>().setup(ti, "health");

        sellButton = Instantiate(sellButtonPrefab);
    }

    public void showInfoWallTower(GameObject obj) {
        clean();
        infoPanel = Instantiate(infoPanelPrefab);
        infoPanel.GetComponent<UIInfoPanel>().setup(0.4f, 0.24f);

        title = Instantiate(titlePrefab);
        title.GetComponent<UIInfoTitle>().setup("Wall Tower", 0.4f, 0.24f);

        sellButton = Instantiate(sellButtonPrefab);
        sellButton.GetComponent<UISellButton>().setup(0.4f, true);

        rebuildButton = Instantiate(rebuildButtonPrefab);
        rebuildButton.GetComponent<UIInfoRebuildWallButton>().setup(0.17f);
    }

    public void showInfoWallSegment(GameObject obj) {
        clean();
        infoPanel = Instantiate(infoPanelPrefab);
        infoPanel.GetComponent<UIInfoPanel>().setup(0.5f, 0.24f);

        title = Instantiate(titlePrefab);
        title.GetComponent<UIInfoTitle>().setup("Wall Segment", 0.5f, 0.24f);

        removeButton = Instantiate(removeButtonPrefab);
        removeButton.GetComponent<UIInfoRemoveWallButton>().setup(0.5f, true);
    }

    public void showInfoFarm() {
        clean();
        infoPanel = Instantiate(infoPanelPrefab);
        infoPanel.GetComponent<UIInfoPanel>().setup(0.4f, 0.24f);

        title = Instantiate(titlePrefab);
        title.GetComponent<UIInfoTitle>().setup("Farm", 0.4f, 0.24f);

        desc = Instantiate(descPrefab);
        desc.GetComponent<UIInfoDesc>().setup("+1Gold/5sec", 0.24f);

        sellButton = Instantiate(sellButtonPrefab);
        sellButton.GetComponent<UISellButton>().setup(0.4f, true);
    }

    public void showInfoTower(GameObject obj, float health, float damage, float attackSpeed, string ti, string des, Sprite upgradeASpr, Sprite upgradeBSpr, bool isFortress) {
        clean();

        upgradeASprite = upgradeASpr;
        upgradeBSprite = upgradeBSpr;

        infoPanel = Instantiate(infoPanelPrefab);
        infoPanel.GetComponent<UIInfoPanel>().setup(0.856f, 0.333f);
        

        title = Instantiate(titlePrefab);
        title.GetComponent<UIInfoTitle>().setup(ti, 0.856f, 0.333f);

        //desc = Instantiate(descPrefab);
        //desc.GetComponent<UIInfoDesc>().setup(des);
        if (!isFortress) {
            sellButton = Instantiate(sellButtonPrefab);
            sellButton.GetComponent<UISellButton>().setup(0.45f);
        }
        else {
            desc = Instantiate(descPrefab);
            desc.GetComponent<UIInfoDesc>().setup("Protect your Fortress to Win!", 0.333f);

            healthBar = Instantiate(healthBarPrefab);
            healthBar.GetComponent<UIHealthBar>().setup(0.831f, 0.23f, 0.8f, 0.06f, obj.GetComponent<Damagable>().health, obj.GetComponent<Damagable>().maxHealth);
        }

        if (upgradeASprite != null) {
            upgradeAButton = Instantiate(upgradeAButtonPrefab);
            upgradeAButton.GetComponent<Image>().sprite = upgradeASprite;
        }
        else {
            upgradeAButton = Instantiate(upgradeAButtonPrefab);
            upgradeAButton.GetComponent<Image>().sprite = lockedSprite;
            upgradeAButton.GetComponent<UIUpgradeAButton>().invalid = true;
        }
        if (upgradeBSprite != null) {
            upgradeBButton = Instantiate(upgradeBButtonPrefab);
            upgradeBButton.GetComponent<Image>().sprite = upgradeBSprite;
        }
        else {
            upgradeBButton = Instantiate(upgradeBButtonPrefab);
            upgradeBButton.GetComponent<Image>().sprite = lockedSprite;
            upgradeBButton.GetComponent<UIUpgradeBButton>().invalid = true;
        }

        if (obj.GetComponent<Barracks>()) {
            rallyButton = Instantiate(rallyButtonPrefab);
        }
        else {
            targetButton = Instantiate(targetButtonPrefab);
            targetMode = Instantiate(targetModePrefab);
            targetMode.GetComponent<UITargetMode>().setText(GetComponent<WorldManager>().selected.gameObject.GetComponent<TowerBase>().getTargetMode());
        }

        
    }

    public void clean() {
        Destroy(infoPanel);
        Destroy(title);
        Destroy(desc);
        Destroy(upgradeAButton);
        Destroy(upgradeBButton);
        Destroy(sellButton);
        Destroy(targetButton);
        Destroy(targetMode);
        Destroy(rallyButton);
        Destroy(rebuildButton);
        Destroy(removeButton);
        Destroy(healthBar);
        APressed = false;
        BPressed = false;
        sellPressed = false;
        cleanUpgradeInfo();
        wm.removeInfo();
    }

    public void cleanUpgradeInfo() {
        Destroy(upgradeInfoText);
        Destroy(upgradeInfoBG);
    }

    public void unclickSpells() {
        if (wm.spellButton1 != null) {
            wm.spellButton1.GetComponent<SpellButton>().unclick();
        }

        if (wm.spellButton2 != null) {
            wm.spellButton2.GetComponent<SpellButton>().unclick();
        }

        if (wm.spellButton3 != null) {
            wm.spellButton3.GetComponent<SpellButton>().unclick();
        }
        wm.spellActive = false;
    }

    public void upgradeA() {
        unclickSpells();
        if (APressed) {
            GetComponent<WorldManager>().selected.gameObject.GetComponent<TowerBase>().upgradeA();
            upgradeAButton.GetComponent<Image>().sprite = upgradeASprite;
            APressed = false;
        }
        else {
            APressed = true;
            upgradeAButton.GetComponent<Image>().sprite = checkButton;
            if (BPressed) {
                cleanUpgradeInfo();
                upgradeBButton.GetComponent<Image>().sprite = upgradeBSprite;
                BPressed = false;
            }
            if (sellPressed) {
                sellPressed = false;
                sellButton.GetComponent<Image>().sprite = sellSprite;
            }
            if (rebuildPressed) {
                rebuildButton.GetComponent<Image>().sprite = rebuildSprite;
                rebuildPressed = false;
            }
            if (removePressed) {
                removeButton.GetComponent<Image>().sprite = removeSprite;
                removePressed = false;
            }
            
            showUpgradeA();
        }
    }

    public void upgradeB() {
        unclickSpells();
        if (BPressed) {
            GetComponent<WorldManager>().selected.gameObject.GetComponent<TowerBase>().upgradeB();
            upgradeBButton.GetComponent<Image>().sprite = upgradeBSprite;
            BPressed = false;
        }
        else {
            BPressed = true;
            upgradeBButton.GetComponent<Image>().sprite = checkButton;
            if (APressed) {
                cleanUpgradeInfo();
                upgradeAButton.GetComponent<Image>().sprite = upgradeASprite;
                APressed = false;
            }
            if (sellPressed) {
                sellPressed = false;
                sellButton.GetComponent<Image>().sprite = sellSprite;
            }
            if (rebuildPressed) {
                rebuildButton.GetComponent<Image>().sprite = rebuildSprite;
                rebuildPressed = false;
            }
            if (removePressed) {
                removeButton.GetComponent<Image>().sprite = removeSprite;
                removePressed = false;
            }
            
            showUpgradeB();
        }
    }

    public void sell() {
        unclickSpells();
        if (sellPressed) {
            //check if it is a house or farm or tower
            if (GetComponent<WorldManager>().selected.gameObject.GetComponent<TowerBase>()) {
                GetComponent<WorldManager>().selected.gameObject.GetComponent<TowerBase>().sell();
            }
            else if (GetComponent<WorldManager>().selected.gameObject.GetComponent<House>()) {
                GetComponent<WorldManager>().selected.gameObject.GetComponent<House>().sell();
            }
            else if (GetComponent<WorldManager>().selected.gameObject.GetComponent<Farm>()) {
                GetComponent<WorldManager>().selected.gameObject.GetComponent<Farm>().sell();
            }
            else if (GetComponent<WorldManager>().selected.gameObject.GetComponent<Wall>()) {
                GetComponent<WorldManager>().selected.gameObject.GetComponent<Wall>().sell();
            }
            sellButton.GetComponent<Image>().sprite = sellSprite;
            sellPressed = false;
        }
        else {
            sellPressed = true;
            sellButton.GetComponent<Image>().sprite = checkButton;
            if (BPressed) {
                cleanUpgradeInfo();
                upgradeBButton.GetComponent<Image>().sprite = upgradeBSprite;
                BPressed = false;
            }
            if (APressed) {
                cleanUpgradeInfo();
                upgradeAButton.GetComponent<Image>().sprite = upgradeASprite;
                APressed = false;
            }
            if (rebuildPressed) {
                rebuildButton.GetComponent<Image>().sprite = rebuildSprite;
                rebuildPressed = false;
            }
            if (removePressed) {
                removeButton.GetComponent<Image>().sprite = removeSprite;
                removePressed = false;
            }
            showSell();
        }
        rebuildPressed = false;
    }

    public void rebuild() {
        unclickSpells();
        if (rebuildPressed) {
            GetComponent<WorldManager>().selected.gameObject.GetComponent<Wall>().respawnWallSegments();
            GetComponent<WorldManager>().sendMessage("Walls Rebuilt");
            rebuildButton.GetComponent<Image>().sprite = rebuildSprite;
            rebuildPressed = false;
        }
        else {
            rebuildPressed = true;
            rebuildButton.GetComponent<Image>().sprite = checkButton;
            if (BPressed) {
                cleanUpgradeInfo();
                upgradeBButton.GetComponent<Image>().sprite = upgradeBSprite;
                BPressed = false;
            }
            if (APressed) {
                cleanUpgradeInfo();
                upgradeAButton.GetComponent<Image>().sprite = upgradeASprite;
                APressed = false;
            }
            if (sellPressed) {
                sellButton.GetComponent<Image>().sprite = sellSprite;
                sellPressed = false;
            }
            if (removePressed) {
                removeButton.GetComponent<Image>().sprite = removeSprite;
                removePressed = false;
            }
        }
    }

    public void remove() {
        unclickSpells();
        if (removePressed) {
            GetComponent<WorldManager>().selected.gameObject.GetComponent<WallSegment>().remove();
            GetComponent<WorldManager>().sendMessage("Wall Removed. Rebuild at a Wall Tower.");
            removeButton.GetComponent<Image>().sprite = removeSprite;
            removePressed = false;
        }
        else {
            removePressed = true;
            removeButton.GetComponent<Image>().sprite = checkButton;
            if (BPressed) {
                cleanUpgradeInfo();
                upgradeBButton.GetComponent<Image>().sprite = upgradeBSprite;
                BPressed = false;
            }
            if (APressed) {
                cleanUpgradeInfo();
                upgradeAButton.GetComponent<Image>().sprite = upgradeASprite;
                APressed = false;
            }
            if (sellPressed) {
                sellButton.GetComponent<Image>().sprite = sellSprite;
                sellPressed = false;
            }
            if (rebuildPressed) {
                rebuildButton.GetComponent<Image>().sprite = rebuildSprite;
                rebuildPressed = false;
            }
        }
    }

    public void cancelSelect() {
        //unclickSpells();
        if (BPressed) {
            cleanUpgradeInfo();
            upgradeBButton.GetComponent<Image>().sprite = upgradeBSprite;
            BPressed = false;
        }
        else if (APressed) {
            cleanUpgradeInfo();
            upgradeAButton.GetComponent<Image>().sprite = upgradeASprite;
            APressed = false;
        }
        else if (sellPressed) {
            cleanUpgradeInfo();
            sellPressed = false;
            sellButton.GetComponent<Image>().sprite = sellSprite;
        }
        else if (rebuildPressed) {
            cleanUpgradeInfo();
            rebuildPressed = false;
            rebuildButton.GetComponent<Image>().sprite = rebuildSprite;
        }
        else if (removePressed) {
            cleanUpgradeInfo();
            removePressed = false;
            removeButton.GetComponent<Image>().sprite = removeSprite;
        }
    }

    public void resetPressedFlags() {
        APressed = false;
        BPressed = false;
        sellPressed = false;
        rebuildPressed = false;
        removePressed = false;
    }

    public void showUpgradeA() {
        upgradeInfoBG = Instantiate(upgradeInfoBGPrefab);
        upgradeInfoBG.GetComponent<UIUpgradeInfoBG>().setup(0.333f, 0.2f, 0.6f);

        upgradeInfoText = Instantiate(upgradeInfoTextPrefab);
        upgradeInfoText.GetComponent<UIUpgradeInfoText>().setup(wm.selected.GetComponent<TowerBase>().upgradeAText, 0.333f, 0.2f, 0.6f);
    }

    public void showUpgradeB() {
        upgradeInfoBG = Instantiate(upgradeInfoBGPrefab);
        upgradeInfoBG.GetComponent<UIUpgradeInfoBG>().setup(0.333f, 0.2f, 0.6f);

        upgradeInfoText = Instantiate(upgradeInfoTextPrefab);
        upgradeInfoText.GetComponent<UIUpgradeInfoText>().setup(wm.selected.GetComponent<TowerBase>().upgradeBText, 0.333f, 0.2f, 0.6f);
    }

    public void showSell() {

    }

    public void updateHealth(float health, float maxHealth) {
        if (healthBar != null) {
            healthBar.GetComponent<UIHealthBar>().setBar(health, maxHealth);
        }
    }
}
