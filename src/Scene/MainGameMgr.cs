using UnityEngine;
using System.Collections;

public class MainGameMgr : SingletonMonoBehaviour<MainGameMgr>
{
	public static int musicNum { set; get; } = 0;
	public static Define.MusicLevel musicLevel { set; get; } = Define.MusicLevel.EASY;
	public static int speedLevel { set; get; } = 5;

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
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
