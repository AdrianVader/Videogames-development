using UnityEngine;
using System.Collections;

public class Basket : MonoBehaviour {

	protected GUIText _scoreCounter;
	protected int _POINTS_ADDED = 100;

	// Use this for initialization
	void Start () {

		this._scoreCounter = GameObject.Find("ScoreCounter").GetComponent<GUIText>();
	}
	
	// Update is called once per frame
	void Update () {
	
		Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3 basket = this.transform.position;
		this.transform.position = new Vector3 (mousePos3D.x, basket.y, basket.z);
	}

	void OnCollisionEnter(Collision collision){

		GameObject collidedWith = collision.gameObject;
		while(collidedWith.tag != "Apple" && collidedWith.transform.parent != null){
			collidedWith = collidedWith.transform.parent.gameObject;
		}

		if(collidedWith.tag == "Apple"){
			GameObject.Find("AppleTree").GetComponent<AppleTree>()._applesCached += 1;
			collidedWith.GetComponent<Apple>().catchApple();
		}
	}

	public GUIText scoreCounter{
		get{return this._scoreCounter;}
		set{this._scoreCounter = value;}
	}

	public int POINTS_ADDED{
		get{return this._POINTS_ADDED;}
		set{this._POINTS_ADDED = value;}
	}
}
