using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SpaceDoor : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseDown (){

		PlayerInfo playerInfo = GameObject.Find (Constants.PLAYER_INFO).GetComponent<PlayerInfo> ();
		if (playerInfo.selectedShip != null) {
			SceneManager.LoadScene (Constants.LEVEL_MENU, LoadSceneMode.Single);
		}
		
	}
}
