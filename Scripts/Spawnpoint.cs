using UnityEngine;
using System.Collections;

public class Spawnpoint : MonoBehaviour {
    WorldManager wm;
    public bool isLand;
	// Use this for initialization
	void Start () {
        wm = GameObject.Find("WorldManager").GetComponent<WorldManager>();
        wm.spawnPoints.Add(gameObject);
        GetComponent<SpriteRenderer>().enabled = false;
	}
}
