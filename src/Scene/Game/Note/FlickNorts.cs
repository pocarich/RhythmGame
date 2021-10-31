using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FlickNorts : NortAbstract
{
    protected override void Start () {
		pushFlag = true;
		gameMgr=GameObject.Find ("GameMgr");
		timer = 0.0f;
		scoreBoard = GameObject.Find ("ScoreMgr").GetComponent<ScoreBoard> ();
		speed = Define.baseSpeed + Define.speedChange * MainGameMgr.speedLevel;
		beatTime = Define.ToBarDistance / speed;
		disappearTime = beatTime + Define.badDifference;
	}

	public void SetPos(Vector3 pos)
	{
		transform.position = pos;
	}

	public override void Init(int size){
		nortSize = size;
		diff_x = Define.diffXRange * Environment.ScreenPercent * size;
		float xsize = 10f;
		float ysize = 37f * size;
		transform.localScale = new Vector3 (xsize, ysize, 6f);
	}
	
	protected override void IsClicked()
	{
		if (Define.platform == Define.Platform.EDITOR) {
			if (FlickCalcurator.firstPosition == Vector3.zero)
				return;
			float diffX = Mathf.Abs (FlickCalcurator.firstPosition.x - Camera.main.WorldToScreenPoint (transform.position).x);
			if (diffX <= Define.diffXRange * Environment.ScreenPercent) {
				bool judge = false;
				if (judge) {
					float timeDifference = Mathf.Abs (timer - beatTime);
					if (timeDifference <= Define.badDifference) {
				
						if (timeDifference <= Define.excellentDifference)
							scoreBoard.Judge (Define.JudgeType.PERFECT);
						else if (timeDifference <= Define.greatDifference)
							scoreBoard.Judge (Define.JudgeType.GREAT);
						else if (timeDifference <= Define.safeDifference)
							scoreBoard.Judge (Define.JudgeType.SAFE);
						else 
							scoreBoard.Judge (Define.JudgeType.BAD);
				
						Destroy (gameObject);
						if (timeDifference <= Define.safeDifference) {
							var obj = Instantiate (effect, new Vector3 (transform.position.x, 3.0f, 4.0f), Quaternion.Euler (90.0f, 0.0f, 0.0f));
							Destroy (obj, 0.15f);
						}
					}
				}
			}
		} else if (Define.platform == Define.Platform.ANDROID) {
			bool findMoveTouch = false;
			foreach (var touch_ in Input.touches) {
				if (touch_.phase == TouchPhase.Moved) {
					float diffX = (touch_.position.x - Camera.main.WorldToScreenPoint (transform.position).x);
					if (Mathf.Abs (diffX) <= Define.diffXRange * Environment.ScreenPercent * Define.FlickRangeHosei) {
						if (Mathf.Abs(touch_.deltaPosition.x) > Define.NeedFlickAmount * Environment.ScreenPercent) {
							findMoveTouch = true;
							break;
						}
					}
				}
			}
			if (!findMoveTouch)
				return;
			float timeDifference = Mathf.Abs (timer - beatTime);
			if (timeDifference <= Define.safeDifference) {
				if (timeDifference <= Define.excellentDifference)
					scoreBoard.Judge (Define.JudgeType.PERFECT);
				else if (timeDifference <= Define.greatDifference)
					scoreBoard.Judge (Define.JudgeType.GREAT);
				else if (timeDifference <= Define.safeDifference)
					scoreBoard.Judge (Define.JudgeType.SAFE);
				else 
					scoreBoard.Judge (Define.JudgeType.BAD);
						
				Destroy (gameObject);
				if (timeDifference <= Define.safeDifference) {
					var obj = Instantiate (effect, new Vector3 (transform.position.x, 3.0f, 4.0f), Quaternion.Euler (90.0f, 0.0f, 0.0f));
					Destroy (obj, 0.15f);
				}
			}

		}
	}
}
