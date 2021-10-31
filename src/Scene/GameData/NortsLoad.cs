using UnityEngine;
using System.Collections;
using System;

public class NortsLoad : MonoBehaviour
{
	GameObject nortsReader;
    bool startLoadFlag = false;

    // Use this for initialization
    void Start () {
		nortsReader=GameObject.Find ("NortsReader");
		if (nortsReader == null) {
			throw new Exception("NortsReaderが見つかりませんでした。");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!startLoadFlag) {
			startLoadFlag=true;
			nortsReader.GetComponent<NortsReader>().StartLoad();
		}
		if (NortsReader.endFlag) {
			Application.LoadLevel ("GameScene");
			NortsReader.endFlag=false;
		}
	}
}
