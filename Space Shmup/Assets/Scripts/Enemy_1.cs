using UnityEngine;
using System.Collections;

public class Enemy_1 : Enemy {

	public float _waveFrequency = 2;
	public float _waveWidth = 4;
	public float _waveRotY = 45;
	private float _x0;
	private float _birthTime;

	// Use this for initialization
	void Start () {
	
		this._x0 = this.transform.position.x;
		this._birthTime = Time.time;
	}

	public override void move() {

		Vector3 tempPos = this.transform.position;

		float age = Time.time - this._birthTime;
		float theta = Mathf.PI * 2 * age / this._waveFrequency;
		float sin = Mathf.Sin(theta);
		tempPos.x = this._x0 + this._waveWidth * sin;
		this.transform.position = tempPos;

		Vector3 rot = new Vector3(0, sin * this._waveRotY, 0);
		this.transform.rotation = Quaternion.Euler(rot);

		base.move();
	}


}
