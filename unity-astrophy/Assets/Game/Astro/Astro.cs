using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A falling Astro, a type of alien.
/// </summary>
public abstract class Astro : MonoBehaviour {
	
	private const float CameraDelay = 1.5f;
	private const float AngleMiddle = 0.01f;
	private const float FullAngle = 180;

	private static Background scene;
	private static Text meters;

	[SerializeField] private Sprite deadSprite;
	[SerializeField] private float rotativeForce = 10f;
	[SerializeField] private float bounciness = 0.4f;
	[SerializeField] private float friction = 4f;

	private Rigidbody2D body;
	private int camFollow;
	private float camDelay;
	
	private bool dead;
	private int fallCount;

	private void Start() {
		gameObject.layer = Constants.ASTRO_LAYER;
		if (scene == null)
			scene = GameObject.Find("Background").GetComponent<Background>();
		if (meters == null)
			meters = GameObject.Find("Meters").GetComponent<Text>();
		body = gameObject.GetComponent<Rigidbody2D>();
		body.sharedMaterial = new PhysicsMaterial2D {
			bounciness = bounciness,
			friction = friction
		};
		if(scene != null)
			scene.StartGame();
		if(meters != null)
			meters.text = "0";
		Init();
	}

	public void Hit() {
		camFollow = 3;
		gameObject.GetComponent<SpriteRenderer>().sprite = deadSprite;
		gameObject.AddComponent<ThreatEnd>();
	}

	public void Die() {
		dead = true;
	}

	private void Reset() {
		Die();
		if(scene != null)
			scene.StopGame();
		Dispose();
	}
	
	void OnBecameInvisible() {
		Die();
	}

	private void Update() {
		//Following camera.
		if(camFollow == 1) {
			Camera.main.transform.position = new Vector3(0, transform.position.y, -10);
		} else if(camFollow == 0) {
			camDelay += Time.deltaTime;
			if(camDelay > CameraDelay) {
				camFollow = 1;
				camDelay = 0;
			}
		}
		
		int y = (int) (-transform.position.y / 5f);
		if(fallCount != y && y > 0) {
			fallCount = y;
			meters.text = fallCount.ToString();
		}

		Frame();
	}

	private void FixedUpdate() {
		if(body.velocity.y < -10) {
			body.velocity = new Vector2(body.velocity.x, -10);
		}

		//Rotation gap to middle.
		float zRot = transform.eulerAngles.z;
		//Ignoring if angle is in boundary.
		if(!(zRot > AngleMiddle) && !(zRot < -AngleMiddle))
			return;
		//Normalize and force down rotation.
		if(zRot > FullAngle) {
			zRot = -FullAngle + zRot % FullAngle;
		}

		zRot = zRot - zRot / rotativeForce;
		transform.eulerAngles = new Vector3(0, 0, zRot);
		FixedFrame();
	}

	public Background Scene() {
		return scene;
	}

	public bool isDead() {
		return dead;
	}

	protected abstract void FixedFrame();

	protected abstract void Frame();

	protected abstract void Init();

	protected abstract void Dispose();
}