using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTitle : MonoBehaviour
{
    [SerializeField] float period;

    Text text;
    float timer = 0f;
    int turn = 0;

	// Use this for initialization
	void Start () {
        text = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if(timer>=period)
        {
            timer = 0f;
            turn = (turn + 1) % 2;
            if(text.text=="")
            {
                text.text = "- PUSH ANY BUTTON TO START -";
            }
            else
            {
                text.text = "";
            }
        }
	}
}
