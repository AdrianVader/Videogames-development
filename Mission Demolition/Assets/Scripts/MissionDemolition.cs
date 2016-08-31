using UnityEngine;
using System.Collections;



public enum GameMode {
	idle,
	playing,
	levelEnd
}



public class MissionDemolition : MonoBehaviour {

	static public MissionDemolition missionDemolition;
	

	public GameObject[] _castles;

	public bool _____________________________;

	public int _level;
	public int _maxLevel;
	public GameObject _currentCastle;
	public GameMode _mode = GameMode.idle;
	public string _showing = "Slingshot";

	// Use this for initialization
	void Start () {
	
		missionDemolition = this;

		char[] sceneName = Application.loadedLevelName.ToCharArray();
		this._level = int.Parse(sceneName[sceneName.Length-1].ToString());
		this._maxLevel = 3;
		startLevel();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void startLevel(){

		GameObject.Find("GT_Level").GetComponent<GUIText>().text = "Level " + this._level.ToString();
		GameObject.Find("GT_Throws").GetComponent<GUIText>().text = "0";

		SwitchView("Both");
		this._mode = GameMode.playing;
		this._currentCastle = GameObject.FindWithTag("Castle");
	}

	public void nextLevel(){
	
		if (this._maxLevel >= this._level + 1) {
			Application.LoadLevel ("Scene " + (this._level + 1).ToString ());
		} else {
			Application.LoadLevel ("Scene 1");
		}
	}

	void OnGUI() {
		// Draw the GUI button for view switching at the top of the screen
		Rect buttonRect = new Rect( (Screen.width/2)-50, 10, 100, 24 );
		switch(this._showing) {
		case "Slingshot":
			if ( GUI.Button( buttonRect, "Show Castle" ) ) {
				SwitchView("Castle");
			}
			break;
		case "Castle":
			if ( GUI.Button( buttonRect, "Show Both" ) ) {
				SwitchView("Both");
			}
			break;
		case "Both":
			if ( GUI.Button( buttonRect, "Show Slingshot" ) ) {
				SwitchView( "Slingshot" );
			}
			break;
		}
	}

	static public void SwitchView( string eView ) {
		missionDemolition._showing = eView;
		switch (missionDemolition._showing) {
		case "Slingshot":
			FollowCam.followCamera._pointOfInterest = GameObject.FindWithTag("Slingshot");
			break;
		case "Castle":
			FollowCam.followCamera._pointOfInterest = missionDemolition._currentCastle;
			break;
		case "Both":
			FollowCam.followCamera._pointOfInterest = GameObject.Find ("ViewBoth");
			break;
		}
	}
}
