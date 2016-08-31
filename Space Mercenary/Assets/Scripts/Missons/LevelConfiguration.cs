using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class LevelConfiguration : MonoBehaviour {
	
	public bool ____________________________;

	public List<float> times;
	public List<string> types;
	public List<Vector3> positions;
	public List<string> behaviours;
	public float time;

	public GameObject enemyParent;
	public GameObject playerStartPoint;

	public GameObject health;
	public GameObject shield;

	public bool exit = false;

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {

		if (this.playerStartPoint == null) {
			if(GameObject.Find (Constants.PLAYER_START_POINT) != null){
				this.time = 0f;
				this.enemyParent = new GameObject (Constants.ENEMY_PARENT);
				
				GameObject playerShip = Instantiate(GameObject.Find (Constants.PLAYER_INFO).GetComponent<PlayerInfo> ().selectedShip);
				playerShip.AddComponent<Player> ();
				this.playerStartPoint = GameObject.Find (Constants.PLAYER_START_POINT);
				Vector3 startPoint = this.playerStartPoint.transform.position;
				playerShip.transform.position = startPoint;

				this.health = GameObject.Find (Constants.HEALTH);
				this.shield = GameObject.Find (Constants.SHIELD);
			}
		} else {
			
			this.time += Time.deltaTime;

			generateEnemies ();

			checkEndGame ();

			updateInfoPlayer ();

		}

	}

	public void generateEnemies(){
		if (this.times.Count > 0 && this.times[0] < this.time) {
			createEnemy(this.types[0], this.positions[0], this.behaviours[0]);
			this.times.RemoveAt(0);
			this.types.RemoveAt(0);
			this.positions.RemoveAt(0);
			this.behaviours.RemoveAt (0);
		}
	}

	public void createEnemy(string type, Vector3 position, string behaviour){
		GameObject enemyPrefab = (GameObject)Resources.Load (Constants.SHIP_RESOURCES_PATH + Constants.SLASH + type);
		GameObject enemy = Instantiate (enemyPrefab);
		addBehaviour(enemy, behaviour);
		enemy.transform.parent = enemyParent.transform;
		enemy.AddComponent<Enemy>();
		
		enemy.transform.position = position;
		enemy.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 180f));
	}

	public void addBehaviour(GameObject enemy, string behaviour){
		if (behaviour.Equals (Constants.BASIC_BEHAVIOUR)) {
			enemy.AddComponent<BasicEnemyBehaviour> ();
		} else if (behaviour.Equals (Constants.RIGHT_DIAGONAL_BEHAVIOUR)) {
			enemy.AddComponent<RightDiagonalEnemyBehaviour> ();
		} else if (behaviour.Equals (Constants.LEFT_DIAGONAL_BEHAVIOUR)) {
			enemy.AddComponent<LeftDiagonalEnemyBehaviour> ();
		} else if (behaviour.Equals (Constants.RANDOM_BEHAVIOUR)) {
			enemy.AddComponent<RandomEnemyBehaviour> ();
		} else if (behaviour.Equals (Constants.SIDE_TO_SIDE_BEHAVIOUR)) {
			enemy.AddComponent<SideToSideEnemyBehaviour> ();
		} else {
			enemy.AddComponent<BasicEnemyBehaviour> ();
		}
	}
	
	public void checkEndGame(){
		if (!this.exit) {
			GameObject[] enemies = GameObject.FindGameObjectsWithTag (Constants.ENEMY_TAG);
			if (this.times.Count == 0 && enemies.Length == 0) {
				PlayerInfo playerInfo = GameObject.Find (Constants.PLAYER_INFO).GetComponent<PlayerInfo> ();
				playerInfo.money += (int)float.Parse (GameObject.Find (Constants.PLAYER_MONEY).GetComponent<TextMesh> ().text);
				playerInfo.savePlayer ();

				this.exit = true;
			} else if (GameObject.FindGameObjectWithTag (Constants.PLAYER_TAG) == null) {
				this.exit = true;
			}
		} else {
			Invoke ("goBackToLobby", 2.0f);
		}

	}

	public void updateInfoPlayer (){
		GameObject player = GameObject.FindGameObjectWithTag (Constants.PLAYER_TAG);
		if (player != null) {
			this.health.GetComponent<TextMesh> ().text = Constants.HEALTH_SYMBOL + player.GetComponent<Ship> ().currentHealth.ToString ();
			this.shield.GetComponent<TextMesh> ().text = Constants.SHIELD_SYMBOL + player.GetComponent<Ship> ().currentShield.ToString ();
		} else {
			this.health.GetComponent<TextMesh> ().text = Constants.HEALTH_SYMBOL + 0.ToString ();
			this.shield.GetComponent<TextMesh> ().text = Constants.SHIELD_SYMBOL + 0.ToString ();
		}
	}

	public void goBackToLobby(){
		Destroy (this.gameObject);
		SceneManager.LoadScene (Constants.LOBBY, LoadSceneMode.Single);
	}

}
