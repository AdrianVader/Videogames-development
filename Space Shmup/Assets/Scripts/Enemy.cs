using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public float _speed = 1f;
	public float _fireRate = 0.3f;
	public float _health = 10;
	public int _score = 100;
	public int showDamageForFrames = 2;
	public float powerUpDropChance = 1f;

	public bool ________________;

	public Color[] _originalColors;
	public Material[] _materials;
	public int remainingDamageFrames = 0;
	public Bounds bounds;
	public Vector3 boundsCenterOffset;

	void Awake() {

		this._materials = Utils.GetAllMaterials( gameObject );
		this._originalColors = new Color[this._materials.Length];
		for (int i=0; i < this._materials.Length; i++) {
			this._originalColors[i] = this._materials[i].color;
		}

		InvokeRepeating( "CheckOffscreen", 0f, 2f );
	}

	// Use this for initialization
	void Start () {
		this._speed = 1f;
	}
	
	// Update is called once per frame
	void Update () {
	
		move();

		restoreColorDamaged();
	}

	public virtual void move(){
		Vector3 tempPos = position;
		tempPos.y -= this._speed * Time.deltaTime;
		position = tempPos;
	}

	public void restoreColorDamaged(){

		if (remainingDamageFrames>0) {
			remainingDamageFrames--;
			if (remainingDamageFrames == 0) {
				unShowDamage();
			}
		}
	}



	public Vector3 position {
		get {
			return( this.transform.position );
		}
		set {
			this.transform.position = value;
		}
	}

	public void CheckOffscreen(){
		if (bounds.size == Vector3.zero) {
			bounds = Utils.CombineBoundsOfChildren(this.gameObject);
			boundsCenterOffset = bounds.center - position;
		}

		bounds.center = position + boundsCenterOffset;

		Vector3 off = Utils.ScreenBoundsCheck(bounds, BoundsTest.offScreen);
		if(off != Vector3.zero){
			if (off.y < 0) {
				Destroy(this.gameObject);
			}
		}
	}

	public void crash(){

		Ship.SingletonShip.shield -=  1;
		Destroy(this.gameObject);
	}
	
	void OnCollisionEnter(Collision coll) {

		GameObject other = coll.gameObject;
		switch (other.tag) {
		case "ShipProjectile":
			Projectile p = other.GetComponent<Projectile>();
			bounds.center = transform.position + boundsCenterOffset;
			if (bounds.extents == Vector3.zero || Utils.ScreenBoundsCheck(bounds, BoundsTest.offScreen) != Vector3.zero) {
				Destroy(other);
				break;
			}

			this._health -= Main.W_DEFS[p.type].damageOnHit;
			showDamage();
			if (this._health <= 0) {
				Main.SingletonMain.shipDestroyed(this);
				Destroy(this.gameObject);
			}
			Destroy(other);
			break;
		}
	}

	void showDamage() {
		foreach (Material m in this._materials) {
			m.color = Color.red;
		}
		remainingDamageFrames = showDamageForFrames;
	}

	void unShowDamage() {
		for ( int i=0; i < this._materials.Length; i++ ) {
			this._materials[i].color = this._originalColors[i];
		}
	}


}
