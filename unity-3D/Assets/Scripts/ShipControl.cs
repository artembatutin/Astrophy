using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControl : MonoBehaviour {
	
	private Vector3 LEFT = new Vector3(2f, 0, 0f);
	private Vector3 RIGHT = new Vector3(-2f, 0, 0f);
	
	private Vector3 UP = new Vector3(0f, 0, -2f);
	private Vector3 DOWN = new Vector3(0f, 0, 2f);

	private const float VEL_CAP = 10f;
	private const int SHOOT_HEIGHT = 18;

	private Camera cam;
	private Vector3 camDamp;
	private Vector3 camOff = new Vector3(10f, 20f, 10f);
	
	private Rigidbody body;
	private LineRenderer lr;
	private Vector3 explosionPlace;
	private int lineHeight;
	private bool shooting;
	
	public Explosion explosion;
	private ParticleSystem particle;
	
	void Start () {
		cam = Camera.main;
		body = gameObject.GetComponent<Rigidbody>();
		lr = gameObject.GetComponentInChildren<LineRenderer>();
		particle = explosion.gameObject.GetComponent<ParticleSystem>();
		lr.enabled = false;
	}
	
	void Update () {
		
		transform.eulerAngles += Vector3.up;
		
		Vector3 vel = body.velocity;
		
		if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
			vel += LEFT;
		} else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
			vel += RIGHT;
		} else {
			vel.x /= 2f;
		}
		
		if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
			vel += UP;
		} else if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
			vel += DOWN;
		} else {
			vel.z /= 2f;
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


		if(Input.GetKeyDown(KeyCode.Space) && !shooting) {
			shooting = true;
			lineHeight = 0;
			lr.enabled = true;
			lr.SetPosition(1, new Vector3(0f, 0f, 0f));
			StartCoroutine(laser());
		}
	}

	private void FixedUpdate() {
		Vector3 camNext = transform.position + camOff;
		cam.transform.position = Vector3.Lerp(cam.transform.position, camNext, 0.05f);
	}

	private void explode() {
		explosionPlace = new Vector3(transform.position.x, 0, transform.position.z);
		particle.gameObject.transform.position = explosionPlace;
		particle.Play();
		explosion.enabled = true;
		shooting = false;
		lr.enabled = false;
	}

	private IEnumerator laser() {
		lineHeight-=2;
		lr.SetPosition(1, new Vector3(0f, lineHeight, 0f));
		yield return new WaitForSeconds(0.02f);
		if(lineHeight > -SHOOT_HEIGHT)
			StartCoroutine(laser());
		else
			explode();
	}
}
