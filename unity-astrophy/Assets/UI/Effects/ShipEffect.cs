using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles the UI Ship wiggle on the main UI.
/// </summary>
public class ShipEffect : MonoBehaviour {
	private const int ShipIndex = 0;
	private const int AstroIndex = 1;
	private const float CentralizeFrames = 30f;

	private bool centered = true;
	private float currRot;
	private float moveOffset;
	private int moveCount;
	
	private int FramesMin = 20;
	private int FramesMax = 90;

	private float MoveMax = 8f;
	private float MoveMin;

	private bool lostControl;
	private float lostMoves;
	private float lostRot;
	private float lostX;
	private float lostY;
	private GameObject fallShip;
	
	private Vector3 initialPosition;
	private Image astroImage;
	private Image shipImage;

	private Vector2 initialSmokePos;
	[SerializeField] private ParticleSystem shipSmoke;
	private Vector2 initialParticlesPos;
	[SerializeField] private ParticleSystem shipParticles;

	private void Start() {
		initialPosition = transform.position;
		initialSmokePos = shipSmoke.transform.position;
		initialParticlesPos = shipParticles.transform.position;
		astroImage = transform.GetChild(AstroIndex).GetComponent<Image>();
		shipImage = transform.GetChild(ShipIndex).GetComponent<Image>();
	}
	
	private void Update() {
		if(lostControl) {
			//ShipEvade();
		}//Turbulence
		else {
			if(moveCount <= 0) {
				if(!centered) {
					moveOffset = -currRot / CentralizeFrames;
					moveCount = (int) CentralizeFrames;
					centered = true;
					return;
				}

				float rot = Random.Range(MoveMin, MoveMax);
				if(Random.Range(1, 3) == 1)
					rot = -rot;
				moveCount = Random.Range(FramesMin, FramesMax);
				moveOffset = rot / moveCount;
				centered = false;
			}
			else {
				moveCount--;
				currRot += moveOffset;
				gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, currRot));
			}
		}
	}

	public void Normalize() {
		currRot = 0;
		FramesMin = 20;
		FramesMax = 90;
		MoveMax = 8f;
		MoveMin = 0f;
		transform.position = initialPosition;
		astroImage.gameObject.SetActive(true);
		shipImage.gameObject.SetActive(true);
		shipSmoke.transform.parent = transform;
		shipSmoke.transform.position = initialSmokePos;
		shipParticles.transform.parent = transform;
		shipParticles.transform.position = initialParticlesPos;
		Destroy(fallShip);
	}

	public void Turbulence(int level) {
		FramesMin = 3;
		FramesMax = 25;
		MoveMax = level * 3.5f;
		MoveMin = level * 2f;
	}

	public Astro SetAstro(AstroGhost ghost) {
		Camera cam = Camera.main;
		Vector3 p = cam.ScreenToWorldPoint(astroImage.transform.position);
		p.z = 0;
		Astro astro = Instantiate(ghost.Astro(), p, astroImage.transform.rotation);
		
		
		fallShip = new GameObject();
		fallShip.name = "Lost Ship";
		ShipFall fall = fallShip.AddComponent<ShipFall>();
		fall.Create(shipImage);

		shipSmoke.transform.parent = fallShip.transform;
		shipSmoke.transform.localPosition = Vector2.zero;
		shipParticles.transform.parent = fallShip.transform;
		shipParticles.transform.localPosition = Vector3.zero;
		
		astroImage.gameObject.SetActive(false);
		shipImage.gameObject.SetActive(false);
		lostControl = true;
		return astro;
	}
	
	public ParticleSystem Smoke() {
		return shipSmoke;
	}
	
	public ParticleSystem Particles() {
		return shipParticles;
	}
}