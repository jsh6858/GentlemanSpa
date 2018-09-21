using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour {
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

	public void Set_DeckInfo()
	{
		deckInfo.Set_DeckInfo();
	}
}
