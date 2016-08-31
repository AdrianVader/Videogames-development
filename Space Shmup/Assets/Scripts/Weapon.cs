using UnityEngine;
using System.Collections;

public enum WeaponType {
	none,
	blaster,
	spread,
	phaser,
	missile,
	laser,
	shield
}

[System.Serializable]
public class WeaponDefinition {
	public WeaponType type = WeaponType.none;
	public string letter;
	public Color color = Color.white;
	public GameObject projectilePrefab;
	public Color projectileColor = Color.white;
	public float damageOnHit = 0;
	public float continuousDamage = 0;
	public float delayBetweenShots = 0;
	public float velocity = 20;
}

public class Weapon : MonoBehaviour {

	static public Transform ProjectileAnchor;

	public bool ____________________;

	[SerializeField]
	private WeaponType _type = WeaponType.none;
	public WeaponDefinition def;
	public GameObject collar;
	public float lastShot;

	// Use this for initialization
	void Start () {
	
		collar = transform.Find("Collar").gameObject;
		SetType(this._type);
		if (ProjectileAnchor == null) {
			GameObject go = new GameObject("_Projectile_Anchor");
			ProjectileAnchor = go.transform;
		}

		GameObject parentGO = transform.parent.gameObject;
		if (parentGO.tag == "Ship") {
			Ship.SingletonShip.fireDelegate += Fire;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public WeaponType type {
		get{return(this._type);}
		set{SetType(value);}
	}

	public void SetType(WeaponType wt){
		this._type = wt;
		if (type == WeaponType.none) {
			this.gameObject.SetActive(false);
			return;
		} else {
			this.gameObject.SetActive(true);
		}
		def = Main.GetWeaponDefinition(this._type);
		collar.GetComponent<Renderer>().material.color = def.color;
		lastShot = 0;
	}

	public void Fire() {

		if (!gameObject.activeInHierarchy) {
			return;
		}

		if (Time.time - lastShot < def.delayBetweenShots) {
			return;
		}

		Projectile p;
		switch (type) {
		case WeaponType.blaster:
			p = MakeProjectile();
			p.GetComponent<Rigidbody>().velocity = Vector3.up * def.velocity;
			break;
		case WeaponType.spread:
			p = MakeProjectile();
			p.GetComponent<Rigidbody>().velocity = new Vector3( 0.2f, 0.9f, 0f ) * def.velocity;
			p = MakeProjectile();
			p.GetComponent<Rigidbody>().velocity = Vector3.up * def.velocity;
			p = MakeProjectile();
			p.GetComponent<Rigidbody>().velocity = new Vector3( -0.2f, 0.9f, 0f ) * def.velocity;
			break;
		}
	}

	public Projectile MakeProjectile() {

		GameObject go = Instantiate(def.projectilePrefab) as GameObject;
		if (transform.parent.gameObject.tag == "Ship") {
			go.tag = "ShipProjectile";
			go.layer = LayerMask.NameToLayer("ShipProjectile");
		} else {
			go.tag = "EnemyProjectile";
			go.layer = LayerMask.NameToLayer("EnemyProjectile");
		}

		go.transform.position = collar.transform.position;
		go.transform.parent = ProjectileAnchor;
		Projectile p = go.GetComponent<Projectile>();
		p.type = type;
		lastShot = Time.time;
		return(p);
	}


}
