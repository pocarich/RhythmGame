﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightMask : MonoBehaviour
{
    [SerializeField] float moveTime;
    [SerializeField] float period;

    float timer = 0f;
    float speed = 0f;
    RectTransform rt;

	// Use this for initialization
	void Start () {
        speed = (-400 - 250) / moveTime;
        rt = gameObject.GetComponent<RectTransform>();
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if(timer>=period)
        {
            timer = 0f;
            rt.localPosition = new Vector3(rt.localPosition.x, 250f, 0f);

        }
        if (0f<=timer)
        {
            rt.localPosition = new Vector3(rt.localPosition.x, rt.localPosition.y + speed*Time.deltaTime, rt.localPosition.z);
        }
	}
}
