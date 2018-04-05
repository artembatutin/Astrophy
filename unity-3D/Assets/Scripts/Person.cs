using UnityEngine;
using UnityEngine.AI;

public class Person : MonoBehaviour {

	public const int PERS_LAYER = 10;
	private NavMeshAgent nav;
	private bool dying;
	
	void Start () {
		gameObject.layer = PERS_LAYER;
		nav = gameObject.GetComponent<NavMeshAgent>();
	}

	private void FixedUpdate() {
		if(!dying && !nav.hasPath) {
			float randX = Random.Range(-4f, 4f);
			float randZ = Random.Range(-4f, 4f);
			nav.SetDestination(transform.position + new Vector3(randX, 0, randZ));
		}
	}

	public void Die() {
		if(!dying) {
			nav.Stop();
			for(int i = 0; i < gameObject.transform.childCount; i++) {
				gameObject.transform.GetChild(i).GetComponent<MeshRenderer>().enabled = false;
			}
			ParticleSystem s = Instantiate(Spawner.instance.splatter, transform);
			s.Play();
			gameObject.AddComponent<AutoDestruct>();
		}

		dying = true;
	}

}
