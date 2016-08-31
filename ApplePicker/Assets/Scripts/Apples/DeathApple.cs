using UnityEngine;
using System.Collections;

public class DeathApple : Apple {
	
	public override void destroyApple(){
		
		Destroy(this.gameObject);
	}
	
	public override void catchApple(){
		
		Application.LoadLevel("MainScene");
	}
	
	public override void update(){}
}
