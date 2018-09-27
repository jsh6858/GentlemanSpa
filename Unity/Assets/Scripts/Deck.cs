using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour {

	public int _iHp;
	 UILabel _txtHp;
	public UILabel txtHp
	{
		get
		{
			if(null == _txtHp)
			{
				_txtHp = transform.Find("Anchor/Hp/Label").GetComponent<UILabel>();
			}
				
			return _txtHp;
		}
	}

	public List<Card> _listCard = new List<Card>();

	UIGrid _grid;
	public UIGrid grid
	{
		get
		{
			if(null == _grid)
				_grid = transform.Find("Anchor/Grid").GetComponent<UIGrid>();
			return _grid;
		}
	}

	DeckInfo _deckInfo;
	public DeckInfo deckInfo
	{
		get
		{
			if(null == _deckInfo)
				_deckInfo = transform.Find("Anchor/DeckInfo").GetComponent<DeckInfo>();
			return _deckInfo;
		}
	}

	void Awake()
	{
	}

	public void Set_DeckInfo()
	{
		deckInfo.Set_DeckInfo();
	}

	public void Set_Hp(int iHp)
	{
		StartCoroutine(Start_HpAnim(iHp));
	}

	// 시간 초과로 가장 낮은 숫자의 카드를 뽑음
	public Card Get_LowestCard()
	{
		Card card = _listCard[0];
		int iNum = 10;

		for(int i=0; i<_listCard.Count; ++i)
		{
			if(_listCard[i]._iNum < iNum)
			{
				iNum = _listCard[i]._iNum;
				card = _listCard[i];
			}
		}

		return card;
	}

	IEnumerator Start_HpAnim(int iHp)
	{
		float iHpSrc = _iHp;
		int iHpDst = _iHp + iHp;

		float fTime = 1f;

		while(true)
		{
			fTime -= Time.deltaTime;
			if(fTime < 0f)
				break;

			iHpSrc += iHp * Time.deltaTime;

			txtHp.text = ((int)iHpSrc).ToString();

			yield return null;
		}

		_iHp = iHpDst;
		txtHp.text = ((int)_iHp).ToString();

		yield break;
	}
}
