using UnityEngine;
using System.Collections;

public class HeartApple : Apple {

	public override void destroyApple(){
		
		Destroy(this.gameObject);
	}

	public override void catchApple(){

		Camera.main.GetComponent<ApplePicker>().LifeUp();
		
		Basket basket = GameObject.FindGameObjectWithTag("Basket").GetComponent<Basket>();
		
		int currentScore = int.Parse(basket.scoreCounter.text);
		currentScore += basket.POINTS_ADDED;
		basket.scoreCounter.text = currentScore.ToString();
		
		if(currentScore > HighScore.highScore){
			HighScore.highScore = currentScore;
			HighScore.text.text = HighScore.HIGH_SCORE_TEXT + HighScore.highScore;
			PlayerPrefs.SetInt("ApplePickerHighScore", HighScore.highScore);
		}
		
		Destroy(this.gameObject);
	}

	public override void update(){}
}