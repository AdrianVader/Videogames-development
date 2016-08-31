using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum CardState {
	drawpile,
	tableau,
	target,
	discard
}

public class CardProspector : Card {// Make sure CardProspector extends Card
	// This is how you use the enum CardState
	public CardState state = CardState.drawpile;
	// The hiddenBy list stores which other cards will keep this one face down
	public List<CardProspector> hiddenBy = new List<CardProspector>();
	// LayoutID matches this card to a Layout XML id if it's a tableau card
	public int layoutID;
	// The SlotDef class stores information pulled in from the LayoutXML <slot>
	public SlotDef slotDef;

	override public void OnMouseUpAsButton() {

		if (this.state == CardState.discard) {
			print(name);
		} else if (this.state == CardState.target) {
			print(name);
		} else if (this.state == CardState.drawpile) {
			GameObject.Find("Main Camera").GetComponent<Prospector>().pushFromDrawPile();
		} else if (this.state == CardState.tableau && this.faceUp) {
			GameObject.Find("Main Camera").GetComponent<Prospector>().tableauCardSelected(this);
		}

	}

}
