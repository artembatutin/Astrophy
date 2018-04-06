using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControl : MonoBehaviour {
	
	private Vector3 LEFT = new Vector3(1f, 0, 0f);
	private Vector3 RIGHT = new Vector3(-1f, 0, 0f);
	
	private Vector3 UP = new Vector3(0f, 0, -1f);
	private Vector3 DOWN = new Vector3(0f, 0, 1f);

	private const float RANDOM_Z = 0.30f;
	private const float VEL_CAP = 20f;
	private const int SHOOT_HEIGHT = 18;
	private const float ROT_MIN = 5f;

	public AudioClip[] splashes;
	private AudioSource source;

	private Camera cam;
	private Vector3 camDamp;
	private Vector3 camOff = new Vector3(10f, 20f, 10f);
	
	private Rigidbody body;
	private LineRenderer lr;
	private Vector3 explosionPlace;
	private bool shooting;
	private byte shootingStage;
	private float lineHeight;
	private float shootTimer;

	private float randomZ;
	private int randomZSteps;
	
	public Explosion explosion;
	private ParticleSystem particle;
	
	void Start () {
		cam = Camera.main;
		body = gameObject.GetComponent<Rigidbody>();
		lr = gameObject.GetComponentInChildren<LineRenderer>();
		source = gameObject.GetComponent<AudioSource>();
		particle = explosion.gameObject.GetComponent<ParticleSystem>();
		lr.enabled = false;
	}
	
	void Update () {
		
		Vector3 vel = body.velocity;
		
		if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
			vel += LEFT;
		} else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
			vel += RIGHT;
		} else {
			vel.x /= 1.7f;
		}
		
		if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
			vel += UP;
		} else if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
			vel += DOWN;
		} else {
			vel.z /= 1.7f;
		}

		if(vel.x > VEL_CAP)
			vel.x = VEL_CAP;
		else if(vel.x < -VEL_CAP)
			vel.x = -VEL_CAP;
		if(vel.z > VEL_CAP)
			vel.z = VEL_CAP;
		else if(vel.z < -VEL_CAP)
			vel.z = -VEL_CAP;
		
		body.velocity = vel;


		float rot = Mathf.Abs(body.velocity.x);
		if (body.velocity.z < -rot || body.velocity.z > rot)
			rot = Mathf.Abs(body.velocity.z);
		if (rot < ROT_MIN)
			rot = ROT_MIN;
		transform.eulerAngles += Vector3.up * rot / 5f;
		
		if (shooting) {
			shootTimer += Time.deltaTime;
			if (shootingStage == 1) {
				lineHeight = shootTimer * 100;
				lr.SetPosition(1, new Vector3(0f, -lineHeight, 0f));
				if (lineHeight > SHOOT_HEIGHT) {
					shootingStage = 2;
					shootTimer = 0f;
					Explode();
				}
			} else if (shootingStage == 2) {
				if (shootTimer > 0.2f) {
					shooting = false;
					lr.enabled = false;
					shootTimer = 0;
					shootingStage = 0;
				}
			}
		} else if(Input.GetKeyDown(KeyCode.Space)) {
			shooting = true;
			lineHeight = 0;
			lr.enabled = true;
			lr.SetPosition(1, new Vector3(0f, 0f, 0f));
			shootingStage = 1;
		}

		if (randomZSteps < 0) {
			randomZ = Random.Range(9 - RANDOM_Z, 9 + RANDOM_Z);
			randomZSteps = Random.Range(15, 40);
			randomZ = (randomZ - transform.position.y) / randomZSteps;
		} else {
			transform.position += Vector3.up * randomZ;
			randomZSteps--;
		}
	} 

	private void FixedUpdate() {
		Vector3 camNext = transform.position + camOff;
		cam.transform.position = Vector3.Lerp(cam.transform.position, camNext, 0.05f);
	}

	private void Explode() {
		PlayRandom(splashes, source);
		explosionPlace = new Vector3(transform.position.x, 0, transform.position.z);
		particle.gameObject.transform.position = explosionPlace;
		particle.Play();
		explosion.enabled = true;
	}

	public static void PlayRandom(AudioClip[] clip, AudioSource source) {
		source.PlayOneShot(clip[Random.Range(0, clip.Length - 1)]);
	}

}
