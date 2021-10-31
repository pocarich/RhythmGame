using UnityEngine;
using System.Collections;

public class SlideNorts : NortAbstract
{
    bool beatFlag = false;

    public void SetPos(Vector3 pos)
    {
        transform.position = pos;
    }

    public override void Init(int size)
    {
        nortSize = size;
        diff_x = Define.diffXRange * Environment.ScreenPercent * size;
        float xsize = 10f;
        float ysize = 37f * size;
        transform.localScale = new Vector3(xsize, ysize, 6f);
    }

    protected override void IsClicked()
    {
        float timeDifference = Mathf.Abs(timer - beatTime);
        if (timeDifference <= Define.greatDifference)
        {
            beatFlag = true;
        }

        if (beatFlag && timer >= beatTime)
        {
            scoreBoard.Judge(Define.JudgeType.PERFECT);
            Destroy(gameObject);
            var obj = Instantiate(effect, new Vector3(transform.position.x, 3.0f, 4.0f), Quaternion.Euler(90.0f, 0.0f, 0.0f));
            Destroy(obj, 0.15f);
        }
    }
}
