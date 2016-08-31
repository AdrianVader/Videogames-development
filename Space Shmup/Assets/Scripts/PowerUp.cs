using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

	public Vector2 _rotMinMax = new Vector2(15,90);
	public Vector2 _driftMinMax = new Vector2(.25f,2);
	public float _lifeTime = 6f;
	public float _fadeTime = 4f;

	public bool ________________;

	public WeaponType _type;
	public GameObject _cube;
	public TextMesh _letter;
	public Vector3 _rotPerSecond;
	public float _birthTime;

	void Awake() {

		this._cube = transform.Find("Cube").gameObject;

		this._letter = GetComponent<TextMesh>();

		Vector3 vel = Random.onUnitSphere;
		vel.z = 0;
		vel.Normalize();

		vel *= Random.Range(this._driftMinMax.x, this._driftMinMax.y);

		GetComponent<Rigidbody>().velocity = vel;

		transform.rotation = Quaternion.identity;

		this._rotPerSecond = new Vector3(Random.Range(this._rotMinMax.x, this._rotMinMax.y), Random.Range(this._rotMinMax.x, this._rotMinMax.y), Random.Range(this._rotMinMax.x, this._rotMinMax.y));
		InvokeRepeating("CheckOffscreen", 2f, 2f);
		this._birthTime = Time.time;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		this._cube.transform.rotation = Quaternion.Euler(this._rotPerSecond*Time.time);

		float u = (Time.time - (this._birthTime + this._lifeTime)) / this._fadeTime;

		if (u >= 1) {
			Destroy( this.gameObject );
			return;
		}

		if (u>0) {
			Color c = this._cube.GetComponent<Renderer>().material.color;
			c.a = 1f - u;
			this._cube.GetComponent<Renderer>().material.color = c;

			c = this._letter.color;
			c.a = 1f - (u*0.5f);
			this._letter.color = c;
		}
	}

	public void SetType(WeaponType wt) {

		WeaponDefinition def = Main.GetWeaponDefinition(wt);

		this._cube.GetComponent<Renderer>().material.color = def.color;

		this._letter.text = def.letter;
		this._type = wt;
	}

	void CheckOffscreen() {
		if (Utils.ScreenBoundsCheck(this._cube.GetComponent<Collider>().bounds, BoundsTest.offScreen) != Vector3.zero) {
			Destroy(this.gameObject);
		}
	}
	
	public void crash(){

		switch (this._type) {
		case WeaponType.shield:
			Ship.SingletonShip.shield += 1;
			break;
		default:

			if (this._type == Ship.SingletonShip._weapons[0].type) {
				Weapon w = Ship.SingletonShip.getEmptyWeaponSlot();
				if (w != null) {
					w.SetType(this._type);
				}
			} else {
				Ship.SingletonShip.clearWeapons();
				Ship.SingletonShip._weapons[0].SetType(this._type);
			}
			break;
		}
		Destroy(this.gameObject);
	}

}
