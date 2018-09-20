using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelect : MonoBehaviour {

	Card[] _selectCard;
	public Card[] selectCard
	{
		get
		{
			if(null == _selectCard)
				_selectCard = new Card[10];

			return _selectCard;
		}
	}

	public void Start_CardSelect()
	{
		gameObject.SetActive(true);

		GameObject card = Resources.Load("Prefab/Card") as GameObject;
		Transform grid = transform.Find("AnchorC/Grid");

		for(int i=0; i<selectCard.Length; ++i)
		{
			GameObject temp = GameObject.Instantiate(card, Vector3.zero, Quaternion.identity);
			temp.transform.SetParent(grid);

			selectCard[i] = temp.GetComponent<Card>();
			selectCard[i].SetCard(i+1);
		}
	}

	public int Get_SelectedCardCount()
	{
		int iCount = 0;

		for(int i=0; i<selectCard.Length; ++i)
		{
			if(selectCard[i]._cardType != CARD_TYPE.END)
				iCount++;
		}

		return iCount;
	}

	public void Show_Select(Card card)
	{
		for(int i=0; i<selectCard.Length; ++i)
		{
			selectCard[i].Show_Select(false);
		}

		card.Show_Select(true);
	}

	public void Play_Game()
	{
		for(int i=0; i<selectCard.Length; ++i)
		{
			// 선택되지 않은 카드는 랜덤지정
			if(selectCard[i]._cardType == CARD_TYPE.END)
				selectCard[i]._cardType = (CARD_TYPE)Random.Range(0, (int)CARD_TYPE.END);
		}

		GameManager.Instance.inGameManger.Start_Game();

	}
}
