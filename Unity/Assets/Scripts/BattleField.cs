using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleField : MonoBehaviour {

	Vector3 _vMyPos;
	Vector3 _vYourPos;

	void Awake()
	{
		_vMyPos = transform.Find("AnchorC/MyPos").localPosition;
		_vYourPos = transform.Find("AnchorC/YourPos").localPosition;
	}

	public void Set_Battle(Card myCard, Cache yourCard)
	{

	}

	IEnumerator Move_Card(Card card, Vector3 vPos)
	{
		while(true)
		{
			

			yield return null;
		}

		yield break;
	}
}
