using UnityEngine;
using System.Collections;

public class MusicPlayerOnSelectScene : MonoBehaviour
{
	AudioSource mAudioSource;

	// Use this for initialization
	void Start () {
		mAudioSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Play(){
		if (mAudioSource.isPlaying) {
			mAudioSource.Stop();
		}

		mAudioSource.clip = MusicList.musicList [MainGameMgr.musicNum];
		mAudioSource.Play ();
	}

	public void Stop(){
		mAudioSource.Stop ();
	}
}
