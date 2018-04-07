using UnityEngine;

public class Projectile : MonoBehaviour {
	
	private Vector3 aim;
	public int speed;
	public int emission = 10;
	public int damage = 5;
	public int steps = 500;

	private Vector3 changeVec;
	
	void Start () {
		aim = Ship.trans.position;
		transform.LookAt(Ship.trans);
		changeVec = transform.forward * speed;
	}

	private void FixedUpdate() {
		steps--;
		transform.position += changeVec * Time.deltaTime;
		if(steps < 0) {
			explode(transform.position);
		}
	}

	void OnTriggerExit(Collider other) {
		Hit(other);
	}

	void OnTriggerStay(Collider other) {
		Hit(other);
	}

	void OnTriggerEnter(Collider other) {
		Hit(other);
	}

	private void Hit(Collider other) {
		Debug.Log(other.gameObject.name);
		if(other.gameObject.layer == Ship.SHIP_LAYER) {
			other.gameObject.GetComponent<Ship>().Damage(damage);
			explode(other.transform.position);
		}
	}

	private void explode(Vector3 pos) {
		ParticleSystem s = Instantiate(Spawner.instance.explosion, pos, Quaternion.identity);
		s.Play();
		s.gameObject.AddComponent<AutoDestruct>();
		Destroy(gameObject);
	}

}
