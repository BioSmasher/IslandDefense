using UnityEngine;
using Pathfinding;
using System.Collections;

public class Farm : MonoBehaviour {

    public float value;

    public float incomeRate;
    public float incomeDelay;

    public SpriteRenderer spriteRenderer;

    public WorldManager wm;

    public Sprite f0;
    public Sprite f1;
    public Sprite f2;
    public Sprite f3;
    public Sprite f4;
    public Sprite f5;
    public Sprite f6;
    public Sprite f7;

    // Use this for initialization
    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = (int)((200f - transform.position.y) * 50f);

        wm = GameObject.Find("WorldManager").GetComponent<WorldManager>();
        wm.goldIncome += incomeRate;
        if (wm.farms == null) wm.farms = new ArrayList();
        wm.farms.Add(transform.position);
        wm.usePop(5);

        changeImage();

        Bounds bound = GetComponent<BoxCollider2D>().bounds;
        bound.Expand(1f);
        var guo = new GraphUpdateObject(bound);
        AstarPath.active.UpdateGraphs(guo);
    }

    public void changeImage() {
        switch ((int)Random.Range(0, 7.99f)) {
            case 0: spriteRenderer.sprite = f0; break;
            case 1: spriteRenderer.sprite = f1; break;
            case 2: spriteRenderer.sprite = f2; break;
            case 3: spriteRenderer.sprite = f3; break;
            case 4: spriteRenderer.sprite = f4; break;
            case 5: spriteRenderer.sprite = f5; break;
            case 6: spriteRenderer.sprite = f6; break;
            case 7: spriteRenderer.sprite = f7; break;
        }
        Invoke("changeImage", Random.Range(35f, 150f));
    }

    public void sell() {
        wm.gold += value;
        wm.farms.Remove(transform.position);
        wm.goldIncome -= incomeRate;
        GetComponent<Selectable>().select();
        GetComponent<Selectable>().setSelect(false);
        wm.gameObject.GetComponent<InfoManager>().clean();
        wm.updateGoldText();
        Destroy(gameObject);
        wm.sendMessage("Farm sold for " + ((int)value).ToString() + " gold");
    }
}
