using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {

	static public Ship SingletonShip;

	public float _speed = 30;
	public float _rollMultiplyer = -45;
	public float _pitchMultiplyer = 30;
	[SerializeField]
	protected int _shield = 1;
	public Weapon[] _weapons;
	public float _gameRestartDelay = 2f;

	public bool ______________________;

	public Bounds _bounds;
	public delegate void WeaponFireDelegate();
	public WeaponFireDelegate fireDelegate;

	void Awake(){

		SingletonShip = this;
		this._bounds = Utils.CombineBoundsOfChildren(this.gameObject);

		clearWeapons();
	}

	// Use this for initialization
	void Start () {
	
		this._weapons[0].SetType(WeaponType.blaster);
	}
	
	// Update is called once per frame
	void Update () {
	
		this.move();

		this.tryToFire();
	}

	protected void move(){

		Vector2 moveDirection = getAxis();
		
		Vector3 position = this.transform.position;
		position.x += moveDirection.x * this._speed * Time.deltaTime;
		position.y += moveDirection.y * this._speed * Time.deltaTime;
		position.z = -2;
		this.transform.position = position;

		this._bounds.center = transform.position;
		Vector3 off = Utils.ScreenBoundsCheck(this._bounds, BoundsTest.onScreen);
		if(off != Vector3.zero){
			position -= off;
			transform.position = position;
		}
		
		this.transform.rotation = Quaternion.Euler(moveDirection.y * this._pitchMultiplyer, moveDirection.x * this._rollMultiplyer, 0);
	}

	protected Vector2 getAxis(){
		return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
	}

	void OnTriggerEnter(Collider other) {

		GameObject go = Utils.FindTaggedParent(other.gameObject);
		if(go != null){
			if(go.tag == "Enemy"){
				go.GetComponent<Enemy>().crash();
			}else if(go.tag == "EnemyProjectile"){
				go.GetComponent<Projectile>().crash();
			}else if(go.tag == "PowerUp"){
				go.GetComponent<PowerUp>().crash();
			}else{
				print("Triggered: " + go.name);
			}
		}
	}

	public int shield{
		get {
			return(this._shield);
		}
		set {
			this._shield = Mathf.Min(value, 4);
			if(value < 0){
				Main.SingletonMain.restartWithDelay(this._gameRestartDelay);
				Destroy(this.gameObject);
			}
		}
	}


	protected void tryToFire(){
		if(Input.GetAxis("Jump") == 1 && fireDelegate != null){
			fireDelegate();
		}
	}

	public Weapon getEmptyWeaponSlot() {
		for (int i=0; i < this._weapons.Length; i++) {
			if (this._weapons[i].type == WeaponType.none) {
				return(this._weapons[i]);
			}
		}
		return( null );
	}

	public void clearWeapons() {
		foreach (Weapon w in this._weapons) {
			w.SetType(WeaponType.none);
		}
	}


}

