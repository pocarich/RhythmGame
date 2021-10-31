using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicInfoPanelOnGameScene : MonoBehaviour
{
    [SerializeField] float dispTime;
    [SerializeField] float fadeTime;

    GameObject musicInfo;
    GameObject musicLevel;
    GameObject musicGraph;
    
    float timer = 0f;
    float alpha = 0f;

    public bool endFlag { private set; get; }

	// Use this for initialization
	void Start () {
        musicInfo = GameObject.Find("MusicInfo");
        musicLevel = GameObject.Find("MusicLevel");
        musicGraph = GameObject.Find("MusicGraph");

        musicInfo.GetComponent<Text>().text = MusicList.GetMusicInfoList()[MainGameMgr.musicNum].musicName;
        musicLevel.GetComponent<Text>().text = MainGameMgr.musicLevel + "";
        musicGraph.GetComponent<Image>().sprite= MusicList.spriteList[MainGameMgr.musicNum];

        endFlag = false;
        alpha = 1f / fadeTime;
    }
	
	// Update is called once per frame
	void Update () {
        if(timer<=fadeTime)
        {
            musicInfo.GetComponent<Text>().color = new Color(255f, 255f, 255f, Mathf.Min(1f,musicInfo.GetComponent<Text>().color.a + alpha * Time.deltaTime));
            musicLevel.GetComponent<Text>().color = new Color(255f, 255f, 255f, Mathf.Min(1f, musicLevel.GetComponent<Text>().color.a + alpha * Time.deltaTime));
            musicGraph.GetComponent<Image>().color = new Color(255f, 255f, 255f, Mathf.Min(1f,musicGraph.GetComponent<Image>().color.a + alpha * Time.deltaTime));
        }
        if(fadeTime<=timer&&timer<= (dispTime - fadeTime))
        {
            musicInfo.GetComponent<Text>().color = new Color(255f, 255f, 255f, 1f);
            musicLevel.GetComponent<Text>().color = new Color(255f, 255f, 255f, 1f);
            musicGraph.GetComponent<Image>().color = new Color(255f, 255f, 255f, 1f);
        }
        if ((dispTime - fadeTime) <= timer)
        {
            musicInfo.GetComponent<Text>().color = new Color(255f, 255f, 255f, Mathf.Max(0f, musicInfo.GetComponent<Text>().color.a - alpha * Time.deltaTime));
            musicLevel.GetComponent<Text>().color = new Color(255f, 255f, 255f, Mathf.Max(0f, musicLevel.GetComponent<Text>().color.a - alpha * Time.deltaTime));
            musicGraph.GetComponent<Image>().color = new Color(255f, 255f, 255f, Mathf.Max(0f, musicGraph.GetComponent<Image>().color.a - alpha * Time.deltaTime));
        }
        if(dispTime<=timer)
        {
            gameObject.GetComponent<Image>().color = new Color(0f, 0f, 0f, Mathf.Max(0f, gameObject.GetComponent<Image>().color.a - alpha * Time.deltaTime));
        }
        if((dispTime+fadeTime)<=timer)
        {
            endFlag = true;
        }
        timer += Time.deltaTime;
	}
}
