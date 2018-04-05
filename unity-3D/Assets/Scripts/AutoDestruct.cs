using UnityEngine;

public class AutoDestruct : MonoBehaviour {
	
	public float destruct = 1f;

	void Update () {
		destruct -= Time.deltaTime;
		if(destruct < 0) {
			Destroy(gameObject);
		}
	}
}
