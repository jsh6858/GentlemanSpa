using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyDeck : MonoBehaviour {

	List<Card> _listCard = new List<Card>();

	public void Set_Deck(Card[] selectCard)
	{
		GameObject card = Resources.Load("Prefab/Card") as GameObject;
		Transform grid = transform.Find("AnchorB/Grid");

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
			temp.transform.SetParent(grid);

			Card tempCard = temp.GetComponent<Card>();

			tempCard.SetCard(listAttackCard[i]._iNum, listAttackCard[i]._cardType);
			_listCard.Add(tempCard);
		}

		// Shield
		for(int i=0; i<listShieldCard.Count; ++i)
		{
			GameObject temp = GameObject.Instantiate(card, Vector3.zero, Quaternion.identity);
			temp.transform.SetParent(grid);

			Card tempCard = temp.GetComponent<Card>();

			tempCard.SetCard(listShieldCard[i]._iNum, listShieldCard[i]._cardType);
			_listCard.Add(tempCard);
		}

		// Heal
		for(int i=0; i<listHealCard.Count; ++i)
		{
			GameObject temp = GameObject.Instantiate(card, Vector3.zero, Quaternion.identity);
			temp.transform.SetParent(grid);

			Card tempCard = temp.GetComponent<Card>();

			tempCard.SetCard(listHealCard[i]._iNum, listHealCard[i]._cardType);
			_listCard.Add(tempCard);
		}
	}


}
