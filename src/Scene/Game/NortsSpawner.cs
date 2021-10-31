using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using System;

public class NortsSpawner : MonoBehaviour
{
    static readonly float[] popX = { -2f, -1f, 0f, 1f, 2f };
    static readonly float[] popY = { 3.05f, 3.05f, 3.05f, 3.05f, 3.05f };
    static readonly float laneWidth = 4.0f;

    [SerializeField] GameObject SingleNortsPrefab;
    [SerializeField] GameObject UpNotePrefab;
    [SerializeField] GameObject DownNotePrefab;
    [SerializeField] GameObject AirNotePrefab;
    [SerializeField] GameObject LongNotePrefab;
    [SerializeField] GameObject LongNoteDownPrefab;
    [SerializeField] GameObject LongNoteUpPrefab;
    [SerializeField] GameObject LongNoteDownUpPrefab;
    [SerializeField] GameObject BarPrefab;
    [SerializeField] float startTime;
    [SerializeField] float endTime;

    GameObject scoreBoard;
	GameObject gameMgr;
    GameObject black;
    SerialHandler serialHandler;
	List<NortInfo> nortInfoList;
	AudioSource mAudioSource;
	bool playFlag=false;
	float preTimer=0.0f;
	float timer=0.0f;
	float musicTimer=0.0f;
	float musicStartTime;
	float endTimer=0.0f;
	int nortCounter=0;
    float fadeTimer = 0f;
    bool endFlag = false;

	// Use this for initialization
	void Start () {
        serialHandler = GameObject.Find("SerialHandler").GetComponent<SerialHandler>();
		mAudioSource = GetComponent<AudioSource> ();
		mAudioSource.clip= MusicList.musicList[MainGameMgr.musicNum];
		timer = MusicList.GetMusicInfoList () [MainGameMgr.musicNum].offset;
        nortInfoList = NortsReader.nortInfoList;
		musicStartTime = Define.ToBarDistance / (Define.baseSpeed+Define.speedChange*MainGameMgr.speedLevel);
		scoreBoard=GameObject.Find ("ScoreMgr");
        black = GameObject.Find("Black");
		if (scoreBoard == null) {
			throw new Exception("ScoreMgrが見つかりませんでした。");
		}
		gameMgr = GameObject.Find ("GameMgr");
        mAudioSource.time = startTime;
        preTimer += startTime;
        timer += startTime;
	}
	
	// Update is called once per frame
	void Update () {
        if (gameMgr.GetComponent<GameMgr>().state != GameMgr.State.Game) 
			return;
		Spawen ();
		UpdateTimer ();
		PlayMusic ();
		End ();
	}

    private void Spawen()//x:-1.8~1.8
    {
        var nortCounter_ = nortCounter;
        List<GameObject> popObject = new List<GameObject>();
        float popTime = 0f;
        foreach (var nortInfo in nortInfoList/*.Skip(nortCounter_)*/)
        {
            if (preTimer <= nortInfo.appearTime && nortInfo.appearTime < timer)
            {
                float posX = -laneWidth / 2 + laneWidth / 4 * nortInfo.appearPos;
                popTime = nortInfo.appearTime;
                switch (nortInfo.nortType)
                {
                    case 0:
                        popObject.Add(AddSingle(posX, (int)nortInfo.appearPos, nortInfo.appearTime));
                        break;
                    case 1:
                        popObject.Add(AddUp(posX, (int)nortInfo.appearPos, (int)nortInfo.value, nortInfo.appearTime, (int)(nortInfo.time) == 1));
                        break;
                    case 2:
                        popObject.Add(AddDown(posX, (int)nortInfo.appearPos, (int)nortInfo.value, nortInfo.appearTime));
                        break;
                    case 3:
                        AddAir(nortInfo.appearPos, nortInfo.value, nortInfo.time, nortInfo.appearTime);
                        break;
                    case 4:
                        popObject.Add(AddLong(posX, nortInfo.time, (int)nortInfo.appearPos, nortInfo.appearTime));
                        break;
                    case 5:
                        popObject.Add(AddLongDown(posX, nortInfo.time, (int)nortInfo.appearPos, (int)nortInfo.value, nortInfo.appearTime));
                        break;
                    case 6:
                        popObject.Add(AddLongUp(posX, nortInfo.time, (int)nortInfo.appearPos, (int)nortInfo.value, nortInfo.appearTime));
                        break;
                    case 7:
                        popObject.Add(AddLongDownUp(posX, nortInfo.time, (int)nortInfo.appearPos, (int)nortInfo.value, nortInfo.appearTime));
                        break;
                }
                nortCounter++;
            }
        }
        if (popObject.Count > 0)
        {
            GameObject[] nortsList = GameObject.FindGameObjectsWithTag("Nort");
            var endPopLongNotes = nortsList.Where(p => p.GetComponent<NortAbstract>().timer >= 0.01f && (p.GetComponent<NortAbstract>() is LongNorts || p.GetComponent<NortAbstract>() is LongNoteUp || p.GetComponent<NortAbstract>() is LongNoteDown || p.GetComponent<NortAbstract>() is LongNoteDownUp))
                .Where(p => Mathf.Abs(p.GetComponent<NortAbstract>().upTime - p.GetComponent<NortAbstract>().downTime + p.GetComponent<NortAbstract>().popTime - popTime) <= 0.02f);
           
            foreach (var obj in endPopLongNotes)
            {
                popObject.Add(obj);
            }
            for (int i = 0; i < (popObject.Count - 1); i++)
            {
                float a = -laneWidth / 2 + laneWidth / 4 * popObject[i].GetComponent<NortAbstract>().appearPos;
                float b = -laneWidth / 2 + laneWidth / 4 * popObject[(i + 1) % popObject.Count].GetComponent<NortAbstract>().appearPos;
                int length = Math.Abs(popObject[i].GetComponent<NortAbstract>().appearPos - popObject[(i + 1) % popObject.Count].GetComponent<NortAbstract>().appearPos);
                GameObject bar = (GameObject)Instantiate(BarPrefab, new Vector3((a + b) / 2, popY[0], transform.position.z), Quaternion.Euler(0.0f, 0.0f, 0.0f));
                bar.transform.localScale = new Vector3(1f * length - 2 * 0.4f, 0.03f, 0.03f);

                bar.GetComponent<SameTimePushBar>().AddObject(popObject[i]);
                bar.GetComponent<SameTimePushBar>().AddObject(popObject[(i + 1) % popObject.Count]);

            }
            foreach (var x in popObject.ToArray().Where(p => p.GetComponent<NortAbstract>().timer <= 0.001f && (p.GetComponent<NortAbstract>() is LongNorts || p.GetComponent<NortAbstract>() is LongNoteUp || p.GetComponent<NortAbstract>() is LongNoteDown || p.GetComponent<NortAbstract>() is LongNoteDownUp)))
            {
                var endSameLongNotes = nortsList.Where(p => p.GetComponent<NortAbstract>().appearPos != x.GetComponent<NortAbstract>().appearPos && (p.GetComponent<NortAbstract>() is LongNorts || p.GetComponent<NortAbstract>() is LongNoteUp || p.GetComponent<NortAbstract>() is LongNoteDown || p.GetComponent<NortAbstract>() is LongNoteDownUp))
                .Where(p => Mathf.Abs(p.GetComponent<NortAbstract>().time + p.GetComponent<NortAbstract>().popTime - (popTime + x.GetComponent<NortAbstract>().time)) <= 0.02f);
                foreach (var y in endSameLongNotes) 
                {
                    float a = -laneWidth / 2 + laneWidth / 4 * x.GetComponent<NortAbstract>().appearPos;
                    float b = -laneWidth / 2 + laneWidth / 4 * y.GetComponent<NortAbstract>().appearPos;
                    int length = Math.Abs(x.GetComponent<NortAbstract>().appearPos - y.GetComponent<NortAbstract>().appearPos);
                    GameObject bar = (GameObject)Instantiate(BarPrefab, new Vector3((a + b) / 2, popY[0], transform.position.z + 10f * (x.GetComponent<NortAbstract>().time * (Define.baseSpeed + Define.speedChange * MainGameMgr.speedLevel) / 10.0f)), Quaternion.Euler(0.0f, 0.0f, 0.0f));
                    bar.transform.localScale = new Vector3(1f * length - 2 * 0.4f, 0.03f, 0.03f);

                    bar.GetComponent<SameTimePushBar>().AddObject(x);
                    bar.GetComponent<SameTimePushBar>().AddObject(y);
                }
            }
        }
    }

	private void End()
	{
		if (!endFlag&&endTimer > Define.musicEndDelayTime) {
			mAudioSource.Stop();
			scoreBoard.GetComponent<ScoreBoard>().End();
            black.GetComponent<GameBlack>().StartFadeOut();
            endFlag = true;
            if (Define.inputType == Define.InputType.ARDUINO)
            {
                serialHandler.Write("1");
                serialHandler.Write("3");
                serialHandler.Write("5");
                serialHandler.Write("7");
                serialHandler.Write("9");
            }
        }
        if(endFlag)
        {
            fadeTimer += Time.deltaTime;
            if(endTime<=fadeTimer)
            {
                Application.LoadLevel("Clear");
            }
        }
	}

	private void UpdateTimer()
	{
		preTimer = timer;
		timer += Time.deltaTime;
		if (nortInfoList.Count == nortCounter && GameObject.FindGameObjectsWithTag ("Nort").Length == 0) {
			endTimer += Time.deltaTime;
		}
	}

	private void PlayMusic()
	{
		if (playFlag)
			return;
		if (musicTimer >= musicStartTime) {
            Debug.Log("jfaiefo");
			mAudioSource.Play ();
			playFlag=true;
		}
		else
			musicTimer += Time.deltaTime;
	}

	private GameObject AddSingle(float x,int pos,float popTime)
	{
		GameObject obj = (GameObject)Instantiate (SingleNortsPrefab, new Vector3(popX[pos],popY[pos],transform.position.z), Quaternion.Euler (0.0f, 0.0f, 0.0f));
		obj.GetComponent<NortAbstract> ().SetAppear (pos,popTime);
        return obj;
	}

    private GameObject AddUp(float x, int pos, int type,float popTime,bool connectFlag)
	{
		GameObject obj = (GameObject)Instantiate (UpNotePrefab, new Vector3(x,transform.position.y,transform.position.z), Quaternion.Euler (0.0f, 0.0f, 0.0f));
		obj.GetComponent<NortAbstract> ().SetAppear (pos,popTime);
        obj.GetComponent<UpNort>().SetType(type);
        obj.GetComponent<UpNort>().SetConnectAir(connectFlag);
        return obj;
	}

    private GameObject AddDown(float x, int pos,int type,float popTime)
    {
        GameObject obj = (GameObject)Instantiate(DownNotePrefab, new Vector3(x, transform.position.y, transform.position.z), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        obj.GetComponent<NortAbstract>().SetAppear(pos,popTime);
        obj.GetComponent<DownNote>().SetType(type);
        return obj;
    }

    private GameObject AddAir(float pos, float connectRane, float height,float popTime)
    {
        GameObject obj = (GameObject)Instantiate(AirNotePrefab, new Vector3(-laneWidth / 2 + laneWidth * pos, transform.position.y + 1f, transform.position.z), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        obj.GetComponent<AirNote>().SetAppear(pos,popTime);

        if (connectRane != -1)
        {
            GameObject[] nortsList = GameObject.FindGameObjectsWithTag("Nort");
            var selectedNotes = nortsList.Where(p => !p.GetComponent<NortAbstract>().pushFlag)
                                 .Where(p => p.GetComponent<NortAbstract>() is AirNote)
                                 .Where(p => (Mathf.Abs(p.GetComponent<NortAbstract>().airAppearPos - (connectRane*4f)) <= 0.01f))
                                 .Where(p => (p.GetComponent<NortAbstract>().timer != 0))
                                 .OrderBy(p => p.GetComponent<NortAbstract>().timer);

            if (selectedNotes.Count() != 0)
            {
                selectedNotes.First().GetComponent<AirNote>().ConnectRing(obj);
            }
        }
        else if (connectRane == -1)
        {
            obj.GetComponent<AirNote>().ConnectUp();
        }

        return obj;
    }

	private GameObject AddLong(float x,float time,int pos,float popTime)
	{
		GameObject obj = (GameObject)Instantiate (LongNotePrefab, new Vector3(x,transform.position.y,transform.position.z), Quaternion.Euler (0.0f, 0.0f, 0.0f));
		obj.GetComponent<LongNorts> ().SetInfo (time);
		obj.GetComponent<NortAbstract> ().SetAppear (pos,popTime);
        return obj;
	}

    private GameObject AddLongDown(float x, float time, int pos,int type,float popTime)
    {
        GameObject obj = (GameObject)Instantiate(LongNoteDownPrefab, new Vector3(x, transform.position.y, transform.position.z), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        obj.GetComponent<LongNoteDown>().SetInfo(time);
        obj.GetComponent<NortAbstract>().SetAppear(pos,popTime);
        obj.GetComponent<LongNoteDown>().SetType(type);
        return obj;
    }

    private GameObject AddLongUp(float x, float time, int pos,int type,float popTime)
    {
        GameObject obj = (GameObject)Instantiate(LongNoteUpPrefab, new Vector3(x, transform.position.y, transform.position.z), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        obj.GetComponent<LongNoteUp>().SetInfo(time);
        obj.GetComponent<NortAbstract>().SetAppear(pos,popTime);
        obj.GetComponent<LongNoteUp>().SetType(type);
        return obj;
    }

    private GameObject AddLongDownUp(float x, float time, int pos,int type,float popTime)
    {
        GameObject obj = (GameObject)Instantiate(LongNoteDownUpPrefab, new Vector3(x, transform.position.y, transform.position.z), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        obj.GetComponent<LongNoteDownUp>().SetInfo(time);
        obj.GetComponent<NortAbstract>().SetAppear(pos,popTime);
        obj.GetComponent<LongNoteDownUp>().SetType(type);
        return obj;
    }

    public void Resume()
	{
		mAudioSource.Play ();
	}

	public void Pause()
	{
		mAudioSource.Pause ();
	}
}
