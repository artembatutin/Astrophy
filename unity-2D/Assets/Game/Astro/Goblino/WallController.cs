using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour {
	private const int WallCount = 10;

	[SerializeField] private Material wallMaterial;
	[SerializeField] private Wall wallObject;

	private new Camera camera;
	private Wall[] walls;

	private void Start() {
		camera = Camera.main;
		walls = new Wall[WallCount];
	}

	private void Update() {
		foreach(Touch t in Input.touches) {
			int i = t.fingerId;
			if(i < WallCount) {
				Vector2 pos = camera.ScreenToWorldPoint(t.position);
				if(t.phase == TouchPhase.Began) {
					walls[i] = Instantiate(wallObject, pos, Quaternion.identity);
					walls[i].transform.parent = transform;
					walls[i].transform.position = new Vector2(0, 0);
					walls[i].name = "Wall_Finger_" + i;
					walls[i].Initialize(pos);
				}
				else if(t.phase == TouchPhase.Moved) {
					walls[i].AddPoint(pos);
				}
				else if(t.phase == TouchPhase.Ended) {
					walls[i].AddPoint(pos);
					walls[i].Terminate();
				}
				else if(t.phase == TouchPhase.Canceled) {
					walls[i].Destroy();
				}
			}
		}
	}

	public Material Mat() {
		return wallMaterial;
	}
}