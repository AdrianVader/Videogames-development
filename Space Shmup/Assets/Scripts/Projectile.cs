using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	[SerializeField]
	protected WeaponType _type;

	public int _damage = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void crash(){
		Ship.SingletonShip.shield -= this._damage;
		Destroy(this.gameObject);
	}

	public WeaponType type{
		get {
			return(this._type);
		}
		set {
			SetType(value);
		}
	}

	public void SetType(WeaponType eType){
		this._type = eType;
		WeaponDefinition def = Main.GetWeaponDefinition(this._type);
		this.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = def.projectileColor;
	}

	public void CheckOffscreen(){
		if(Utils.ScreenBoundsCheck(GetComponent<Collider>().bounds, BoundsTest.offScreen) != Vector3.zero){
		   Destroy(this.gameObject);
		}
	}


}
