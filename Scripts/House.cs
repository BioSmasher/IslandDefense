using UnityEngine;
using System.Collections;

public class House : MonoBehaviour {

    public Sprite house1;
    public Sprite house2;
    public Sprite house3;
    public Sprite house4;

    public float value;

    public WorldManager wm;

    Damagable dm;

    SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start () {
        wm = GameObject.Find("WorldManager").GetComponent<WorldManager>();
        wm.addMaxPop(5);
        if (wm.houses == null) wm.houses = new ArrayList();
        wm.houses.Add(transform.position);
        dm = GetComponent<Damagable>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = (int)((200f - transform.position.y) * 50f);
        InvokeRepeating("heal", 15f, 2.5f);
    }

    public void setup(int num) {
        spriteRenderer = GetComponent<SpriteRenderer>();
        switch (num) {
            case 0: spriteRenderer.sprite = house1; break;
            case 1: spriteRenderer.sprite = house2; break;
            case 2: spriteRenderer.sprite = house3; break;
            case 3: spriteRenderer.sprite = house4; break;
            case 4: spriteRenderer.sprite = house1; transform.localScale = new Vector3(-1, 1, 1);  break;
            case 5: spriteRenderer.sprite = house2; transform.localScale = new Vector3(-1, 1, 1); break;
            case 6: spriteRenderer.sprite = house3; transform.localScale = new Vector3(-1, 1, 1); break;
            case 7: spriteRenderer.sprite = house4; transform.localScale = new Vector3(-1, 1, 1); break;
        }
    }

    public void sell() {
        wm.gold += value * GetComponent<Damagable>().health / GetComponent<Damagable>().maxHealth;
        //wm.addMaxPop(-5);
        wm.houses.Remove(transform.position);
        GetComponent<Selectable>().select();
        GetComponent<Selectable>().setSelect(false);
        wm.gameObject.GetComponent<InfoManager>().clean();
        wm.updateGoldText();
        Destroy(gameObject);
        wm.sendMessage("House sold for " + ((int)(value * GetComponent<Damagable>().health / GetComponent<Damagable>().maxHealth)).ToString() + " gold. -5 Population.");
    }

    void OnDestroy() {
        wm.addMaxPop(-5);
        wm.houses.Remove(transform.position);
    }

    void heal() {
        
        if (dm.health < dm.maxHealth) {
            GetComponent<Damagable>().health++;
        }
        if (dm.hb != null)
            dm.hb.rb.setBar(dm.health, dm.maxHealth);
    }
}
