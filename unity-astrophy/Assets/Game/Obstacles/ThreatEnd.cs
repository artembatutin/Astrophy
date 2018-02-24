using UnityEngine;

public class ThreatEnd : MonoBehaviour {

	private SpriteRenderer sr;
	private float alpha;
	
	void Start () {
		sr = gameObject.GetComponent<SpriteRenderer>();
	}
	
	void Update () {
		alpha += Time.deltaTime / 5f;
		if(alpha > 1)
			alpha = 1;
		sr.color = new Color(1f, 1f, 1f, 1f - alpha);
		if (alpha == 1f) {
			Destroy(gameObject);
			Destroy(this);
		}
	}
}
