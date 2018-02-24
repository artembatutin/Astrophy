using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSize : MonoBehaviour {

	public static CameraSize size;
	
	private Camera cam;
	private float width;
	private float height;
	
	void Start () {
		size = this;
		cam = gameObject.GetComponent<Camera>();
		height = 2f * cam.orthographicSize;
		width = height * cam.aspect;
	}

	public float Width() {
		return width;
	}

	public float Height() {
		return height;
	}
}
