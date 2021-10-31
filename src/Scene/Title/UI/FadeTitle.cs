using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeTitle : MonoBehaviour
{
    [SerializeField] float period;
    [SerializeField] float amplitude;

    Image image;
    float timer = 0f;

	// Use this for initialization
	void Start () {
        image = gameObject.GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        image.color = new Color(255f, 255f, 255f, (amplitude + amplitude * Mathf.Sin(Mathf.PI * 2 / period * timer)) / 255);
        timer += Time.deltaTime;
	}
}
