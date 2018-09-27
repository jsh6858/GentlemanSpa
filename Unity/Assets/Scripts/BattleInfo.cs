using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleInfo : MonoBehaviour {

	readonly string[] _strAttribute = new string[4] {"Energy_Orange", "Shield", "Potion", "Item_Base"};
	UISprite _sprMyAttribute;
	UISprite sprMyAttribute
	{
		get
		{
			if(null == _sprMyAttribute)
				_sprMyAttribute = transform.Find("MyInfo/Sprite").GetComponent<UISprite>();
			return _sprMyAttribute;
		}
	}

	UILabel _txtMyNum;
	UILabel txtMyNum
	{
		get
		{
			if(null == _txtMyNum)
				_txtMyNum = transform.Find("MyInfo/Label").GetComponent<UILabel>();
			return _txtMyNum;
		}
	}

	UISprite _sprYourAttribute;
	UISprite sprYourAttribute
	{
		get
		{
			if(null == _sprYourAttribute)
				_sprYourAttribute = transform.Find("YourInfo/Sprite").GetComponent<UISprite>();
			return _sprYourAttribute;
		}
	}

	UILabel _txtYourNum;
	UILabel txtYourNum
	{
		get
		{
			if(null == _txtYourNum)
				_txtYourNum = transform.Find("YourInfo/Label").GetComponent<UILabel>();
			return _txtYourNum;
		}
	}

	UILabel _txtRandNum;
	UILabel txtRandNum
	{
		get
		{
			if(null == _txtRandNum)
				_txtRandNum = transform.Find("RandNum").GetComponent<UILabel>();
			return _txtRandNum;
		}
	}

	public void Set_BattleInfo(CARD_TYPE myType, int iMyNum, CARD_TYPE yourType, int iYourNum, int RandNum)
	{
		sprMyAttribute.spriteName = _strAttribute[(int)myType];
		txtMyNum.text = iMyNum.ToString();

		sprYourAttribute.spriteName = _strAttribute[(int)yourType];
		txtYourNum.text = iYourNum.ToString();

		txtRandNum.text = RandNum.ToString();
	}

}
