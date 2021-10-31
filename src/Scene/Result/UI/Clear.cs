//演奏成功画面

using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class Clear : MonoBehaviour
{
    static readonly string[] achieveMessage = { "", "CLEAR!", "FULLCOMBO!!", "ALLPERFECT!!" };
    static readonly Color[] colorList = { new Color(0f, 0f, 0f, 0f), new Color(0f, 118f, 255f, 1f), new Color(255f, 255f, 0f, 1f), new Color(255f, 0f, 255f, 1f) };

    [SerializeField] float maskAnimTime;
    [SerializeField] float clearTextAnimTime;
    [SerializeField] float blackAnimTime;
    [SerializeField] float scoreAnimTime;
    [SerializeField] float judgeAnimTime;
    [SerializeField] float maxComboAnimTime;
    [SerializeField] float rankAnimTime;
    [SerializeField] float maskAnimTimeFailed;
    [SerializeField] float clearTextAnimTimeFailed;
    [SerializeField] float blackAnimTimeFailed;
    [SerializeField] float scoreAnimTimeFailed;
    [SerializeField] float judgeAnimTimeFailed;
    [SerializeField] float maxComboAnimTimeFailed;
    [SerializeField] float rankAnimTimeFailed;

    GameObject back;
	GameObject achieve;
	GameObject score;
	GameObject perfect;
	GameObject great;
	GameObject safe;
	GameObject bad;
	GameObject miss;
	GameObject maxCombo;
	GameObject rank;
    GameObject rank2;
    GameObject mask;
    GameObject black;
    GameObject white;
    GameObject musicName;
    GameObject musicLevel;
    InputSystem inputSystem;
    SerialHandler serialHandler;

    float timer = 0f;
    float endTimer = 0f;
    bool pushFlag = false;
    bool clearFlag = false;

    public static Define.ClearRank clearRank { private set; get; }
    public static Define.Achieve achieveType { private set; get; }

    // Use this for initialization
    void Start () {
        serialHandler = GameObject.Find("SerialHandler").GetComponent<SerialHandler>();
        inputSystem = GameObject.Find("InputSystem").GetComponent<InputSystem>();
        back = GameObject.Find ("Back");
		if (back == null) {
			throw new Exception("Backが見つかりませんでした。");
		}

		achieve=GameObject.Find ("ClearText");
		if (achieve == null) {
			throw new Exception("ClearTextが見つかりませんでした。");
		}

		score=GameObject.Find ("Score");
		if (score == null) {
			throw new Exception("Scoreが見つかりませんでした。");
		}

		perfect=GameObject.Find ("Perfect");
		if (perfect == null) {
			throw new Exception("Perfectが見つかりませんでした。");
		}

		great=GameObject.Find ("Great");
		if (great == null) {
			throw new Exception("Greatが見つかりませんでした。");
		}

		safe=GameObject.Find ("Safe");
		if (safe == null) {
			throw new Exception("Safeが見つかりませんでした。");
		}

		bad=GameObject.Find ("Bad");
		if (bad == null) {
			throw new Exception("Badが見つかりませんでした。");
		}

		miss=GameObject.Find ("Miss");
		if (miss == null) {
			throw new Exception("Missが見つかりませんでした。");
		}

		maxCombo=GameObject.Find ("MaxCombo");
		if (maxCombo == null) {
			throw new Exception("MaxComboが見つかりませんでした。");
		}

		rank=GameObject.Find ("Rank");
		if (rank == null) {
			throw new Exception("Rankが見つかりませんでした。");
		}
        rank2 = GameObject.Find("Rank2");
        if (rank2 == null)
        {
            throw new Exception("Rank2が見つかりませんでした。");
        }
        mask = GameObject.Find("Mask");
        if (mask == null)
        {
            throw new Exception("Maskが見つかりませんでした。");
        }
        black = GameObject.Find("Black");
        if (black == null)
        {
            throw new Exception("Blackが見つかりませんでした。");
        }
        white = GameObject.Find("White");
        if (white == null)
        {
            throw new Exception("Whiteが見つかりませんでした。");
        }
        musicName = GameObject.Find("MusicName");
        if (musicName == null)
        {
            throw new Exception("MusicNameが見つかりませんでした。");
        }
        musicLevel = GameObject.Find("MusicLevel");
        if (musicLevel == null)
        {
            throw new Exception("MusicLevelが見つかりませんでした。");
        }
        SetText ();
	}
	
	// Update is called once per frame
	void Update () {
        if (clearFlag)
        {
            if (maskAnimTime <= timer && !mask.GetComponent<ClearMask>().animationFlag)
            {
                mask.GetComponent<ClearMask>().animationFlag = true;
            }
            if (clearTextAnimTime <= timer && !achieve.GetComponent<ClearText>().animationFlag)
            {
                achieve.GetComponent<ClearText>().animationFlag = true;
                GameObject.Find("WhiteClearMask").GetComponent<WhiteClearMask>().animFlag = true;
            }
            if (blackAnimTime <= timer && !black.GetComponent<ClearBlack>().animationFlag)
            {
                GameObject.Find("MusicPlayer").GetComponent<AudioSource>().Play();
                black.GetComponent<ClearBlack>().animationFlag = true;
            }
            if (scoreAnimTime <= timer && !score.GetComponent<ClearParam>().animationFlag)
            {
                score.GetComponent<ClearParam>().animationFlag = true;
            }
            if (judgeAnimTime <= timer && !perfect.GetComponent<ClearParam>().animationFlag)
            {
                perfect.GetComponent<ClearParam>().animationFlag = true;
                great.GetComponent<ClearParam>().animationFlag = true;
                safe.GetComponent<ClearParam>().animationFlag = true;
                bad.GetComponent<ClearParam>().animationFlag = true;
                miss.GetComponent<ClearParam>().animationFlag = true;
            }
            if (maxComboAnimTime <= timer && !maxCombo.GetComponent<ClearParam>().animationFlag)
            {
                maxCombo.GetComponent<ClearParam>().animationFlag = true;
            }
            if (rankAnimTime <= timer && !rank.GetComponent<ClearRank>().animFlag)
            {
                rank.GetComponent<ClearRank>().animFlag = true;
            }
        }
        else
        {
            if (maskAnimTimeFailed <= timer && !mask.GetComponent<ClearMask>().animationFlag)
            {
                mask.GetComponent<ClearMask>().animationFlag = true;
            }
            if (clearTextAnimTimeFailed <= timer && !achieve.GetComponent<ClearText>().animationFlag)
            {
                achieve.GetComponent<ClearText>().animationFlag = true;
            }
            if (blackAnimTimeFailed <= timer && !black.GetComponent<ClearBlack>().animationFlag)
            {
                black.GetComponent<ClearBlack>().animationFlag = true;
            }
            if (scoreAnimTimeFailed <= timer && !score.GetComponent<ClearParam>().animationFlag)
            {
                score.GetComponent<ClearParam>().animationFlag = true;
            }
            if (judgeAnimTimeFailed <= timer && !perfect.GetComponent<ClearParam>().animationFlag)
            {
                perfect.GetComponent<ClearParam>().animationFlag = true;
                great.GetComponent<ClearParam>().animationFlag = true;
                safe.GetComponent<ClearParam>().animationFlag = true;
                bad.GetComponent<ClearParam>().animationFlag = true;
                miss.GetComponent<ClearParam>().animationFlag = true;
            }
            if (maxComboAnimTimeFailed <= timer && !maxCombo.GetComponent<ClearParam>().animationFlag)
            {
                maxCombo.GetComponent<ClearParam>().animationFlag = true;
            }
            if (rankAnimTimeFailed <= timer && !rank.GetComponent<ClearRank>().animFlag)
            {
                rank.GetComponent<ClearRank>().animFlag = true;
            }
        }

        if (!pushFlag&&rank.GetComponent<ClearRank>().endFlag)
        {
            if (Define.inputType == Define.InputType.ARDUINO)
            {
                serialHandler.Write("4");
            }
            if (Define.inputType==Define.InputType.KEYBOARD&&Input.GetMouseButtonDown(0))
            {
                pushFlag = true;
                GameObject.Find("SoundPlayer").GetComponent<SoundPlayerClear>().PlaySound(2);
            }
            if (Define.inputType == Define.InputType.ARDUINO && inputSystem.input[(int)Define.ArduinoInput.BUTTON_3, (int)Define.InputState.IN])
            {
                pushFlag = true;
                GameObject.Find("SoundPlayer").GetComponent<SoundPlayerClear>().PlaySound(2);
            }
        }

        if(pushFlag)
        {
            white.GetComponent<Image>().color = new Color(255f, 255f, 255f, Mathf.Min(1f, white.GetComponent<Image>().color.a + (1f / 1.5f) * Time.deltaTime));
            endTimer += Time.deltaTime;
            if (endTimer >= 3f)
            {
                if (Define.inputType == Define.InputType.ARDUINO)
                {
                    serialHandler.Write("5");
                }
                if (clearFlag)
                {
                    Application.LoadLevel("SaveScene");
                }
                else
                {
                    Application.LoadLevel("MusicSelectScene");
                }
            }
        }

        timer += Time.deltaTime;
	}

	void SetText(){
		clearRank = CalcRank ();
		achieveType = CheckAchieve ();

        if(achieveType!=Define.Achieve.FAILED)
        {
            clearFlag = true;
        }

		back.GetComponent<Image> ().sprite = MusicList.spriteList [MainGameMgr.musicNum];
		achieve.GetComponent<ClearText> ().SetSprite((int)achieveType);
        musicName.GetComponent<Text>().text = MusicList.GetMusicInfoList()[MainGameMgr.musicNum].musicName;
        musicLevel.GetComponent<Text>().text = MainGameMgr.musicLevel + "";

        score.GetComponent<ClearParam>().maxValue= (int)ScoreBoard.score;
		perfect.GetComponent<ClearParam>().maxValue = ScoreBoard.excellentCounter;
		great.GetComponent<ClearParam>().maxValue = ScoreBoard.greatCounter;
		safe.GetComponent<ClearParam>().maxValue = ScoreBoard.safeCounter;
		bad.GetComponent<ClearParam>().maxValue = ScoreBoard.badCounter;
		miss.GetComponent<ClearParam>().maxValue = ScoreBoard.missCounter;
		maxCombo.GetComponent<ClearParam>().maxValue = ScoreBoard.maxCombo;

        rank.GetComponent<ClearRank>().SetSprite(clearRank);
        rank2.GetComponent<ClearRank>().SetSprite(clearRank);
    }

    float CalcMaxScore(){
		float maxScore = 0f;
		for (int i=0; i<NortsReader.maxCombo; i++) {
            maxScore += (Define.baseScore * 1.3f);
		}
		return maxScore;
	}

	Define.ClearRank CalcRank(){
        if (ScoreGuage.guageParsent >= Define.borderLine)
        {
            float maxScore = CalcMaxScore();
            float scorePercent = ScoreBoard.score / maxScore;

            if (scorePercent >= Define.S_BorderLine)
            {
                return Define.ClearRank.S;
            }
            else if (scorePercent >= Define.A_BorderLine)
            {
                return Define.ClearRank.A;
            }
            else if (scorePercent >= Define.B_BorderLine)
            {
                return Define.ClearRank.B;
            }
            else if (scorePercent >= Define.C_BorderLine)
            {
                return Define.ClearRank.C;
            }
            else if (scorePercent >= Define.D_BorderLine)
            {
                return Define.ClearRank.D;
            }
            else {
                return Define.ClearRank.E;
            }
        }
        else
        {
            return Define.ClearRank.F;
        }
	}

	Define.Achieve CheckAchieve(){
        if (ScoreGuage.guageParsent >= Define.borderLine)
        {
            if (ScoreBoard.excellentCounter == NortsReader.maxCombo)
            {
                return Define.Achieve.ALLEXCELLENT;
            }
            else if (ScoreBoard.maxCombo == NortsReader.maxCombo)
            {
                return Define.Achieve.FULLCOMBO;
            }
            else {
                return Define.Achieve.CLEAR;
            }
        }
        else
        {
            return Define.Achieve.FAILED;
        }
	}
}
