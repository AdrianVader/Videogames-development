using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInfo : MonoBehaviour {

	public string playerNumber;
	public string playerName;
	public int money;
	public List<string> ships;

	public Object[] allPossibleShips;
	
	public bool ____________________________;

	public GameObject selectedShip;

	public void loadPlayer (string playerNumber){

		this.playerNumber = playerNumber;

		this.playerName = PlayerPrefs.GetString(this.playerNumber + Constants.PLAYER_NAME);
		this.money = PlayerPrefs.GetInt(this.playerNumber + Constants.PLAYER_MONEY);

		this.ships = new List<string> ();
		foreach(GameObject ship in this.allPossibleShips){
			string shipName = ship.GetComponent<Ship>().shipName;
			if(PlayerPrefs.HasKey(this.playerNumber + shipName)){
				this.ships.Add(shipName);
			}
		}

	}

	public void saveNewPlayer (string playerNumber, string playerName){

		this.playerNumber = playerNumber;
		this.playerName = playerName;
		PlayerPrefs.SetString (this.playerNumber + Constants.PLAYER_NAME, this.playerName);
		this.money = Constants.INITIAL_MONEY;
		PlayerPrefs.SetInt (this.playerNumber + Constants.PLAYER_MONEY, this.money);

		this.ships = new List<string> ();
		foreach(GameObject ship in this.allPossibleShips){
			string shipName = ship.GetComponent<Ship>().shipName;
			PlayerPrefs.DeleteKey(this.playerNumber + shipName);
		}

	}

	public void savePlayer (){

		PlayerPrefs.SetString(this.playerNumber + Constants.PLAYER_NAME, this.playerName);
		PlayerPrefs.SetInt(this.playerNumber + Constants.PLAYER_MONEY, this.money);

		foreach (string shipName in this.ships) {
			PlayerPrefs.SetString(this.playerNumber + shipName, shipName);
		}

	}

	public void deletePlayer(string name){

		PlayerPrefs.DeleteKey (name + Constants.PLAYER_NAME);

	}

	// Use this for initialization
	void Start () {
		//PlayerPrefs.DeleteAll ();
		this.playerNumber = null;
		this.allPossibleShips = Resources.LoadAll (Constants.SHIP_RESOURCES_PATH);
		this.ships = new List<string> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
