using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ScoreText : MonoBehaviour
{
	static readonly float[] ratio = {1.3f,1f,0.5f,0.3f,0f};

	Text mText;

	public float score{ private set; get;}

	// Use this for initialization
	void Start () {
		score = 0f;
		mText = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		mText.text = "SCORE:" + String.Format ("{0:#,0}",((int)score));
	}

	public void UpdateScore(Define.JudgeType judgeType,int combo){
        score += (Define.baseScore * ratio[(int)judgeType]);
	}
}
