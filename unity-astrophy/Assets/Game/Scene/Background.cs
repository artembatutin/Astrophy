using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A scene script which handles the camera background and the sprites material
/// emission coloring in an ambient smooth way by using the <code>ColorShifter</code> utility.
/// </summary>
public class Background : MonoBehaviour {
	
	[SerializeField] private Material spritesMat;
	[SerializeField] private Image logoImage;

	private new Camera camera;
	private ColorShifter color;
	private ParticleSystem emitter;

	private void Start() {
		color = new ColorShifter();
		camera = Camera.main;
		color.Start(RandomColor(), RandomColor());
		Apply(color.Color());
		emitter = GetComponentInChildren<ParticleSystem>();
		emitter.Stop();
	}

	public void StartGame() {
		emitter.Play();
		ThreatController.controller.Activate();
	}

	public void StopGame() {
		emitter.Stop();
		ThreatController.controller.KillAll();
	}

	private void Update() {
		Vector3 cam = camera.transform.position;
		transform.position = new Vector3(cam.x, cam.y);
		Shift();
	}

	private void Shift() {
		if(color.Shifting()) {
			Apply(color.Shift());
		} else {
			color.Push(RandomColor());
			Apply(color.Color());
		}
	}

	private void Apply(Color c) {
		camera.backgroundColor = c;
		logoImage.color = c;
		float h;
		float s;
		float v;
		Color.RGBToHSV(c, out h, out s, out v);
		spritesMat.SetColor("_EmisColor", Color.HSVToRGB(h, s / 1.5f, 1f));
	}

	private static Color RandomColor() {
		float h = Random.Range(0f, 1f);
		float s = Random.Range(0.3333f, 0.47f);
		float v = Random.Range(0.3333f, 0.47f);
		return Color.HSVToRGB(h, s, v);
	}
}