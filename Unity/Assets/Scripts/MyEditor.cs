using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyEditor : MonoBehaviour {

	Dictionary<string, UILabel> _dicEdit;

	public UIGrid _Grid;

	string[] _editStrings;

	UILabel _txtHp;
	UILabel txtHp
	{
		get
		{
			if(null == _txtHp)
				_txtHp = transform.Find("Edit/Input/Label").GetComponent<UILabel>();
			return _txtHp;
		}
	}

	UILabel _txtSuperior;
	UILabel txtSuperior
	{
		get
		{
			if(null == _txtSuperior)
				_txtSuperior = transform.Find("Edit1/Input/Label").GetComponent<UILabel>();
			return _txtSuperior;
		}
	}

	void Awake()
	{
		_dicEdit = new Dictionary<string, UILabel>();

		_editStrings = Global.EDIT_STRINGS;
		float[] fDefaultArr = Global.EDIT_DEFAULTS;

		GameObject objEdit = Resources.Load("Prefab/EditValue") as GameObject;

		for(int i=0; i<_editStrings.Length; ++i)
		{
			GameObject obj = GameObject.Instantiate(objEdit, transform.position, Quaternion.identity);
			obj.transform.SetParent(_Grid.transform, false);

			obj.transform.Find("Label").GetComponent<UILabel>().text = _editStrings[i];

			_dicEdit.Add(_editStrings[i], obj.transform.Find("Input/Label").GetComponent<UILabel>());

			_dicEdit[_editStrings[i]].text = PlayerPrefs.GetFloat(_editStrings[i], fDefaultArr[i]).ToString();
		}
	}

	public void Goto_Ingame()
	{
		Scene_Manager.Instance.Goto_Scene("InGame");
	}
}
