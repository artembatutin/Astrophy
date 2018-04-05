using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public int spawning = 30;
	public Person[] civilians;
	private Vector3[] spawns;

	public static Spawner instance;
	public ParticleSystem splatter;

	void Start () {
		instance = this;
		spawns = new Vector3[transform.childCount];
		for(int t = 0; t < spawns.Length; t++) {
			spawns[t] = transform.GetChild(t).transform.position;
			Destroy(transform.GetChild(t).gameObject);
		}

		for(int i = 0; i < spawning; i++) {
			Person p = Instantiate(
				civilians[Random.Range(0, civilians.Length)],
				spawns[
					i
					//Random.Range(0, spawns.Length-1)
				],
				Quaternion.identity);
			p.name = "Civilian";
			p.transform.parent = transform;
		}
	}
	
}
