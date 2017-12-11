using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformHandles : MonoBehaviour {

	public static TransformHandles instance;
	Renderer myRenderer;
	Transform myTransform;
	public static List<Selectable> selected;
	public static Vector3 selectionMeanPos;
	public Transform perspCam;
	public float distanceToScaleRatio = 1f;

	private void Start () {
		instance = this;
		selected = new List<Selectable> ();
		myRenderer = GetComponent<Renderer> ();
		myTransform = transform;
	} 

	private void FixedUpdate () {
		if (selected.Count != 0) {
			myRenderer.enabled = true;
			CalculateSelectionPos ();
			myTransform.position = selectionMeanPos;
			myTransform.localScale = Vector3.one * distanceToScaleRatio * Vector3.Distance (myTransform.position, perspCam.position);
		} else {
			myRenderer.enabled = false;
		}
	}

	private void Update () {
		if ((Input.GetKey (KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKeyDown (KeyCode.D)) {
			DeselectAll ();
		}
	}

	public void DeselectAll () {
		myRenderer.enabled = false;
		foreach (Selectable selectedObj in selected) {
			selectedObj.selected = false;
		}
		selected.Clear ();
	}

	void CalculateSelectionPos () {
		Vector3 pos = Vector3.zero;
		foreach (Selectable selectedObj in selected) {
			pos += selectedObj.trans.position;
		}
		pos /= selected.Count;
		selectionMeanPos = pos;
	}
}
