using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour {

	public GameObject _poi;
	public GameObject[] _panels;
	public float _scrollSpeed = -30f;
	public float _motionMult = 0.25f;
	private float _panelHt;
	private float _depth;

	// Use this for initialization
	void Start () {

		this._panelHt = this._panels[0].transform.localScale.y;
		this._depth = this._panels[0].transform.position.z;

		this._panels[0].transform.position = new Vector3(0, 0, this._depth);
		this._panels[1].transform.position = new Vector3(0, this._panelHt, this._depth);
	}
	
	// Update is called once per frame
	void Update () {
	
		float tY, tX = 0;
		tY = Time.time * this._scrollSpeed % this._panelHt + (this._panelHt*0.5f);
		if (this._poi != null) {
			tX = -this._poi.transform.position.x * this._motionMult;
		}

		this._panels[0].transform.position = new Vector3(tX, tY, this._depth);

		if (tY >= 0) {
			this._panels[1].transform.position = new Vector3(tX, tY-this._panelHt, this._depth);
		} else {
			this._panels[1].transform.position = new Vector3(tX, tY+this._panelHt, this._depth);
		}
	}
}
