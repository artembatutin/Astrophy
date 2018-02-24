using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A created fall-down ship effect.
/// </summary>
public class ShipFall : MonoBehaviour {
	
	private void Start() {
		name = "Lost Ship";
	}

	public void Create(Image ship) {
		Vector3 p = Camera.main.ScreenToWorldPoint(ship.transform.position);
		p.z = 0;
		transform.position = p;
		SpriteRenderer sr = gameObject.AddComponent<SpriteRenderer>();
		sr.material = ship.material;
		sr.sprite = ship.sprite;
		Rigidbody2D b = gameObject.AddComponent<Rigidbody2D>();
		b.velocity = new Vector2(Random.Range(-3f, 3f), Random.Range(-3f, 0f));
		b.angularVelocity = Random.Range(-30f, 30f);
	}

}