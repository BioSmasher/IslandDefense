using UnityEngine;
using System.Collections;

public class Alert : MonoBehaviour {
    public GameObject alertPrefab;
    GameObject alert;
    // Use this for initialization
    void Start () {
        if (!GetComponent<SpriteRenderer>().isVisible) {
            OnBecameInvisible();
        }
	}

    void OnDestroy() {
        Destroy(alert);
    }

    void OnBecameVisible() {
        Destroy(alert);
    }

    void OnBecameInvisible() {
        if (alert == null) {
            alert = Instantiate(alertPrefab);
            alert.GetComponent<AlertIcon>().setup(gameObject);
        }
    }
}
