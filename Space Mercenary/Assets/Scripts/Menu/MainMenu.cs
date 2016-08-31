using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public enum State{
	StartMenu,
	LoadGame,
	Options,
	Credits,
	Quit
}

public class MainMenu : MonoBehaviour {

	public Button newGame;
	public Button loadGame;
	public Button multiplayer;
	public Button options;
	public Button credits;
	public Button quit;
	public Text creditBoard;
	public State _currentState = State.StartMenu;

	public bool ____________________________;

	public GameObject _newGame;
	public GameObject _loadGame;
	public GameObject _multiplayer;
	public GameObject _options;
	public GameObject _credits;
	public GameObject _quit;
	public GameObject _creditBoard;

	void Awake(){

		this._newGame = this.newGame.transform.gameObject;
		this._loadGame = this.loadGame.transform.gameObject;
		this._multiplayer = this.multiplayer.transform.gameObject;
		this._options = this.options.transform.gameObject;
		this._credits = this.credits.transform.gameObject;
		this._quit = this.quit.transform.gameObject;
		this._creditBoard = this.creditBoard.transform.gameObject;
	}
	
	public void newGamePressed(){

		SceneManager.LoadScene (Constants.NEW_PLAYER, LoadSceneMode.Single);
	}
	
	public void loadGamePressed(){

		SceneManager.LoadScene (Constants.MANAGE_PLAYERS, LoadSceneMode.Single);
	}

	public void multiplayerPressed(){

		SceneManager.LoadScene (Constants.MULTIPLAYER, LoadSceneMode.Single);
	}
	
	public void optionsPressed(){
		
	}
	
	public void creditsPressed(){

		this._currentState = State.Credits;

		this._creditBoard.transform.gameObject.SetActive(true);

		this._newGame.transform.gameObject.SetActive(false);
		this._loadGame.transform.gameObject.SetActive(false);
		this._multiplayer.SetActive (false);
		this._options.transform.gameObject.SetActive(false);
		this._credits.transform.gameObject.SetActive(false);
		this._quit.transform.gameObject.SetActive(false);
	}
	
	public void quitPressed(){
		Application.Quit ();
	}

	// Use this for initialization
	void Start () {
	
		if (GameObject.Find (Constants.PLAYER_INFO) == null) {
			GameObject playerInfo = new GameObject(Constants.PLAYER_INFO);
			playerInfo.AddComponent<PlayerInfo> ();
			DontDestroyOnLoad(playerInfo);
		}

	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetKeyDown(KeyCode.Escape)){
			if(this._currentState == State.StartMenu){


			}else if(this._currentState == State.LoadGame){


			}else if(this._currentState == State.Credits){
				this._newGame.SetActive(true);
				this._loadGame.SetActive(true);
				this._multiplayer.SetActive (true);
				this._options.SetActive(true);
				this._credits.SetActive(true);
				this._quit.SetActive(true);
				
				this._creditBoard.SetActive(false);

			}else if(this._currentState == State.Options){


			}else if(this._currentState == State.Quit){


			}
		}
	}
}
