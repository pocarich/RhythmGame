using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] AudioClip[] sound;
    [SerializeField] float[] offset;

    AudioSource[] audioSources;
    int counter=0;

	// Use this for initialization
	void Start () {
        audioSources = gameObject.GetComponents<AudioSource>();
        audioSources[0].clip = sound[0];
        audioSources[1].clip = sound[1];
        audioSources[2].clip = sound[2];
        audioSources[3].clip = sound[3];
        audioSources[4].clip = sound[4];
        audioSources[0].time = offset[0];
        audioSources[1].time = offset[1];
        audioSources[2].time = offset[2];
        audioSources[3].time = offset[3];
        audioSources[4].time = offset[4];
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void PlaySound(Define.NortsType type)
    {
        audioSources[counter % 5].time = offset[counter % 5];
        counter++;
    }
}
