using UnityEngine;
using System.Collections;
using System;

public class GameMgr : MonoBehaviour
{
    public enum State
    {
        DispMusicInfo,
        Ready,
        Game,
        Pause
    }

    SerialHandler serialHandler;
	GameObject pausePanel;
	GameObject nortsSpawner;
    GameObject musicInfoPanel;

    public State state { private set; get; }

	// Use this for initialization
	void Start () {
		MusicName.musicName=MusicList.GetMusicInfoList()[MainGameMgr.musicNum].musicName;
		ScoreBoard.score = 0f;
        serialHandler = GameObject.Find("SerialHandler").GetComponent<SerialHandler>();
        if (Define.inputType == Define.InputType.ARDUINO)
        {
            serialHandler.Write("0");
            serialHandler.Write("2");
            serialHandler.Write("4");
            serialHandler.Write("6");
            serialHandler.Write("8");
        }
        pausePanel =GameObject.Find ("PausePanel");
		if (pausePanel == null) {
			throw new Exception("PausePanelが見つかりません。");
		}
		nortsSpawner=GameObject.Find ("NortsSpawner");
		if (nortsSpawner == null) {
			throw new Exception("NortsSpawnerが見つかりません。");
		}
        musicInfoPanel = GameObject.Find("MusicInfoPanel");
        if (musicInfoPanel == null)
        {
            throw new Exception("MusicInfoPanelが見つかりません。");
        }

        pausePanel.SetActive (false);
       // nortsSpawner.SetActive(false);
        state = State.DispMusicInfo;
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.LoadLevel("MusicSelectScene");
        }
	    switch(state)
        {
            case State.DispMusicInfo:
                if(musicInfoPanel.GetComponent<MusicInfoPanelOnGameScene>().endFlag)
                {
                    state = State.Game;
                    musicInfoPanel.SetActive(false);
                    nortsSpawner.SetActive(true);
                }
                break;
            case State.Ready:
                break;
        }
	}

	public void PushPauseButton()
	{
		pausePanel.SetActive (true);
        state = State.Pause;
		nortsSpawner.GetComponent<NortsSpawner> ().Pause ();
	}

	public void PushContinueButton()
	{
		pausePanel.SetActive (false);
        state = State.Game;
		nortsSpawner.GetComponent<NortsSpawner> ().Resume ();
	}

	public void PushEscapeButton()
	{
		Application.LoadLevel("MusicSelectScene");
	}
}
