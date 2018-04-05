using UnityEngine;

/// <summary>
/// Represents a UI scene.
/// </summary>
public abstract class UIScene : MonoBehaviour {

	/// <summary>
	/// Rect transform of this scene.
	/// </summary>
	private RectTransform rect;

	/// <summary>
	/// Awaken flag if scene was already used.
	/// </summary>
	private bool awaken;

	public void Awake() {
		if(awaken)
			return;
		rect = GetComponent<RectTransform>();
		Init();
		awaken = true;
	}

	/// <summary>
	/// Initializes the scene, same as Start method.
	/// </summary>
	protected abstract void Init();

	/// <summary>
	/// Trigger when going to another scene.
	/// </summary>
	public abstract void To(UIScene s);

	/// <summary>
	/// Trigger when going from another scene.
	/// </summary>
	public abstract void From(UIScene s);

	/// <summary>
	/// The UI scene type of this scene.
	/// </summary>
	/// <returns>the scene type</returns>
	public abstract UISceneType Type();

	/// <summary>
	/// The rect transform of this scene.
	/// </summary>
	/// <returns>rect transform</returns>
	public RectTransform Rect() {
		return rect;
	}
	
}