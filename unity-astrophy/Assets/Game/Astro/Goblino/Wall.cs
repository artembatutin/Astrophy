using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {
	private const int SkipPoints = 3;
	private const float LifeTime = 4f;

	private int pointsCount;
	private Vector2 startPoint;

	private readonly List<Vector2> points = new List<Vector2>();
	private LineRenderer line;
	private EdgeCollider2D col;

	private bool terminating;
	private float time;

	private void Start() {
		time = LifeTime;
		line = gameObject.GetComponent<LineRenderer>();
		col = gameObject.GetComponent<EdgeCollider2D>();
		line.SetPosition(0, startPoint);
		pointsCount++;
	}

	private void Update() {
		if(!terminating)
			return;
		time -= Time.deltaTime;
		if(time <= 0) {
			Destroy();
		}
	}

	public void Initialize(Vector2 start) {
		startPoint = start;
	}

	public void Terminate() {
		terminating = true;
	}

	public void Destroy() {
		Destroy(gameObject);
	}

	public void AddPoint(Vector2 point) {
		pointsCount++;
		if(pointsCount % SkipPoints == 0) { //Registered full point.
			points.Add(point);
			if(line != null) {
				UpdateLine(point);
			}

			if(col != null) {
				UpdateCollider();
			}
		}
		else { //Visual last point.
			points.Add(point);
			UpdateLine(point);
			UpdateCollider();
			points.RemoveAt(points.Count - 1);
		}
	}

	private void UpdateCollider() {
		col.points = points.ToArray();
	}

	private void UpdateLine(Vector2 point) {
		Vector3 p3 = new Vector3(point.x, point.y, 0);
		line.positionCount = points.Count;
		line.SetPosition(line.positionCount - 1, p3);
	}

	private void OnCollisionEnter2D(Collision2D col) {
		//if(time > 0.8f)
		//	time = 0.8f;
	}

	public LineRenderer Line() {
		return line;
	}
}