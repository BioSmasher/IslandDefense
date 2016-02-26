using UnityEngine;
using System.Collections;

public class UnselectBackground : MonoBehaviour {
    
    public WorldManager wm;
    // Use this for initialization
    void Start() {
        wm = GameObject.Find("WorldManager").GetComponent<WorldManager>();
    }

    public void selectGround() {
        //print("Selected ground");
        if (!wm) {
            wm = GameObject.Find("WorldManager").GetComponent<WorldManager>();
        }
        //check if the select was just after a pan or zoom
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
                    wm.gameObject.GetComponent<InfoManager>().clean();
                }
                wm.selected = null;
            }
        }
    }
}
