using UnityEngine;
using System.Collections;
using System.Linq;

public class DownNote : NortAbstract
{
    [SerializeField] GameObject direction;
    [SerializeField] GameObject AirBarPrefab;

    GameObject airBar;

    protected override void Start()
    {
        base.Start();
        airBar = null;

        GameObject[] nortsList = GameObject.FindGameObjectsWithTag("Nort");
        GameObject[] downNortsList = GameObject.FindGameObjectsWithTag("DownNort");

        var selectedNotes = nortsList.Where(p => !p.GetComponent<NortAbstract>().pushFlag)
                             .Where(p => p.GetComponent<NortAbstract>() is AirNote)
                             .Where(p => Mathf.Abs(p.GetComponent<NortAbstract>().airAppearPos - appearPos) <= 0.01f)
                             .Where(p => (p.GetComponent<NortAbstract>().timer < 0.3))
                             .OrderBy(p => p.GetComponent<NortAbstract>().timer);

        var selectedDownNotes = downNortsList
                            .Where(p => Mathf.Abs(p.transform.position.x - transform.position.x) <= 0.01f)
                            .Where(p => (p.GetComponent<LongNortsStart>().timer < 0.3))
                            .OrderBy(p => p.GetComponent<LongNortsStart>().timer);

        if (selectedNotes.Count() != 0)
        {
            GameObject To = selectedNotes.First();

            var pos = (transform.position + To.transform.position) / 2;
            var from = transform.position;
            var to = To.transform.position;
            var angle = Mathf.Atan2(to.z - from.z, to.x - from.x);
            float length = (to - from).magnitude;
            var angle2 = Mathf.Atan2(to.y - from.y, Mathf.Sqrt((to.x - from.x) * (to.x - from.x) + (to.z - from.z) * (to.z - from.z)));
            airBar = (GameObject)Instantiate(AirBarPrefab, pos, Quaternion.Euler(0f, -angle * (180 / Mathf.PI), angle2 * (180 / Mathf.PI)));
            airBar.transform.localScale = new Vector3(length, 0.03f, 0.03f);
        }
        else if(selectedDownNotes.Count()!=0)
        {
            GameObject To = selectedDownNotes.First();

            var pos = (transform.position + To.transform.position) / 2;
            var from = transform.position;
            var to = To.transform.position;
            var angle = Mathf.Atan2(to.z - from.z, to.x - from.x);
            float length = (to - from).magnitude;
            var angle2 = Mathf.Atan2(to.y - from.y, Mathf.Sqrt((to.x - from.x) * (to.x - from.x) + (to.z - from.z) * (to.z - from.z)));
            airBar = (GameObject)Instantiate(AirBarPrefab, pos, Quaternion.Euler(0f, -angle * (180 / Mathf.PI), angle2 * (180 / Mathf.PI)));
            airBar.transform.localScale = new Vector3(length, 0.03f, 0.03f);
        }
    }

    protected override void IsClicked()
    {
        float timeDifference = Mathf.Abs(timer - beatTime);
        if (timeDifference <= Define.badDifference)
        {
            if (timeDifference <= Define.excellentDifference)
            {
                scoreBoard.Judge(Define.JudgeType.PERFECT);
                soundPlayer.PlaySound(Define.NortsType.DOWN);
            }
            else if (timeDifference <= Define.greatDifference)
            {
                scoreBoard.Judge(Define.JudgeType.GREAT);
                soundPlayer.PlaySound(Define.NortsType.DOWN);
            }
            else if (timeDifference <= Define.safeDifference)
            {
                scoreBoard.Judge(Define.JudgeType.SAFE);
                soundPlayer.PlaySound(Define.NortsType.DOWN);
            }
            else
                scoreBoard.Judge(Define.JudgeType.BAD);

            Destroy(gameObject);
            if (timeDifference <= Define.safeDifference)
            {
                var obj = Instantiate(effect, new Vector3(transform.position.x, 3.0f, 1.4f), Quaternion.Euler(90.0f, 0.0f, 0.0f));
                Destroy(obj, 0.25f);
            }
            if (airBar != null)
            {
                Destroy(airBar);
            }
        }
    }

    protected override void Disappear()
    {
        if (timer > disappearTime)
        {
            scoreBoard.Judge(Define.JudgeType.MISS);
            Destroy(gameObject);
            if (airBar != null)
            {
                Destroy(airBar);
            }
        }
    }

    public void SetType(int type)
    {
        switch (type)
        {
            case 0:
                break;
            case 1:
                direction.transform.Rotate(new Vector3(0f, 0f, 30f));
                direction.transform.Translate(new Vector3(-0.2f, 0.1f, 0f));
                break;
            case 2:
                direction.transform.Rotate(new Vector3(0f, 0f, -30f));
                direction.transform.Translate(new Vector3(0.2f, 0.1f, 0f));
                break;
        }
    }
}
