using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour {

	CardSelect _cardSelect;
	public CardSelect cardSelect
	{
		set
		{
			_cardSelect = value;
		}
		get
		{
			if(null == _cardSelect)
				_cardSelect = GameObject.Instantiate((Resources.Load("Prefab/CardSelect") as GameObject)).GetComponent<CardSelect>();
			return _cardSelect;
		}
	}

	MyDeck _myDeck;
	public MyDeck myDeck
	{
		set
		{
			_myDeck = value;
		}
		get
		{
			if(null == _myDeck)
				_myDeck = GameObject.Instantiate((Resources.Load("Prefab/MyDeck") as GameObject)).GetComponent<MyDeck>();
			return _myDeck;
		}
	}

	YourDeck _yourDeck;
	public YourDeck yourDeck
	{
		set
		{
			_yourDeck = value;
		}
		get
		{
			if(null == _yourDeck)
				_yourDeck = GameObject.Instantiate((Resources.Load("Prefab/YourDeck") as GameObject)).GetComponent<YourDeck>();
			return _yourDeck;
		}
	}

	BattleField _battleField;
	public BattleField battleField
	{
		set
		{
			_battleField = value;
		}
		get
		{
			if(null == _battleField)
				_battleField = GameObject.Instantiate((Resources.Load("Prefab/BattleField") as GameObject)).GetComponent<BattleField>();
			return _battleField;
		}
	}

	void Awake()
	{
		Start_CardSelect();
	}	

	public void Start_CardSelect()
	{
		Global._gameMode = GAME_MODE.CARD_SELECT;

		DestroyAll();

		cardSelect.Start_CardSelect();
	}

	public void Start_Game()
	{
		Global._gameMode = GAME_MODE.BATTLE;

		Destroy(myDeck.gameObject);
		myDeck = null;

		myDeck.Set_Deck(cardSelect.selectCard); // 덱 복사
		
		Destroy(cardSelect.gameObject);

		yourDeck.Set_YourDeck_AI(); // AI 덱 생성
		
		
	}

	public void Fight()
	{
		battleField.Set_Battle(myDeck, yourDeck);
	}

	void DestroyAll()
	{
		Destroy(cardSelect.gameObject);
		cardSelect = null;
		Destroy(myDeck.gameObject);
		myDeck = null;
		Destroy(yourDeck.gameObject);
		yourDeck = null;
	}
}
