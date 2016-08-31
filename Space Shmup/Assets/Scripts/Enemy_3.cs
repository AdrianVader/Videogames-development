using UnityEngine;
using System.Collections;

public class Enemy_3 : Enemy {

	public Vector3[] _points;
	public float _birthTime;
	public float _lifeTime = 10;

	// Use this for initialization
	void Start () {
	
		this._points = new Vector3[3];
		this._points[0] = this.transform.position;

		float xMin = Utils.camBounds.min.x + Main.SingletonMain._enemySpawnPadding;
		float xMax = Utils.camBounds.max.x - Main.SingletonMain._enemySpawnPadding;
		Vector3 v;

		v = Vector3.zero;
		v.x = Random.Range(xMin, xMax);
		v.y = Random.Range(Utils.camBounds.min.y, 0);
		this._points[1] = v;

		v = Vector3.zero;
		v.y = this.transform.position.y;
		v.x = Random.Range(xMin, xMax);
		this._points[2] = v;

		this._birthTime = Time.time;
	}
	
	public override void move() {

		float u = (Time.time - this._birthTime) / this._lifeTime;
		if (u > 1) {
			Destroy(this.gameObject);
			return;
		}

		Vector3 p01, p12;
		p01 = (1-u) * this._points[0] + u * this._points[1];
		p12 = (1-u) * this._points[1] + u * this._points[2];
		this.transform.position = (1-u) * p01 + u * p12;
		this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -2);
	}


}
