using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayerClear : MonoBehaviour
{
    [SerializeField] AudioClip[] soundList;

    AudioSource audioSource;

	// Use this for initialization
	void Start () {
        audioSource = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlaySound(int n)
    {
        audioSource.clip = soundList[n];
        audioSource.Play();
    }
}
