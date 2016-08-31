using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	static public Player Singleton;


	public bool __________________________;
	
	public Ship ship;

	public Vector3 velocity;
	public GameObject explode;
	public float money;
	public Vector3 downLeftDot;
	public Vector3 upRightDot;
	public float shieldRegenerateTime;
	public Material[] materials;
	public Color[] originalColors;
	
	void Awake () {

		Singleton = this;
	}

	// Use this for initialization
	void Start () {
	
		this.ship = this.gameObject.GetComponent<Ship> ();

		this.transform.tag = Constants.PLAYER_TAG;

		this.velocity = Vector3.zero;

		this.ship.currentHealth = this.ship.maxHealth;
		this.ship.currentShield = this.ship.maxShield;

		initializeWeapons ();

		this.shieldRegenerateTime = 0f;

		this.materials = Utils.getAllMaterials (this.gameObject);
		this.originalColors = new Color[this.materials.Length];
		for(int i = 0; i < this.materials.Length; i++){
			if (this.materials [i].HasProperty("_Color")) {
				this.originalColors [i] = this.materials [i].color;
			}
		}

		Utils.setCameraEdges (ref this.downLeftDot, ref this.upRightDot);
	}
	
	// Update is called once per frame
	void Update () {
	
		move ();

		fire ();

		regenerateShield ();

		unshowDamage ();
	}

	public void move(){
		
		float x = Input.GetAxis ("Horizontal");
		float y = Input.GetAxis ("Vertical");

		float xVel = 0f;
		if (x > 0f) {
			xVel = Mathf.Min (this.velocity.x + x * this.ship.speed, x * this.ship.speed);
		} else if (x < 0f) {
			xVel = Mathf.Max (this.velocity.x + x * this.ship.speed, x * this.ship.speed);
		} else {
			xVel = this.velocity.x + x * this.ship.speed;
		}

		if (this.downLeftDot.x > this.transform.position.x) {
			this.transform.position = new Vector3(this.downLeftDot.x, this.transform.position.y, this.transform.position.z);
		}
		if (this.downLeftDot.y > this.transform.position.y) {
			this.transform.position = new Vector3(this.transform.position.x, this.downLeftDot.y, this.transform.position.z);
		}
		if (this.upRightDot.x < this.transform.position.x) {
			this.transform.position = new Vector3(this.upRightDot.x, this.transform.position.y, this.transform.position.z);
		}
		if (this.upRightDot.y < this.transform.position.y) {
			this.transform.position = new Vector3(this.transform.position.x, this.upRightDot.y, this.transform.position.z);
		}

		float yVel = 0f;
		if (y > 0f) {
			yVel = Mathf.Min (this.velocity.y + y * this.ship.speed, y * this.ship.speed);
		} else if (y < 0f) {
			yVel = Mathf.Max (this.velocity.y + y * this.ship.speed, y * this.ship.speed);
		} else {
			yVel = this.velocity.y + y * this.ship.speed;
		}

		this.velocity = new Vector3(xVel, yVel, 0f);
		
		this.transform.position += new Vector3 (xVel * Time.deltaTime, yVel * Time.deltaTime, 0f);
		
		this.transform.localRotation = Quaternion.Euler(new Vector3((yVel/this.ship.speed) * this.ship.yRotation, (xVel/this.ship.speed) * this.ship.xRotation, 0f));
	}

	public void fire(){
	
		if (Input.GetButton ("Jump")) {
			foreach (GameObject weapon in this.ship.weapons)
				weapon.GetComponent<Weapon> ().fire ();
		} else {
			foreach (GameObject weapon in this.ship.weapons)
				weapon.GetComponent<Weapon> ().stopFire ();
		}
	}

	public void regenerateShield(){

		if (this.ship.currentShield < this.ship.maxShield) {
			this.shieldRegenerateTime += Time.deltaTime;
			if(this.shieldRegenerateTime >= this.ship.shieldRegenerateTime){
				this.shieldRegenerateTime = 0f;
				this.ship.currentShield += 1;
			}
		} else {
			this.shieldRegenerateTime = 0f;	
		}

	}

	public void unshowDamage (){
		if(this.ship.currentShowDamageFrames > 0){
			this.ship.currentShowDamageFrames--;
			if (this.ship.currentShowDamageFrames <= 0) {
				Utils.unshowDamage (this.materials, this.originalColors);
			}
		}
	}

	void OnCollisionEnter(Collision collision){

		this.GetComponent<Rigidbody> ().isKinematic = true;
		this.GetComponent<Rigidbody> ().isKinematic = false;
		
		if (collision.gameObject.tag == Constants.ENEMY_TAG) {
			recieveDamage(collision.gameObject.GetComponent<Enemy> ().ship.collisionDamage);
		} else if (collision.gameObject.tag == Constants.PLAYER2_TAG) {
			recieveDamage(collision.gameObject.GetComponent<Player2> ().ship.collisionDamage);
		} else if (collision.gameObject.tag == Constants.ASTEROID_TAG) {
			this.ship.currentHealth = 0f;
		}

		
		if (this.ship.currentHealth <= (this.ship.maxHealth*0.3f) && this.transform.Find ("Smokes").GetChild (0).childCount == 0) {
			GameObject gO = Instantiate (this.ship.smoke) as GameObject;
			gO.transform.parent = this.transform.Find ("Smokes").GetChild (0);
			gO.transform.localPosition = Vector3.zero;
		}
		if (this.ship.currentHealth <= 0) {
			GameObject gO = Instantiate (this.ship.explosion) as GameObject;
			gO.transform.position = this.transform.position;
			this.explode = gO;
			this.gameObject.SetActive (false);
		}
		
	}

	public void recieveDamage(float damage){

		float damageToShield = damage;
		float damageToHealth = 0f;
		if (this.ship.currentShield > 0) {
			this.ship.currentShield -= damageToShield;
			if (this.ship.currentShield < 0f) {
				damageToHealth = this.ship.currentShield * -1;
				this.ship.currentShield = 0f;
			}
			Utils.showDamage (this.materials, Color.blue);
		} else {
			damageToHealth = damage;
		}

		if (damageToHealth > 0) {
			this.ship.currentHealth -= damageToHealth;
			Utils.showDamage (this.materials, Color.red);
		}

		this.ship.currentShowDamageFrames = this.ship.showDamageFrames;
	}

	public void initializeWeapons(){

		for(int i = 0; i < this.ship.weaponsPositions.Count; i++){
			this.ship.weapons[i] = Instantiate(this.ship.weapons[i]) as GameObject;
			this.ship.weapons[i].transform.parent = this.ship.weaponsPositions[i];
			this.ship.weapons[i].transform.localPosition = new Vector3(0f, 0f, 0f);

			this.ship.weapons [i].GetComponent<Weapon> ().fireRate *= 0.5f;
			this.ship.weapons[i].GetComponent<Weapon>().setOwnerPlayer();
		}
	}
}
