using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ClearParam : MonoBehaviour
{
    [SerializeField] float takeTime;
    [SerializeField] bool commaSplitFlag;

    Text text;
    float timer = 0f;
    float tempValue = 0f;
    bool firstFlag = false;

    public int maxValue { set; get; }
    public bool animationFlag { set; get; }

    // Use this for initialization
    void Start () {
        animationFlag = false;
        text = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		if(animationFlag)
        {
            if(!firstFlag)
            {
                GameObject.Find("SoundPlayer").GetComponent<SoundPlayerClear>().PlaySound(1);
                firstFlag = true;
            }
            if (timer<=takeTime)
            {
                int preValue = (int)tempValue;
                tempValue += (maxValue / takeTime) * Time.deltaTime;
                if(preValue!=(int)tempValue)
                {
                    GameObject.Find("SoundPlayer").GetComponent<SoundPlayerClear>().PlaySound(1);
                }
                if (commaSplitFlag)
                {
                    text.text = String.Format("{0:#,0}", (int)tempValue);
                }
                else
                {
                    text.text = (int)tempValue + "";
                }
                timer += Time.deltaTime;
            }
            else
            {
                if (commaSplitFlag)
                {
                    text.text = String.Format("{0:#,0}", maxValue);
                }
                else
                {
                    text.text = maxValue + "";
                }
            }
        }
	}
}
