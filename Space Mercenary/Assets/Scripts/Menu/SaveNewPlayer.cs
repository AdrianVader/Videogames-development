using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SaveNewPlayer : MonoBehaviour {
	
	public InputField playerName;
	public Button submit;
	public Button cancel;

	public GameObject managePlayersMenu;

	public bool ____________________________;

	public string associatedButtonName;

	void Awake () {

		if (managePlayersMenu == null) {
			if (!PlayerPrefs.HasKey(Constants.PLAYER_1 + Constants.PLAYER_NAME)) {
				associatedButtonName = Constants.PLAYER_1;
			} else if (!PlayerPrefs.HasKey(Constants.PLAYER_2 + Constants.PLAYER_NAME)) {
				associatedButtonName = Constants.PLAYER_2;
			} else if (!PlayerPrefs.HasKey(Constants.PLAYER_3 + Constants.PLAYER_NAME)) {
				associatedButtonName = Constants.PLAYER_3;
			} else if (!PlayerPrefs.HasKey(Constants.PLAYER_4 + Constants.PLAYER_NAME)) {
				associatedButtonName = Constants.PLAYER_4;
			} else {
				SceneManager.LoadScene (Constants.MANAGE_PLAYERS, LoadSceneMode.Single);
			}
		}

	}

	public void submitPressed(){

		if (this.playerName.text != Constants.EMPTY_STRING) {
			savePlayer (this.playerName.text);
			SceneManager.LoadScene (Constants.LOBBY, LoadSceneMode.Single);
		}

	}
	
	public void cancelPressed(){

		if (this.managePlayersMenu != null) {
			// Hide the menu that creates a Player.
			this.gameObject.SetActive (false);

			// Show the manage menu.
			this.managePlayersMenu.SetActive (true);
		} else {
			SceneManager.LoadScene (Constants.MAIN_MENU, LoadSceneMode.Single);
		}

	}

	public void savePlayer (string playerName){

		GameObject.Find (Constants.PLAYER_INFO).GetComponent<PlayerInfo> ().saveNewPlayer (this.associatedButtonName, playerName);

	}

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {}
}
