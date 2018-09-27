using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditValue : MonoBehaviour {

	public void Submit_Value(GameObject edit)
	{
		string name = edit.transform.Find("Label").GetComponent<UILabel>().text;
		float value = float.Parse(edit.transform.Find("Input/Label").GetComponent<UILabel>().text);

		PlayerPrefs.SetFloat(name, value);
	}
}
