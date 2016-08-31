using UnityEngine;
using System.Collections;
using System.Collections.Generic;


// The SlotDef class is not a subclass of MonoBehaviour, so it doesn't need a
// separate C# file.
[System.Serializable] // This makes SlotDefs visible in the Unity Inspector pane
public class SlotDef {
	public float x;
	public float y;
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
	// This holds all of the possible names for the layers set by layerID
	public string[] sortingLayerNames = new string[] { "Row0", "Row1", "Row2", "Row3", "Discard", "Draw"};


	public void ReadLayout(string xmlText) {

		this.xmlr = new PT_XMLReader();
		xmlr.Parse(xmlText);

		this.multiplier.x = float.Parse(xmlr.xml["xml"][0]["multiplier"][0].att("x"));
		this.multiplier.y = float.Parse(xmlr.xml["xml"][0]["multiplier"][0].att("y"));

		this.slotDefs = new List<SlotDef>();
		PT_XMLHashList xmlSlots = xmlr.xml["xml"][0]["slot"];
		for (int i = 0; i < xmlSlots.Count; i++) {

			if(xmlr.xml["xml"][0]["slot"][i].att("type").Equals("")){

				SlotDef card = new SlotDef();

				card.x = float.Parse(xmlr.xml["xml"][0]["slot"][i].att("x"));

				card.y = float.Parse(xmlr.xml["xml"][0]["slot"][i].att("y"));

				if (int.Parse(xmlr.xml["xml"][0]["slot"][i].att("faceup")) == 0) {
					card.faceUp = false;
				} else {
					card.faceUp = true;
				}

				card.layerID = int.Parse(xmlr.xml["xml"][0]["slot"][i].att("layer"));
				
				card.hiddenBy = new List<int>();
				string upperCardsString = xmlr.xml["xml"][0]["slot"][i].att("hiddenby");
				string[] upperCards = upperCardsString.Split(',');
				if (!upperCardsString.Equals("")) {
					foreach(string upperCard in upperCards){
						card.hiddenBy.Add(int.Parse(upperCard));
					}
				}

				card.id = int.Parse(xmlr.xml["xml"][0]["slot"][i].att("id"));

				card.layerName = "Row" + card.layerID.ToString ();

				this.slotDefs.Add (card);

			} else if(xmlr.xml["xml"][0]["slot"][i].att("type").Equals("drawpile")){

				this.drawPile.x = float.Parse(xmlr.xml["xml"][0]["slot"][i].att("x"));

				this.drawPile.y = float.Parse(xmlr.xml["xml"][0]["slot"][i].att("y"));

				float x = 0;
				if(!xmlr.xml["xml"][0]["slot"][i].att("xstagger").Equals("")){x = float.Parse(xmlr.xml["xml"][0]["slot"][i].att("xstagger"));}
				float y = 0;
				if(!xmlr.xml["xml"][0]["slot"][i].att("ystagger").Equals("")){y = float.Parse(xmlr.xml["xml"][0]["slot"][i].att("ystagger"));}
				this.drawPile.stagger = new Vector2(x, y);

				this.drawPile.layerID = int.Parse(xmlr.xml["xml"][0]["slot"][i].att("layer"));

			}else if(xmlr.xml["xml"][0]["slot"][i].att("type").Equals("discardpile")){

				this.discardPile.x = float.Parse(xmlr.xml["xml"][0]["slot"][i].att("x"));

				this.discardPile.y = float.Parse(xmlr.xml["xml"][0]["slot"][i].att("y"));

				this.drawPile.layerID = int.Parse(xmlr.xml["xml"][0]["slot"][i].att("layer"));

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
