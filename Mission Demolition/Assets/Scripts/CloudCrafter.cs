using UnityEngine;
using System.Collections;

public class CloudCrafter : MonoBehaviour {

	public int _numClouds = 40;
	public GameObject[] _cloudPrefabs;
	public Vector3 _cloudPosMin;
	public Vector3 _cloudPosMax;
	public float _cloudScaleMin = 0.3f;
	public float _cloudScaleMax = 1f;
	public float _cloudSpeedMult = 0.5f;
	public bool _____________________________;

	public GameObject[] _cloudInstances;

	// Use this for initialization
	void Start () {

		this._cloudInstances = new GameObject[this._numClouds];

		GameObject anchor = GameObject.Find("CloudAnchor");

		for (int i=0; i<this._numClouds; i++) {
			int prefabNum = Random.Range(0,this._cloudPrefabs.Length);
			GameObject cloud = Instantiate( this._cloudPrefabs[prefabNum] ) as GameObject;

			Vector3 cloudPosition = Vector3.zero;
			cloudPosition.x = Random.Range(this._cloudPosMin.x, this._cloudPosMax.x);
			cloudPosition.y = Random.Range(this._cloudPosMin.y, this._cloudPosMax.y);

			float scaleU = Random.value;
			float scaleVal = Mathf.Lerp(this._cloudScaleMin, this._cloudScaleMax, scaleU);
			cloudPosition.y = Mathf.Lerp(this._cloudPosMin.y, cloudPosition.y, scaleU);
			cloudPosition.z = -(5*scaleU);

			cloud.transform.position = cloudPosition;
			cloud.transform.localScale = Vector3.one * scaleVal;

			cloud.transform.parent = anchor.transform;

			this._cloudInstances[i] = cloud;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
		foreach (GameObject cloud in this._cloudInstances) {
			float scaleVal = cloud.transform.localScale.x;
			Vector3 cPos = cloud.transform.position;

			cPos.x -= scaleVal * Time.deltaTime * this._cloudSpeedMult;

			if (cPos.x <= this._cloudPosMin.x) {
				cPos.x = this._cloudPosMax.x;
			}

			cloud.transform.position = cPos;
		}
	}
}
