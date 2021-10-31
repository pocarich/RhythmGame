using UnityEngine;
using System.Collections;

public class EscapeButton : MonoBehaviour
{
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Push()
	{
		GameObject.Find ("GameMgr").GetComponent<GameMgr> ().PushEscapeButton ();
	}
}
