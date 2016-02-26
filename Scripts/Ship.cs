using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {
    public ArrayList cargo;
    int spawnCount;
    public float spawnSpeed;
    public float health;

	// Use this for initialization
	void Start () {
        spawnCount = 0;
	}

    //prefabs char types, and amount is how many of each corresponding type.
    public void setup(GameObject prefab1, short amount1) {
        health = 400;
        cargo = new ArrayList();
        for (int i = 0; i < amount1; i++) {
            cargo.Add(prefab1);
        }
        health += prefab1.GetComponent<Damagable>().maxHealth * amount1 / 2f;
        GetComponent<Damagable>().maxHealth = health * .7f;
    }

    public void setup(GameObject prefab1, short amount1, GameObject prefab2, short amount2) {
        health = 400;
        cargo = new ArrayList();
        for (int i = 0; i < amount1; i++) {
            cargo.Add(prefab1);
        }
        health += prefab1.GetComponent<Damagable>().maxHealth * amount1 / 2f;
        for (int i = 0; i < amount2; i++) {
            cargo.Add(prefab2);
        }
        health += prefab2.GetComponent<Damagable>().maxHealth * amount2 / 2f;
        GetComponent<Damagable>().maxHealth = health * .7f;
    }

    public void setup(GameObject prefab1, short amount1, GameObject prefab2, short amount2, GameObject prefab3, short amount3) {
        health = 400;
        cargo = new ArrayList();
        for (int i = 0; i < amount1; i++) {
            cargo.Add(prefab1);
        }
        health += prefab1.GetComponent<Damagable>().maxHealth * amount1 / 2f;
        for (int i = 0; i < amount2; i++) {
            cargo.Add(prefab2);
        }
        health += prefab2.GetComponent<Damagable>().maxHealth * amount2 / 2f;
        for (int i = 0; i < amount3; i++) {
            cargo.Add(prefab3);
        }
        health += prefab3.GetComponent<Damagable>().maxHealth * amount3 / 2f;
        GetComponent<Damagable>().maxHealth = health * .7f;
    }

    public void setup(GameObject prefab1, short amount1, GameObject prefab2, short amount2, GameObject prefab3, short amount3, GameObject prefab4, short amount4) {
        health = 400;
        cargo = new ArrayList();
        for (int i = 0; i < amount1; i++) {
            cargo.Add(prefab1);
        }
        health += prefab1.GetComponent<Damagable>().maxHealth * amount1 / 2f;
        for (int i = 0; i < amount2; i++) {
            cargo.Add(prefab2);
        }
        health += prefab2.GetComponent<Damagable>().maxHealth * amount2 / 2f;
        for (int i = 0; i < amount3; i++) {
            cargo.Add(prefab3);
        }
        health += prefab3.GetComponent<Damagable>().maxHealth * amount3 / 2f;
        for (int i = 0; i < amount4; i++) {
            cargo.Add(prefab4);
        }
        health += prefab4.GetComponent<Damagable>().maxHealth * amount4 / 2f;
        GetComponent<Damagable>().maxHealth = health * .7f;
    }

    public void land() {
		GetComponent<ShipMotion>().moving = false;
        InvokeRepeating("spawnNext", .5f, 1.3f);
    }

    void spawnNext() {
        if (cargo == null) {
            CancelInvoke("spawnNext");
            Invoke("destroy", 0.5f);
        }
        GameObject enemy = (GameObject) Instantiate((GameObject) cargo[spawnCount], transform.position + GetComponent<ShipMotion>().dir * 150f, Quaternion.identity);
        enemy.GetComponent<Damagable>().startHealthRatio = GetComponent<Damagable>().health / GetComponent<Damagable>().maxHealth;
        spawnCount++;
        if (spawnCount + 1 >= cargo.Count) {
            CancelInvoke("spawnNext");
			Invoke("destroy", 0.5f);
        }
    }
	
	void destroy() {
		Destroy(gameObject);
	}

}
