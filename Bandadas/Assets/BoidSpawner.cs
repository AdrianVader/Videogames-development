using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoidSpawner : MonoBehaviour {

	public static BoidSpawner SPAWNER;
	public static List<Boid> ALL_BOIDS;
	public GameObject _prefab;
	protected int _numberOfObjects;
	protected float _radius;
	public float _lerp;
	public float _matching;
	public float mouseAttractionAmt;
	public float mouseAttractionDist;
	public float mouseAvoidanceAmt;
	public float mouseAvoidanceDist;
	public Vector3 mousePos;

	// Use this for initialization
	void Start () {
		_numberOfObjects = 50;
		_radius = 40f;
		_lerp = 0.05f;
		_matching = 0.01f;
		mouseAttractionAmt = 05.3f;//0.01f;
		mouseAttractionDist = 15f;//100f;
		mouseAvoidanceAmt = 10.75f;//0.75f;
		mouseAvoidanceDist = 15f;//50f;

		SPAWNER = this;
		ALL_BOIDS = new List<Boid> ();

		for (int i=0; i<_numberOfObjects; i++) {
			ALL_BOIDS.Add(Instantiate(_prefab).GetComponent<Boid>());
		}
	}
	// Update is called once per frame
	void Update () {
	}
	void LateUpdate () {
		Vector3 mousePos2d = new Vector3( Input.mousePosition.x, Input.mousePosition.y, this.transform.position.y );
		mousePos = GetComponent<Camera>().ScreenToWorldPoint( mousePos2d );
	}
	public float radius{
		get{return _radius;}
		set{_radius = value;}
	}
}