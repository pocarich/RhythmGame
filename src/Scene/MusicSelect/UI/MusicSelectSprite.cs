using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MusicSelectSprite : MonoBehaviour
{
    [SerializeField] float startDispTime;
    [SerializeField] float endDispTime;
    [SerializeField] float maxAlpha;

    Image mImage;
    float timer = 0f;
    float alpha = 0f;

	// Use this for initialization
	void Start () {
		mImage = GetComponent<Image> ();
		mImage.sprite = MusicList.spriteList[MainGameMgr.musicNum];
	}
	
	// Update is called once per frame
	void Update () {
		if(timer<(startDispTime+endDispTime))
        {
            timer += Time.deltaTime;
        }
        else
        {
            alpha = maxAlpha;
        }
        if(startDispTime<=timer&&timer<=(startDispTime+endDispTime))
        {
            alpha += maxAlpha / endDispTime * Time.deltaTime;
            alpha = Mathf.Min(maxAlpha, alpha);
        }
        mImage.color = new Color(255f, 255f, 255f, alpha);
	}

	public void UpdateSprite()
	{
		mImage.sprite = MusicList.spriteList[MainGameMgr.musicNum];
        timer = 0f;
        alpha = 0f;
	}
}
