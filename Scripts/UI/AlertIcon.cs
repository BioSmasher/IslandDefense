using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AlertIcon : MonoBehaviour {
    public GameObject ship;
    Vector3 dir;
    RectTransform rectTransform;
    public float angle;
    float cameraWidth;
    float cameraHeight;
    float trackWidth;
    float trackHeight;

    public float boundaryAngle1;
    public float boundaryAngle2;

    public float borderRatio;
    float iconRatio;

    float ratio;
    public Vector2 center;

    float screenh;
    float screenw;

    float angleSize1;
    float angleSize2;

    // Use this for initialization
    void Start () {
        screenh = Screen.height;
        screenw = Screen.width;
        boundaryAngle1 = Mathf.Rad2Deg * Mathf.Atan(screenh / screenw);
        boundaryAngle2 = (180f - boundaryAngle1 * 2f) + boundaryAngle1;

        angleSize1 = (boundaryAngle2 - boundaryAngle1) / 2f;
        angleSize2 = (90f - angleSize1);

        iconRatio = borderRatio * 2f;

        rectTransform = GetComponent<RectTransform>();

        transform.SetSiblingIndex(2);
    }
	
	// Update is called once per frame
	void Update () {
        if (ship == null) {
            Destroy(gameObject);
        }

        dir = ship.transform.position - Camera.main.transform.position ;
        dir.Normalize();
        angle = Mathf.Rad2Deg * Mathf.Atan(dir.y / dir.x);
        if (dir.x < 0) {
            if (dir.y < 0) {
                angle = -180f + angle;
            }
            else {
                angle = 180f + angle;
            }
        }

        if (dir.y > 0) {
            if (angle < boundaryAngle2 && angle > boundaryAngle1) {
                //(-((angleSize1 / 2f) - (angle - boundaryAngle1)) / (angleSize1 / 2f))
                center = new Vector2((Screen.width / 2f - Screen.height / borderRatio) * ((angleSize1 - (angle - boundaryAngle1)) / angleSize1), Screen.height / 2f - Screen.height / borderRatio);
            }
            else if (dir.x > 0) {
                center = new Vector2((Screen.width / 2f - Screen.height / borderRatio), (Screen.height / 2f - Screen.height / borderRatio) * (angle / angleSize2));
            }
            else {
                center = new Vector2((-Screen.width / 2f + Screen.height / borderRatio), (Screen.height / 2f - Screen.height / borderRatio) * ((180f - angle) / angleSize2));
            }
        }
        else {
            if (angle < -boundaryAngle1 && angle > -boundaryAngle2) {
                //(((angleSize1 / 2f) + (angle - boundaryAngle1)) / (angleSize1 / 2f))
                center = new Vector2((Screen.width / 2f - Screen.height / borderRatio) * ((angleSize1 + (angle + boundaryAngle1)) / angleSize1), -Screen.height / 2f + Screen.height / borderRatio);
            }
            else if (dir.x > 0) {
                center = new Vector2((Screen.width / 2f - Screen.height / borderRatio), (Screen.height / 2f - Screen.height / borderRatio) * (angle / angleSize2));
            }
            else {
                center = new Vector2((-Screen.width / 2f + Screen.height / borderRatio), (Screen.height / 2f - Screen.height / borderRatio) * ((-180f - angle) / angleSize2));
            }
        }
        rectTransform.offsetMax = new Vector2(center.x + Screen.height / iconRatio, center.y + Screen.height / iconRatio);
        rectTransform.offsetMin = new Vector2(center.x - Screen.height / iconRatio, center.y - Screen.height / iconRatio);
    }

    public void setup(GameObject obj) {
        ship = obj;
        transform.SetParent(GameObject.Find("Canvas").transform);
    }
}
