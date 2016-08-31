using UnityEngine;
using System.Collections;

public class CloudBehaviour : MonoBehaviour {

	public float _speed = 1f;
	public float _leftAndRightEdge = 15f;

	// Use this for initialization
	void Start(){}
	
	// Update is called once per frame
	void Update(){

		this.move();
	}

	protected void move(){
		
		if(transform.position.x >= _leftAndRightEdge){
			_speed = -Mathf.Abs(_speed);
		}else if(transform.position.x <= -_leftAndRightEdge){
			_speed = Mathf.Abs(_speed);
		}
		
		Vector3 position = transform.position;
		position.x += _speed * Time.deltaTime;
		transform.position = position;
	}
}
