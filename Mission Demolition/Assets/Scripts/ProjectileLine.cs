using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProjectileLine : MonoBehaviour {
	static public ProjectileLine projectileLine;

	public float _minDist = 0.1f;
	public bool _____________________________;

	public LineRenderer _line;
	private GameObject _pointOfInterest;
	public List<Vector3> _points;
	void Awake() {
		projectileLine = this;

		this._line = GetComponent<LineRenderer>();

		this._line.enabled = false;

		this._points = new List<Vector3>();
	}

	public GameObject pointOfInterest {
		get {
			return( this._pointOfInterest );
		}
		set {
			this._pointOfInterest = value;
			if ( this._pointOfInterest != null ) {
				this._line.enabled = false;
				this._points = new List<Vector3>();
				AddPoint();
			}
		}
	}

	public void Clear() {
		this._pointOfInterest = null;
		this._line.enabled = false;
		this._points = new List<Vector3>();
	}

	public void AddPoint() {

		Vector3 pt = this._pointOfInterest.transform.position;
		if ( this._points.Count > 0 && (pt - lastPoint).magnitude < this._minDist ) {
			return;
		}
		if ( this._points.Count == 0 ) {
			Vector3 launchPos = Slingshot.slingshot._launchPoint.transform.position;
			Vector3 launchPosDiff = pt - launchPos;

			this._points.Add( pt + launchPosDiff );
			this._points.Add(pt);
			this._line.SetVertexCount(2);

			this._line.SetPosition(0, this._points[0] );
			this._line.SetPosition(1, this._points[1] );

			this._line.enabled = true;
		} else {

			this._points.Add( pt );
			this._line.SetVertexCount( this._points.Count );
			this._line.SetPosition( this._points.Count-1, lastPoint );
			this._line.enabled = true;
		}
	}

	public Vector3 lastPoint {
		get {
			if (this._points == null) {

				return( Vector3.zero );
			}
			return( this._points[this._points.Count-1] );
		}
	}

	void FixedUpdate () {
		if ( this._pointOfInterest == null ) {

			if (FollowCam.followCamera._pointOfInterest != null) {
				if (FollowCam.followCamera._pointOfInterest.tag == "Projectile") {
					this._pointOfInterest = FollowCam.followCamera._pointOfInterest;
				} else {
					return;
				}
			} else {
				return;
			}
		}

		AddPoint();
		if ( this._pointOfInterest.GetComponent<Rigidbody>().IsSleeping() ) {

			this._pointOfInterest = null;
		}
	}
}