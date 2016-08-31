using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Ship : MonoBehaviour {

	public string shipName;
	public int price;

	public float maxHealth;

	public float maxShield;
	public float shieldRegenerateTime;

	public float speed;
	public float weaponLvlAllowed;

	public float value;

	public float collisionDamage;
	
	public float xRotation;
	public float yRotation;
	
	public List<Transform> weaponsPositions;
	public List<GameObject> weapons;
	
	public GameObject smoke;
	public GameObject explosion;
	
	public bool __________________________;
	
	public float currentHealth;
	public float currentShield;
	public int showDamageFrames;
	public int currentShowDamageFrames;


	// Use this for initialization
	void Start () {
		this.currentHealth = this.maxHealth;
		this.currentShield = this.maxShield;
		this.showDamageFrames = 2;
		this.currentShowDamageFrames = 0;
	}

	// Update is called once per frame
	void Update () {}
}
