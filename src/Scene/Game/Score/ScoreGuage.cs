using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreGuage : MonoBehaviour
{
	static readonly float[] ratio = {1.3f,1.0f,0.7f,-0.5f,-1.0f};
    static readonly float[] borderList = { 0f, 0.75f, 1.0f };
    static readonly Color[] colorList = { new Color(255f, 255f, 255f, 1f), new Color(0f, 255f, 0f, 1f), new Color(255f, 255f, 0f, 1f) }; 

	[SerializeField] GameObject score;
	ScoreText scoreText;
	int maxCombo;
	float addPoint;
    Image image;

    public static float guageParsent { private set; get; }

    // Use this for initialization
    void Start () {
		scoreText = score.GetComponent<ScoreText> ();
		maxCombo = NortsReader.maxCombo;
		addPoint = 1.0f / maxCombo;
		guageParsent = 0.0f;
		transform.localScale = new Vector3 (1f, guageParsent, 0f);
        image = GetComponent<Image>();
        image.color = colorList[0];
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void UpdateGuage(Define.JudgeType judgeType)
    {
        guageParsent += addPoint * ratio[(int)judgeType];
        guageParsent = Mathf.Clamp(guageParsent, 0f, 1f);
        transform.localScale = new Vector3(0.8f, guageParsent, 0f);

        for (int i = 0; i != borderList.Length; i++)
        {
            if(borderList[i]<=guageParsent)
            {
                image.color = colorList[i];
            }
        }
    }
}
