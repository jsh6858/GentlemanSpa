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

	int[] _randomNumbers;
	int _iBattleRound = 0;

	UILabel _txtRandomNumber;
	UILabel txtRandomNumber
	{
		get
		{
			if(null == _txtRandomNumber)
				_txtRandomNumber = transform.Find("AnchorC/RandomNumber/Label").GetComponent<UILabel>();
			return _txtRandomNumber;
		}
	}

	GameObject _objRandomNumber;
	GameObject objRandomNumber
	{
		get
		{
			if(null == _objRandomNumber)
				_objRandomNumber = transform.Find("AnchorC/RandomNumber").gameObject;
			return _objRandomNumber;
		}
	}

	public UIGrid _grid;
	public UIScrollView _scrollview;

	void Awake()
	{
		_vMyPos = transform.Find("AnchorC/MyPos").position;
		_vYourPos = transform.Find("AnchorC/YourPos").position;

		_randomNumbers = new int[10] { 11, 12, 13, 14, 15, 16, 17, 18, 19, 20};
		UsefulFunction.Shuffle(_randomNumbers);
	}

	public void Set_Battle(MyDeck myDeck, YourDeck yourDeck)
	{
		if(_iBattleRound == 10)
			return;

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

		// 카드가 목표지점까지 도착하기를 기다린다
		while(true)
		{
			if(_bMyCardSet && _bYourCardSet)
				break;

			yield return null;
		}

		// 상대편 카드 확인!
		yourCard.Reverse_Card();

		StartCoroutine(Show_RandomNumber());

		yield return new WaitForSeconds(4f);

		Fight_Result(myCard, yourCard);

		yield return new WaitForSeconds(1f);

		// Gird Reset, Deck Info Set...
		_myDeck.Reset_Select();
		_yourDeck.Reset_Select();

		objRandomNumber.SetActive(false);

		yield break;
	}

	// CARD_TYPE.ATTACK, CARD_TYPE.SHIELD, CARD_TYPE.HEAL
	void Fight_Result(Card myCard, Card yourCard)
	{
		CARD_TYPE myType = myCard._cardType;
		CARD_TYPE yourType = yourCard._cardType;

		int iMyNum = myCard._iNum;
		int iYourNum = yourCard._iNum;

		int iRandNum = _randomNumbers[_iBattleRound];

		float fSuperior = PlayerPrefs.GetFloat("Superior", 1.5f);

		if(myType == CARD_TYPE.ATTACK) // Attack
		{

			if(yourType == CARD_TYPE.ATTACK) // Attack
			{
				_myDeck.Set_Hp(-iYourNum * iRandNum);
				_yourDeck.Set_Hp(-iMyNum * iRandNum);
			}
			else if(yourType == CARD_TYPE.SHIELD) // SHIELD
			{
				if(iMyNum > iYourNum)
					_myDeck.Set_Hp(-(int)(iYourNum * iRandNum * fSuperior));
				else
					_myDeck.Set_Hp(-(int)(iMyNum * iRandNum * fSuperior));
			}
			else if(yourType == CARD_TYPE.HEAL) // HEAL
			{
				_yourDeck.Set_Hp(-(int)(iMyNum * iRandNum * fSuperior));
			}
		}

		else if(myType == CARD_TYPE.SHIELD) // SHIELD
		{

			if(yourType == CARD_TYPE.ATTACK) // Attack
			{
				if(iMyNum > iYourNum)
					_yourDeck.Set_Hp(-(int)(iYourNum * iRandNum * fSuperior));
				else
					_yourDeck.Set_Hp(-(int)(iMyNum * iRandNum * fSuperior));
			}
			else if(yourType == CARD_TYPE.SHIELD) // SHIELD
			{
				//
			}
			else if(yourType == CARD_TYPE.HEAL) // HEAL
			{
				_yourDeck.Set_Hp((int)(iYourNum * iRandNum * fSuperior));
			}
		}

		else if(myType == CARD_TYPE.HEAL) // HEAL
		{

			if(yourType == CARD_TYPE.ATTACK) // Attack
			{
				_myDeck.Set_Hp(-(int)(iYourNum * iRandNum * fSuperior));
			}
			else if(yourType == CARD_TYPE.SHIELD) // SHIELD
			{
				_myDeck.Set_Hp((int)(iMyNum * iRandNum * fSuperior));
			}
			else if(yourType == CARD_TYPE.HEAL) // HEAL
			{
				_myDeck.Set_Hp(iMyNum * iRandNum);
				_yourDeck.Set_Hp(iYourNum * iRandNum);
			}
		}

		_myDeck._listCard.Remove(myCard);
		Destroy(myCard.gameObject);

		_yourDeck._listCard.Remove(yourCard);
		Destroy(yourCard.gameObject);

		_iBattleRound++;

		Create_BattleInfo(myType, iMyNum, yourType, iYourNum);
	}

	void Create_BattleInfo(CARD_TYPE myType, int iMyNum, CARD_TYPE yourType, int iYourNum)
	{
		GameObject obj = GameObject.Instantiate(Resources.Load("Prefab/BattleInfo") as GameObject, Vector3.zero, Quaternion.identity);

		obj.transform.SetParent(_grid.transform);
		obj.transform.localScale = new Vector3(1f, 1f, 1f);
		obj.transform.SetAsFirstSibling();

		_grid.Reposition();
		_scrollview.ResetPosition();

		BattleInfo battleInfo = obj.GetComponent<BattleInfo>();
		battleInfo.Set_BattleInfo(myType, iMyNum, yourType, iYourNum);

	}

	IEnumerator Show_RandomNumber()
	{
		objRandomNumber.SetActive(true);

		float fTime = 1f;
		int iTemp = 0;

		int[] iArr = new int[10] {11,12,13,14,15,16,17,18,19,20};

		while(true)
		{
			fTime -= Time.deltaTime;
			iTemp = (iTemp + 1) % iArr.Length;

			if(fTime < 0f)
				break;

			txtRandomNumber.text = iArr[iTemp].ToString();

			yield return null;
		}

		txtRandomNumber.text = _randomNumbers[_iBattleRound].ToString();
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
