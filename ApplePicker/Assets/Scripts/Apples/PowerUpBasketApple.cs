using UnityEngine;
using System.Collections;

public class PowerUpBasketApple : Apple {

	public override void destroyApple(){
		
		Destroy(this.gameObject);
	}

	public override void catchApple(){

		Transform basketTransform = GameObject.Find("Basket").transform;
		foreach (Transform child in basketTransform) {
			if(child.localScale.x < 2.5f){
				child.localScale += new Vector3(0.5f, 0, 0);
			}
		}
		
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
