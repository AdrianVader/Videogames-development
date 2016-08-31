using UnityEngine;
using System.Collections;

public class Slingshot : MonoBehaviour {

	static public Slingshot slingshot;

	public GameObject _prefabProjectile;
	public float _velocity = 4f;
	public bool _____________________________________;

	public GameObject _launchPoint;
	public Vector3 _launchPos;
	public GameObject _projectile;
	public bool _aimingMode;

	// Use this for initialization
	void Start () {
	
		slingshot = this;

		this._launchPoint = this.transform.Find("LaunchPoint").gameObject;
		this._launchPoint.SetActive(false);
		this._launchPos = this.transform.Find ("LaunchPoint").position;
	}
	
	// Update is called once per frame
	void Update () {
	
		if (this._aimingMode) {
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			mousePos.z = 0f;

			Vector3 mouseDelta = mousePos-this._launchPos;

			float maxMagnitude = this.GetComponent<SphereCollider>().radius;
			if (mouseDelta.magnitude > maxMagnitude) {
				mouseDelta.Normalize();
				mouseDelta *= maxMagnitude;
			}

			Vector3 projectilePos = this._launchPos + mouseDelta;
			this._projectile.transform.position = projectilePos;
			if (Input.GetMouseButtonUp(0)) {
				this._aimingMode = false;
				this._projectile.GetComponent<Rigidbody>().isKinematic = false;
				this._projectile.GetComponent<Rigidbody>().velocity = -mouseDelta * this._velocity;
				FollowCam.followCamera._pointOfInterest = this._projectile;
				FollowCam.followCamera._timeSinceThrow = 0;
				this._projectile = null;
				ProjectileLine.projectileLine.Clear();

				GUIText gtThrows = GameObject.Find("GT_Throws").GetComponent<GUIText>();
				gtThrows.text = (int.Parse(gtThrows.text)+1).ToString();
			}
		}
	}

	void OnMouseEnter(){
		this._launchPoint.SetActive(true);
	}

	void OnMouseExit(){
		this._launchPoint.SetActive(false);
	}

	void OnMouseDown(){

		this._aimingMode = true;
		this._projectile = Instantiate (this._prefabProjectile) as GameObject;
		this._projectile.transform.position = this._launchPos;
		this._projectile.GetComponent<Rigidbody>().isKinematic = true;
	}
}
