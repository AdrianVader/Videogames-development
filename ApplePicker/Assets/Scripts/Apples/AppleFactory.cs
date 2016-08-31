using UnityEngine;
using System.Collections;

public class AppleFactory : MonoBehaviour {
	
	public GameObject _normalApple;
	public GameObject _powerUpPointsApple;
	public GameObject _powerUpBasketApple;
	public GameObject _powerDownBasketApple;
	public GameObject _heartApple;
	public GameObject _deathApple;

	public float _normalProbability = 0.75f;
	public float _upPointsProbability = 0.8f;
	public float _upBasketProbability = 0.85f;
	public float _downBasketProbability = 0.90f;
	public float _heartProbability = 0.95f;
	public float _deathProbability = 1f;

	// Use this for initialization
	void Start(){}
	
	// Update is called once per frame
	void Update(){}

	public GameObject generateApple(){

		float probability = Random.value;

		if(probability <= _normalProbability){

			return this.NormalApple ();
		}else if(probability > _normalProbability && probability <= _upPointsProbability){

			return this.PowerUpPointsApple();
		}else if(probability > _upPointsProbability && probability <= _upBasketProbability){
			
			return this.PowerUpBasketApple();
		}else if(probability > _upBasketProbability && probability <= _downBasketProbability){
			
			return this.PowerDownBasketApple();
		}else if(probability > _downBasketProbability && probability <= _heartProbability){
			
			return this.HeartApple();
		}else{ // probability > 0.95f
			
			return this.DeathApple();	
		}
	}

	protected GameObject NormalApple(){

		return this._normalApple;
	}

	protected GameObject PowerUpPointsApple(){
		
		return this._powerUpPointsApple;
	}
	
	protected GameObject PowerUpBasketApple(){
		
		return this._powerUpBasketApple;
	}
	
	protected GameObject PowerDownBasketApple(){
		
		return this._powerDownBasketApple;
	}

	protected GameObject HeartApple(){
		
		return this._heartApple;
	}

	protected GameObject DeathApple(){
		
		return this._deathApple;
	}
}
