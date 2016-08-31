using UnityEngine;
using System.Collections;

public class RandomEnemyBehaviour : EnemyBehaviour {

	public float timeToChangeDirection;
	public float timeSinceLastChange;

	public float x;
	public float y;

	public void Start(){
		this.timeToChangeDirection = 2.0f;
		this.timeSinceLastChange = 0.0f;

		this.x = 0f;

		this.y = 0.85f;
	}

	public override void move(Enemy enemy){

		this.timeSinceLastChange += Time.deltaTime;
		if (this.timeSinceLastChange >= this.timeToChangeDirection) {
			this.timeSinceLastChange = 0.0f;
			this.x = Random.Range (-1, 1);
			this.y = Random.Range (-1, 1);
		}

		float x = this.x * enemy.ship.speed;
		float y = this.y * enemy.ship.speed * -1f;

		enemy.transform.position += new Vector3 (x * Time.deltaTime, y * Time.deltaTime, 0f);

		enemy.transform.localRotation = Quaternion.Euler(new Vector3((y/enemy.ship.speed) * enemy.ship.yRotation, (x/enemy.ship.speed) * enemy.ship.xRotation, 180f));
	}

}
