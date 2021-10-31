using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearText : MonoBehaviour
{
    [SerializeField] Sprite[] achieveGraphList;
    [SerializeField] float takeTime;
    [SerializeField] float timerFailed1;
    [SerializeField] float timerFailed2;
    [SerializeField] float takeTimeFailed;

    Image image;
    RectTransform rt;
    float timer = 0f;
    int animState = 0;
    bool firstFlag = false;
    int kind;

    public bool animationFlag { set; get; }

    // Use this for initialization
    void Start()
    {
        animationFlag = false;
        image = gameObject.GetComponent<Image>();
        rt = gameObject.GetComponent<RectTransform>();
        if (ScoreGuage.guageParsent >= Define.borderLine)
        {
            animState = 1;
        }
        else
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
        }
    }
	
	// Update is called once per frame
	void Update () {
		if(animationFlag)
        {
            switch (animState)
            {
                case 0:
                    if(timer<=timerFailed1)
                    {
                        image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.Min(1f, image.color.a + (1f / timerFailed1) * Time.deltaTime));
                    }
                    else
                    {
                        image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
                    }
                    if (timerFailed2 <= timer && timer <= takeTimeFailed) 
                    {
                        rt.localPosition = new Vector3(rt.localPosition.x, rt.localPosition.y + (175f / (takeTimeFailed-timerFailed2)) * Time.deltaTime, rt.localPosition.z);
                        rt.localScale = new Vector3(rt.localScale.x - (0.3f / (takeTimeFailed - timerFailed2)) * Time.deltaTime, rt.localScale.y - (0.3f / (takeTimeFailed - timerFailed2)) * Time.deltaTime, rt.localScale.z);
                    }
                    else if(takeTimeFailed<timer)
                    {
                        rt.localPosition = new Vector3(rt.localPosition.x, 175f, rt.localPosition.z);
                        rt.localScale = new Vector3(0.7f, 0.7f, 1.0f);
                    }
                    timer += Time.deltaTime;
                    break;
                case 1:
                    if (timer <= takeTime)
                    {
                        rt.localPosition = new Vector3(rt.localPosition.x, rt.localPosition.y + (175f / takeTime) * Time.deltaTime, rt.localPosition.z);
                        rt.localScale = new Vector3(rt.localScale.x - (0.3f / takeTime) * Time.deltaTime, rt.localScale.y - (0.3f / takeTime) * Time.deltaTime, rt.localScale.z);
                        timer += Time.deltaTime;
                    }
                    else
                    {
                        rt.localPosition = new Vector3(rt.localPosition.x, 175f, rt.localPosition.z);
                        rt.localScale = new Vector3(0.7f, 0.7f, 1.0f);
                    }
                    break;
            }
        }
    }

    public void SetSprite(int n)
    {
        image.sprite = achieveGraphList[n];
        kind = n;
    }
}
