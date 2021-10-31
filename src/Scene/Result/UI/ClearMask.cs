using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearMask : MonoBehaviour
{
    [SerializeField] float takeTime;

    Image image;
    float timer = 0f;
    int animState = 0;
    bool firstFlag = false;

    public bool animationFlag { set; get; }

    // Use this for initialization
    void Start()
    {
        image = gameObject.GetComponent<Image>();
        if (ScoreGuage.guageParsent >= Define.borderLine)
        {
            animState = 1;
        }
        else
        {
            image.fillAmount = 1f;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (animationFlag)
        {

            if (!firstFlag)
            {
                firstFlag = true;
                if (animState == 1)
                {
                    GameObject.Find("SoundPlayer").GetComponent<SoundPlayerClear>().PlaySound(0);
                }
                else
                {
                    GameObject.Find("SoundPlayer").GetComponent<SoundPlayerClear>().PlaySound(3);
                }
            }
            switch (animState)
            {
                case 0:
                    break;
                case 1:
                    if (timer <= takeTime)
                    {
                        image.fillAmount += (1f / takeTime) * Time.deltaTime;
                        image.fillAmount = Mathf.Min(1f, image.fillAmount);
                        timer += Time.deltaTime;
                    }
                    else
                    {
                        image.fillAmount = 1f;
                    }
                    break;
            }
        }
    }
}
