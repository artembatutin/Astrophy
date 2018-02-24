using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The gameplay UI scene.
/// </summary>
public class GameScene : UIScene {
	private const float SmokeDelay = 0.4f;
	
	private MenuScene menu;

	private bool gameStart;
	private int smokesCount = 1;
	private float smokeTime;
	private bool menuReturn;
	private float rectHide;

	private Astro astro;

	protected override void Init() {
		//Reseting on the right of the screen to slide to left.
		Vector3 p = Rect().position;
		p.x += Screen.width;
		Rect().position = p;
	}

	private void Update() {
		if(astro != null && astro.isDead() && !menu.HidingMenu()) {
			EndGame();
		}
		if(menuReturn)
			MenuReturn();
		else if(gameStart)
			GameStart();
	}

	public override void To(UIScene s) {
		//Never used.
	}

	public override void From(UIScene s) {
		gameStart = true;
		if(menu != null)
			return;
		if(!s.Type().Equals(UISceneType.MENU))
			return;
		menu = s.GetComponent<MenuScene>();
	}

	public override UISceneType Type() {
		return UISceneType.GAME;
	}

	private void EndGame() {
		Camera cam = Camera.main;
		cam.transform.position = new Vector3(0, 0, -10);
		menuReturn = true;
		menu.DisplayMenu();
		rectHide = Screen.width;
		menu.Ship().Normalize();
		UIManager.ui.SetLock(true);
	}

	private void MenuReturn() {
		float movement = rectHide / 10f;
		if(rectHide < 1) {
			movement = rectHide;
			menuReturn = false;
			UIManager.ui.SetLock(false);
		}
		Vector3 p = Rect().position;
		p.x += movement;
		Rect().position = p;
		rectHide -= movement;
	}

	private void GameStart() {
		smokeTime += Time.deltaTime;
		if(smokeTime > SmokeDelay) {
			smokeTime -= SmokeDelay;
			smokesCount++;
			Smokes(smokesCount);
			menu.Ship().Turbulence(smokesCount);
			if(smokesCount == 7) {
				gameStart = false;
				smokesCount = 1;
				astro = menu.Ship().SetAstro(menu.Ghost);
			}
		}
	}

	private void Smokes(int amount) {
		ParticleSystem.EmissionModule emission = menu.Ship().Smoke().emission;
		emission.rateOverTime = new ParticleSystem.MinMaxCurve(amount);
	}

}