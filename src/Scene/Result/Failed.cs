using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Failed : MonoBehaviour
{
    static readonly string[] achieveMessage = { "", "", "FULLCOMBO!", "ALLPERFECT!!" };

    GameObject back;
	GameObject score;
	GameObject perfect;
	GameObject great;
	GameObject safe;
	GameObject bad;
	GameObject miss;
	GameObject maxCombo;
		
	// Use this for initialization
	void Start () {
		back=GameObject.Find ("Back");
		if (back == null) {
			throw new Exception("Backが見つかりませんでした。");
		}
	
		score=GameObject.Find ("Score");
		if (score == null) {
			throw new Exception("Scoreが見つかりませんでした。");
		}
		
		perfect=GameObject.Find ("Perfect");
		if (perfect == null) {
			throw new Exception("Perfectが見つかりませんでした。");
		}
		
		great=GameObject.Find ("Great");
		if (great == null) {
			throw new Exception("Greatが見つかりませんでした。");
		}
		
		safe=GameObject.Find ("Safe");
		if (safe == null) {
			throw new Exception("Safeが見つかりませんでした。");
		}
		
		bad=GameObject.Find ("Bad");
		if (bad == null) {
			throw new Exception("Badが見つかりませんでした。");
		}
		
		miss=GameObject.Find ("Miss");
		if (miss == null) {
			throw new Exception("Missが見つかりませんでした。");
		}
		
		maxCombo=GameObject.Find ("MaxCombo");
		if (maxCombo == null) {
			throw new Exception("MaxComboが見つかりませんでした。");
		}
		
		SetText ();
	}
	
	// Update is called once per frame
	void Update () {
		switch (Define.platform) {
		case Define.Platform.EDITOR:
			if (Input.GetMouseButtonDown (0))
				Application.LoadLevel ("MusicSelectScene");
			break;
		case Define.Platform.ANDROID:
			if (Input.touches [0].phase == TouchPhase.Ended)
				Application.LoadLevel ("MusicSelectScene");
			break;
		}
	}
	
	void SetText(){
		back.GetComponent<Image> ().sprite = MusicList.spriteList [MainGameMgr.musicNum];
		score.GetComponent<Text> ().text = String.Format ("{0:#,0}",((int)ScoreBoard.score));
		perfect.GetComponent<Text> ().text = ScoreBoard.excellentCounter+"";
		great.GetComponent<Text> ().text = ScoreBoard.greatCounter+"";
		safe.GetComponent<Text> ().text = ScoreBoard.safeCounter+"";
		bad.GetComponent<Text> ().text = ScoreBoard.badCounter+"";
		miss.GetComponent<Text> ().text = ScoreBoard.missCounter+"";
		maxCombo.GetComponent<Text> ().text = ScoreBoard.maxCombo+"";
	}
}
