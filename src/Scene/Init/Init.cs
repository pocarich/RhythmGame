using UnityEngine;
using System.Collections;
using System;

public class Init : MonoBehaviour
{
	MusicList musicList;

	// Use this for initialization
	void Start () {
		musicList=GameObject.Find ("MusicList").GetComponent<MusicList>();
		if (musicList == null) {
			throw new Exception("MusicListが見つかりませんでした。");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(CheckLoad())Application.LoadLevel("Title");
	}

	bool CheckLoad()
	{
		return musicList.loadedFlag;
	}
}
