using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsefulFunction : MonoBehaviour {

	public static void Shuffle(int[] iArr)
	{
		int iTemp = 0;

		for(int i=0; i<100; ++i)
		{
			int iRand1 = Random.Range(0, iArr.Length);
			int iRand2 = Random.Range(0, iArr.Length);

			iTemp = iArr[iRand1];
			iArr[iRand1] = iArr[iRand2];
			iArr[iRand2] = iTemp;
		}
	}


}
