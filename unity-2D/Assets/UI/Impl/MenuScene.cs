using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The main menu UI scene.
/// </summary>
public class MenuScene : UIScene {
	private int initialAstro = 0;
	private const int LogoIndex = 0;
	private const int AstroTextIndex = 1;
	
	private const int AstroIndex = 2;
	private const int AstroShipIndex = 0;
	private const int AstroImageIndex = 1;
	
	private const int PlayButtonIndex = 3;
	private const int SelectionButtonIndex = 4;
	private const int MarketButtonIndex = 5;
	private const int SettingButtonIndex = 6;

	private Image logo;

	public AstroGhost Ghost { get; set; }
	private ShipEffect ship;
	private Text astroText;
	private Image astroShip;
	private Image astroImage;

	private Button playButton;
	private Button SelectionButton;
	private Button MarketButton;
	private Button SettingButton;

	private bool inGame;
	private float menuHidout;
	private readonly bool[] menuOut = new bool[6];

	protected override void Init() {
		Application.targetFrameRate = 60;
		logo = transform.GetChild(LogoIndex).GetComponent<Image>();
		astroText = transform.GetChild(AstroTextIndex).GetComponent<Text>();
		ship = transform.GetChild(AstroIndex).GetComponent<ShipEffect>();
		astroShip = transform.GetChild(AstroIndex).GetChild(AstroShipIndex).GetComponent<Image>();
		astroImage = transform.GetChild(AstroIndex).GetChild(AstroImageIndex).GetComponent<Image>();
		playButton = transform.GetChild(PlayButtonIndex).GetComponent<Button>();
		SelectionButton = transform.GetChild(SelectionButtonIndex).GetComponent<Button>();
		MarketButton = transform.GetChild(MarketButtonIndex).GetComponent<Button>();
		SettingButton = transform.GetChild(SettingButtonIndex).GetComponent<Button>();
		playButton.onClick.AddListener(Play);
		//Setting inital astro.
		SelectionScene ss = transform.parent.GetChild(1).GetComponent<SelectionScene>();
		Ghost = ss.Astro(initialAstro);
		Ghost.Awake();//Force awake to avoid nullpointers.
		UpdateAstro();
	}
	
	private void Update() {
		if(HidingMenu()) {
			ProcessMenu();
		}
	}

	public override void To(UIScene s) {
		if(!s.Type().Equals(UISceneType.GAME)) {
			ship.Particles().Stop();
			ship.Smoke().Stop();
		}
	}

	public override void From(UIScene s) {
		ship.Particles().Play();
		ship.Smoke().Play();
	}

	public override UISceneType Type() {
		return UISceneType.MENU;
	}

	public void UpdateAstro() {
		astroText.text = Ghost.Name();
		astroText.color = Ghost.Color();
		astroShip.sprite = Ghost.ShipImage();
		astroShip.SetNativeSize();
		astroImage.sprite = Ghost.AstroImage();
		astroImage.SetNativeSize();
		Vector3 p = astroImage.rectTransform.anchoredPosition;
		p.y = Ghost.Offset();
		astroImage.rectTransform.anchoredPosition = p;
		ParticleSystem.MainModule main = ship.Particles().main;
		main.startColor = new ParticleSystem.MinMaxGradient(Ghost.Color());
	}

	private void Play() {
		if(UIManager.ui.Locked())
			return;
		inGame = true;
		//Menus slide-off effect.
		menuHidout = Screen.width;
		for(int i = 0; i < menuOut.Length; i++) {
			menuOut[i] = Random.Range(1, 3) == 1;
		}
	}

	public void DisplayMenu() {
		if(!inGame)
			return;
		inGame = false;
		menuHidout = Screen.width;
		UIManager.ui.SetLock(true);
		for(int i = 0; i < menuOut.Length; i++) {
			menuOut[i] = !menuOut[i];
		}
	}

	private void ProcessMenu() {
		float movement = menuHidout / 10f;
		if(menuHidout < 1) {
			movement = menuHidout;
			UIManager.ui.SetLock(false);
		}

		Vector3 p = logo.transform.position;
		p.x += menuOut[0] ? -movement : movement;
		logo.transform.position = p;
			
		p = playButton.transform.position;
		p.x += menuOut[1] ? -movement : movement;
		playButton.transform.position = p;
			
		p = astroText.transform.position;
		p.x += menuOut[2] ? -movement : movement;
		astroText.transform.position = p;
			
		p = SelectionButton.transform.position;
		p.x += menuOut[3] ? -movement : movement;
		SelectionButton.transform.position = p;
			
		p = MarketButton.transform.position;
		p.x += menuOut[4] ? -movement : movement;
		MarketButton.transform.position = p;
			
		p = SettingButton.transform.position;
		p.x += menuOut[5] ? -movement : movement;
		SettingButton.transform.position = p;
			
		menuHidout -= movement;
	}

	public bool HidingMenu() {
		return menuHidout > 0;
	}

	public ShipEffect Ship() {
		return ship;
	}

}