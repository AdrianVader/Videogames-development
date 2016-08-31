using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ApplePicker : MonoBehaviour {

	public GameObject _basketPrefab;

	public int _numBaskets = 3;
	protected List<GameObject> _baskets;

	public float _basketBottomY = 0f;
	public float _basketSpacingY = 0.25f;

	// Use this for initialization
	void Start () {

		GameObject myBaskets = GameObject.Find ("Basket");

		this._baskets = new List<GameObject> ();
		for(int i = 0; i < _numBaskets; i++){
			GameObject basket = Instantiate(_basketPrefab) as GameObject;
			this._baskets.Add(basket);
			basket.transform.parent = myBaskets.gameObject.transform;

			Vector3 position = myBaskets.gameObject.transform.position;
			position.y += _basketSpacingY * i;
			basket.transform.position = position;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AppleLost(){

		GameObject[] allApples = GameObject.FindGameObjectsWithTag("Apple");
		foreach(GameObject apple in allApples){
			Destroy(apple);
		}
		
		this._numBaskets -= 1;
		if (this._numBaskets > 0) {
			Destroy (this._baskets [this._numBaskets]);
			this._baskets.RemoveAt (this._numBaskets);
		} else {
			Application.LoadLevel("MainScene");
		}

		GameObject.Find("AppleTree").GetComponent<AppleTree>()._applesCached = 0;
	}

	public void LifeUp(){
		if(this._numBaskets < 3){

			GameObject myBaskets = GameObject.Find("Basket").gameObject;

			GameObject basket = Instantiate(_basketPrefab) as GameObject;
			this._baskets.Add(basket);

			Vector3 position = myBaskets.transform.position;
			position.y += _basketSpacingY * (this._numBaskets);
			basket.transform.position = position;

			basket.transform.localScale = this._baskets[0].transform.localScale;
			
			basket.transform.parent = myBaskets.transform;
			
			this._numBaskets += 1;
		}
	}
}
