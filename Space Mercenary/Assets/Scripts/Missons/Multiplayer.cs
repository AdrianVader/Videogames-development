using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Multiplayer : MonoBehaviour {
	
	public GameObject prefabPlayer1;
	public GameObject prefabPlayer2;
	
	public bool ______________;

	public GameObject player1;
	public GameObject player2;

	public GameObject playerPoint1;
	public GameObject playerPoint2;

	// Use this for initialization
	void Start () {
	
		this.playerPoint1 = GameObject.Find ("Player1 start point");
		this.playerPoint2 = GameObject.Find ("Player2 start point");

		this.player1 = Instantiate (this.prefabPlayer1) as GameObject;
		this.player1.transform.position = this.playerPoint1.transform.position;
		this.player1.AddComponent<Player> ();

		this.player2 = Instantiate (this.prefabPlayer2) as GameObject;
		this.player2.transform.position = this.playerPoint2.transform.position;
		this.player2.transform.rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, 180.0f));
		this.player2.AddComponent<Player2> ();

	}
	
	// Update is called once per frame
	void Update () {
		checkEndGame ();
	}

	public void checkEndGame () {
		if (!this.player1.activeSelf || !this.player2.activeSelf) {
			Invoke ("goBackToMainMenu", 2f);
		}
	}

	public void goBackToMainMenu () {
		SceneManager.LoadScene (Constants.MAIN_MENU);
	}

}
