using UnityEngine;
using System.Collections;

public class LightBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		this.gameObject.transform.Rotate(new Vector3(0f, 0.2f, 0f));
	}
}
