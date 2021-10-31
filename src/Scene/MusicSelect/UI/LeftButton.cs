using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class LeftButton : MonoBehaviour
{
    [SerializeField] float period;

    GameObject musicSelectMgr;
    RectTransform rt;
    float speed = 0f;
    float timer = 0f;

    // Use this for initialization
    void Start()
    {
        musicSelectMgr = GameObject.Find("MusicSelectMgr");
        if (musicSelectMgr == null)
        {
            throw new Exception("MusicSelectMgrが見つかりませんでした。");
        }
        rt = gameObject.GetComponent<RectTransform>();
        speed = 15f / period;
    }

    // Update is called once per frame
    void Update()
    {
        rt.localPosition = new Vector3(rt.localPosition.x-speed*Time.deltaTime, rt.localPosition.y, rt.localPosition.z);
        timer += Time.deltaTime;
        if(timer>=period)
        {
            timer = 0f;
            rt.localPosition = new Vector3(-360f, rt.localPosition.y, rt.localPosition.z);
        }
    }

    public void PushButton()
    {
        musicSelectMgr.GetComponent<MusicSelectMgr>().PushLeft();
    }
}
