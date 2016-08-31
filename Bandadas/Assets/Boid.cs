using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Boid : MonoBehaviour {

	protected Vector3 _velocity;
	public Vector3 _newVelocity;
	public Vector3 _newPosition;
	protected float VELOCITY_INCREASE = 10f;
	protected float RADIUS = 1.8f;
	protected Vector3 INFLUENCE_AREA = new Vector3(5f, 5f, 5f);
	protected Boid me;

	void Awake () {
		me = this;
		me.transform.parent = GameObject.Find("Boids").transform;
		Vector3 randPosition = Random.insideUnitSphere * BoidSpawner.SPAWNER.radius;
		randPosition.y = 0;
		me.transform.position = randPosition;
		_velocity = Random.onUnitSphere;
		_velocity *= VELOCITY_INCREASE;
		Color randomColor = new Color(Random.value, Random.value, Random.value);
		/*Color randomColor = Color.black;
		while ( randomColor.r + randomColor.g + randomColor.b < 1.0f ) {
		randomColor = new Color(Random.value, Random.value, Random.value);
		}*/

		Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();
		foreach ( Renderer r in renderers ) {
			r.material.color = randomColor;
		}
	}

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		_newVelocity = _velocity;
		_newPosition = me.transform.position;

		//Vector3 randomVel = Random.insideUnitSphere;
		//_newVelocity += randomVel * BoidSpawner.SPAWNER._matching;

		Vector3 dist;
		dist = BoidSpawner.SPAWNER.mousePos - me.transform.position;

		/*if (dist.magnitude > BoidSpawner.SPAWNER.mouseAttractionDist) {
			_newVelocity = dist.normalized * BoidSpawner.SPAWNER.mouseAttractionAmt;
		} else if (dist.magnitude > BoidSpawner.SPAWNER.mouseAvoidanceDist) {
		} else {
			_newVelocity -= dist.normalized * BoidSpawner.SPAWNER.mouseAvoidanceAmt;
		}*/
		if (dist.magnitude > BoidSpawner.SPAWNER.mouseAvoidanceDist) {
			_newVelocity += dist.normalized * BoidSpawner.SPAWNER.mouseAttractionAmt;
		} else {
			_newVelocity -= (dist.normalized * BoidSpawner.SPAWNER.mouseAvoidanceAmt);
		}

		List<Boid> someBoids = new List<Boid>(BoidSpawner.ALL_BOIDS);
		someBoids.Remove(me);
		Vector3 v1 = Rule1Cohesion(someBoids);
		Vector3 v2 = rule2Separation(someBoids);
		Vector3 v3 = rule3Aligment(someBoids);
		me._newVelocity += (v1*0.5f) + (v2*3f) + (v3/10f);
		//me._newPosition += me._newVelocity;
	}

	void LateUpdate () {
		_velocity = (1-BoidSpawner.SPAWNER._lerp)*_velocity +
			BoidSpawner.SPAWNER._lerp*_newVelocity;

		if(_velocity.magnitude < 10f){
			_velocity = _velocity.normalized *12f;
		}else if(_velocity.magnitude < 20f){
			_velocity = _velocity.normalized *15f;
		}

		_newPosition = me.transform.position + _velocity * Time.deltaTime;
		_newPosition.y = 0;
		this.transform.LookAt(_newPosition);
		this.transform.position = _newPosition;
	}

	public Vector3 Rule1Cohesion( List<Boid> someBoids )
	{
		Vector3 sum = Vector3.zero;
		foreach (Boid b in someBoids) {
			sum += b.transform.position;
		}
		Vector3 center = sum / someBoids.Count;
		return (center - me.transform.position);
	}

	Vector3 rule2Separation( List<Boid> someBoids )
	{
		Vector3 sum = Vector3.zero;
		foreach (Boid b in someBoids) {
			float dist = (b.transform.position - me.transform.position).magnitude;
			if(dist < RADIUS){
				sum -= (b.transform.position - me.transform.position);
			}
		}
		return sum;
	}

	Vector3 rule3Aligment( List<Boid> someBoids )
	{
		Vector3 sum = Vector3.zero;
		foreach (Boid b in someBoids) {
			sum += b._velocity;
		}
		sum /= someBoids.Count;
		return (sum - me._velocity);
	}
}