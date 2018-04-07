using UnityEngine;
using UnityEngine.UI;

public class EntityController : MonoBehaviour {
	
	public static EntityController instance;

	public int spawning = 30;
	public Person[] civilians;
	public Text civiliansText;
	
	public Enemy[] army;
	public Text militaryText;
	private int activeSoldiers;

	private int activCivilians;
	private int spawned;
	private Vector3[] spawns;
	public ParticleSystem splatter;
	public ParticleSystem explosion;

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

		activCivilians = spawned;
		civiliansText.text = activCivilians + "/" + spawned;
		militaryText.text = activeSoldiers.ToString();
	}

	public void CivilianDown() {
		activCivilians--;
		civiliansText.text = activCivilians + "/" + spawned;
	}
	
	public void MilitaryDown() {
		activeSoldiers--;
		militaryText.text = activeSoldiers.ToString();
	}
	
}
