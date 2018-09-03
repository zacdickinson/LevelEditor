using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class CameraUtil : MonoBehaviour {

	[DllImport ("user32.dll")]
	public static extern bool SetCursorPos (int X, int Y);
	[DllImport ("user32.dll")]
	[return: MarshalAs (UnmanagedType.Bool)]
	public static extern bool GetCursorPos (out Point pos);
	public bool wireframe = false;
	protected Camera myCam;
	protected Transform myTransform;

	protected virtual void Start () {
		myTransform = transform;
		myCam = GetComponent<Camera> ();
	}

	protected Vector2 UnityCursorToScreenSpace (Vector2 position) {

		Vector2 inputCursor = Input.mousePosition;
		inputCursor.y = Screen.height - 1 - inputCursor.y;
		Point p;
		GetCursorPos (out p);
		var renderRegionOffset = p - inputCursor;

		var newXPos = (int)(position.x + renderRegionOffset.x);
		var newYPos = (int)(Screen.height - (position.y) + renderRegionOffset.y);

		return new Vector2 (newXPos, newYPos);
	}

	void OnPreRender () {
		if (wireframe) {
			//GL.ClearWithSkybox (true, myCam);
			GL.wireframe = true;
		} else {
			GL.wireframe = false;
		}
	}

	private void OnPostRender () {
		GL.wireframe = false;
	}
}

[StructLayout (LayoutKind.Sequential)]
public struct Point {
	public int X;
	public int Y;
	public static implicit operator Vector2 (Point p) {
		return new Vector2 (p.X, p.Y);
	}
}