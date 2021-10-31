using UnityEngine;
using System.Collections;

public class SingleNorts : NortAbstract
{
	protected override void IsClicked()
	{
		float timeDifference = Mathf.Abs (timer - beatTime);
		if (timeDifference <= Define.badDifference) {
            if (timeDifference <= Define.excellentDifference)
            {
                scoreBoard.Judge(Define.JudgeType.PERFECT);
                soundPlayer.PlaySound(Define.NortsType.SINGLE);
            }
            else if (timeDifference <= Define.greatDifference)
            {
                scoreBoard.Judge(Define.JudgeType.GREAT);
                soundPlayer.PlaySound(Define.NortsType.SINGLE);
            }
            else if (timeDifference <= Define.safeDifference)
            {
                scoreBoard.Judge(Define.JudgeType.SAFE);
                soundPlayer.PlaySound(Define.NortsType.SINGLE);
            }
            else
                scoreBoard.Judge(Define.JudgeType.BAD);

			Destroy (gameObject);
			if (timeDifference <= Define.safeDifference) {
				var obj = Instantiate (effect, new Vector3 (transform.position.x, 3.0f, 1.4f), Quaternion.Euler (90.0f, 0.0f, 0.0f));
				Destroy (obj, 0.25f);
			}
		}
	}
}
