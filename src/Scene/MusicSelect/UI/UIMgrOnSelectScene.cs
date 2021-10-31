using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class UIMgrOnSelectScene : MonoBehaviour
{
    [SerializeField] Sprite[] achieveSprite;
    [SerializeField] float delayTime;

    GameObject musicInfo;
	GameObject musicName;
	GameObject scoreRanking1;
	GameObject scoreRanking2;
	GameObject scoreRanking3;
	GameObject rank1;
	GameObject rank2;
	GameObject rank3;
	GameObject musicGraph;
	GameObject levelButton;
	GameObject optionButton;
	GameObject optionPanel;
	GameObject optionCancelButton;
	GameObject speedSlider;
	GameObject achieve;
	GameObject level;
    GameObject soundPlayer;
	
	bool initFlag=false;
    float timer = 0f;

    public bool pushFlag { private set; get; }

    // Use this for initialization
    void Start () {
		
		musicGraph=GameObject.Find("MusicGraph");
		if (musicGraph == null) {
			throw new Exception("MusicGraphが見つかりませんでした。");
		}
		
		levelButton=GameObject.Find ("LevelButton");
		if (levelButton == null) {
			throw new Exception("LevelButtonが見つかりませんでした。");
		}
		
		optionPanel=GameObject.Find ("OptionPanel");
		if (optionPanel == null) {
			throw new Exception("OptionPanelが見つかりませんでした。");
		}
		
		optionButton=GameObject.Find ("OptionButton");
		if (optionButton == null) {
			throw new Exception("OptionButtonが見つかりませんでした。");
		}
		
		optionCancelButton=GameObject.Find ("OptionCancelButton");
		if (optionCancelButton == null) {
			throw new Exception("OptionCancelButtonが見つかりませんでした。");
		}
		
		speedSlider=GameObject.Find ("SpeedSlider");
		if (speedSlider == null) {
			throw new Exception("SpeedSliderが見つかりませんでした。");
		}
		
		musicInfo=GameObject.Find ("MusicInfo");
		if (musicInfo == null) {
			throw new Exception("MusicInfoが見つかりませんでした。");
		}
		
		musicName=GameObject.Find ("MusicName");
		if (musicName == null) {
			throw new Exception("MusicNameが見つかりませんでした。");
		}
		
		scoreRanking1=GameObject.Find ("ScoreRanking1");
		if (scoreRanking1 == null) {
			throw new Exception("ScoreRanking1が見つかりませんでした。");
		}
		
		scoreRanking2=GameObject.Find ("ScoreRanking2");
		if (scoreRanking2 == null) {
			throw new Exception("ScoreRanking2が見つかりませんでした。");
		}
		
		scoreRanking3=GameObject.Find ("ScoreRanking3");
		if (scoreRanking3 == null) {
			throw new Exception("ScoreRanking3が見つかりませんでした。");
		}

		rank1=GameObject.Find ("Rank1");
		if (rank1 == null) {
			throw new Exception("Rank1が見つかりませんでした。");
		}

		rank2=GameObject.Find ("Rank2");
		if (rank2 == null) {
			throw new Exception("Rank2が見つかりませんでした。");
		}

		rank3=GameObject.Find ("Rank3");
		if (rank3 == null) {
			throw new Exception("Rank3が見つかりませんでした。");
		}

		achieve=GameObject.Find ("Achieve");
		if (achieve == null) {
			throw new Exception("Acieveが見つかりませんでした。");
		}

		level=GameObject.Find("Level");
		if (level == null) {
			throw new Exception("Levelが見つかりませんでした。");
		}

        soundPlayer = GameObject.Find("SoundPlayer");
		
		optionPanel.SetActive (false);
        pushFlag = false;
	}

    // Update is called once per frame
    void Update()
    {
        if (!initFlag)
        {
            InitUI();
        }
        if (pushFlag)
        {
            timer += Time.deltaTime;
            if (timer >= delayTime)
            {
                Application.LoadLevel("NortsLoad");
            }
        }
    }
	
	void InitUI()
	{
		initFlag = true;

		MusicInfo musicInfo = MusicList.GetMusicInfoList () [MainGameMgr.musicNum];

		musicName.GetComponent<Text> ().text = musicInfo.musicName;
		rank1.GetComponent<Text> ().text = musicInfo.GetRankList () [(int)MainGameMgr.musicLevel, 0] == 0 ? "-" : (Define.ClearRank)musicInfo.GetRankList () [(int)MainGameMgr.musicLevel, 0] + "";
		rank2.GetComponent<Text> ().text = musicInfo.GetRankList () [(int)MainGameMgr.musicLevel, 1] == 0 ? "-" : (Define.ClearRank)musicInfo.GetRankList () [(int)MainGameMgr.musicLevel, 1] + "";
		rank3.GetComponent<Text> ().text = musicInfo.GetRankList () [(int)MainGameMgr.musicLevel, 2] == 0 ? "-" : (Define.ClearRank)musicInfo.GetRankList () [(int)MainGameMgr.musicLevel, 2] + "";
		scoreRanking1.GetComponent<Text> ().text = String.Format ("{0:#,0}",musicInfo.GetScoreList () [(int)MainGameMgr.musicLevel, 0]);
		scoreRanking2.GetComponent<Text> ().text = String.Format ("{0:#,0}",musicInfo.GetScoreList () [(int)MainGameMgr.musicLevel, 1]);
		scoreRanking3.GetComponent<Text> ().text = String.Format ("{0:#,0}",musicInfo.GetScoreList () [(int)MainGameMgr.musicLevel, 2]);
		level.GetComponent<Text> ().text = "Lv:";
		for (int i=0; i<musicInfo.GetLevelList() [(int)MainGameMgr.musicLevel]; i++) {
			level.GetComponent<Text>().text+="★";
		}

		if (musicInfo.GetAchievementList() [(int)MainGameMgr.musicLevel] == 0) {
			achieve.GetComponent<Image> ().color=new Color(255f,255f,255f,0f);
		} else {
			achieve.GetComponent<Image> ().color=new Color(255f,255f,255f,255f);
			achieve.GetComponent<Image> ().sprite=achieveSprite[musicInfo.GetAchievementList() [(int)MainGameMgr.musicLevel]-1];
		}
	}

	public void UpdateUI(){
        if(pushFlag)
        {
            return;
        }
		MusicInfo musicInfo = MusicList.GetMusicInfoList () [MainGameMgr.musicNum];
		
		musicName.GetComponent<Text> ().text = musicInfo.musicName;
		rank1.GetComponent<Text> ().text = musicInfo.GetRankList() [(int)MainGameMgr.musicLevel, 0] == 0 ? "-" : (Define.ClearRank)musicInfo.GetRankList() [(int)MainGameMgr.musicLevel, 0]+"";
		rank2.GetComponent<Text> ().text = musicInfo.GetRankList() [(int)MainGameMgr.musicLevel, 1] == 0 ? "-" : (Define.ClearRank)musicInfo.GetRankList() [(int)MainGameMgr.musicLevel, 1]+"";
		rank3.GetComponent<Text> ().text = musicInfo.GetRankList() [(int)MainGameMgr.musicLevel, 2] == 0 ? "-" : (Define.ClearRank)musicInfo.GetRankList() [(int)MainGameMgr.musicLevel, 2]+"";
		scoreRanking1.GetComponent<Text> ().text = String.Format ("{0:#,0}",musicInfo.GetScoreList () [(int)MainGameMgr.musicLevel, 0]);
		scoreRanking2.GetComponent<Text> ().text = String.Format ("{0:#,0}",musicInfo.GetScoreList () [(int)MainGameMgr.musicLevel, 1]);
		scoreRanking3.GetComponent<Text> ().text = String.Format ("{0:#,0}",musicInfo.GetScoreList () [(int)MainGameMgr.musicLevel, 2]);
		musicGraph.GetComponent<MusicSelectSprite> ().UpdateSprite ();
		level.GetComponent<Text> ().text = "Lv:";
		for (int i=0; i<musicInfo.GetLevelList() [(int)MainGameMgr.musicLevel]; i++) {
			level.GetComponent<Text>().text+="★";
		}

		if (musicInfo.GetAchievementList() [(int)MainGameMgr.musicLevel] == 0) {
			achieve.GetComponent<Image> ().color=new Color(255f,255f,255f,0f);
		} else {
			achieve.GetComponent<Image> ().color=new Color(255f,255f,255f,255f);
			achieve.GetComponent<Image> ().sprite=achieveSprite[musicInfo.GetAchievementList() [(int)MainGameMgr.musicLevel]-1];
		}
	}

    public void UpdateUIExceptGraph()
    {
        if(pushFlag)
        {
            return;
        }
        MusicInfo musicInfo = MusicList.GetMusicInfoList()[MainGameMgr.musicNum];

        musicName.GetComponent<Text>().text = musicInfo.musicName;
        rank1.GetComponent<Text>().text = musicInfo.GetRankList()[(int)MainGameMgr.musicLevel, 0] == 0 ? "-" : (Define.ClearRank)musicInfo.GetRankList()[(int)MainGameMgr.musicLevel, 0] + "";
        rank2.GetComponent<Text>().text = musicInfo.GetRankList()[(int)MainGameMgr.musicLevel, 1] == 0 ? "-" : (Define.ClearRank)musicInfo.GetRankList()[(int)MainGameMgr.musicLevel, 1] + "";
        rank3.GetComponent<Text>().text = musicInfo.GetRankList()[(int)MainGameMgr.musicLevel, 2] == 0 ? "-" : (Define.ClearRank)musicInfo.GetRankList()[(int)MainGameMgr.musicLevel, 2] + "";
        scoreRanking1.GetComponent<Text>().text = String.Format("{0:#,0}", musicInfo.GetScoreList()[(int)MainGameMgr.musicLevel, 0]);
        scoreRanking2.GetComponent<Text>().text = String.Format("{0:#,0}", musicInfo.GetScoreList()[(int)MainGameMgr.musicLevel, 1]);
        scoreRanking3.GetComponent<Text>().text = String.Format("{0:#,0}", musicInfo.GetScoreList()[(int)MainGameMgr.musicLevel, 2]);
        level.GetComponent<Text>().text = "Lv:";
        for (int i = 0; i < musicInfo.GetLevelList()[(int)MainGameMgr.musicLevel]; i++)
        {
            level.GetComponent<Text>().text += "★";
        }

        if (musicInfo.GetAchievementList()[(int)MainGameMgr.musicLevel] == 0)
        {
            achieve.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);
        }
        else {
            achieve.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
            achieve.GetComponent<Image>().sprite = achieveSprite[musicInfo.GetAchievementList()[(int)MainGameMgr.musicLevel] - 1];
        }
    }

    public void PushGraph(){
        if (!optionPanel.activeSelf)
        {
            soundPlayer.GetComponent<SoundPlayerSelectScene>().PlaySound(0);
            GameObject.Find("White").GetComponent<TitleWhite>().StartFadeOut();
            pushFlag = true;
        }
	}
	
	public void PushLevelButton(int n){
        if(pushFlag)
        {
            return;
        }
        if (!optionPanel.activeSelf)
        {
            if (n == 0)
            {
                MainGameMgr.musicLevel = (Define.MusicLevel)(((int)MainGameMgr.musicLevel + 1) % Enum.GetNames(typeof(Define.MusicLevel)).Length);
            }
            else
            {
                MainGameMgr.musicLevel = (Define.MusicLevel)(((int)MainGameMgr.musicLevel + 2) % Enum.GetNames(typeof(Define.MusicLevel)).Length);
            }
            levelButton.GetComponent<LevelButton>().UpdateInfo();
            UpdateUIExceptGraph();
            soundPlayer.GetComponent<SoundPlayerSelectScene>().PlaySound(2);
        }
    }
	
	public void PushOptionButton(){
        if (pushFlag)
        {
            return;
        }
        if (!optionPanel.activeSelf) {
			optionPanel.SetActive(true);
			levelButton.SetActive(false);
			optionButton.SetActive(false);
		}
	}
	
	public void PushOptionCancelButton(){
		if (optionPanel.activeSelf){
			optionPanel.SetActive(false);
			levelButton.SetActive(true);
			optionButton.SetActive(true);
			Debug.Log (MainGameMgr.speedLevel);
		}
	}
	
	public void ChangeSpeedSlider(){
		
	}
}
