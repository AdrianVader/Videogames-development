using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public float damage;
	public float velocity;
	public GameObject sparks;
	public AudioSource[] sounds;

	public bool __________________________;

	public float maxYPos;
	public float minYPos;
	public Vector3 downLeftDot;
	public Vector3 upRightDot;

	public AudioSource mySound;

	// Use this for initialization
	void Start () {
	
		string projectileParentName = "Projectiles Parent";
		GameObject projectilesParent = GameObject.Find (projectileParentName);
		if (projectilesParent == null) {
			projectilesParent = new GameObject (projectileParentName);
		}
		this.transform.parent = projectilesParent.transform;

		Utils.setCameraEdges (ref this.downLeftDot, ref this.upRightDot);
		this.maxYPos = this.upRightDot.y + 5;
		this.minYPos = this.downLeftDot.y - 5;

		this.sparks.GetComponent<ParticleSystem> ().Stop ();

		int randomSound = Random.Range (0, this.sounds.Length);
		this.mySound = Instantiate (this.sounds[randomSound]);
		this.mySound.transform.parent = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
	
		this.transform.position += new Vector3(0f, this.velocity * Time.deltaTime, 0f);

		if(this.transform.position.y > this.maxYPos || this.transform.position.y < this.minYPos){
			Destroy (this.gameObject);
		}
	}

	void OnCollisionEnter(Collision collision){
		
		if (this.damage > 0) {
			
			if (collision.gameObject.tag == Constants.ENEMY_TAG && this.transform.tag == Constants.PLAYER_PROJECTILE_TAG) {
				collision.gameObject.GetComponent<Enemy> ().recieveDamage (this.damage);

			} else if (collision.gameObject.tag == Constants.PLAYER_TAG && this.transform.tag == Constants.ENEMY_PROJECTILE_TAG) {
				collision.gameObject.GetComponent<Player> ().recieveDamage (this.damage);

			} else if (collision.gameObject.tag == Constants.PLAYER2_TAG && this.transform.tag == Constants.PLAYER_PROJECTILE_TAG) {
				collision.gameObject.GetComponent<Player2> ().recieveDamage (this.damage);

			} else if (collision.gameObject.tag == Constants.ASTEROID_TAG) {
				
			}

			if (this.tag != collision.gameObject.tag && !(this.tag == Constants.PLAYER_PROJECTILE_TAG && collision.gameObject.tag == Constants.PLAYER_TAG) && !(this.tag == Constants.ENEMY_PROJECTILE_TAG && collision.gameObject.tag == Constants.ENEMY_TAG)) {
				this.sparks = Instantiate (this.sparks);
				this.damage = 0f;
				this.sparks.transform.position = this.transform.position;

				this.transform.gameObject.SetActive (false);
				Invoke ("destroy", 1f);
			} else {
				this.GetComponent<Rigidbody> ().isKinematic = true;
				this.GetComponent<Rigidbody> ().isKinematic = false;
			}
		}

	}

	public void destroy(){
		DestroyImmediate (this.sparks);
		DestroyImmediate (this.mySound);
		Destroy (this);
	}
}
