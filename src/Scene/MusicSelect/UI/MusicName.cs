using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MusicName : MonoBehaviour
{
	public static string musicName="";

	Text mText;

	// Use this for initialization
	void Start () {
		mText = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		mText.text = "♪ " + musicName;
	}
}
