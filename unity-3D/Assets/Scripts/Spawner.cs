using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour {
	
	public static Spawner instance;

	public int spawning = 30;
	public Person[] civilians;
	public Text civiliansText;

	private int active;
	private int spawned;
	private Vector3[] spawns;
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
			spawned++;
		}

		active = spawned;
		civiliansText.text = active + "/" + spawned;
	}

	public void CountDown() {
		active--;
		civiliansText.text = active + "/" + spawned;
	}
	
}
