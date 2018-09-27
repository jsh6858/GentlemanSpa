using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manager {

	static Scene_Manager _Instance;
	public static Scene_Manager Instance
	{
		get
		{
			if(null == _Instance)
				_Instance = new Scene_Manager();
			return _Instance;
		}
	}

	public void Goto_Scene(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}	
}
