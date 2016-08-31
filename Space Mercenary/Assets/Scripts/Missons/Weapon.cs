using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public GameObject projectile;

	public float fireRate;

	public bool __________________________;

	public float lastTimeShot;
	public bool fireGun;
	public bool isPlayerTheOwner;

	void Awake(){
		
		this.isPlayerTheOwner = false;
	}

	// Use this for initialization
	void Start () {

		this.lastTimeShot = float.MaxValue;
	}
	
	// Update is called once per frame
	void Update () {
	
		this.lastTimeShot += Time.deltaTime;
		if(this.lastTimeShot > this.fireRate && this.fireGun){
			GameObject gO = Instantiate(this.projectile) as GameObject;

			if(this.isPlayerTheOwner){
				gO.transform.tag = "PlayerProjectile";
				gO.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 1, 0f);
			}else{
				gO.GetComponent<Projectile>().velocity *= -1f;
				gO.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 1, 0f);
			}

			this.lastTimeShot = 0f;
			this.fireGun = false;
		}
	}

	public void fire(){
		this.fireGun = true;
	}

	public void stopFire(){
		this.fireGun = false;
	}

	public void setOwnerPlayer(){
		this.isPlayerTheOwner = true;
	}
}
