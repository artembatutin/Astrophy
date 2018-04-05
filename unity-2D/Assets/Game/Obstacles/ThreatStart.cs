using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreatStart : MonoBehaviour {

	private Threat t;
	private SpriteRenderer sr;
	private float alpha;
	
	void Start () {
		t = gameObject.GetComponent<Threat>();
		sr = gameObject.GetComponent<SpriteRenderer>();
	}
	
	void Update () {
		alpha += Time.deltaTime;
		if(alpha > 1)
			alpha = 1;
		sr.color = new Color(1f, 1f, 1f, alpha);
		if (alpha == 1f) {
			t.Alive = true;
			Destroy(this);
		}
	}
}
