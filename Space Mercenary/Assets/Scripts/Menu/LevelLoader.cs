using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class LevelLoader : MonoBehaviour {

	public void loadLevel (int levelNumber){

		XmlTextReader reader = new XmlTextReader (Constants.PATH_TO_LEVELS + Constants.LEVEL_TEXT + levelNumber + Constants.XML_EXTENSION);

		List<float> times = new List<float> ();
		List<string> types = new List<string> ();
		List<Vector3> positions = new List<Vector3> ();
		List<string> behaviours = new List<string> ();

		while (reader.Read()) 
		{
			if(reader.Name.Equals(Constants.ENEMY_TEXT) || reader.Name.Equals(Constants.BOSS_TEXT)){
				times.Add(float.Parse(reader.GetAttribute (Constants.TIME_ATTRIBUTE)));
				types.Add(reader.GetAttribute (Constants.TYPE_ATTRIBUTE));
				positions.Add(new Vector3(float.Parse(reader.GetAttribute(Constants.X_ATTRIBUTE)), float.Parse(reader.GetAttribute(Constants.Y_ATTRIBUTE)), 0f));
				behaviours.Add(reader.GetAttribute (Constants.BEHAVIOUR_ATTRIBUTE));
			}

		}

		GameObject levelConfiguration = new GameObject (Constants.LEVEL_CONFIGURATION);
		levelConfiguration.AddComponent<LevelConfiguration>();
		LevelConfiguration lC = levelConfiguration.GetComponent<LevelConfiguration>();
		lC.times = times;
		lC.types = types;
		lC.positions = positions;
		lC.behaviours = behaviours;
		DontDestroyOnLoad (levelConfiguration);
		SceneManager.LoadScene (Constants.TEST_LEVEL, LoadSceneMode.Single);

	}

	public void NextLevels (int previousLevel, int numberOfFields, ref bool previous, ref bool next, ref int first, ref int last){

		if(previousLevel <= 0)
			previous = false;
		else 
			previous = true;

		Object nextLevel = Resources.Load(Constants.LEVEL_TEXT + (previousLevel + numberOfFields + 1));
		if (nextLevel != null) {
			next = true;
		} else {
			next = false;
		}

		first = previousLevel + 1;
		if (next) {
			last = previousLevel + numberOfFields;
		} else {
			int i = previousLevel + 1;
			Object level;
			do {
				level = Resources.Load (Constants.LEVEL_TEXT + i);
				if (level == null)
					last = i - 1;
				i++;
			} while (i <= previousLevel + numberOfFields && level != null);
		}

	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
