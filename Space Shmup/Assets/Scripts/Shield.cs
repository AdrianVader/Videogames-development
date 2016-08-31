using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {

	public float _rotationsPerSecond = 0.1f;

	public bool _________________;

	public int _levelShown = 0;

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
	
		int currentLevel = Ship.SingletonShip.shield;

		if(this._levelShown != currentLevel){
			this._levelShown = currentLevel;
			Material material = this.GetComponent<Renderer>().material;
			material.mainTextureOffset = new Vector2(0.2f * this._levelShown, 0);
		}

		float rotationInZ = (this._rotationsPerSecond * Time.time * 360f) % 360f;
		this.transform.rotation = Quaternion.Euler(0, 0, rotationInZ);
	}
}
