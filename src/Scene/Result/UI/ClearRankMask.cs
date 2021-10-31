using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearRankMask : MonoBehaviour
{
    [SerializeField] float moveTime;
    [SerializeField] float period;

    float timer = 0f;
    float speed = 0f;
    RectTransform rt;

    public bool animFlag { set; get; }

    // Use this for initialization
    void Start()
    {
        speed = (-100 - 60) / moveTime;
        rt = gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animFlag)
        {
            timer += Time.deltaTime;
            if (timer >= period)
            {
                timer = 0f;
                rt.localPosition = new Vector3(rt.localPosition.x, 60f, 0f);
                Debug.Log(rt.localPosition.y);

            }
            if (0f <= timer)
            {
                rt.localPosition = new Vector3(rt.localPosition.x, rt.localPosition.y + speed * Time.deltaTime, rt.localPosition.z);
            }
        }
    }
}
