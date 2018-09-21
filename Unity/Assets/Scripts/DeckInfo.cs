using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckInfo : MonoBehaviour {

	public UILabel _txtAttack;
	public UILabel _txtShield;
	public UILabel _txtPortion;

	Deck _deck;
	Deck deck
	{
		get
		{
			if(_deck == null)
				_deck = transform.parent.parent.GetComponent<Deck>();
			return _deck;
		}
	}

	void Awake()
	{
		_txtAttack = transform.Find("txtAttack").GetComponent<UILabel>();
		_txtShield = transform.Find("txtShield").GetComponent<UILabel>();
		_txtPortion = transform.Find("txtPortion").GetComponent<UILabel>();

		//Set_DeckInfo();
	}

	public void Set_DeckInfo()
	{
		List<Card> listCard = deck._listCard;

		int[] iCount = new int[(int)CARD_TYPE.END];

		for(int i=0; i<listCard.Count; ++i)
		{
			iCount[(int)listCard[i]._cardType]++;
		}

		_txtAttack.text = iCount[(int)CARD_TYPE.ATTACK].ToString();
		_txtShield.text = iCount[(int)CARD_TYPE.SHIELD].ToString();
		_txtPortion.text = iCount[(int)CARD_TYPE.HEAL].ToString();
	}
}
