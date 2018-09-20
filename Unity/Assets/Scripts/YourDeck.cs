using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YourDeck : MonoBehaviour {

	List<Card> _listCard = new List<Card>();
	public void Set_YourDeck_AI()
	{
		GameObject card = Resources.Load("Prefab/Card") as GameObject;
		Transform grid = transform.Find("AnchorT/Grid");

		int[] iArray = new int[10] { 1,2,3,4,5,6,7,8,9,10};
		UsefulFunction.Shuffle(iArray);

		for(int i=0; i<10; ++i)
		{
			GameObject temp = GameObject.Instantiate(card, Vector3.zero, Quaternion.identity);
			temp.transform.SetParent(grid);

			Card tempCard = temp.GetComponent<Card>();

			tempCard.SetCard(iArray[i], (CARD_TYPE)Random.Range(0, (int)CARD_TYPE.END));

			_listCard.Add(tempCard);
		}

		
	}
}
