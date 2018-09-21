using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleField : MonoBehaviour {

	Vector3 _vMyPos;
	Vector3 _vYourPos;

	bool _bMyCardSet = false;
	bool _bYourCardSet = false;

	MyDeck _myDeck;
	YourDeck _yourDeck;

	void Awake()
	{
		_vMyPos = transform.Find("AnchorC/MyPos").position;
		_vYourPos = transform.Find("AnchorC/YourPos").position;

		Debug.Log(transform.Find("AnchorC/MyPos").localPosition);
		Debug.Log(transform.Find("AnchorC/MyPos").position);
	}

	public void Set_Battle(MyDeck myDeck, YourDeck yourDeck)
	{
		_myDeck = myDeck;
		_yourDeck = yourDeck;

		// 내 카드
		Card myCard = myDeck.Get_SelectedCard();
		if(null == myCard)
			return;

		// 니 카드 
		Card yourCard = yourDeck.Get_SelectedCard_AI();

		StartCoroutine(Battle_Start(myCard, yourCard));
	}

	IEnumerator Battle_Start(Card myCard, Card yourCard)
	{
		_myDeck.Activate_Selection(false);

		_bMyCardSet = false;
		_bYourCardSet = false;
		
		StartCoroutine(Move_MyCard(myCard, _vMyPos));
		StartCoroutine(Move_YourCard(yourCard, _vYourPos));

		while(true)
		{
			if(_bMyCardSet && _bYourCardSet)
				break;

			yield return null;
		}

		yourCard.Reverse_Card();

		yield return new WaitForSeconds(2f);

		_myDeck._listCard.Remove(myCard);
		Destroy(myCard.gameObject);

		_yourDeck._listCard.Remove(yourCard);
		Destroy(yourCard.gameObject);

		yield return new WaitForSeconds(1f);

		_myDeck.Reset_Select();
		_yourDeck.Reset_Select();

		yield break;
	}

	IEnumerator Move_MyCard(Card card, Vector3 vPos)
	{
		Vector3 vDir = vPos - card.transform.position;
		vDir.Normalize();

		while(true)
		{
			card.transform.Translate(vDir * Time.deltaTime);

			if(Vector3.Distance(vPos, card.transform.position) < 0.01f)
				break;

			yield return null;
		}

		_bMyCardSet = true;
		yield break;
	}

	IEnumerator Move_YourCard(Card card, Vector3 vPos)
	{
		Vector3 vDir = vPos - card.transform.position;
		vDir.Normalize();

		while(true)
		{
			card.transform.Translate(vDir * Time.deltaTime);

			if(Vector3.Distance(vPos, card.transform.position) < 0.01f)
				break;

			yield return null;
		}

		_bYourCardSet = true;
		yield break;
	}
}
