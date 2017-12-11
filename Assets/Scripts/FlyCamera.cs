using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyCamera : CameraUtil {

	public float mouseSensitivity = 1.4f;
	public float flySpeed = 4f;
	Transform myTransform;
	Vector2 rotation;
	Vector2 cursorLockPos;
	bool isActive = false;

	void Start () {
		myTransform = transform;
		rotation = myTransform.eulerAngles;
	}
	
	void Update () {
		if (Input.GetMouseButtonDown(1) && Input.mousePosition.x < Screen.width / 2 && Input.mousePosition.y > Screen.height / 2) {
			cursorLockPos = UnityCursorToScreenSpace(Input.mousePosition);
			Cursor.visible = false;
			isActive = true;
		}
		if (Input.GetMouseButton(1) && isActive) {
			rotation.x -= mouseSensitivity * Input.GetAxisRaw ("Mouse Y");
			rotation.y += mouseSensitivity * Input.GetAxisRaw ("Mouse X");
			myTransform.rotation = Quaternion.Euler (rotation);
			SetCursorPos ((int)cursorLockPos.x, (int)cursorLockPos.y);
			myTransform.position += myTransform.forward * Input.GetAxis ("Vertical") * flySpeed * Time.deltaTime;
			myTransform.position += myTransform.right * Input.GetAxis ("Horizontal") * flySpeed * Time.deltaTime;
		}
		if (Input.GetMouseButtonUp (1) && isActive) {
			isActive = false;
			Cursor.visible = true;
		}
	}
}
