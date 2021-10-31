using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayerTitle : MonoBehaviour
{
    AudioSource audioSource;

    public bool pushFlag { set; get; }

	// Use this for initialization
	void Start () {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.time = 27f;
        audioSource.volume = 0f;
        audioSource.Play();
	}
	
	// Update is called once per frame
	void Update () {
		if(!pushFlag)
        {
            if(audioSource.volume<0.5f)
            {
                audioSource.volume += (0.5f / 3f) * Time.deltaTime;
                audioSource.volume = Mathf.Min(0.5f, audioSource.volume);
            }
        }
        else
        {
            if (audioSource.volume > 0f)
            {
                audioSource.volume -= (0.5f / 3f) * Time.deltaTime;
                audioSource.volume = Mathf.Max(0f, audioSource.volume);
            }
        }
	}
}
