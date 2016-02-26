using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    public GameObject shipPrefab;

    public GameObject bandit;
    public GameObject brute;
    public GameObject bomber;
    public GameObject enemy4;
    public GameObject enemy5;

    WorldManager wm;
    public short spawnPointAmount;
    public short spawnCount;

    public int wave;


    // Use this for initialization
    void Start () {
        wm = GameObject.Find("WorldManager").GetComponent<WorldManager>();
        spawnPointAmount = (short) wm.spawnPoints.Count;
        spawnCount = 0;
        wave = 0;
	}

    //spawns the provided object at the next spawnPoint, if ship, then spawns on next water spawnpoint
    public GameObject spawn(GameObject obj) {
        spawnCount++;
        GameObject unit = spawn(obj, spawnCount);

        
        if (spawnCount >= spawnPointAmount) {
            spawnCount = 0;
        }

        return unit;
    }

    //spawns from specified spawnPoint
    public GameObject spawn(GameObject obj, int point) {
        if (obj == shipPrefab) {
            while (((GameObject)wm.spawnPoints[point - 1]).GetComponent<Spawnpoint>().isLand) {
                if (point >= spawnPointAmount) {
                    point = 0;
                }
                point++;
                
            }
        }
        GameObject unit = (GameObject)Instantiate(obj, ((GameObject)wm.spawnPoints[point - 1]).transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)), Quaternion.identity);
        return unit;
    }

    public void spawn(int w) {
        switch (w) {
            case 1: wave1(); break;
            case 2: wave2(); break;
            case 3: wave3(); break;
            case 4: wave4(); break;
            case 5: wave5(); break;
            default: wavex((short)w); break;
        }
		wave++;
    }

    public void wave1() {
        
        spawn(shipPrefab).GetComponent<Ship>().setup(bandit, 5);
		spawn(shipPrefab).GetComponent<Ship>().setup(brute, 1, bandit, 3);
		spawn(shipPrefab).GetComponent<Ship>().setup(bandit, 5);
    }

    public void wave2() {
		spawn(shipPrefab).GetComponent<Ship>().setup(bandit, 8);
		spawn(shipPrefab).GetComponent<Ship>().setup(brute, 1, bandit, 4);
		spawn(shipPrefab).GetComponent<Ship>().setup(bandit, 8);
    }

    public void wave3() {
		spawn(shipPrefab).GetComponent<Ship>().setup(brute, 1, bandit, 8);
		spawn(shipPrefab).GetComponent<Ship>().setup(brute, 2, bandit, 4);
		spawn(shipPrefab).GetComponent<Ship>().setup(brute, 1, bandit, 8);
        spawn(shipPrefab).GetComponent<Ship>().setup(brute, 1, bandit, 8);
    }

    public void wave4() {
        spawn(shipPrefab).GetComponent<Ship>().setup(brute, 2, bandit, 8);
        spawn(shipPrefab).GetComponent<Ship>().setup(brute, 2, bandit, 8);
        spawn(shipPrefab).GetComponent<Ship>().setup(brute, 2, bandit, 8);
        spawn(shipPrefab).GetComponent<Ship>().setup(brute, 2, bandit, 8);
    }

    public void wave5() {
        spawn(shipPrefab).GetComponent<Ship>().setup(brute, 3, bandit, 6);
        spawn(shipPrefab).GetComponent<Ship>().setup(brute, 3, bandit, 6);
        spawn(shipPrefab).GetComponent<Ship>().setup(brute, 3, bandit, 6);
        spawn(shipPrefab).GetComponent<Ship>().setup(brute, 2, bandit, 8);
    }

    public void wavex(short w) {
        spawn(shipPrefab).GetComponent<Ship>().setup(brute, w, bandit, 6);
        spawn(shipPrefab).GetComponent<Ship>().setup(brute, w, bandit, 6);
        spawn(shipPrefab).GetComponent<Ship>().setup(brute, w, bandit, 6);
        spawn(shipPrefab).GetComponent<Ship>().setup(brute, w, bandit, 8);
    }
}
