using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearRank : MonoBehaviour
{
    [SerializeField] Sprite[] spriteList;

    Image image;
    RectTransform rt;
    GameObject rank2;
    GameObject rankMask;
    float timer = 0f;
    bool effectFlag = false;
    bool playFlag = false;

    public Define.ClearRank rank { private set; get; }
    public bool endFlag { private set; get; }
    public bool animFlag { set; get; }

    // Use this for initialization
    void Start () {
        image = gameObject.GetComponent<Image>();
        rt = gameObject.GetComponent<RectTransform>();
        rankMask = GameObject.Find("RankMask");
        rank2 = GameObject.Find("Rank2");
        endFlag = false;
        animFlag = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (animFlag)
        {
            switch (rank)
            {
                case Define.ClearRank.S:
                    if (timer < 0.2f)
                    {
                        image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.Min(1f, image.color.a + (1f / 0.2f) * Time.deltaTime));
                        rt.localScale = new Vector3(rt.localScale.x - (0.5f / 0.2f) * Time.deltaTime, rt.localScale.y - (0.5f / 0.2f) * Time.deltaTime, rt.localScale.z);
                    }
                    else if (timer < 0.7f)
                    {
                        if(!playFlag)
                        {
                            playFlag = true;
                            GameObject.Find("SoundPlayer").GetComponent<SoundPlayerClear>().PlaySound(4);
                        }
                        if (!rankMask.GetComponent<ClearRankMask>().animFlag)
                        {
                            rankMask.GetComponent<ClearRankMask>().animFlag = true;
                        }
                        image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
                        rt.localScale = new Vector3(1f, 1f, 1.0f);
                    }
                    if (0.2f < timer)
                    {
                        if (timer < 0.5f)
                        {
                            rank2.GetComponent<RectTransform>().localScale = new Vector3(rank2.GetComponent<RectTransform>().localScale.x + (0.5f / 0.3f) * Time.deltaTime, rank2.GetComponent<RectTransform>().localScale.y + (0.5f / 0.2f) * Time.deltaTime, rank2.GetComponent<RectTransform>().localScale.z);
                        }
                        else
                        {
                            rank2.GetComponent<RectTransform>().localScale = new Vector3(1.5f, 1.5f, 1f);
                        }
                    }
                    if (0.2f < timer)
                    {
                        if (!effectFlag)
                        {
                            rank2.GetComponent<Image>().color = new Color(rank2.GetComponent<Image>().color.r, rank2.GetComponent<Image>().color.g, rank2.GetComponent<Image>().color.b, 1f);
                            effectFlag = true;
                        }
                        if (timer < 0.5f)
                        {
                            rank2.GetComponent<Image>().color = new Color(rank2.GetComponent<Image>().color.r, rank2.GetComponent<Image>().color.g, rank2.GetComponent<Image>().color.b, Mathf.Max(0f, rank2.GetComponent<Image>().color.a - (1f / 0.3f) * Time.deltaTime));
                        }
                        else
                        {
                            rank2.GetComponent<Image>().color = new Color(rank2.GetComponent<Image>().color.r, rank2.GetComponent<Image>().color.g, rank2.GetComponent<Image>().color.b, 0f);
                        }
                    }
                    if (0.9f < timer)
                    {
                        endFlag = true;
                    }
                    break;
                case Define.ClearRank.A:
                    if (timer < 0.2f)
                    {
                        image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.Min(1f, image.color.a + (1f / 0.2f) * Time.deltaTime));
                        rt.localScale = new Vector3(rt.localScale.x - (0.3f / 0.2f) * Time.deltaTime, rt.localScale.y - (0.3f / 0.2f) * Time.deltaTime, rt.localScale.z);
                    }
                    else if (timer < 0.7f)
                    {
                        if (!playFlag)
                        {
                            playFlag = true;
                            GameObject.Find("SoundPlayer").GetComponent<SoundPlayerClear>().PlaySound(4);
                        }
                        image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
                        rt.localScale = new Vector3(1f, 1f, 1.0f);
                    }
                    if (0.2f < timer)
                    {
                        if (timer < 0.5f)
                        {
                            rank2.GetComponent<RectTransform>().localScale = new Vector3(rank2.GetComponent<RectTransform>().localScale.x + (0.5f / 0.3f) * Time.deltaTime, rank2.GetComponent<RectTransform>().localScale.y + (0.5f / 0.2f) * Time.deltaTime, rank2.GetComponent<RectTransform>().localScale.z);
                        }
                        else
                        {
                            rank2.GetComponent<RectTransform>().localScale = new Vector3(1.5f, 1.5f, 1f);
                        }
                    }
                    if (0.2f < timer)
                    {
                        if (!effectFlag)
                        {
                            rank2.GetComponent<Image>().color = new Color(rank2.GetComponent<Image>().color.r, rank2.GetComponent<Image>().color.g, rank2.GetComponent<Image>().color.b, 1f);
                            effectFlag = true;
                        }
                        if (timer < 0.5f)
                        {
                            rank2.GetComponent<Image>().color = new Color(rank2.GetComponent<Image>().color.r, rank2.GetComponent<Image>().color.g, rank2.GetComponent<Image>().color.b, Mathf.Max(0f, rank2.GetComponent<Image>().color.a - (1f / 0.3f) * Time.deltaTime));
                        }
                        else
                        {
                            rank2.GetComponent<Image>().color = new Color(rank2.GetComponent<Image>().color.r, rank2.GetComponent<Image>().color.g, rank2.GetComponent<Image>().color.b, 0f);
                        }
                    }
                    if (0.9f < timer)
                    {
                        endFlag = true;
                    }
                    break;
                case Define.ClearRank.B:
                    if (timer < 0.2f)
                    {
                        image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.Min(1f, image.color.a + (1f / 0.2f) * Time.deltaTime));
                        rt.localScale = new Vector3(rt.localScale.x - (0.3f / 0.2f) * Time.deltaTime, rt.localScale.y - (0.3f / 0.2f) * Time.deltaTime, rt.localScale.z);
                    }
                    else if (timer < 0.7f)
                    {
                        image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
                        rt.localScale = new Vector3(1f, 1f, 1.0f);
                    }
                    else
                    {
                        endFlag = true;
                    }
                    break;
                case Define.ClearRank.C:
                    if (timer < 0.2f)
                    {
                        image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.Min(1f, image.color.a + (1f / 0.2f) * Time.deltaTime));
                        rt.localScale = new Vector3(rt.localScale.x - (0.3f / 0.2f) * Time.deltaTime, rt.localScale.y - (0.3f / 0.2f) * Time.deltaTime, rt.localScale.z);
                    }
                    else if (timer < 0.7f) 
                    {
                        image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
                        rt.localScale = new Vector3(1f, 1f, 1.0f);
                    }
                    else
                    {
                        endFlag = true;
                    }
                    break;
                case Define.ClearRank.D:
                    if (timer <= 1.5f)
                    {
                        image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.Min(1f, image.color.a + (1f / 1.5f) * Time.deltaTime));
                    }
                    else if (timer <= 2.0f)
                    {
                        image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
                    }
                    else
                    {
                        endFlag = true;
                    }
                    break;
                case Define.ClearRank.E:
                    if (timer <= 1.5f)
                    {
                        image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.Min(1f, image.color.a + (1f / 1.5f) * Time.deltaTime));
                    }
                    else if (timer <= 2.0f)
                    {
                        image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
                    }
                    else
                    {
                        endFlag = true;
                    }
                    break;
                case Define.ClearRank.F:
                    if(timer<=1.5f)
                    {
                        image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.Min(1f, image.color.a + (1f / 1.5f) * Time.deltaTime));
                    }
                    else if(timer<=2.0f)
                    {
                        image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
                    }
                    else
                    {
                        endFlag = true;
                    }
                    break;
            }
            timer += Time.deltaTime;
        }
    }

    public void SetSprite(Define.ClearRank n)
    {
        if(image==null)
        {
            image = gameObject.GetComponent<Image>();
            rt = gameObject.GetComponent<RectTransform>();
            rank2 = GameObject.Find("Rank2");
            rankMask = GameObject.Find("RankMask");
        }
        rank = n;
        image.sprite = spriteList[(int)n];

        image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
        switch (n)
        {
            case Define.ClearRank.S:
                rt.localScale = new Vector3(1.5f, 1.5f, 1f);
                break;
            case Define.ClearRank.A:
                rt.localScale = new Vector3(1.3f, 1.3f, 1f);
                break;
            case Define.ClearRank.B:
                rt.localScale = new Vector3(1.3f, 1.3f, 1f);
                break;
            case Define.ClearRank.C:
                rt.localScale = new Vector3(1.3f, 1.3f, 1f);               
                break;
            case Define.ClearRank.D:
                break;
            case Define.ClearRank.E:
                break;
            case Define.ClearRank.F:
                break;
        }
    }
}
