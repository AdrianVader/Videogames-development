using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Prospector : MonoBehaviour {
	static public Prospector    S;
	
	public Deck                 deck;
	public TextAsset            deckXML;

	public Layout layout;
	public TextAsset layoutXML;

	public Vector3 layoutCenter;
	public float xOffset = 3;
	public float yOffset = -2.5f;
	public Transform layoutAnchor;
	public CardProspector target;
	public List<CardProspector> tableau;
	public List<CardProspector> discardPile;
	public List<CardProspector> drawPile;
	
	void Awake() {
		S = this; // Set up a Singleton for Prospector
	}
	
	void Start () {
		deck = GetComponent<Deck>(); // Get the Deck
		deck.InitDeck(deckXML.text); // Pass DeckXML to it
		Deck.Shuffle(ref deck.cards);    // This shuffles the deck

		layout = GetComponent<Layout>();
		layout.ReadLayout(layoutXML.text);

		drawPile = ConvertListCardsToListCardProspectors( deck.cards );
		LayoutGame();

		prepareDrawPile ();
	}

	List<CardProspector> ConvertListCardsToListCardProspectors(List<Card> lCD) {
		List<CardProspector> lCP = new List<CardProspector>();
		CardProspector tCP;// = gameObject.AddComponent<CardProspector> ();
		foreach( Card tCD in lCD ) {
			//tCP = tCP.convertCard(tCD);
			tCP = tCD as CardProspector;
			lCP.Add( tCP );
		}
		return( lCP );
	}

	// The Draw function will pull a single card from the drawPile and return it
	CardProspector Draw() {
		CardProspector cd = drawPile[0]; // Pull the 0th CardProspector
		drawPile.RemoveAt(0); // Then remove it from List<> drawPile
		return(cd); // And return it
	}

	// LayoutGame() positions the initial tableau of cards, a.k.a. the "mine"
	void LayoutGame() {
		// Create an empty GameObject to serve as an anchor for the tableau
		if (layoutAnchor == null) {
			GameObject tGO = new GameObject("_LayoutAnchor");
			// ^ Create an empty GameObject named _LayoutAnchor in the Hierarchy
			layoutAnchor = tGO.transform; // Grab its Transform
			layoutAnchor.transform.position = layoutCenter; // Position it
		}
		CardProspector cp;
		// Follow the layout
		foreach (SlotDef tSD in layout.slotDefs) {
			// ^ Iterate through all the SlotDefs in the layout.slotDefs as tSD
			cp = Draw(); // Pull a card from the top (beginning) of the drawPile
			cp.faceUp = tSD.faceUp; // Set its faceUp to the value in SlotDef
			cp.transform.parent = layoutAnchor; // Make its parent layoutAnchor
			// This replaces the previous parent: deck.deckAnchor, which appears
			// as _Deck in the Hierarchy when the scene is playing.
			cp.transform.localPosition = new Vector3(
				layout.multiplier.x * tSD.x,
				layout.multiplier.y * tSD.y,
				-tSD.layerID );
			// ^ Set the localPosition of the card based on slotDef
			cp.layoutID = tSD.id;
			cp.slotDef = tSD;
			cp.state = CardState.tableau;
			cp.SetSortingLayerName(tSD.layerName); // Set the sorting layers
			// CardProspectors in the tableau have the state CardState.tableau
			tableau.Add(cp); // Add this CardProspector to the List<> tableau
		}

		foreach (SlotDef tSD in layout.slotDefs) {
			CardProspector currentCard = null;
			foreach (CardProspector card in this.tableau) {
				if (card.layoutID == tSD.id) {
					currentCard = card;
				}
			}
			foreach (int id in tSD.hiddenBy) {
				foreach (CardProspector card in this.tableau) {
					if (card.layoutID == id){
						currentCard.hiddenBy.Add (card);
					}
				}
			}
		}

	}

	public void prepareDrawPile(){

		Transform layoutAnchor = GameObject.Find ("_LayoutAnchor").transform;
		Vector2 drawPilePosition = new Vector2(layout.drawPile.x, layout.drawPile.y);
		Vector2 stagger = layout.drawPile.stagger;
		int iterationCounter = 0;
		foreach (CardProspector cp in this.drawPile) {
			cp.faceUp = false;
			cp.transform.parent = layoutAnchor;
			cp.transform.localPosition = new Vector3(drawPilePosition.x + (stagger.x * iterationCounter), drawPilePosition.y + (stagger.y * iterationCounter), 0f);
			cp.layoutID = 0;
			//cp.SlotDef;
			cp.state = CardState.drawpile;
			cp.SetSortingLayerName ("Draw");

			iterationCounter++;
		}

		pushFromDrawPile ();

	}

	public void pushFromDrawPile(){

		CardProspector cp = this.drawPile[0];
		this.drawPile.RemoveAt (0);
		
		insertToDiscardPile (cp);

		if (this.drawPile.Count == 0) {
			checkGameOver ();
		}
	}

	public void insertToDiscardPile (CardProspector cp){
		if (this.discardPile.Count > 0) {
			this.discardPile[this.discardPile.Count-1].faceUp = false;
			this.discardPile[this.discardPile.Count-1].SetSortingLayerName ("Default");
		}
		cp.faceUp = true;
		cp.transform.localPosition = new Vector3(layout.discardPile.x, layout.discardPile.y, 0f);
		cp.state = CardState.discard;
		cp.SetSortingLayerName ("Discard");
		
		this.discardPile.Add(cp);
	}

	public void tableauCardSelected (CardProspector cp) {
		CardProspector currentCard = this.discardPile[this.discardPile.Count-1];

		bool correct = false;
		if (isNextNumberCard(currentCard.rank, cp.rank)) {
			this.tableau.Remove (cp);
			insertToDiscardPile (cp);
			correct = true;
		}

		if (correct) {
			// TODO: Mirar la fila inferior para ver si desbloquea alguna de las cartas.
			foreach (CardProspector card in this.tableau) {
				bool faceup = false;
				if(!card.faceUp){
					faceup = true;
					foreach (CardProspector upperCard in card.hiddenBy) {
						if (!this.discardPile.Contains(upperCard)) {
							faceup = false;
						}
					}
				} else {
					faceup = true;
				}
				card.faceUp = faceup;
			}
			checkGameOver ();
		}
	}

	private bool isNextNumberCard(int baseNumber, int nextNumber) {
		if (baseNumber == (nextNumber + 1) || baseNumber == (nextNumber - 1)) {
			return true;
		} else if ((baseNumber == 13 && nextNumber == 1) || ((baseNumber == 1 && nextNumber == 13))) {
			return true;
		} else {
			return false;
		}
	}

	public void checkGameOver(){
		CardProspector currentCard = this.discardPile[this.discardPile.Count-1];
		bool gameOver = false;
		if (this.tableau.Count == 0) {
			gameOver = true;
		} else if (this.drawPile.Count == 0) {
			gameOver = true;
			foreach (CardProspector card in this.tableau) {
				if(card.faceUp && isNextNumberCard(currentCard.rank, card.rank)){
					gameOver = false;
				}
			}
		}
		if (gameOver) {
			Application.LoadLevel ("__Prospector_Scene_0");
		}
	}

	/*void Update(){

	}*/

}

