using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ShopMenu : MonoBehaviour {

	public Button next;
	public Button previous;
	public Button buy;
	public Button exit;
	public Text money;

	public GameObject statsText;
	public GameObject stats;
	public GameObject info;
	public GameObject shipsHolograms;

	public bool ____________________________;

	public List<GameObject> ships;
	public Ship currentShowingShip;
	public int currentShowingShipIndex;
	
	public void nextButtonPressed (){

		if (this.currentShowingShipIndex >= (this.ships.Count -1)) {
			this.currentShowingShipIndex = 0;
		} else {
			this.currentShowingShipIndex++;
		}

		Transform point1 = this.shipsHolograms.transform.FindChild ("Ship point (1)");
		Transform point2 = this.shipsHolograms.transform.FindChild ("Ship point (2)");
		Transform point3 = this.shipsHolograms.transform.FindChild ("Ship point (3)");
		DestroyImmediate(point1.transform.GetChild (0).gameObject);
		point2.transform.GetChild (0).transform.parent = point1.transform;
		point1.transform.GetChild (0).transform.localPosition = Vector3.zero;
		point3.transform.GetChild (0).transform.parent = point2.transform;
		point2.transform.GetChild (0).transform.localPosition = Vector3.zero;
		int nextShipIndexToShow = (this.currentShowingShipIndex + 2) % (this.ships.Count);
		GameObject ship = Instantiate(this.ships[nextShipIndexToShow]);
		ship.transform.parent = point3.transform;
		ship.transform.localPosition = Vector3.zero;
		ship.transform.localRotation = Quaternion.Euler(new Vector3(1f, 1f, 1f));
		ship.transform.localScale = Vector3.one;

		this.currentShowingShip = point1.transform.GetChild (0).GetComponent<Ship> ();
		updateDataShown ();

	}
	
	public void previousButtonPressed (){

		if (this.currentShowingShipIndex <= 0) {
			this.currentShowingShipIndex = this.ships.Count-1;
		} else {
			this.currentShowingShipIndex--;
		}
		
		Transform point1 = this.shipsHolograms.transform.FindChild ("Ship point (1)");
		Transform point2 = this.shipsHolograms.transform.FindChild ("Ship point (2)");
		Transform point3 = this.shipsHolograms.transform.FindChild ("Ship point (3)");
		DestroyImmediate(point3.transform.GetChild (0).gameObject);
		point2.transform.GetChild (0).transform.parent = point3.transform;
		point3.transform.GetChild (0).transform.localPosition = Vector3.zero;
		point1.transform.GetChild (0).transform.parent = point2.transform;
		point2.transform.GetChild (0).transform.localPosition = Vector3.zero;
		GameObject ship = Instantiate(this.ships[this.currentShowingShipIndex]);
		ship.transform.parent = point1.transform;
		ship.transform.localPosition = Vector3.zero;
		ship.transform.localRotation = Quaternion.Euler(new Vector3(1f, 1f, 1f));
		ship.transform.localScale = Vector3.one;
		
		this.currentShowingShip = point1.transform.GetChild (0).GetComponent<Ship> ();
		updateDataShown ();

	}
	
	public void buyButtonPressed (){

		PlayerInfo player = GameObject.Find (Constants.PLAYER_INFO).GetComponent<PlayerInfo> ();
		string currentShipName = this.currentShowingShip.GetComponent<Ship> ().shipName;
		if(player.money >= this.currentShowingShip.price && !player.ships.Contains(currentShipName)){
			player.money -= this.currentShowingShip.price;
			player.ships.Add(currentShipName);
			player.savePlayer();
		}
		updateDataShown ();

	}
	
	public void exitButtonPressed (){

		SceneManager.LoadScene (Constants.LOBBY, LoadSceneMode.Single);

	}

	// Use this for initialization
	void Start () {
	
		//this.info.GetComponent<TextMesh> ().text = Constants.SHOP_INITIAL_MESSAGE;
		//this.statsText.SetActive (false);
		//this.stats.SetActive (false);
		
		this.ships = this.shipsHolograms.GetComponent<ShipHolograms> ().ships;
		this.currentShowingShip = this.ships [0].GetComponent<Ship> ();
		this.currentShowingShipIndex = 0;
		updateDataShown ();

	}

	public void updateDataShown(){
		
		this.info.GetComponent<TextMesh> ().text = 
			Constants.SHIP_TEXT + this.currentShowingShip.shipName + "\n" + 
			Constants.PRICE_TEXT + this.currentShowingShip.price + Constants.DOLLAR_SYMBOL;
		
		this.stats.GetComponent<TextMesh> ().text = 
			this.currentShowingShip.maxHealth + "\n" + 
			this.currentShowingShip.maxShield + "\n" + 
			this.currentShowingShip.speed + "\n" + 
			this.currentShowingShip.weaponLvlAllowed;

		money.text = Constants.MONEY_TEXT + GameObject.Find (Constants.PLAYER_INFO).GetComponent<PlayerInfo> ().money + Constants.DOLLAR_SYMBOL;

	}
	
	// Update is called once per frame
	void Update () {}
}
