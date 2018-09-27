using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyEditor : MonoBehaviour {

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
		int iHp = PlayerPrefs.GetInt("Hp", 200);
		txtHp.text = iHp.ToString();

		float fSuperior = PlayerPrefs.GetFloat("Superior", 1.5f);
		txtSuperior.text = fSuperior.ToString();
	}

	public void Goto_Ingame()
	{
		Scene_Manager.Instance.Goto_Scene("InGame");
	}

	public void Submit_Hp()
	{
		PlayerPrefs.SetInt("Hp", int.Parse(txtHp.text));
	}

	public void Submit_Superior()
	{
		PlayerPrefs.SetFloat("Superior", float.Parse(txtSuperior.text));
	}

	
}
