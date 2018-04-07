using UnityEngine;
using UnityEngine.UI;

public class ArmySupportHandler : MonoBehaviour {

	public Color[] colors;
	public Image[] stars;

	void Start () {
		for(int i = 1; i < stars.Length; i++) {
			stars[i].gameObject.SetActive(false);
		}
	}

	private void Reinforce() {
		
	}

	private void HigherLevel() {
		
	}
	
}
