using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Shop : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseDown (){
		
		SceneManager.LoadScene (Constants.SHOP, LoadSceneMode.Single);
		
	}
}
