using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LevelMenu : MonoBehaviour {
	
	public Button level1;
	public Button level2;
	public Button level3;
	public Button level4;
	public Button level5;
	public Button level6;
	public Button next;
	public Button previous;
	public Button back;
	
	public bool ____________________________;

	public LevelLoader levelLoader;
	public int numberOfFields = 6;
	public int currentMaxLevelShown;

	// Use this for initialization
	void Start () {
		this.levelLoader = this.gameObject.GetComponent<LevelLoader>();

		this.currentMaxLevelShown = 0;

		loadShowingLevels (currentMaxLevelShown);

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void loadShowingLevels (int first){
		bool previous = true;
		bool next = true;
		int last = 0;
		levelLoader.NextLevels (first, this.numberOfFields, ref previous, ref next, ref first, ref last);

		this.next.interactable = next;
		this.previous.interactable = previous;


		int currentLevel = first;
		this.level1.gameObject.SetActive (true);
		this.level1.transform.FindChild (Constants.NUMBER).GetComponent<Text> ().text = currentLevel.ToString();
		this.currentMaxLevelShown = currentLevel;

		currentLevel++;
		if (currentLevel <= last) {
			this.level2.gameObject.SetActive (true);
			this.level2.transform.FindChild (Constants.NUMBER).GetComponent<Text> ().text = currentLevel.ToString();
			this.currentMaxLevelShown = currentLevel;
		} else {
			this.level2.gameObject.SetActive (false);
		}
		
		currentLevel++;
		if (currentLevel <= last) {
			this.level3.gameObject.SetActive (true);
			this.level3.transform.FindChild (Constants.NUMBER).GetComponent<Text> ().text = currentLevel.ToString();
			this.currentMaxLevelShown = currentLevel;
		} else {
			this.level3.gameObject.SetActive (false);
		}
		
		currentLevel++;
		if (currentLevel <= last) {
			this.level4.gameObject.SetActive (true);
			this.level4.transform.FindChild (Constants.NUMBER).GetComponent<Text> ().text = currentLevel.ToString();
			this.currentMaxLevelShown = currentLevel;
		} else {
			this.level4.gameObject.SetActive (false);
		}
		
		currentLevel++;
		if (currentLevel <= last) {
			this.level5.gameObject.SetActive (true);
			this.level5.transform.FindChild (Constants.NUMBER).GetComponent<Text> ().text = currentLevel.ToString();
			this.currentMaxLevelShown = currentLevel;
		} else {
			this.level5.gameObject.SetActive (false);
		}
		
		currentLevel++;
		if (currentLevel <= last) {
			this.level6.gameObject.SetActive (true);
			this.level6.transform.FindChild (Constants.NUMBER).GetComponent<Text> ().text = currentLevel.ToString();
			this.currentMaxLevelShown = currentLevel;
		} else {
			this.level6.gameObject.SetActive (false);
		}
	}

	public void levelButtonPressed (Button button){
		this.levelLoader.loadLevel (int.Parse(button.transform.FindChild (Constants.NUMBER).GetComponent<Text> ().text));
	}

	public void nextButtonPressed (){
		loadShowingLevels (this.currentMaxLevelShown);
	}

	public void previousButtonPressed (){
		int lastActiveLevel = int.Parse(this.level1.transform.FindChild (Constants.NUMBER).GetComponent<Text> ().text);
		lastActiveLevel -= 7;
		loadShowingLevels (lastActiveLevel);
	}

	public void backButtonPressed (){
		SceneManager.LoadScene (Constants.LOBBY, LoadSceneMode.Single);
	}

}
