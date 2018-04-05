using System;
using UnityEngine;

/// <summary>
/// UI manager handling all UI scene transitioning.
/// </summary>
public class UIManager : MonoBehaviour {
	public static UIManager ui;

	private UIScene current;
	private UIScene[] scenes;
	private bool locked;

	private void Start() {
		ui = this;
		scenes = new UIScene[transform.childCount];
		for(int c = 0; c < transform.childCount; c++) {
			scenes[c] = transform.GetChild(c).GetComponent<UIScene>();
		}
		current = scenes[0];
	}

	public void Start(UIScene s) {
		current.To(s);
	}

	public void End(UIScene s) {
		s.From(current);
		current = s;
	}

	public void SetLock(bool locked) {
		this.locked = locked;
	}

	public bool Locked() {
		return locked;
	}

	public RectTransform Get(UISceneType t) {
		return scenes[(int) t].Rect();
	}
}