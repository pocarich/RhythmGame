using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameBlack : MonoBehaviour
{
    [SerializeField] float fadeOutTime;

    Image image;
    bool fadeOutFlag = false;

    // Use this for initialization
    void Start()
    {
        image = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeOutFlag)
        {
            image.color = new Color(0f, 0f, 0f, Mathf.Min(1f, image.color.a + 1f / fadeOutTime * Time.deltaTime));
        }
    }

    public void StartFadeOut()
    {
        fadeOutFlag = true;
    }
}
