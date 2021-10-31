using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ComboText : MonoBehaviour
{
	public enum UpdateType
	{
		ADD,
		ZERO
	}

	[SerializeField] float ShowTime=1.0f;

	GameObject gameMgr;
    Text mText;
    TextAlpha mTextAlpha;
    float timer=0.0f;

	public int combo{ private set; get;}
	public int maxCombo{ private set; get;}

	// Use this for initialization
	void Start () {
		mTextAlpha = GetComponent<TextAlpha> ();
		mText = GetComponent<Text> ();
		combo = 0;
		maxCombo = 0;
		gameMgr=GameObject.Find ("GameMgr");
	}
	
	// Update is called once per frame
	void Update () {
        if (gameMgr.GetComponent<GameMgr>().state != GameMgr.State.Game)
            return;
		if (combo > 0)
			mText.text = combo.ToString ();
		else
			mText.text = "";

		if (timer >= ShowTime) {
			mText.text="";
		}
		timer += Time.deltaTime;
	}

	public void UpdateCombo(UpdateType updateType){
		timer = 0.0f;
		switch (updateType) {
		case UpdateType.ADD:
			combo++;
			mTextAlpha.Flash();
			break;
		case UpdateType.ZERO:
			maxCombo=Mathf.Max (maxCombo,combo);
			combo=0;
			break;
		}
	}
}
