using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class MusicStartButton : MonoBehaviour
{
    GameObject musicSelectMgr;

	// Use this for initialization
	void Start () {
        musicSelectMgr = GameObject.Find("MusicSelectMgr");
        if(musicSelectMgr==null)
        {
            throw new Exception("MusicSelectMgrが見つかりませんでした。");
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PushButton()
    {
        musicSelectMgr.GetComponent<MusicSelectMgr>().PushGraph();
    }
}
