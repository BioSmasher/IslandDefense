using UnityEngine;
using System.Collections;

public class UITargetButton : MonoBehaviour {

    float buttonSize;
    float borderSize;
    // Use this for initialization
    void Start() {
        transform.SetParent(GameObject.Find("Canvas").transform);
        buttonSize = (Screen.height / 7f) - ((Screen.height / 7f) / 8);
        borderSize = ((Screen.height / 7f) / 16);
        GetComponent<RectTransform>().offsetMax = new Vector2((Screen.height / 7f) * -1.3f - borderSize, borderSize + buttonSize);
        GetComponent<RectTransform>().offsetMin = new Vector2((Screen.height / 7f) * -2.3f + borderSize, borderSize);
    }

    public void changeTarget() {
        GameObject wm = GameObject.Find("WorldManager");
        wm.GetComponent<InfoManager>().cancelSelect();

        if (wm.GetComponent<WorldManager>().selected.gameObject.GetComponent<TowerBase>() != null)
            wm.GetComponent<WorldManager>().selected.gameObject.GetComponent<TowerBase>().toggleTargetMode();
        else {
            wm.GetComponent<WorldManager>().selected.gameObject.GetComponent<Archer3A>().toggleTargetMode();
        }
        //update text on info panel
    }
}
