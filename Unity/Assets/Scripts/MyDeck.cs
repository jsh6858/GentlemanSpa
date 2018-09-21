using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyDeck : Deck {

	GameObject _objSelect;
	GameObject objSelect
	{
		get
		{
			if(null == _objSelect)
				_objSelect = transform.Find("Anchor/Select").gameObject;
			return _objSelect;
		}
	}

	public void Set_Deck(Card[] selectCard)
	{
		GameObject card = Resources.Load("Prefab/Card") as GameObject;

		List<Card> listAttackCard = new List<Card>();
		List<Card> listShieldCard = new List<Card>();
		List<Card> listHealCard = new List<Card>();

		// 분류
		for(int i=0; i<selectCard.Length; ++i)
		{
			switch(selectCard[i]._cardType)
			{
				case CARD_TYPE.ATTACK:
					listAttackCard.Add(selectCard[i]);
					break;
				case CARD_TYPE.SHIELD:
					listShieldCard.Add(selectCard[i]);
					break;
				case CARD_TYPE.HEAL:
					listHealCard.Add(selectCard[i]);
					break;
			}
		}

		// Attack
		for(int i=0; i<listAttackCard.Count; ++i)
		{
			GameObject temp = GameObject.Instantiate(card, Vector3.zero, Quaternion.identity);
			temp.transform.SetParent(grid.transform);

			Card tempCard = temp.GetComponent<Card>();

			tempCard.SetCard(listAttackCard[i]._iNum, listAttackCard[i]._cardType);
			_listCard.Add(tempCard);
		}

		// Shield
		for(int i=0; i<listShieldCard.Count; ++i)
		{
			GameObject temp = GameObject.Instantiate(card, Vector3.zero, Quaternion.identity);
			temp.transform.SetParent(grid.transform);

			Card tempCard = temp.GetComponent<Card>();

			tempCard.SetCard(listShieldCard[i]._iNum, listShieldCard[i]._cardType);
			_listCard.Add(tempCard);
		}

		// Heal
		for(int i=0; i<listHealCard.Count; ++i)
		{
			GameObject temp = GameObject.Instantiate(card, Vector3.zero, Quaternion.identity);
			temp.transform.SetParent(grid.transform);

			Card tempCard = temp.GetComponent<Card>();

			tempCard.SetCard(listHealCard[i]._iNum, listHealCard[i]._cardType);
			_listCard.Add(tempCard);
		}

		deckInfo.Set_DeckInfo();
	}

	public void Select_Card(Card card)
	{
		for(int i=0; i<_listCard.Count; ++i)
			_listCard[i].Emphasize_myCard(false);

		if(null != card)
			card.Emphasize_myCard(true);
	}

	public void Play_Card()
	{
		GameManager.Instance.inGameManger.Fight();
	}

	public void Activate_Selection(bool b)
	{
		objSelect.SetActive(b);
	}

	public void Reset_Select()
	{
		Activate_Selection(true);

		Select_Card(null);

		Set_DeckInfo();
		grid.Reposition();
	}

	public Card Get_SelectedCard()
	{
		Card card = null;

		for(int i=0; i<_listCard.Count; ++i)
		{
			if(_listCard[i]._bSelected == false)
				continue;

			card = _listCard[i];
			card.Emphasize_myCard(false);
			break;
		}

		return card;
	}
}
