using UnityEngine;
using UnityEngine.AI;

public class Person : MonoBehaviour {

	public const int PERS_LAYER = 10;
	protected NavMeshAgent nav;
	protected bool dying;
	private float lastHit;

	public int health;
	protected bool civilian = true;
	
	void Start () {
		gameObject.layer = PERS_LAYER;
		nav = gameObject.GetComponent<NavMeshAgent>();
	}

	void FixedUpdate() {
		if(!dying && !nav.hasPath) {
			float randX = Random.Range(-4f, 4f);
			float randZ = Random.Range(-4f, 4f);
			nav.SetDestination(transform.position + new Vector3(randX, 0, randZ));
		}
	}

	public void Die() {
		if(lastHit - Time.time > -0.3)
			return;
		health--;
		if(health < 0 && !dying) {
			nav.Stop();
			ParticleSystem s = Instantiate(part(), transform.position, Quaternion.identity);
			s.Play();
			s.gameObject.AddComponent<AutoDestruct>();
			Destroy(gameObject);
			if(civilian)
				EntityController.instance.CivilianDown();
			else
				EntityController.instance.MilitaryDown();
		}

		lastHit = Time.time;
		if(health < 0)
			dying = true;
	}

	protected virtual ParticleSystem part() {
		return EntityController.instance.splatter;
	}

}
