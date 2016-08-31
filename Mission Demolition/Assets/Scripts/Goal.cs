using UnityEngine;
using System.Collections;
public class Goal : MonoBehaviour {

	static public bool goalMet = false;

	void Start(){

		Color c = GetComponent<Renderer>().material.color;
		c.a = 0.7f;
		GetComponent<Renderer>().material.color = c;
	}

	void OnTriggerEnter( Collider coll ) {

		GameObject collision = coll.gameObject;
		while (collision.transform.parent != null) {
			collision = collision.transform.parent.gameObject;
		}
		

		if ( collision.tag == "Projectile" ) {

			Goal.goalMet = true;

			Color c = GetComponent<Renderer>().material.color;
			c.a = 1f;
			GetComponent<Renderer>().material.color = c;

			Invoke("goalAchieved", 3);
		}
	}

	void goalAchieved(){
		MissionDemolition.missionDemolition.nextLevel();
	}
}
