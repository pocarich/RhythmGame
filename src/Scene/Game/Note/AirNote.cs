using UnityEngine;
using System.Linq;
using System.Collections;

public class AirNote : NortAbstract
{
    [SerializeField] GameObject AirBarPrefab;

    GameObject airBar;
    bool beatFlag = false;

    protected override void Start()
    {
        base.Start();
        airBar = null;
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
            soundPlayer.PlaySound(Define.NortsType.AIR);
            Destroy(gameObject);
            var obj = Instantiate(effect, new Vector3(transform.position.x, 3.0f, 1.4f), Quaternion.Euler(90.0f, 0.0f, 0.0f));
            Destroy(obj, 0.25f);
            if(airBar!=null)
            {
                Destroy(airBar);
            }
        }
    }

    public void SetAppear(float pos, float _popTime)
    {
        appearPos = 0;
        airAppearPos = (int)(pos * 4f);
        popTime = _popTime;
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

    public void ConnectRing(GameObject To)
    {
        /*var radius = 0.25f;
        var pos = (transform.position + To.transform.position) / 2;
        var from = transform.position;
        var to = To.transform.position;
        var angle = Mathf.Atan2(to.z - from.z, to.x - from.x);
        var fromRing = from + new Vector3(radius * Mathf.Cos(angle), 0f, radius * Mathf.Sin(angle));
        var toRing = to + new Vector3(radius * Mathf.Cos(angle + Mathf.PI), 0f, radius * Mathf.Sin(angle + Mathf.PI));
        float length = (toRing - fromRing).magnitude;
        var angle2 = Mathf.Atan2(toRing.y - fromRing.y, Mathf.Sqrt((toRing.x - fromRing.x) * (toRing.x - fromRing.x) + (toRing.z - fromRing.z) * (toRing.z - fromRing.z)));
        airBar = (GameObject)Instantiate(AirBarPrefab, pos, Quaternion.Euler(0f, -angle*(180/Mathf.PI), angle2 * (180 / Mathf.PI)));
        airBar.transform.localScale = new Vector3(length, 0.03f, 0.03f);*/

        var pos = (transform.position + To.transform.position) / 2;
        var from = transform.position;
        var to = To.transform.position;
        var angle = Mathf.Atan2(to.z - from.z, to.x - from.x);
        float length = (to - from).magnitude;
        var angle2 = Mathf.Atan2(to.y - from.y, Mathf.Sqrt((to.x - from.x) * (to.x - from.x) + (to.z - from.z) * (to.z - from.z)));
        airBar = (GameObject)Instantiate(AirBarPrefab, pos, Quaternion.Euler(0f, -angle * (180 / Mathf.PI), angle2 * (180 / Mathf.PI)));
        airBar.transform.localScale = new Vector3(length, 0.03f, 0.03f);

    }

    public void ConnectUp()
    {
        GameObject[] nortsList = GameObject.FindGameObjectsWithTag("Nort");
        GameObject[] upNortsList = GameObject.FindGameObjectsWithTag("UpNort");

        var selectedNotes = nortsList.Where(p => !p.GetComponent<NortAbstract>().pushFlag)
                             .Where(p => p.GetComponent<NortAbstract>() is UpNort)
                             .Where(p => Mathf.Abs(p.GetComponent<NortAbstract>().appearPos - airAppearPos) <= 0.01f)
                             .Where(p => (p.GetComponent<NortAbstract>().timer < 0.3))
                             .OrderBy(p => p.GetComponent<NortAbstract>().timer);

        var selectedUpNotes = upNortsList
                            .Where(p => Mathf.Abs(p.transform.position.x - transform.position.x) <= 0.01f)
                            .Where(p => (0f<= p.GetComponent<LongNortsEnd>().timer&&p.GetComponent<LongNortsEnd>().timer < 0.3f))
                            .OrderBy(p => p.GetComponent<LongNortsEnd>().timer);

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
        else if (selectedUpNotes.Count() != 0)
        {
            GameObject To = selectedUpNotes.First();

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
}
