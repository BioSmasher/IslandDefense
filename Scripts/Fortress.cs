using UnityEngine;
using System.Collections;

public class Fortress : MonoBehaviour {
    Damagable dm;
	// Use this for initialization
	void Start () {
        GameObject.Find("WorldManager").GetComponent<WorldManager>().fortress = transform.position;
        dm = GetComponent<Damagable>();
        InvokeRepeating("heal", 15f, 3f);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void heal() {

        if (dm.health < dm.maxHealth) {
            GetComponent<Damagable>().health += 2f;
        }
        if (dm.hb != null)
            dm.hb.rb.setBar(dm.health, dm.maxHealth);
    }
}
