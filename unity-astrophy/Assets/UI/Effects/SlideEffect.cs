using UnityEngine;
using UnityEngine.UI;

public class SlideEffect : MonoBehaviour {
	private const float MovementCap = 1f;
	private const float SmoothnessFactor = 5f;

	public UIScene scene;
	public bool FromRight;
	public bool HideCurrent = true;

	private Button button;
	private RectTransform curr;
	private float movement;

	private void Start() {
		scene.Awake();
		curr = transform.parent.gameObject.GetComponent<RectTransform>();
		button = gameObject.GetComponent<Button>();
		button.onClick.AddListener(Submit);
	}

	private void Update() {
		if(movement > MovementCap) {
			float move = (FromRight ? movement : -movement) / SmoothnessFactor;
			movement -= FromRight ? move : -move;
			Move(scene.Rect(), move);
			if(HideCurrent)
				Move(curr, move);
		} else if(movement != 0) {
			float move = FromRight ? movement : -movement;
			Move(scene.Rect(), move);
			if(HideCurrent)
				Move(curr, move);
			UIManager.ui.SetLock(false);
			UIManager.ui.End(scene);
			if(HideCurrent)
				curr.gameObject.SetActive(false);
			movement = 0;
		}
	}

	private void Submit() {
		if(UIManager.ui.Locked())
			return;
		movement = Screen.width;
		UIManager.ui.Start(scene);
		UIManager.ui.SetLock(true);
		scene.gameObject.SetActive(true);
		if(!FromRight)
			Set(scene.Rect(), Screen.width * 1.5f);
	}

	private static void Move(Transform rec, float horizontalMove) {
		Vector3 pos = rec.position;
		pos.x += horizontalMove;
		rec.position = pos;
	}

	private static void Set(Transform rec, float xPos) {
		Vector3 pos = rec.position;
		pos.x = xPos;
		rec.position = pos;
	}

}