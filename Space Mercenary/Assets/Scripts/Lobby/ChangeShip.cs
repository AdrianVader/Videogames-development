using UnityEngine;
using System.Collections;

public class ChangeShip : MonoBehaviour {

	public GameObject shipPoint;
	
	public bool ____________________________;

	public int currentIndex;

	public void nextButtonPressed(){

		changeShip (1);

	}

	public void previousButtonPressed(){
		
		changeShip (-1);
		
	}

	public void changeShip(int number){
		PlayerInfo playerInfo = GameObject.Find (Constants.PLAYER_INFO).GetComponent<PlayerInfo> ();
		
		if(playerInfo.ships.Count > 1){
			DestroyImmediate(this.shipPoint.transform.GetChild (0).gameObject);
			this.currentIndex += number;
			if(this.currentIndex < 0){
				this.currentIndex = playerInfo.ships.Count - 1;
			}else if(this.currentIndex >= playerInfo.ships.Count){
				this.currentIndex = 0;
			}
			foreach (GameObject ship in playerInfo.allPossibleShips) {
				if (playerInfo.ships [this.currentIndex].Equals (ship.GetComponent<Ship> ().shipName)) {
					GameObject currentShip = Instantiate(ship);
					currentShip.transform.parent = this.shipPoint.transform;
					currentShip.transform.localPosition = Vector3.zero;
					currentShip.transform.localRotation = Quaternion.Euler(Vector3.zero);
					playerInfo.selectedShip = ship;
				}
			}
		}
	}

	// Use this for initialization
	void Start () {

		PlayerInfo playerInfo = GameObject.Find (Constants.PLAYER_INFO).GetComponent<PlayerInfo> ();
		if (playerInfo.ships.Count > 0) {
			this.currentIndex = 0;
			bool found = false;
			foreach (GameObject ship in playerInfo.allPossibleShips) {
				if (playerInfo.ships [0].Equals (ship.GetComponent<Ship> ().shipName)) {
					GameObject currentShip = Instantiate (ship);
					currentShip.transform.parent = this.shipPoint.transform;
					currentShip.transform.localPosition = Vector3.zero;
					currentShip.transform.localRotation = Quaternion.Euler (Vector3.zero);
					playerInfo.selectedShip = ship;
					found = true;
				}
				if(!found){
					this.currentIndex++;
				}
			}
		} else {
			this.currentIndex = -1;
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
