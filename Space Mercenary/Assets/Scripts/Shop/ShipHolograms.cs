using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipHolograms : MonoBehaviour {

	public List<GameObject> ships;
	public GameObject firstStipPoint;
	public GameObject secondStipPoint;
	public GameObject thirdStipPoint;

	void Awake () {

		loadShips ();

		int numberOfShips = this.ships.Count;
		if (numberOfShips >= 1) {
			GameObject ship = Instantiate(this.ships[0]);
			ship.transform.parent = this.firstStipPoint.transform;
			ship.transform.localPosition = Vector3.zero;
		}
		if (numberOfShips >= 2) {
			GameObject ship = Instantiate(this.ships[1]);
			ship.transform.parent = this.secondStipPoint.transform;
			ship.transform.localPosition = Vector3.zero;
		}
		if (numberOfShips >= 3) {
			GameObject ship = Instantiate(this.ships[2]);
			ship.transform.parent = this.thirdStipPoint.transform;
			ship.transform.localPosition = Vector3.zero;
		}

	}

	// Use this for initialization
	void Start () {

		if (this.firstStipPoint != null) {
			this.firstStipPoint.transform.Rotate (new Vector3(90f,180f,0f));
			this.firstStipPoint.transform.localScale = new Vector3(0.2f,0.2f,0.2f);
		}
		if (this.secondStipPoint != null) {
			this.secondStipPoint.transform.Rotate (new Vector3(90f,180f,0f));
			this.secondStipPoint.transform.localScale = new Vector3(0.2f,0.2f,0.2f);
		}
		if (this.thirdStipPoint != null) {
			this.thirdStipPoint.transform.Rotate (new Vector3(90f,180f,0f));
			this.thirdStipPoint.transform.localScale = new Vector3(0.2f,0.2f,0.2f);
		}

	}
	
	// Update is called once per frame
	void Update () {
	
		if (this.firstStipPoint != null) {
			this.firstStipPoint.transform.Rotate (new Vector3(0f,0f,1f));
		}
		if (this.secondStipPoint != null) {
			this.secondStipPoint.transform.Rotate (new Vector3(0f,0f,1f));
		}
		if (this.thirdStipPoint != null) {
			this.thirdStipPoint.transform.Rotate (new Vector3(0f,0f,1f));
		}

	}

	private void loadShips (){
		Object[] prefabs = Resources.LoadAll(Constants.SHIP_RESOURCES_PATH);
		foreach(GameObject prefab in prefabs){
			this.ships.Add(prefab);
		}

	}
}
