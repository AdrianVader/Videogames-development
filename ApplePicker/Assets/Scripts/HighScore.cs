using UnityEngine;
using System.Collections;

public class HighScore : MonoBehaviour {

	static public int highScore = 1000;
	static public GUIText text;
	static public string HIGH_SCORE_TEXT = "HighScore: ";

	// Use this for initialization
	void Start () {
		//PlayerPrefs.SetInt("ApplePickerHighScore", 1500); // Para Debug
		if(PlayerPrefs.HasKey("ApplePickerHighScore")){
			highScore = PlayerPrefs.GetInt("ApplePickerHighScore");
		}
	
		text = this.GetComponent<GUIText>();
		text.text = HIGH_SCORE_TEXT + highScore;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
