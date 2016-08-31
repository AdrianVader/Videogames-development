using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum BartokGameState {
	Player1,
	Player2,
	Player3,
	Player4,
	Waiting
}

public class Bartok : MonoBehaviour {
	static public Bartok    S;
	
	public Deck                 deck;
	public TextAsset            deckXML;

	public Layout layout;
	public TextAsset layoutXML;

	public Vector3 layoutCenter;
	public float xOffset = 3;
	public float yOffset = -2.5f;
	public Transform layoutAnchor;
	public CardBartok target;
	public List<CardBartok> tableau;
	public List<CardBartok> discardPile;
	public List<CardBartok> drawPile;
	public List<List<CardBartok>> hands;

	public BartokGameState gameState;
	public BartokGameState prevousGameState;
	
	void Awake() {
		S = this; // Set up a Singleton for Prospector
	}
	
	void Start () {
		this.prevousGameState = BartokGameState.Waiting;
		this.gameState = BartokGameState.Waiting;
		deck = GetComponent<Deck>(); // Get the Deck
		deck.InitDeck(deckXML.text); // Pass DeckXML to it
		Deck.Shuffle(ref deck.cards);    // This shuffles the deck

		layout = GetComponent<Layout>();
		layout.ReadLayout(layoutXML.text);

		drawPile = ConvertListCardsToListCardBartoks( deck.cards );
		LayoutGame();

		prepareDrawPile ();
	}

	List<CardBartok> ConvertListCardsToListCardBartoks(List<Card> lCD) {
		List<CardBartok> lCB = new List<CardBartok>();
		CardBartok tCB;// = gameObject.AddComponent<CardProspector> ();
		foreach( Card tCD in lCD ) {
			//tCP = tCP.convertCard(tCD);
			tCB = tCD as CardBartok;
			lCB.Add( tCB );
		}
		return( lCB );
	}

	// The Draw function will pull a single card from the drawPile and return it
	CardBartok Draw() {
		CardBartok cd = drawPile[0]; // Pull the 0th CardProspector
		drawPile.RemoveAt(0); // Then remove it from List<> drawPile
		foreach (CardBartok card in this.drawPile) {
			card.transform.position = new Vector3(card.transform.position.x - this.layout.drawPile.stagger.x, card.transform.position.y - this.layout.drawPile.stagger.y, 0f);
		}
		return(cd); // And return it
	}

	// LayoutGame() positions the initial tableau of cards, a.k.a. the "mine"
	void LayoutGame() {
		// Create an empty GameObject to serve as an anchor for the tableau
		if (layoutAnchor == null) {
			GameObject tGO = new GameObject("_LayoutAnchor");
			tGO.transform.position = Vector3.zero;
			// ^ Create an empty GameObject named _LayoutAnchor in the Hierarchy
			layoutAnchor = tGO.transform; // Grab its Transform
			layoutAnchor.transform.position = layoutCenter; // Position it
		}

		this.hands = new List<List<CardBartok>> ();

		prepareDrawPile ();

		StartCoroutine (prapareAllHands ());

		pushFromDrawPile ();

	}

	public void prepareDrawPile(){

		Transform layoutAnchor = GameObject.Find ("_LayoutAnchor").transform;
		Vector2 drawPilePosition = new Vector2(layout.drawPile.x, layout.drawPile.y);
		Vector2 stagger = layout.drawPile.stagger;
		int iterationCounter = 0;
		for (int i = 0; i < this.drawPile.Count; i++) {
			CardBartok cb = this.drawPile[this.drawPile.Count - (i+1)];
			cb.faceUp = false;
			cb.transform.parent = layoutAnchor;
			cb.transform.localPosition = new Vector3(drawPilePosition.x + (stagger.x * this.drawPile.Count) - (stagger.x * iterationCounter), drawPilePosition.y + (stagger.y * this.drawPile.Count) - (stagger.y * iterationCounter), 0f);
			cb.state = CBState.drawpile;
			cb.SetSortingLayerName ("Draw");

			iterationCounter++;
		}

	}

	public void pushFromDrawPile(){

		CardBartok cb = Draw ();
		cb.faceUp = true;
		cb.state = CBState.discard;
		cb.MoveTo (new Vector3(this.layout.discardPile.x, this.layout.discardPile.y, 0f), Quaternion.Euler(new Vector3(0f, 0f, 0f)));
		this.discardPile.Add (cb);
	}

	public IEnumerator prapareAllHands (){

		float waitingSeconds = 0.05f;

		for (int i = 0; i < 4; i++) {
			this.hands.Add (new List<CardBartok>());
		}
		for (int i = 0; i < 7; i++) {
			int playerCounter = 0;
			for (int j = 0; j < this.hands.Count; j++) {
				giveCardToPlayer (playerCounter);
				playerCounter++;
				yield return new WaitForSeconds (waitingSeconds);
			}
		}

		this.prevousGameState = BartokGameState.Player1;
		this.gameState = BartokGameState.Player2;
	}
	
	public void giveCardToPlayer (int playerNumber) {
		CardBartok cb = Draw ();
		cb.state = CBState.idle;
		if (playerNumber == 0) {
			cb.faceUp = true;
			cb.state = CBState.hand;
		}
		orderedInsertionCard (playerNumber, cb);
		//Vector3 position = new Vector3 (this.layout.hands [playerNumber - 1].x, this.layout.hands [playerNumber - 1].y, 0f);
		//position.y = position.y + 2f;
		//cb.MoveTo (position, Quaternion.Euler (new Vector3 (0f, 0f, this.layout.hands[playerNumber].rot)));
	}

	public void orderedInsertionCard (int playerNumber, CardBartok card) {
		List<CardBartok> cards = this.hands[playerNumber];
		int foundIndex = -1;
		if (cards.Count > 0) {
			int i = 0;
			while (i < cards.Count && foundIndex == -1) {
				if (cards [i].rank > card.rank) {
					cards.Insert (i, card);
					foundIndex = i;
				}
				i++;
			}
			if (foundIndex == -1) {
				cards.Add(card);
			}
		} else {
			cards.Add(card);
		}

		orderCards (cards, playerNumber);
	}

	public void orderCards (List<CardBartok> cards, int playerNumber){
		
		Vector3 playerPosition = new Vector3 (this.layout.hands[playerNumber].x, this.layout.hands[playerNumber].y, 0f);

		float minimumSpacing = 0.6f;
		float availableSpace = 10.0f;
		if (availableSpace > cards.Count * minimumSpacing) {
			availableSpace = cards.Count * minimumSpacing;
		}
		if (availableSpace / cards.Count < minimumSpacing) {
			minimumSpacing = availableSpace / cards.Count;
		}
		//float range = 160f;
		//float percentage = range/cards.Count;
		//if (percentage < minimumRotation) {percentage = minimumRotation;}
		//float startingRotation = -60f;
		//Quaternion ret = new Quaternion();

		for(int i = 0; i < cards.Count; i++) {
			Vector3 position = new Vector3();
			if(playerNumber == 0 || playerNumber == 2){
				position = new Vector3(playerPosition.x + (availableSpace/2) - (i * minimumSpacing), playerPosition.y, i * 0.5f);
			}else{
				position = new Vector3(playerPosition.x, playerPosition.y + (availableSpace/2) - (i * minimumSpacing), i * 0.5f);
			}
			cards[i].SetSortingLayerName (i.ToString());
			cards[i].MoveTo(position, Quaternion.Euler(new Vector3(0f, 0f, this.layout.hands[playerNumber].rot)));
		}
	}

	public void checkGameOver(){
		bool gameOver = false;
		int numberOfPlayer = 1;
		foreach (List<CardBartok> playerCards in this.hands) {
			if (playerCards.Count == 0) {
				gameOver = true;
			}
			if (!gameOver) {
				numberOfPlayer++;
			}
		}

		if (gameOver) {
			 StartCoroutine(reloadGame ());
		}
	}

	public bool tryCard (CardBartok card, int playerNumber) {

		CardBartok target = this.discardPile [0];
		if (target.rank == card.rank || target.suit.Equals (card.suit)) {
			this.drawPile.Insert (this.drawPile.Count - 1, target);
			this.discardPile.RemoveAt (0);
			target.state = CBState.drawpile;
			target.SetSortingLayerName ("Draw");
			target.faceUp = false;
			target.MoveTo (new Vector3 (this.drawPile [this.drawPile.Count - 1].transform.position.x - this.layout.drawPile.stagger.x, this.drawPile [this.drawPile.Count - 1].transform.position.y - this.layout.drawPile.stagger.y, 0f));
			//prepareDrawPile ();
			this.hands [playerNumber].Remove (card);
			card.state = CBState.discard;
			card.faceUp = true;
			card.MoveTo (new Vector3 (this.layout.discardPile.x, this.layout.discardPile.y, 0f));
			this.discardPile.Add (card);
			orderCards (this.hands [playerNumber], playerNumber);
			return true;
		} else {
			return false;
		}
	}

	public CardBartok findCardFittingTarget (int playerNumber) {
		for (int i = 0; i < this.hands[playerNumber].Count; i++) {
			CardBartok card = this.hands[playerNumber][i];
			if(tryCard (card, playerNumber)){
				return card;
			}
		}

		return null;
	}

	public IEnumerator reloadGame () {
		yield return new WaitForSeconds(2.0f);
		Application.LoadLevel ("__Prospector_Scene_0");
	}

	public IEnumerator turn() {
		if (this.gameState == BartokGameState.Player1) {
			GameObject light = GameObject.Find("Spot light");
			light.transform.position = new Vector3(this.layout.hands[0].x, this.layout.hands[0].y, light.transform.position.z);
			
		} else if (this.gameState == BartokGameState.Player2) {
			GameObject light = GameObject.Find("Spot light");
			light.transform.position = new Vector3(this.layout.hands[1].x, this.layout.hands[1].y, light.transform.position.z);
			yield return new WaitForSeconds(1.0f);
			CardBartok found = findCardFittingTarget (1);
			if(found == null){
				giveCardToPlayer (1);
			}
			this.gameState = BartokGameState.Player3;

		} else if (this.gameState == BartokGameState.Player3) {
			GameObject light = GameObject.Find("Spot light");
			light.transform.position = new Vector3(this.layout.hands[2].x, this.layout.hands[2].y, light.transform.position.z);
			yield return new WaitForSeconds(1.0f);
			CardBartok found = findCardFittingTarget (2);
			if(found == null){
				giveCardToPlayer (2);
			}
			this.gameState = BartokGameState.Player4;

		} else if (this.gameState == BartokGameState.Player4) {			
			GameObject light = GameObject.Find("Spot light");
			light.transform.position = new Vector3(this.layout.hands[3].x, this.layout.hands[3].y, light.transform.position.z);
			yield return new WaitForSeconds(1.0f);
			CardBartok found = findCardFittingTarget (3);
			if(found == null){
				giveCardToPlayer (3);
			}
			this.gameState = BartokGameState.Player1;

		}
	}

	void Update(){

		if (this.prevousGameState != this.gameState) {
			checkGameOver ();
			this.prevousGameState = this.gameState;
			StartCoroutine (turn ());
		}

	}

}

