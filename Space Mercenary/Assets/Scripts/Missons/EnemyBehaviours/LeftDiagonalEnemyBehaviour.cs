﻿using UnityEngine;
using System.Collections;

public class LeftDiagonalEnemyBehaviour : EnemyBehaviour {

	public override void move(Enemy enemy){
		
		float x = -0.5f * enemy.ship.speed;

		float y = 0.75f * enemy.ship.speed * -1f;

		enemy.transform.position += new Vector3 (x * Time.deltaTime, y * Time.deltaTime, 0f);

		enemy.transform.localRotation = Quaternion.Euler(new Vector3((y/enemy.ship.speed) * enemy.ship.yRotation, (x/enemy.ship.speed) * enemy.ship.xRotation, 180f));
	}

}