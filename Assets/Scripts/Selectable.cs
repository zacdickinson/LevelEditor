using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour {

	public bool selected = false;
	public Transform trans;
	Renderer myRenderer;
	Material outlineMat;

	private void Start () {
		myRenderer = GetComponent<Renderer> ();
		outlineMat = myRenderer.materials[1];
		myRenderer.materials[1] = null;
		trans = transform;
	}

	private void OnMouseDown () {
		if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift)) {
			TransformHandles.instance.DeselectAll ();
		}
		selected = true;
	}

	private void FixedUpdate () {
		if (selected) {
			if (!TransformHandles.selected.Contains(this))
				TransformHandles.selected.Add (this);
			myRenderer.materials = new Material[] { myRenderer.material, outlineMat };
		} else {
			if (TransformHandles.selected.Contains (this))
				TransformHandles.selected.Remove (this);
			myRenderer.materials = new Material[] { myRenderer.material };
		}

		if (Input.GetKey(KeyCode.Delete) && selected) {
			if (TransformHandles.selected.Contains (this))
				TransformHandles.selected.Remove (this);
			Destroy (gameObject);
		}
	}
}
