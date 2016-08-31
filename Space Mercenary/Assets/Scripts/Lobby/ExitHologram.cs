using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ExitHologram : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		this.transform.Rotate (new Vector3(0f,1f,0f));

	}

	void OnMouseDown (){

		SceneManager.LoadScene (Constants.MAIN_MENU, LoadSceneMode.Single);

	}
}
