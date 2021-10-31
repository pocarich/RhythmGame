using UnityEngine;
using System.Collections;

public class Environment : SingletonMonoBehaviour<Environment>
{
    	public static bool isQuit { private set; get; } = false;
	public static float ScreenPercent { private set; get; }

	public void Awake()
	{
		if(this != Instance)
		{
			Destroy(this);
			return;
		}
		
		DontDestroyOnLoad(this.gameObject);
	}    

	// Use this for initialization
	void Start () {
		ScreenPercent = (float)Screen.width / (float)Define.baseWidth;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnApplicationQuit()
	{
		isQuit = true;
	}
}
