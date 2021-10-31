using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearRank2 : MonoBehaviour
{
    [SerializeField] public Sprite[] spriteList;

    Image image;
    RectTransform rt;

    // Use this for initialization
    void Start()
    {
        image = gameObject.GetComponent<Image>();
        rt = gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void SetSprite(Define.ClearRank n)
    {
        if (image == null)
        {
            image = gameObject.GetComponent<Image>();
            rt = gameObject.GetComponent<RectTransform>();
        }
        image.sprite = spriteList[(int)n];

        image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
    }
}
