using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// The creation of an <code>Astro</code>, therefore it's ghost.
/// </summary>
public class AstroGhost : MonoBehaviour, IPointerClickHandler {
	private const int ShipIndex = 0;
	private const int AlienIndex = 1;

	[SerializeField] private int index;
	[SerializeField] private Astro astro;
	[SerializeField] private string description;
	[SerializeField] private Color mainColor;
	private new string name;
	private Image background;
	private Image shipImage;
	private Image astroImage;
	private float offset;

	public void Awake() {
		name = gameObject.name;
		background = gameObject.GetComponent<Image>();
		shipImage = transform.GetChild(ShipIndex).GetComponent<Image>();
		astroImage = transform.GetChild(AlienIndex).GetComponent<Image>();
		offset = astroImage.GetComponent<RectTransform>().anchoredPosition.y;
	}

	public void OnPointerClick(PointerEventData eventData) {
		SelectionScene.select.Select(this);
		SelectionScene.select.UpdateDesc(description, mainColor);
		Color c = background.color;
		c.a = 150f / 255f;
		background.color = c;
	}

	public void Deselect() {
		Color c = background.color;
		c.a = 80f / 255f;
		background.color = c;
	}

	public int Index() {
		return index;
	}

	public string Name() {
		return name;
	}

	public Astro Astro() {
		return astro;
	}

	public Color Color() {
		return mainColor;
	}

	public Sprite ShipImage() {
		return shipImage.sprite;
	}

	public Sprite AstroImage() {
		return astroImage.sprite;
	}

	public float Offset() {
		return offset;
	}
}