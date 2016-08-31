using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class ManagePlayers : MonoBehaviour {

	public Button slot1;
	public Button slot2;
	public Button slot3;
	public Button slot4;
	public Toggle delete;

	public GameObject newPlayerMenu;
	
	public bool ____________________________;

	void Awake () {

		loadAllSlots ();

	}

	public void loadAllSlots (){
		loadSlot(Constants.PLAYER_1 + Constants.PLAYER_NAME, this.slot1);
		loadSlot(Constants.PLAYER_2 + Constants.PLAYER_NAME, this.slot2);
		loadSlot(Constants.PLAYER_3 + Constants.PLAYER_NAME, this.slot3);
		loadSlot(Constants.PLAYER_4 + Constants.PLAYER_NAME, this.slot4);
	}

	public void loadSlot (string name, Button button){
		if(PlayerPrefs.HasKey(name)){
			button.transform.GetComponentInChildren<Text>().text = PlayerPrefs.GetString(name);
		}
	}

	public void slotPressed (Button button){
	
		string playerName = button.name;

		if (this.delete.isOn) {

			deletePlayer (playerName);
			button.transform.GetComponentInChildren<Text>().text = Constants.EMPTY_SLOT;

		} else {

			if (button.transform.FindChild(Constants.TEXT).GetComponent<Text> ().text.Equals(Constants.EMPTY_SLOT)) {
				goToNewPlayerMenu (playerName);
			} else {
				loadGame (playerName);
			}

		}

	}

	public void deletePlayer (string name){

		GameObject.Find (Constants.PLAYER_INFO).GetComponent<PlayerInfo> ().deletePlayer (name);

	}
	
	public void goToNewPlayerMenu (string buttonName){

		// Hide the manage menu.
		this.gameObject.SetActive (false);
		
		// Show the menu that creates a Player.
		this.newPlayerMenu.SetActive (true);
		this.newPlayerMenu.GetComponent<SaveNewPlayer> ().associatedButtonName = buttonName;
		
	}
	
	public void loadGame (string name){

		GameObject.Find (Constants.PLAYER_INFO).GetComponent<PlayerInfo> ().loadPlayer (name);

		SceneManager.LoadScene (Constants.LOBBY, LoadSceneMode.Single);
		
	}

	public void returnToMainMenu (){
		SceneManager.LoadScene (Constants.MAIN_MENU, LoadSceneMode.Single);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
