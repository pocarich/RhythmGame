using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    static readonly Color[] colorAccordingLevel = new Color[] { Color.blue, Color.green, Color.red };

    Image mImage;
	Text mText;
	GameObject UIMgr;
	
	// Use this for initialization
	void Start () {
		mText=GameObject.Find ("LevelButtonText").GetComponent<Text>();
		mImage = GetComponent<Image> ();
		mText.text = MainGameMgr.musicLevel.ToString();
		mImage.color = colorAccordingLevel[(int)MainGameMgr.musicLevel];
        mImage.color = new Color(mImage.color.r, mImage.color.g, mImage.color.b, 200f / 255f);

        UIMgr = GameObject.Find ("UIMgr");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void UpdateInfo(){
		mText.text = MainGameMgr.musicLevel.ToString();
		mImage.color = colorAccordingLevel[(int)MainGameMgr.musicLevel];
        mImage.color = new Color(mImage.color.r, mImage.color.g, mImage.color.b, 200f / 255f);
	}

	public void Push()
	{
		UIMgr.GetComponent<UIMgrOnSelectScene> ().PushLevelButton (0);
	}
}
