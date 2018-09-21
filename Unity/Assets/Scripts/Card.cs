using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {

	readonly string[] _strAttribute = new string[4] {"Energy_Orange", "Shield", "Potion", "Item_Base"};

	public bool _bSelected = false;

	public int _iNum = 0;
	public CARD_TYPE _cardType = CARD_TYPE.END;
	UILabel _txtNum;
	UILabel txtNum
	{
		get
		{
			if(null == _txtNum)
				_txtNum = transform.Find("Front/Label").GetComponent<UILabel>();
			return _txtNum;
		}
	}
	UISprite _sprAttribute;
	UISprite sprAttribute
	{
		get
		{
			if(null == _sprAttribute)
				_sprAttribute = transform.Find("Front/Sprite").GetComponent<UISprite>();
			return _sprAttribute;
		}
	}
	GameObject _objSelect;
	GameObject objSelect
	{
		get
		{
			if(null == _objSelect)
				_objSelect = transform.Find("Select").gameObject;

			return _objSelect;
		}
	}

	GameObject _objFront;
	GameObject objFront
	{
		get
		{
			if(null == _objFront)
				_objFront = transform.Find("Front").gameObject;
			return _objFront;
		}
	}
	GameObject _objBack;
	GameObject objBack
	{
		get
		{
			if(null == _objBack)
				_objBack = transform.Find("Back").gameObject;
			return _objBack;
		}
	}

	void Awake()
	{
	}

	public void SetCard(int iNum, CARD_TYPE cardType = CARD_TYPE.END)
	{
		_iNum = iNum;
		txtNum.text = _iNum.ToString();

		_cardType = cardType;
		sprAttribute.spriteName = _strAttribute[(int)_cardType];
	}
	
	// 카드 선택되었을 때
	public void Select_Card()
	{
		// 선택 모드일 때
		if(Global._gameMode == GAME_MODE.CARD_SELECT)
			GameManager.Instance.inGameManger.cardSelect.Show_Select(this);

		// 배틀 모드일 때	
		else
			GameManager.Instance.inGameManger.myDeck.Select_Card(this);
	}

	// Select중 커지기
	public void Show_Select(bool bShow)
	{
		if(bShow && !_bSelected)
		{
			transform.localPosition = transform.localPosition + new Vector3(0f, 13f, 0f);
			transform.localScale = new Vector3(1.4f, 1.4f, 1f);
		}
		else if(!bShow && _bSelected)
		{
			transform.localPosition = transform.localPosition + new Vector3(0f, -13f, 0f);
			transform.localScale = new Vector3(1f, 1f, 1f);
		}

		objSelect.SetActive(bShow);

		_bSelected = bShow;
	}

	public void Emphasize_myCard(bool bEmphasize)
	{
		if(bEmphasize && !_bSelected)
		{
			transform.localPosition = transform.localPosition + new Vector3(0f, 13f, 0f);
			transform.localScale = new Vector3(1.4f, 1.4f, 1f);
		}
		else if(!bEmphasize && _bSelected)
		{
			transform.localPosition = transform.localPosition + new Vector3(0f, -13f, 0f);
			transform.localScale = new Vector3(1f, 1f, 1f);
		}

		_bSelected = bEmphasize;
	}

	public void Select_Attribute(GameObject obj)
	{
		switch(obj.name)
		{
			case "Attack":
				_cardType = CARD_TYPE.ATTACK;
				break;

			case "Shield":
				_cardType = CARD_TYPE.SHIELD;
				break;

			case "Heal":
				_cardType = CARD_TYPE.HEAL;
				break;
			
			default:
				_cardType = CARD_TYPE.END;
				break;
		}

		sprAttribute.spriteName = _strAttribute[(int)_cardType];

		Show_Select(false);
	}
	
	public void Reverse_Card()
	{
		objFront.SetActive(!objFront.activeSelf);
		objBack.SetActive(!objBack.activeSelf);
	}
}
