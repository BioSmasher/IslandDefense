using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public partial class SpellButton : MonoBehaviour {

    protected float buttonSize;
    protected float borderSize;
    protected WorldManager wm;
    public bool clicked;
    public bool active;

    public Sprite buttonSprite;
    public Sprite cancelSprite;
    public Sprite lockedSprite;

    public GameObject spellPrefab;
    // Use this for initialization
    public void Start() {
        setup();
        clicked = false;
        active = true;
        wm = GameObject.Find("WorldManager").GetComponent<WorldManager>();
    }

    /*public void castSpell(Vector2 pos) {
        unclick();
        wm.sendMessage("Firing spell 1 BASE");
    }*/

    public void setup() {
        return;
    }

    public void unclick() {
        if (clicked) {
            click();
        }
    }

    public void click() {
        wm.gameObject.GetComponent<InfoManager>().cancelSelect();
        if (active) {
            if (clicked) {
                GetComponent<Image>().sprite = buttonSprite;
                wm.spellActive = false;
                wm.activeSpell = null;
            }
            else {
                GetComponent<Image>().sprite = cancelSprite;
                wm.spellActive = true;
                wm.activeSpell = gameObject;
            }
            clicked = !clicked;
        }
        else {
            wm.sendMessage("Build a Temple to unlock spells.");
        }
    }

    public void activate() {
        active = true;
        clicked = false;
        GetComponent<Image>().sprite = buttonSprite;
    }

    public void deactivate() {
        active = false;
        clicked = false;
        GetComponent<Image>().sprite = lockedSprite;
    }
}
