using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager {
	static GameManager _Instance;
	public static GameManager Instance
	{
		get
		{
			if(null == _Instance)
				_Instance = new GameManager();
			return _Instance;
		}
	}

	InGameManager _inGameManger;
	public InGameManager inGameManger
	{
		get
		{
			if(null == _inGameManger)
				_inGameManger = GameObject.Find("InGameManager").GetComponent<InGameManager>();
			return _inGameManger;
		}
	}

	
}
