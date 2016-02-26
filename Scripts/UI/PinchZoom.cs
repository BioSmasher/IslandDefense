using UnityEngine;
using System.Collections;

public class PinchZoom : MonoBehaviour {

    public float zoomSpeed;
    public WorldManager wm;

    void Start() {
        wm = GetComponent<WorldManager>();
    }

	void Update () {
        if (Input.touchCount == 2) {
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            if (touchZero.position.x > wm.width && touchOne.position.x > wm.width) {

                // Find the position in the previous frame of each touch.
                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                // Find the magnitude of the vector (the distance) between the touches in each frame.
                float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                // Find the difference in the distances between each frame.
                float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

                // ... change the orthographic size based on the change in distance between the touches.
                Camera.main.orthographicSize += deltaMagnitudeDiff * zoomSpeed * Camera.main.orthographicSize / 10f;

                if (Camera.main.orthographicSize < 2f) {
                    Camera.main.orthographicSize = 2f;
                }

                else if (Camera.main.orthographicSize > 15f) {
                    Camera.main.orthographicSize = 15f;
                }

                wm.panning = false;

            }
        }
	}
}
