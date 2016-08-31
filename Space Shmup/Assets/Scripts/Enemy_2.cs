using UnityEngine;
using System.Collections;

public class Enemy_2 : Enemy {

	public Vector3[] _points;
	public float _birthTime;
	public float _lifeTime = 10;
	public float _sinEccentricity = 0.6f;

	// Use this for initialization
	void Start () {

		this._points = new Vector3[2];
		Vector3 cbMin = Utils.camBounds.min;
		Vector3 cbMax = Utils.camBounds.max;
		Vector3 v = Vector3.zero;

		v.x = cbMin.x - Main.SingletonMain._enemySpawnPadding;
		v.y = Random.Range(cbMin.y, cbMax.y);
		this._points[0] = v;

		v = Vector3.zero;
		v.x = cbMax.x + Main.SingletonMain._enemySpawnPadding;
		v.y = Random.Range(cbMin.y, cbMax.y);
		this._points[1] = v;

		if (Random.value < 0.5f) {
			this._points[0].x *= -1;
			this._points[1].x *= -1;
		}

		this._birthTime = Time.time;
	}
	
	public override void move() {

		float u = (Time.time - this._birthTime) / this._lifeTime;

		if (u > 1) {
			Destroy(this.gameObject);
			return;
		}

		u = u + this._sinEccentricity * (Mathf.Sin(u * Mathf.PI * 2));

		this.transform.position = (1-u) * this._points[0] + u * this._points[1];
		this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -2);
	}


}
