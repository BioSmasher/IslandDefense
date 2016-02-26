using UnityEngine;
using System.Collections;

public class Damagable : MonoBehaviour {
    public float health;
    public float maxHealth;
    public float armor;
    public float magicResist;
    public bool isEnemy;
    public float value;
    public float healthBarScale;
    public float healthBarOffset;
    public float startHealthRatio = 1f;

    public GameObject healthBarPrefab;
    public GameObject healthBar;

    public Healthbar hb;

    public WorldManager wm;
    public Selectable sel;
    public InfoManager im;

    float burnDamage;
    GameObject fire;
    bool burning;

	void Start () {
        burning = false;
        wm = GameObject.Find("WorldManager").GetComponent<WorldManager>();
        im = wm.gameObject.GetComponent<InfoManager>();
        sel = GetComponent<Selectable>();
        if (isEnemy) {
            maxHealth *= wm.healthMultiplier * wm.difficultyMultiplier;
        }
        health = maxHealth * startHealthRatio;
        InvokeRepeating("fullHealthCheck", 20f, 15f);
        if (isEnemy) {
            spawnHealthBar();
        }
	}

    public void spawnHealthBar() {
        healthBar = (GameObject)Instantiate(healthBarPrefab);
        hb = healthBar.GetComponent<Healthbar>();
        hb.setup(gameObject, healthBarScale, healthBarOffset);
    }

    public void setup(int h, float ar) {
        setup(h, ar, 0);
    }

    public void setup(int h) {
        setup(h, 0, 0);
    }

    public void setup(int h, float ar, float mr) {
        health = h;
        maxHealth = h;
        armor = ar;
        magicResist = mr;
    }

    public void damage(float dmg, GameObject obj) {
        damagePhysical(dmg, obj);
    }

    public void damagePhysical(float dmg, GameObject obj) {
        if (healthBar == null) spawnHealthBar();
        damageTrue(dmg - dmg * armor);
        hb.rb.setBar(health, maxHealth);
        deathCheck();
        if (GetComponent<Unit>()) {
            GetComponent<Unit>().attack(obj);
        }

        if (sel.selected) {
            im.updateHealth(health, maxHealth);
        }
    }

    public void damageMagic(float dmg, GameObject obj) {
        if (healthBar == null) spawnHealthBar();
        damageTrue(dmg - dmg * magicResist);
        hb.rb.setBar(health, maxHealth);
        deathCheck();
        if (GetComponent<Unit>()) {
            GetComponent<Unit>().attack(obj);
        }
        if (sel.selected) {
            im.updateHealth(health, maxHealth);
        }

    }

    public void damageMagicPercent(float percent, GameObject obj) {
        damageMagic(maxHealth * percent, obj);
    }

    public void damagePhysicalPercent(float percent, GameObject obj) {
        damagePhysical(maxHealth * percent, obj);
    }

    public void damageTrue(float amount) {
        if (isEnemy && wm.overpopulated) {
            health -= amount / 2f;
        }
        else {
            health -= amount;
        }
    }

    public void deathCheck() {
        if (health <= 0) {
            if (isEnemy) {
                wm.gold += value;
                wm.updateGoldText();
            }
            Destroy(gameObject);
        }
    }

    public void fullHealthCheck() {
        if (health >= maxHealth) {
            health = maxHealth;
            if (hb != null)
                Destroy(hb.red);
            Destroy(healthBar);
        }
    }

    public void burn(float bd, GameObject firePrefab) {
        burnDamage = bd;
        
        CancelInvoke("unBurn");
        Invoke("unBurn", 3f);
        if (!burning) {
            InvokeRepeating("applyBurn", .2f, .2f);
            fire = (GameObject)Instantiate(firePrefab, transform.position, Quaternion.identity);
            fire.transform.SetParent(transform);
            fire.GetComponent<SpriteRenderer>().sortingOrder = (int)((200f - transform.position.y + .2f) * 50f);
            float sc = GetComponent<Selectable>().scale * .7f;
            fire.transform.localScale = new Vector3(sc, sc, sc);
        }
        else if (bd > burnDamage) {
            burnDamage = bd;
        }
        burning = true;
    }

    public void applyBurn() {
        if (healthBar == null) spawnHealthBar();
        health -= burnDamage / 15f;
        hb.rb.setBar(health, maxHealth);
        deathCheck();
        if (sel.selected) {
            im.updateHealth(health, maxHealth);
        }
        fire.GetComponent<SpriteRenderer>().sortingOrder = (int)((200f - transform.position.y + .2f) * 50f);
    }

    public void unBurn() {
        Destroy(fire);
        burning = false;
        CancelInvoke("applyBurn");
        burnDamage = 0;
    }

    public void setMaxHealth(float h) {
        float diff = h - maxHealth;
        maxHealth = h;
        health += diff;
    }

}
