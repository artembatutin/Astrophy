using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The selection UI handler which handles <code>AstroGhost</code> selecting.
/// </summary>
public class SelectionScene : UIScene {
	private const int SelectedIndex = 0;
	private const int DescriptionIndex = 1;
	private const int ScrollContent = 2;

	public static SelectionScene select;

	private bool selectionConfirmed;
	private AstroGhost selected;

	private float descWidth;
	private Text descText;
	private RectTransform descRect;

	private float selectWidth;
	private float selectMove;
	private bool selectable;
	private Text selectText;
	private Button selectButton;
	private RectTransform selectRect;

	private Button backButton;

	protected override void Init() {
		select = this;
		backButton = transform.GetChild(transform.childCount - 1).GetComponent<Button>();
		descRect = transform.GetChild(DescriptionIndex).GetComponent<RectTransform>();
		descText = transform.GetChild(DescriptionIndex).transform.GetChild(0).GetComponent<Text>();
		selectButton = transform.GetChild(SelectedIndex).GetComponent<Button>();
		selectText = transform.GetChild(SelectedIndex).transform.GetChild(0).GetComponent<Text>();
		selectRect = selectButton.GetComponent<RectTransform>();
		selectWidth = selectRect.sizeDelta.x;
		descWidth = descRect.sizeDelta.x + selectWidth;
		selectButton.onClick.AddListener(Apply);
		Close();
	}

	public override void To(UIScene s) {
		Close();
		if(!selectionConfirmed || !s.Type().Equals(UISceneType.MENU))
			return;
		MenuScene menu = s.GetComponent<MenuScene>();
		selectionConfirmed = false;
		menu.Ghost = selected;
		menu.UpdateAstro();
	}

	public override void From(UIScene s) {
		if(!s.Type().Equals(UISceneType.MENU))
			return;
		MenuScene menu = s.GetComponent<MenuScene>();
		selected = menu.Ghost;
		menu.UpdateAstro();
	}

	public override UISceneType Type() {
		return UISceneType.SELECTION;
	}

	private void Update() {
		if(!(selectMove > 1) && !(selectMove < -1))
			return; //Select button movement no triggered.
		//Moving the select button and the description panel.
		float movement = selectMove / 10f;
		Vector2 size = selectRect.sizeDelta;
		size.x += movement;
		selectRect.sizeDelta = size;
		size = descRect.sizeDelta;
		size.x -= movement;
		descRect.sizeDelta = size;
		selectMove -= movement;
	}

	public void Select(AstroGhost ghost) {
		selected.Deselect();
		if(ghost.Index() == selected.Index()) {
			if(selectable)
				selectMove = -selectWidth;
			selectable = false;
			selectText.enabled = false;
		} else {
			if(!selectable)
				selectMove = selectWidth;
			selectable = true;
			selectText.enabled = true;
		}
		selected = ghost;
	}

	private void Apply() {
		selectionConfirmed = true;
		backButton.onClick.Invoke();
	}

	private void Close() {
		float height = selectRect.sizeDelta.y;
		selectRect.sizeDelta = new Vector2(0, height);
		Vector2 size = descRect.sizeDelta;
		size.x = descWidth;
		descRect.sizeDelta = size;
		selectText.enabled = false;
		selectable = false;
		selectMove = 0;
	}
	
	public void UpdateDesc(string desc, Color color) {
		descText.text = desc;
		descText.color = color;
	}

	public AstroGhost Astro(int index) {
		Transform t = transform.GetChild(ScrollContent).GetChild(0).GetChild(0);
		if(index < 0 || index >= t.childCount)
			return null;
		return t.GetChild(index).GetComponent<AstroGhost>();
	}
}