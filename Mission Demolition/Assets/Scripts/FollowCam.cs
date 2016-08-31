using UnityEngine;
using System.Collections;

public class FollowCam : MonoBehaviour {

	static public FollowCam followCamera;
	public float _easing = 0.05f;
	public Vector2 _minXY;
	public Vector2 _maxXY;
	public bool _____________________________________;

	public GameObject _pointOfInterest;
	public float _cameraZ;

	public float _timeSinceThrow;

	void Awake(){
		
		FollowCam.followCamera = this;
		this._cameraZ = this.transform.position.z;
		
		this._minXY = new Vector2(-50f,8f);
		this._maxXY = new Vector2(50f,50f);
		
		this._pointOfInterest = GameObject.Find("Slingshot");
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		Vector3 destination;
		if (this._pointOfInterest != null) {

			this._timeSinceThrow += Time.deltaTime;

			if(this._pointOfInterest.tag == "Projectile" && this._pointOfInterest.GetComponent<Rigidbody>().velocity.magnitude <= 1.7f && this._timeSinceThrow > 3f){
				this._pointOfInterest = GameObject.Find("Slingshot");
				destination = Vector3.zero;
			} else {
				destination = this._pointOfInterest.transform.position;
			}


			float height = Screen.height;
			float width = Screen.width;
			float rateH = this.GetComponent<Camera>().orthographicSize;
			float rateW = this.GetComponent<Camera>().orthographicSize * (width/height);

			destination.y = Mathf.Max (this._minXY.y, Mathf.Min(this._maxXY.y-rateH, destination.y));
			destination.x = Mathf.Max (this._minXY.x+rateW, Mathf.Min(this._maxXY.x-rateW, destination.x));

			destination = Vector3.Lerp (this.transform.position, destination, this._easing);

			destination.z = this._cameraZ;
			this.transform.position = destination;

			this.GetComponent<Camera> ().orthographicSize = this.transform.position.y;
		}
	}


}
