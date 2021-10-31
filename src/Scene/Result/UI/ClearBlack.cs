using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearBlack : MonoBehaviour
{
    [SerializeField] float takeTime;

    Image image;
    float timer = 0f;

    public bool animationFlag { set; get; }

	// Use this for initialization
	void Start () {
        image = gameObject.GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        if (animationFlag)
        {
            if (timer <= takeTime)
            {
                timer += Time.deltaTime;
                image.color = new Color(0f, 0f, 0f, Mathf.Max(0f, image.color.a - (1f / takeTime) * Time.deltaTime));

            }
            else
            {
                image.color = new Color(0f, 0f, 0f, 0f);
            }
        }
	}
}
