using UnityEngine;
using System.Collections;

public class PowerUpPointsApple : Apple {

	protected int _MULTIPLY_FACTOR = 10;
	protected float _SWITCH_COLOR_TIME = 0.1f;
	protected float _time = 0.0f;

	public override void destroyApple(){

		Destroy(this.gameObject);
	}

	public override void catchApple(){

		Basket basket = GameObject.FindGameObjectWithTag("Basket").GetComponent<Basket>();

		// Cambiamos el tag para que no me borren cuando se cae una manzana.
		this.gameObject.tag = "Untagged";
		basket.POINTS_ADDED *= this._MULTIPLY_FACTOR;
		Invoke("finishPowerUp", 5.5f);

		int currentScore = int.Parse(basket.scoreCounter.text);
		currentScore += basket.POINTS_ADDED;
		basket.scoreCounter.text = currentScore.ToString();
		
		if(currentScore > HighScore.highScore){
			HighScore.highScore = currentScore;
			HighScore.text.text = HighScore.HIGH_SCORE_TEXT + HighScore.highScore;
			PlayerPrefs.SetInt("ApplePickerHighScore", HighScore.highScore);
		}
		
		Destroy(this.GetComponent<Rigidbody>());
		foreach(Transform child in this.gameObject.transform){
			Destroy(child.gameObject);
		}
	}

	protected void finishPowerUp(){

		Basket basket = GameObject.FindGameObjectWithTag("Basket").GetComponent<Basket>();
		basket.POINTS_ADDED /= this._MULTIPLY_FACTOR;
		GameObject.Find ("ScoreCounter").GetComponent<GUIText> ().material.color = new Color(1f, 1f, 1f);
		Destroy(this.gameObject);
	}

	public override void update(){

		this._time += Time.deltaTime;

		if(this.gameObject.tag == "Apple" && this._time > this._SWITCH_COLOR_TIME){
			this._time = 0.0f;
			this.gameObject.transform.Find("AppleBody").GetComponent<Renderer>().material.color = new Color (Random.value, Random.value, Random.value);
		}

		if(this._time > this._SWITCH_COLOR_TIME){
			this._time = 0.0f;
			GameObject.Find("ScoreCounter").GetComponent<GUIText>().material.color = new Color (Random.value, Random.value, Random.value);
		}
	}
}
