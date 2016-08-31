using UnityEngine;
using System.Collections;

public abstract class Apple : MonoBehaviour {

	public static float _bottomY = -4.2f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (transform.position.y < _bottomY){
			this.destroyApple();
		}

		this.update();
	}

	public abstract void destroyApple();

	public abstract void catchApple();

	public abstract void update();
}
