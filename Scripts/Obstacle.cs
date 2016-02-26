using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {
    public float scale;
    // Use this for initialization
    void Start() {

    }

    public void setup(float sc) {
        scale = sc;
        transform.localScale = new Vector3(scale, scale, scale);
    }
}
