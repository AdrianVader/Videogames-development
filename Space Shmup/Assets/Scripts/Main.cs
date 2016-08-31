using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Main : MonoBehaviour {

	static public Main SingletonMain;
	static public Dictionary<WeaponType, WeaponDefinition> W_DEFS;

	public GameObject[] _prefabEnemies;
	public float _enemySpawnPerSecond = 0.1f;
	public float _enemySpawnPadding = 1f;
	public WeaponDefinition[] _weapons;
	public GameObject prefabPowerUp;
	public WeaponType[] powerUpFrequency = new WeaponType[] {WeaponType.blaster, WeaponType.blaster, WeaponType.spread, WeaponType.shield};

	public bool ________________;

	public float _enemySpawnRate;
	public WeaponType[] _activeWeaponTypes;

	void Awake() {

		SingletonMain = this;

		Utils.SetCameraBounds(this.GetComponent<Camera>());

		this._enemySpawnRate = 1f/this._enemySpawnPerSecond;

		Invoke("SpawnEnemy", this._enemySpawnRate);

		W_DEFS = new Dictionary<WeaponType, WeaponDefinition>();
		foreach(WeaponDefinition def in this._weapons){
			W_DEFS[def.type] = def;
		}
	}

	// Use this for initialization
	void Start () {
	
		this._activeWeaponTypes = new WeaponType[this._weapons.Length];
		for(int i=0; i < this._weapons.Length; i++){
			this._activeWeaponTypes[i] = this._weapons[i].type;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SpawnEnemy(){

		int index = Random.Range(0, this._prefabEnemies.Length);
		GameObject go = Instantiate(this._prefabEnemies[index]) as GameObject;

		Vector3 pos = Vector3.zero;
		float xMin = Utils.camBounds.min.x + this._enemySpawnPadding;
		float xMax = Utils.camBounds.max.x - this._enemySpawnPadding;
		pos.x = Random.Range(xMin, xMax);
		pos.y = Utils.camBounds.max.y + this._enemySpawnPadding;
		pos.z = -2;
		go.transform.position = pos;

		Invoke("SpawnEnemy", this._enemySpawnRate);
	}

	public void restartWithDelay(float gameRestartDelay){
		Invoke("restart", gameRestartDelay);
	}

	protected void restart(){
		Application.LoadLevel("Test Scene");
	}

	static public WeaponDefinition GetWeaponDefinition(WeaponType wt){
		if(W_DEFS.ContainsKey(wt)){
			return(W_DEFS[wt]);
		}

		return(new WeaponDefinition());
	}

	public void shipDestroyed(Enemy e) {

		if (Random.value <= e.powerUpDropChance) {
			int ndx = Random.Range(0, powerUpFrequency.Length);
			WeaponType puType = powerUpFrequency [ndx];

			GameObject go = Instantiate(prefabPowerUp) as GameObject;
			PowerUp pu = go.GetComponent<PowerUp> ();
			pu.SetType(puType);

			pu.transform.position = e.transform.position;
		}
	}


}
