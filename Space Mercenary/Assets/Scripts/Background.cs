using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Background : MonoBehaviour {

	public GameObject farStarPrefab;
	public GameObject starPrefab;
	public GameObject closeStarPrefab;
	public int maxStars;
	public float farStarVelocity;
	public float starVelocity;
	public float closeStarVelocity;
	public float zPosition;

	public bool __________________________;

	public List<Transform> farStars;
	public List<Transform> stars;
	public List<Transform> closeStars;
	public Vector3 downLeftDot;
	public Vector3 upRightDot;
	public GameObject starsParent;

	// Use this for initialization
	void Start () {

		this.starsParent = new GameObject("Stars Parent");

		Utils.setCameraEdges (ref this.downLeftDot, ref this.upRightDot);
	
		this.farStars = new List<Transform>();
		this.stars = new List<Transform>();
		this.closeStars = new List<Transform>();

		setRandomStars ();
	}
	
	// Update is called once per frame
	void Update () {
	
		moveStars ();
	}

	public void setRandomStars(){

		for (int i = 0; i < this.maxStars; i++) {
			float layer = Random.value;
			if(layer <= 1f/3f){
				GameObject gO = Instantiate(this.farStarPrefab) as GameObject;
				gO.transform.position = getRandomPosition();
				this.farStars.Add(gO.transform);
				gO.transform.parent = this.starsParent.transform;
			}else if(layer <= 2f/3f){
				GameObject gO = Instantiate(this.starPrefab) as GameObject;
				gO.transform.position = getRandomPosition();
				this.stars.Add(gO.transform);
				gO.transform.parent = this.starsParent.transform;
			}else{
				GameObject gO = Instantiate(this.closeStarPrefab) as GameObject;
				gO.transform.position = getRandomPosition();
				this.closeStars.Add(gO.transform);
				gO.transform.parent = this.starsParent.transform;
			}
		}
	}

	public Vector3 getRandomPosition(){

		Vector3 randomPsition = new Vector3();
		randomPsition.x = Random.Range(downLeftDot.x, upRightDot.x);
		randomPsition.y = Random.Range(downLeftDot.y, upRightDot.y);
		randomPsition.z = this.zPosition;

		return randomPsition;
	}

	public void moveStars(){

		foreach(Transform star in this.farStars){
			if(star.position.y >= this.downLeftDot.y){
				star.position += new Vector3(0, -this.farStarVelocity, 0);
			}else{
				star.position = new Vector3(star.position.x, this.upRightDot.y, this.zPosition);
			}
		}
		
		foreach(Transform star in this.stars){
			if(star.position.y >= this.downLeftDot.y){
				star.position += new Vector3(0, -this.starVelocity, 0);
			}else{
				star.position = new Vector3(star.position.x, this.upRightDot.y, this.zPosition);
			}
		}
		
		foreach(Transform star in this.closeStars){
			if(star.position.y >= this.downLeftDot.y){
				star.position += new Vector3(0, -this.closeStarVelocity, 0);
			}else{
				star.position = new Vector3(star.position.x, this.upRightDot.y, this.zPosition);
			}
		}
	}
}
