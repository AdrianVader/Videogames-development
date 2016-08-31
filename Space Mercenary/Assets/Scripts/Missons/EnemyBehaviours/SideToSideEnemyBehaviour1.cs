using UnityEngine;
using System.Collections;

public class SideToSideEnemyBehaviour : EnemyBehaviour {
	
	public float timeToChangeDirection;
	public float timeSinceLastChange;

	public float x;
	public float xIncrement;
	public float xMax;

	public void Start(){
		this.timeToChangeDirection = 1f;
		this.timeSinceLastChange = 0.0f;

		this.x = 0f;
		this.xIncrement = 0.05f;
		this.xMax = 0.8f;
	}

	public override void move(Enemy enemy){

		this.timeSinceLastChange += Time.deltaTime;
		if (this.timeSinceLastChange >= this.timeToChangeDirection) {
			this.xIncrement *= -1f;
			this.timeSinceLastChange = 0.0f;
		}

		if ((this.xIncrement > 0 && this.x < this.xMax) || (this.xIncrement < 0 && this.x > -this.xMax)) {
			this.x += this.xIncrement;
		}

		float x = this.x * enemy.ship.speed;
		float y = 0.7f * enemy.ship.speed * -1f;

		enemy.transform.position += new Vector3 (x * Time.deltaTime, y * Time.deltaTime, 0f);

		enemy.transform.localRotation = Quaternion.Euler(new Vector3((y/enemy.ship.speed) * enemy.ship.yRotation, (x/enemy.ship.speed) * enemy.ship.xRotation, 180f));
	}

}
