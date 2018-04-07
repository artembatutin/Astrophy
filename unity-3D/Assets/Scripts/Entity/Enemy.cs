using UnityEngine;
using UnityEngine.AI;

public class Enemy : Person {

	public int attackTimer = 10;
	public bool rotatingAim;
	public Projectile projectile;
	

	private float walkTime;
	private float attackTime;
	private Transform aim;
	private Transform weapon;
	
	void Start () {
		civilian = false;
		gameObject.layer = PERS_LAYER;
		nav = gameObject.GetComponent<NavMeshAgent>();
		weapon = transform.GetChild(0);
		aim = weapon.GetChild(0).GetChild(0);
	}
	
	void FixedUpdate() {
		if(!dying) {
			walkTime -= Time.deltaTime;
			attackTime -= Time.deltaTime;
			if(inCombat() && attackTime < 0) {
				if(rotatingAim)
					weapon.rotation = Quaternion.LookRotation(Ship.trans.position);
				attack();
				attackTime = attackTimer;
			} else if(!nav.hasPath || walkTime < 0) {
				float randX = Random.Range(-6f, 6f);
				float randZ = Random.Range(-6f, 6f);
				nav.SetDestination(new Vector3(Ship.trans.position.x + randX, 0, Ship.trans.position.z + randZ));
				walkTime = 3;
			}
		}
	}

	private void attack() {
		Instantiate(projectile, aim.position, Quaternion.identity);
	}

	protected override ParticleSystem part() {
		return EntityController.instance.explosion;
	}

	private bool inCombat() {
		float x = Ship.trans.position.x - transform.position.x;
		float z = Ship.trans.position.z - transform.position.z;

		return x > -6f && x < 6f && z > -6f && z < 6f;
	}

}
