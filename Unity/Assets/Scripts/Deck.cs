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
		_iHp = 300;
		txtHp.text = _iHp.ToString();
	}

	public void Set_DeckInfo()
	{
		deckInfo.Set_DeckInfo();
	}

	public void Set_Hp(int iHp)
	{
		StartCoroutine(Start_HpAnim(iHp));
	}

	IEnumerator Start_HpAnim(int iHp)
	{
		int iHpSrc = _iHp;
		int iHpDst = _iHp + iHp;

		int iHpDir = iHp / Mathf.Abs(iHp); // 1 or -1

		while(true)
		{
			iHpSrc += iHpDir;

			txtHp.text = iHpSrc.ToString();

			if(iHpSrc == iHpDst)
				break;

			yield return null;
		}

		_iHp = iHpDst;

		yield break;
	}
}
