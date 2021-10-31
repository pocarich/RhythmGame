using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class JudgeText : MonoBehaviour
{
	public enum JudgeType
	{
		PERFECT,
		GREAT,
		SAFE,
		BAD,
		MISS
	}

    [SerializeField] float ShowTime;

    Text mText;
	TextAlpha mTextAlpha;
	GameObject gameMgr;
	float timer=0.0f;
	
	// Use this for initialization
	void Start () {
		mText = GetComponent<Text> ();
		mTextAlpha = GetComponent<TextAlpha> ();
		gameMgr=GameObject.Find ("GameMgr");
	}
	
	// Update is called once per frame
	void Update () {
        if (gameMgr.GetComponent<GameMgr>().state != GameMgr.State.Game)
            return;
		if (timer >= ShowTime) {
			mText.text="";
		}
		timer += Time.deltaTime;
	}
	
	public void UpdateJudge(JudgeType judgeType)
	{
		timer = 0.0f;
		mText.text = judgeType.ToString();
		mTextAlpha.Flash ();
	}
}
