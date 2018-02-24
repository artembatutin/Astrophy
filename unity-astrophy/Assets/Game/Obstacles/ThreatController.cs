using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreatController : MonoBehaviour {

	public static ThreatController controller;

	public Threat[] threats;
	public Material material;

	private Camera cam;
	private float spawnDelay = 1;
	private float spawnTime;
	private bool active;

	void Start () {
		controller = this;
		cam = Camera.main;
	}
	
	void Update () {
		if(!active)
			return;
		if (spawnTime <= 0) {
			int id = Random.Range(0, threats.Length - 1);
			Threat t = threats[id];
			Threat spawned = Instantiate(t, randomPos(), Quaternion.identity);
			spawned.transform.parent = transform;
			spawned.transform.localPosition = randomPos();
			spawned.Apply(material);
			if(spawnDelay > 0.5f)
				spawnDelay -= 0.05f;
			spawnTime = spawnDelay;
		} else {
			spawnTime -= Time.deltaTime;
		}
	}

	public void Activate() {
		active = true;
		spawnDelay = 1f;
		spawnTime = 0f;
	}

	public void KillAll() {
		active = false;
		foreach (Transform o in transform) {
			o.GetComponent<Threat>().End();
		}
	}

	private Vector2 randomPos() {
		CameraSize s = CameraSize.size;
		float x = Random.RandomRange(-s.Width() / 2f, s.Width() / 2f);
		float y = cam.transform.position.y - s.Height() - Random.RandomRange(-s.Height() / 2f, s.Height() / 2f);
		return new Vector2(x, y);
	}
	
}
