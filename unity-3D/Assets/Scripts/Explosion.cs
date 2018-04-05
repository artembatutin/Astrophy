using UnityEngine;

public class Explosion : MonoBehaviour {

	private const int EXPLOSION_LAYER = 11;
	private const float TIME_MOD = 4f;
	private const float RADIUS_MAX = 2f;
	
	private SphereCollider col;
	private float timer;
	
	public void Start () {
		gameObject.layer = EXPLOSION_LAYER;
		if(col == null)
			col = gameObject.GetComponent<SphereCollider>();
		col.enabled = false;
	}

	private void OnEnable() {
		timer = 0;
		if(col == null)
			col = gameObject.GetComponent<SphereCollider>();
		col.enabled = true;
	}

	private void Update() {
		timer += Time.deltaTime * TIME_MOD;
		col.radius = timer;
		if(timer > RADIUS_MAX) {
			Stop();
		}
	}
	
	private void Stop() {
		col.enabled = false;
		enabled = false;
	}
	
	void OnTriggerExit(Collider other) {
		Kill(other);
	}

	void OnTriggerStay(Collider other) {
		Kill(other);
	}

	void OnTriggerEnter(Collider other) {
		Kill(other);
	}

	private void Kill(Collider other) {
		Debug.Log(other.gameObject.name);
		if(other.gameObject.layer == Person.PERS_LAYER) {
			Person p = other.gameObject.GetComponent<Person>();
			p.Die();
		}
	}
}
