using UnityEngine;

public abstract class Threat : MonoBehaviour {
	
	[SerializeField] private float movement;

	private bool alive;
	private float xMove;
	private float yMove;

	void Start() {
		gameObject.layer = Constants.ENEMY_LAYER;
		xMove = Random.Range(-movement, movement);
		yMove = Random.Range(-movement, movement);
		gameObject.AddComponent<ThreatStart>();
		Init();
	}

	void Update() {
		transform.position += new Vector3(xMove, yMove);
		Frame();
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if (alive && other.gameObject.layer == Constants.ASTRO_LAYER) {
			Astro a = other.gameObject.GetComponent<Astro>();
			a.Hit();
		}
	}

	public void End() {
		gameObject.AddComponent<ThreatEnd>();
	}

	public void Apply(Material mat) {
		gameObject.GetComponent<SpriteRenderer>().material = mat;
	}
	
	void OnBecameInvisible() {
		Destroy(gameObject);
	}

	public bool Alive {
		get { return alive; }
		set { alive = value; }
	}

	protected abstract void Init();

	protected abstract void Frame();

}