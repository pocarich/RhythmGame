using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
	[SerializeField] GameObject ComboObject;
    [SerializeField] GameObject JudgeObject;
    [SerializeField] GameObject GuageObject;
    [SerializeField] GameObject ScoreObject;

	ComboText comboText;
	JudgeText judgeText;
	ScoreText scoreText;
	ScoreGuage scoreGuage;

    public static int excellentCounter { private set; get; }
    public static int greatCounter { private set; get; }
    public static int safeCounter { private set; get; }
    public static int badCounter { private set; get; }
    public static int missCounter { private set; get; }
    public static int maxCombo { private set; get; }
    public static float score { set; get; }

    // Use this for initialization
    void Start () {
		comboText = ComboObject.GetComponent<ComboText> ();
		judgeText = JudgeObject.GetComponent<JudgeText> ();
		scoreText = ScoreObject.GetComponent<ScoreText> ();
		scoreGuage = GuageObject.GetComponent<ScoreGuage> ();

		excellentCounter = 0;
		greatCounter = 0;
		safeCounter = 0;
		badCounter = 0;
		missCounter = 0;
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void Judge(Define.JudgeType judgeType){
		scoreGuage.UpdateGuage (judgeType);
		switch (judgeType) {
		case Define.JudgeType.PERFECT:
			comboText.UpdateCombo(ComboText.UpdateType.ADD);
			judgeText.UpdateJudge(JudgeText.JudgeType.PERFECT);
			excellentCounter++;
			scoreText.UpdateScore(Define.JudgeType.PERFECT,comboText.combo);
			break;
		case Define.JudgeType.GREAT:
			comboText.UpdateCombo(ComboText.UpdateType.ADD);
			judgeText.UpdateJudge(JudgeText.JudgeType.GREAT);
			greatCounter++;
			scoreText.UpdateScore(Define.JudgeType.GREAT,comboText.combo);
			break;
		case Define.JudgeType.SAFE:
			comboText.UpdateCombo(ComboText.UpdateType.ADD);
			judgeText.UpdateJudge(JudgeText.JudgeType.SAFE);
			safeCounter++;
			scoreText.UpdateScore(Define.JudgeType.SAFE,comboText.combo);
			break;
		case Define.JudgeType.BAD:
			comboText.UpdateCombo(ComboText.UpdateType.ZERO);
			judgeText.UpdateJudge(JudgeText.JudgeType.BAD);
			badCounter++;
			scoreText.UpdateScore(Define.JudgeType.BAD,comboText.combo);
			break;
		case Define.JudgeType.MISS:
			comboText.UpdateCombo(ComboText.UpdateType.ZERO);
			judgeText.UpdateJudge(JudgeText.JudgeType.MISS);
			missCounter++;
			scoreText.UpdateScore(Define.JudgeType.MISS,comboText.combo);
			break;
		}
	}

	public void End(){
		maxCombo = Mathf.Max (comboText.maxCombo, comboText.combo);
		score = scoreText.score;
	}
}
