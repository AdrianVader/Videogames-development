using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Utils {

	static public void setCameraEdges(ref Vector3 downLeftDot, ref Vector3 upRightDot){

		Camera cam = Camera.main;
		float height = 2f * cam.orthographicSize;
		float width = height * cam.aspect;
		
		downLeftDot = new Vector3(-(width/2f), -(height/2f -1), 0);
		upRightDot = new Vector3(width/2f, height/2f +1, 0);
	}

	static public Material[] getAllMaterials (GameObject go){
		List<Material> materials = new List<Material> ();
		if (go.GetComponent<Renderer>() != null) {
			materials.Add (go.GetComponent<Renderer>().material);
		}
		foreach (Transform t in go.transform) {
			materials.AddRange (getAllMaterials(t.gameObject));
		}

		return materials.ToArray();
	}

	static public void showDamage (Material[] materials, Color color){
		foreach(Material m in materials){
			m.color = color;
		}
	}

	static public void unshowDamage (Material[] materials, Color[] originalColors){
		for(int i = 0; i < materials.Length; i++){
			materials [i].color = originalColors [i];
		}
	}
}
