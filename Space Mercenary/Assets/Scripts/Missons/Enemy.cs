using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	
	public bool __________________________;
	
	public Ship ship;
	public GameObject explode;
	public Vector3 downLeftDot;
	public Vector3 upRightDot;
	public Material[] materials;
	public Color[] originalColors;
	public bool alive = true;

	// Use this for initialization
	void Start () {

		this.ship = this.gameObject.GetComponent<Ship> ();

		this.ship.currentHealth = this.ship.maxHealth;
		this.ship.currentShield = this.ship.maxShield;

		initializeWeapons ();

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

		unshowDamage ();
	}

	public void move(){

		if (this.gameObject.GetComponent<EnemyBehaviour> () == null) {
			this.transform.position += new Vector3 (0f, this.ship.speed * Time.deltaTime * -1f, 0f);
		} else {
			this.gameObject.GetComponent<EnemyBehaviour> ().move(this);
		}

		if (this.transform.position.y > this.upRightDot.y + 5f ||
			this.transform.position.y < this.downLeftDot.y - 5f ||
			this.transform.position.x > this.upRightDot.x + 5f ||
			this.transform.position.x < this.downLeftDot.x - 5f) {
			Destroy (this.gameObject);
		}
	}

	public void fire(){
		foreach(GameObject weapon in this.ship.weapons){
			weapon.GetComponent<Weapon> ().fire ();
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

		if (collision.gameObject.tag == Constants.PLAYER_TAG) {
			recieveDamage (collision.gameObject.GetComponent<Player> ().ship.collisionDamage);
		} else if (collision.gameObject.tag == Constants.ASTEROID_TAG) {
			this.ship.currentHealth = 0f;
		}

	
		if (this.ship.currentHealth <= (this.ship.maxHealth * 0.3f) && this.transform.Find ("Smokes").GetChild (0).childCount == 0) {
			GameObject gO = Instantiate (this.ship.smoke) as GameObject;
			gO.transform.parent = this.transform.Find ("Smokes").GetChild (0);
			gO.transform.localPosition = Vector3.zero;
		}
		if (this.ship.currentHealth <= 0) {

			if (this.alive) {
				this.alive = false;
				GameObject gO = Instantiate (this.ship.explosion) as GameObject;
				gO.transform.position = this.transform.position;
				this.explode = gO;

				TextMesh moneyText = GameObject.Find (Constants.PLAYER_MONEY).GetComponent<TextMesh> ();
				float money = float.Parse (moneyText.text);
				money += this.ship.value;
				moneyText.text = money.ToString ();

				this.gameObject.SetActive (false);
				Invoke ("destroy", 1f);
			} else {
				this.ship.collisionDamage = 0f;
			}
		}


	}

	public void recieveDamage(float damage){
		
		this.ship.currentHealth -= damage;
		Utils.showDamage (this.materials, Color.red);

		this.ship.currentShowDamageFrames = this.ship.showDamageFrames;
	}
	
	public void initializeWeapons(){
		
		for(int i = 0; i < this.ship.weaponsPositions.Count; i++){
			this.ship.weapons[i] = Instantiate(this.ship.weapons[i]) as GameObject;
			this.ship.weapons[i].transform.parent = this.ship.weaponsPositions[i];
			this.ship.weapons[i].transform.localPosition = new Vector3(0f, 0f, 0f);
			this.ship.weapons[i].transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));

			this.ship.weapons [i].GetComponent<Weapon> ().fireRate *= 2.5f;
		}
	}

	public void destroy(){

		DestroyImmediate (this.explode);
		Invoke ("destroyShip", 1f);
	}

	public void destroyShip(){

		Destroy (this);
	}
}
