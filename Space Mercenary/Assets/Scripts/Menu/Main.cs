using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Main : MonoBehaviour {

	static public Main Singleton;

	public bool __________________________;

	public List<GameObject> gameObjectsToDestroy;

	void Awake(){

		Singleton = this;

		this.gameObjectsToDestroy = new List<GameObject>();

		Screen.SetResolution (1080, 1920, true);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
