using UnityEngine;
using System.Collections;

public class AppleTree : MonoBehaviour {
	
	public float _speed = 1f;
	public float _leftAndRightEdge = 10f;
	public float _chanceToChangeDirections = 0.005f;
	public float _secondsToFirstAppleDrop = 2f;
	public float _secondsBetweenAppleDrops = 1f;
	public float _timeLastAppleDrop = 0f;

	public int _applesCached = 0;

	// Use this for initialization
	void Start () {
	
		Invoke("dropApple", 2f);
	}
	
	// Update is called once per frame
	void Update () {
	
		this.move();
	}
	
	void FixedUpdate(){
		
		tryToChangeDirection();
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

	protected void tryToChangeDirection(){

		if (Random.value <= _chanceToChangeDirections) {
			_speed *= -1;
		}
	}

	void dropApple() {

		if(this._applesCached > 0){
			GameObject newApple = GameObject.Find ("AppleFactory").GetComponent<AppleFactory> ().generateApple ();

			GameObject apple = Instantiate (newApple) as GameObject;
			apple.transform.position = new Vector3 (transform.position.x, transform.position.y + 1, 0f);
		}else{
			this._applesCached += 1;
			this._speed = 2f;
			this._secondsBetweenAppleDrops = 1f;
			this._chanceToChangeDirections = 0.005f;
		}

		if (this._applesCached == 5) {
			
			this._speed *= 1.35f;
			this._secondsBetweenAppleDrops -= 0.15f;
		}else if (this._applesCached == 10) {
			
			this._speed *= 1.35f;
			//this._secondsBetweenAppleDrops -= 0.15f;
		}else if (this._applesCached == 15) {
			
			this._speed *= 1.35f;
			this._secondsBetweenAppleDrops -= 0.15f;
			this._chanceToChangeDirections *= 1.25f;
		}else if (this._applesCached == 20) {
			
			this._speed *= 1.35f;
			//this._secondsBetweenAppleDrops -= 0.15f;
		}else if (this._applesCached == 25) {
			
			this._speed *= 1.35f;
			this._secondsBetweenAppleDrops -= 0.15f;
		}

		Invoke("dropApple", this._secondsBetweenAppleDrops);
	}
}
