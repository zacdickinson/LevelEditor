using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTrack : MonoBehaviour {

	public float snapInterval = 1f;
	public Transform trackObject;
	public Vector3 offset;
	Transform myTransform;
	
	void Start () {
		myTransform = transform;
	}
	
	void Update () {
		Vector3 newPos = trackObject.position;
		newPos.x -= newPos.x % snapInterval;
		newPos.y -= newPos.y % snapInterval;
		newPos.z -= newPos.z % snapInterval;
		newPos += offset;
		myTransform.position = newPos;
	}
}
