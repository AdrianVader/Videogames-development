using UnityEngine;
using System.Collections;
using System.Collections.Generic;


// The SlotDef class is not a subclass of MonoBehaviour, so it doesn't need a
// separate C# file.
[System.Serializable] // This makes SlotDefs visible in the Unity Inspector pane
public class SlotDef {
	public float x;
	public float y;
	public float rot;
	public int player;
	public bool faceUp=false;
	public string layerName="Default";
	public int layerID = 0;
	public int id;
	public List<int> hiddenBy = new List<int>();
	public string type="slot";
	public Vector2 stagger;
}
public class Layout : MonoBehaviour {

	public PT_XMLReader xmlr; // Just like Deck, this has a PT_XMLReader
	public PT_XMLHashtable xml; // This variable is for easier xml access
	public Vector2 multiplier; // Sets the spacing of the tableau
	// SlotDef references
	public List<SlotDef> slotDefs; // All the SlotDefs for Row0-Row3
	public SlotDef drawPile;
	public SlotDef discardPile;
	public List<SlotDef> hands;
	// This holds all of the possible names for the layers set by layerID
	public string[] sortingLayerNames = new string[] { "Row0", "Row1", "Row2", "Row3", "Discard", "Draw"};


	public void ReadLayout(string xmlText) {

		this.xmlr = new PT_XMLReader();
		xmlr.Parse(xmlText);

		this.multiplier.x = float.Parse(xmlr.xml ["xml"] [0] ["multiplier"] [0].att ("x"));
		this.multiplier.y = float.Parse(xmlr.xml ["xml"] [0] ["multiplier"] [0].att ("y"));

		this.hands = new List<SlotDef> ();
		for (int i = 0; i < xmlr.xml ["xml"] [0] ["slot"].Count ; i++) {
			if (xmlr.xml ["xml"] [0] ["slot"] [i].att ("type") == "drawpile") {
				this.drawPile.x = float.Parse(xmlr.xml ["xml"] [0] ["slot"] [i].att ("x"));
				this.drawPile.y = float.Parse(xmlr.xml ["xml"] [0] ["slot"] [i].att ("y"));
				this.drawPile.layerID = int.Parse(xmlr.xml ["xml"] [0] ["slot"] [i].att ("layer"));
				float x = 0;
				if (!xmlr.xml ["xml"] [0] ["slot"] [i].att ("xstagger").Equals("")){
					x = float.Parse(xmlr.xml ["xml"] [0] ["slot"] [i].att ("xstagger"));
				}
				float y = 0;
				if (!xmlr.xml ["xml"] [0] ["slot"] [i].att ("ystagger").Equals("")){
					y = float.Parse(xmlr.xml ["xml"] [0] ["slot"] [i].att ("ystagger"));
				}
				this.drawPile.stagger = new Vector2 (x, y);
			} else if (xmlr.xml ["xml"] [0] ["slot"] [i].att ("type") == "discardpile") {
				this.discardPile.x = float.Parse(xmlr.xml ["xml"] [0] ["slot"] [i].att ("x"));
				this.discardPile.y = float.Parse(xmlr.xml ["xml"] [0] ["slot"] [i].att ("y"));
				this.discardPile.layerID = int.Parse(xmlr.xml ["xml"] [0] ["slot"] [i].att ("layer"));
			} else if (xmlr.xml ["xml"] [0] ["slot"] [i].att ("type") == "hand") {
				SlotDef hand = new SlotDef();
				hand.x = float.Parse(xmlr.xml ["xml"] [0] ["slot"] [i].att ("x"));
				hand.y = float.Parse(xmlr.xml ["xml"] [0] ["slot"] [i].att ("y"));
				hand.rot = int.Parse(xmlr.xml ["xml"] [0] ["slot"] [i].att ("rot"));
				hand.player = int.Parse(xmlr.xml ["xml"] [0] ["slot"] [i].att ("player"));
				hand.layerID = int.Parse(xmlr.xml ["xml"] [0] ["slot"] [i].att ("layer"));

				this.hands.Add (hand);
			}
		}

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
