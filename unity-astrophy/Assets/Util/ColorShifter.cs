using UnityEngine;

public class ColorShifter {
	private const int Scale = 200;

	private int shifted;
	private float currentR;
	private float currentG;
	private float currentB;
	private float moverR;
	private float moverG;
	private float moverB;

	public void Start(Color c, Color f) {
		currentR = c.r;
		currentG = c.g;
		currentB = c.b;
		moverR = (f.r - c.r) / Scale;
		moverG = (f.g - c.g) / Scale;
		moverB = (f.b - c.b) / Scale;
		shifted = Scale;
	}

	public void Push(Color f) {
		moverR = (f.r - currentR) / Scale;
		moverG = (f.g - currentG) / Scale;
		moverB = (f.b - currentB) / Scale;
		shifted = Scale;
	}

	public Color Shift() {
		currentR += moverR;
		currentG += moverG;
		currentB += moverB;
		shifted--;
		return new Color(currentR, currentG, currentB);
	}

	public bool Shifting() {
		return shifted > 0;
	}

	public Color Color() {
		return new Color(currentR, currentG, currentB);
	}
}