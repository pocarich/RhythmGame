using UnityEngine;
using System.Collections;

public class OptionCancelButton : MonoBehaviour
{
	GameObject UIMgr;

	// Use this for initialization
	void Start () {
		UIMgr=GameObject.Find ("UIMgr");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Push()
	{
		UIMgr.GetComponent<UIMgrOnSelectScene> ().PushOptionCancelButton ();
	}
}
