using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpeedSlider : MonoBehaviour
{
    Slider slider;

	// Use this for initialization
	void Start () {
		slider = GetComponent<Slider> ();
        slider.value = (float)MainGameMgr.speedLevel;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

	public void MoveSlider()
	{
		MainGameMgr.speedLevel = (int)slider.value;
	}
}
