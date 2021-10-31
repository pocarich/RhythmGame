using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextAlpha : MonoBehaviour
{
    [SerializeField] float minusSpeed;

    GameObject gameMgr;
    Text mText;

    // Use this for initialization
    void Start () {
		mText = GetComponent<Text> ();
		mText.color= new Color (mText.color.r, mText.color.g, mText.color.b, 0.5f);
		gameMgr=GameObject.Find ("GameMgr");
	}
	
	// Update is called once per frame
	void Update () {
        if (gameMgr.GetComponent<GameMgr>().state != GameMgr.State.Game)
            return;
		if (mText.color.a > 0.5f) {
			mText.color=new Color (mText.color.r, mText.color.g, mText.color.b, Mathf.Max (0.5f,mText.color.a-minusSpeed*Time.deltaTime));
		}
	}

	public void Flash(){
		mText.color= new Color (mText.color.r, mText.color.g, mText.color.b, 1.0f);
	}
}
