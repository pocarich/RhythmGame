using UnityEngine;
using System.Collections;
using System;

public class MusicSelectMgr : MonoBehaviour
{
	GameObject UIMgr;
	GameObject musicPlayer;
	GameObject optionPanel;
    GameObject soundPlayer;
    InputSystem inputSystem;
    SerialHandler serialHandler;

    Vector2 musicStartPoint1 =Vector2.zero;
	Vector2 musicStartPoint2=Vector2.zero;
	float touchMoveAmount=0f;
	const float flickRange = 5f;
	bool flickFlag=false;
	bool playMusicFlagFirst=false;

	// Use this for initialization
	void Start () {
        serialHandler = GameObject.Find("SerialHandler").GetComponent<SerialHandler>();
        inputSystem = GameObject.Find("InputSystem").GetComponent<InputSystem>();
        UIMgr = GameObject.Find("UIMgr");
		if (UIMgr == null) {
			throw new Exception("UIMgrが見つかりませんでした。");
		}

		musicPlayer=GameObject.Find ("MusicPlayer");
		if (musicPlayer == null) {
			throw new Exception("MusicPlayerが見つかりませんでした。");
		}

		optionPanel=GameObject.Find ("OptionPanel");
		if (optionPanel == null) {
			throw new Exception("OptionPanelが見つかりませんでした。");
		}

        soundPlayer = GameObject.Find("SoundPlayer");

		musicStartPoint1.x = (Screen.width / 2f) - (1120f * Environment.ScreenPercent) / 2f;
		musicStartPoint1.y = (Screen.height / 2f) - (675f * Environment.ScreenPercent) / 2f;
		musicStartPoint2.x = (Screen.width / 2f) + (1120f * Environment.ScreenPercent) / 2f;
		musicStartPoint2.y = (Screen.height / 2f) + (675f * Environment.ScreenPercent) / 2f;
        if (Define.inputType == Define.InputType.ARDUINO)
        {
            serialHandler.Write("0");
            serialHandler.Write("2");
            serialHandler.Write("4");
            serialHandler.Write("6");
            serialHandler.Write("8");
        }
	}
	
	// Update is called once per frame
	void Update () {
		if (!playMusicFlagFirst)
			PlayMusicFirst ();
		if (!optionPanel.activeSelf)
			CheckInput ();
        if(Define.inputType==Define.InputType.ARDUINO)
        {
            if (inputSystem.input[(int)Define.ArduinoInput.BUTTON_1, (int)Define.InputState.IN])
            {
                PushLeft();
            }
            if (inputSystem.input[(int)Define.ArduinoInput.BUTTON_2, (int)Define.InputState.IN])
            {
                UIMgr.GetComponent<UIMgrOnSelectScene>().PushLevelButton(1);
            }
            if (inputSystem.input[(int)Define.ArduinoInput.BUTTON_3, (int)Define.InputState.IN])
            {
                PushGraph();
                if (Define.inputType == Define.InputType.ARDUINO)
                {
                    serialHandler.Write("1");
                    serialHandler.Write("3");
                    serialHandler.Write("5");
                    serialHandler.Write("7");
                    serialHandler.Write("9");
                }
            }
            if (inputSystem.input[(int)Define.ArduinoInput.BUTTON_4, (int)Define.InputState.IN])
            {
                UIMgr.GetComponent<UIMgrOnSelectScene>().PushLevelButton(0);
            }
            if (inputSystem.input[(int)Define.ArduinoInput.BUTTON_5, (int)Define.InputState.IN])
            {
                PushRight();
            }
        }
	}

	void PlayMusicFirst(){
		musicPlayer.GetComponent<MusicPlayerOnSelectScene>().Play();
		playMusicFlagFirst = true;
	}

    public void PushGraph()
    {
        Debug.Log(UIMgr.GetComponent<UIMgrOnSelectScene>().pushFlag);
        if (UIMgr.GetComponent<UIMgrOnSelectScene>().pushFlag)
        {
            return;
        }
        musicPlayer.GetComponent<MusicPlayerOnSelectScene>().Stop();
        UIMgr.GetComponent<UIMgrOnSelectScene>().PushGraph();
    }

    public void PushRight()
    {
        Debug.Log(UIMgr.GetComponent<UIMgrOnSelectScene>().pushFlag);

        if (UIMgr.GetComponent<UIMgrOnSelectScene>().pushFlag)
        {
            return;
        }
        soundPlayer.GetComponent<SoundPlayerSelectScene>().PlaySound(1);
        MainGameMgr.musicNum = (MainGameMgr.musicNum + MusicList.GetMusicInfoList().Count - 1) % MusicList.GetMusicInfoList().Count;
        musicPlayer.GetComponent<MusicPlayerOnSelectScene>().Play();
        UIMgr.GetComponent<UIMgrOnSelectScene>().UpdateUI();
    }

    public void PushLeft()
    {
        if (UIMgr.GetComponent<UIMgrOnSelectScene>().pushFlag)
        {
            return;
        }
        soundPlayer.GetComponent<SoundPlayerSelectScene>().PlaySound(1);
        MainGameMgr.musicNum = (++MainGameMgr.musicNum) % MusicList.GetMusicInfoList().Count;
        musicPlayer.GetComponent<MusicPlayerOnSelectScene>().Play();
        UIMgr.GetComponent<UIMgrOnSelectScene>().UpdateUI();
    }

	void CheckInput()
	{
		switch (Define.platform) {
		case Define.Platform.EDITOR:
			break;
		case Define.Platform.ANDROID:
			if (Input.touchCount == 0) {
				flickFlag = false;
				return;
			}
			var touch = Input.touches [0];
			if ((musicStartPoint1.x <= touch.position.x && touch.position.x <= musicStartPoint2.x) && (musicStartPoint1.y <= touch.position.y && touch.position.y <= musicStartPoint2.y)) {
				if (touch.phase == TouchPhase.Moved) {
					touchMoveAmount = touch.deltaPosition.x;
					if (Mathf.Abs (touchMoveAmount) > flickRange) 
					{
						flickFlag = true;
					}
				}
				if (touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended) {
					if (!flickFlag) {
						if (Mathf.Abs (touch.deltaPosition.y) <= flickRange) {
							musicPlayer.GetComponent<MusicPlayerOnSelectScene>().Stop();
							UIMgr.GetComponent<UIMgrOnSelectScene> ().PushGraph ();
						}
					} else {
						if (touchMoveAmount >= flickRange) {
							MainGameMgr.musicNum = (++MainGameMgr.musicNum) % MusicList.GetMusicInfoList ().Count;
							musicPlayer.GetComponent<MusicPlayerOnSelectScene>().Play();
							UIMgr.GetComponent<UIMgrOnSelectScene> ().UpdateUI ();
						} else if (touchMoveAmount < -flickRange) {
							MainGameMgr.musicNum = (MainGameMgr.musicNum + MusicList.GetMusicInfoList ().Count - 1) % MusicList.GetMusicInfoList ().Count;
							musicPlayer.GetComponent<MusicPlayerOnSelectScene>().Play();
							UIMgr.GetComponent<UIMgrOnSelectScene> ().UpdateUI ();
						}
					}
				}
			}
			break;
		}
	}

	void UpdateUI()
	{
        if (UIMgr.GetComponent<UIMgrOnSelectScene>().pushFlag)
        {
            return;
        }
        UIMgr.GetComponent<UIMgrOnSelectScene> ().UpdateUI ();
	}

	public void UpdateMusicNumRight(){
        if (UIMgr.GetComponent<UIMgrOnSelectScene>().pushFlag)
        {
            return;
        }
        MainGameMgr.musicNum = (MainGameMgr.musicNum + 1) % MusicList.GetMusicInfoList ().Count;
		UpdateUI ();
	}

	public void UpdateMusicNumLeft(){
        if (UIMgr.GetComponent<UIMgrOnSelectScene>().pushFlag)
        {
            return;
        }
        MainGameMgr.musicNum = (MainGameMgr.musicNum + MusicList.GetMusicInfoList ().Count - 1) % MusicList.GetMusicInfoList ().Count;
		UpdateUI ();
	}
}
