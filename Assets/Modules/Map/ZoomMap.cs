using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomMap : MonoBehaviour {
    Vector3 touchStart;
    public float zoomOutMin = 1;
    public float zoomOutMax = 8;

    void Start() {
        Screen.orientation = ScreenOrientation.Portrait;
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.touchCount == 2) {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            Zoom(difference * 1f);
        }
        else if (Input.GetMouseButton(0)) {
            Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.transform.position += direction;
        }
        Zoom(Input.GetAxis("Mouse ScrollWheel"));
    }

    void Zoom(float increment) {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax);

        Vector2 cameraPos = Camera.main.transform.position;

        float minX = -850f, maxX = 850f;
        float minY = -600f, maxY = 600f;

        cameraPos.x = Mathf.Clamp(cameraPos.x, minX, maxX);
        cameraPos.y = Mathf.Clamp(cameraPos.y, minY, maxY);

        Camera.main.transform.position = cameraPos;
    }
}