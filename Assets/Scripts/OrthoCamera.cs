using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrthoCamera : CameraUtil {

	public Vector2 minBounds;
	public Vector2 maxBounds;
	public float panSpeed = 4f;
	Transform myTransform;
	Vector2 cursorLockPos;
	bool isActive = false;
	Camera myCam;

	void Start () {
		myTransform = transform;
		myCam = GetComponent<Camera> ();
	}

	void Update () {
		if (Input.GetMouseButtonDown (1) &&
			Input.mousePosition.x / Screen.width > minBounds.x && Input.mousePosition.x / Screen.width < maxBounds.x &&
			Input.mousePosition.y / Screen.height > minBounds.y && Input.mousePosition.y / Screen.height < maxBounds.y) {
			cursorLockPos = UnityCursorToScreenSpace (Input.mousePosition);
			Cursor.visible = false;
			isActive = true;
		}
		if (Input.GetMouseButton (1) && isActive) {
			SetCursorPos ((int)cursorLockPos.x, (int)cursorLockPos.y);
			myTransform.position -= myTransform.up * Input.GetAxisRaw ("Mouse Y") * panSpeed * myCam.orthographicSize;
			myTransform.position -= myTransform.right * Input.GetAxisRaw ("Mouse X") * panSpeed * myCam.orthographicSize;
		}
		if (Input.GetMouseButtonUp (1) && isActive) {
			isActive = false;
			Cursor.visible = true;
		}
	}
}
