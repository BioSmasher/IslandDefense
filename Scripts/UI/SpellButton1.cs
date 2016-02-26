using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpellButton1 : SpellButton {
    // Use this for initialization

    public void Start() {
        setup();
        clicked = false;
        active = true;
        GetComponent<Image>().sprite = buttonSprite;
        wm = GameObject.Find("WorldManager").GetComponent<WorldManager>();
        //Instantiate(spellPrefab, new Vector3(-8.7f, 5.3f, 0), Quaternion.identity);
    }

    public void castSpell(Vector2 pos) {
        Vector3 position = Camera.main.ScreenToWorldPoint(pos);
        position = position - new Vector3(0, 0, position.z);
        unclick();
        wm.sendMessage("Firing spell 1");
        Instantiate(spellPrefab, position, Quaternion.identity);
    }

    new public void setup() {
        float width = (Screen.height / 7f) * 1.25f;
        buttonSize = (Screen.height / 7f) - ((Screen.height / 7f) / 8);
        borderSize = ((Screen.height / 7f) / 16);
        transform.SetParent(GameObject.Find("Canvas").transform);
        buttonSize = (Screen.height / 7f) - ((Screen.height / 7f) / 8);
        borderSize = ((Screen.height / 7f) / 16);
        //Debug.LogError("KASJDLASDJLKD");
        GetComponent<RectTransform>().offsetMax = new Vector2(width + borderSize + buttonSize, borderSize + buttonSize);
        GetComponent<RectTransform>().offsetMin = new Vector2(width + borderSize, borderSize);
    }

    new public void click() {
        base.click();
    }

}