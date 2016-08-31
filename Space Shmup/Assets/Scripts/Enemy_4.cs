using UnityEngine;
using System.Collections;

[System.Serializable]
public class Part {
	public string name;
	public float health;
	public string[] protectedBy;
	public GameObject go;
	public Material mat;
}

public class Enemy_4 : Enemy {

	public Vector3[] _points;
	public float _timeStart;
	public float _duration = 4;
	public Part[] _parts;

	// Use this for initialization
	void Start () {
	
		this._points = new Vector3[2];
		this._points[0] = this.transform.position;
		this._points[1] = this.transform.position;

		initMovement();

		Transform t;
		foreach(Part prt in this._parts) {
			t = transform.Find(prt.name);
			if (t != null) {
				prt.go = t.gameObject;
				prt.mat = prt.go.GetComponent<Renderer>().material;
			}
		}
	}
	
	void initMovement() {

		Vector3 p1 = Vector3.zero;
		float esp = Main.SingletonMain._enemySpawnPadding;
		Bounds cBounds = Utils.camBounds;
		p1.x = Random.Range(cBounds.min.x + esp, cBounds.max.x - esp);
		p1.y = Random.Range(cBounds.min.y + esp, cBounds.max.y - esp);
		this._points[0] = this._points[1];
		this._points[1] = p1;

		this._timeStart = Time.time;
	}

	public override void move () {

		float u = (Time.time - this._timeStart) / this._duration;
		if (u>=1) {
			initMovement();
			u=0;
		}
		u = 1 - Mathf.Pow(1-u, 2);
		this.transform.position = (1-u) * this._points[0] + u * this._points[1];
		this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -2);
	}

	void OnCollisionEnter(Collision coll) {

		GameObject other = coll.gameObject;
		switch (other.tag) {
		case "ShipProjectile":
			Projectile p = other.GetComponent<Projectile> ();
			bounds.center = transform.position + boundsCenterOffset;
			if (bounds.extents == Vector3.zero || Utils.ScreenBoundsCheck (bounds, BoundsTest.offScreen) != Vector3.zero) {
				Destroy(other);
				break;
			}

			GameObject goHit = coll.contacts[0].thisCollider.gameObject;
			Part prtHit = findPart(goHit);
			if (prtHit == null) {
				goHit = coll.contacts [0].otherCollider.gameObject;
				prtHit = findPart(goHit);
			}

			if (prtHit.protectedBy != null) {
				foreach (string s in prtHit.protectedBy) {
					if (!destroyed(s)) {
						Destroy(other);
						return;
					}
				}
			}

			prtHit.health -= Main.W_DEFS [p.type].damageOnHit;
			showLocalizedDamage(prtHit.mat);
			if (prtHit.health <= 0) {
				prtHit.go.SetActive(false);
			}

			bool allDestroyed = true;
			foreach (Part prt in this._parts) {
				if (!destroyed(prt)) {
					allDestroyed = false;
					break;
				}
			}
			if (allDestroyed) {
				Main.SingletonMain.shipDestroyed(this);
				Destroy (this.gameObject);
			}
			Destroy(other);
			break;
		}
	}

	Part findPart(string n) {
		foreach(Part prt in this._parts) {
			if (prt.name == n) {
				return(prt);
			}
		}
		return(null);
	}

	Part findPart(GameObject go) {
		foreach(Part prt in this._parts) {
			if (prt.go == go) {
				return(prt);
			}
		}
		return(null);
	}
	
	bool destroyed(GameObject go) {
		return(destroyed(findPart(go)));
	}

	bool destroyed(string n) {
		return(destroyed(findPart(n)));
	}

	bool destroyed(Part prt) {
		if (prt == null) {
			return true;
		}
		return (prt.health <= 0);
	}
	
	void showLocalizedDamage(Material m) {
		m.color = Color.red;
		remainingDamageFrames = showDamageForFrames;
	}

	
}
