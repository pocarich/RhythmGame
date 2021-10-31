using UnityEngine;
using System.Collections;
using System.Linq;

public class UpNort : NortAbstract
{
    [SerializeField] GameObject direction;
    [SerializeField] GameObject AirBarPrefab;

    GameObject airBar;

    public float pushedTimer { private set; get; } = 0f;
    public bool connectFlag { private set; get; }

    protected override void Start()
    {
        base.Start();
        airBar = null;
    }

    protected override void Update()
    {
        if (gameMgr.GetComponent<GameMgr>().state != GameMgr.State.Game)
            return;
        if (pushFlag) IsClicked();
        else
        {
            UpdateTimer();
            Disappear();
        }
        Move();
    }

    protected override void IsClicked()
    {
        pushedTimer += Time.deltaTime;
        if (pushedTimer <= Define.badDifference)
        {
            if (inputSystem.airInput[appearPos])
            {
                scoreBoard.Judge(Define.JudgeType.PERFECT);
                soundPlayer.PlaySound(Define.NortsType.UP);

                Destroy(gameObject);

                var obj = Instantiate(effect, new Vector3(transform.position.x, 3.0f, 1.4f), Quaternion.Euler(90.0f, 0.0f, 0.0f));
                Destroy(obj, 0.25f);

                if (airBar != null)
                {
                    Destroy(airBar);
                }
            }
        }
        else
        {
            scoreBoard.Judge(Define.JudgeType.MISS);
            Destroy(gameObject);
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
                direction.transform.Rotate(new Vector3(0f, 0f, -30f));
                direction.transform.Translate(new Vector3(0.2f, 0.1f, 0f));
                break;
            case 2:
                direction.transform.Rotate(new Vector3(0f, 0f, 30f));
                direction.transform.Translate(new Vector3(-0.2f, 0.1f, 0f));
                break;
        }
    }

    public void SetConnectAir(bool connect)
    {
        connectFlag = connect;

        if(connectFlag)
        {
            GameObject[] nortsList = GameObject.FindGameObjectsWithTag("Nort");
            var selectedNotes = nortsList.Where(p => !p.GetComponent<NortAbstract>().pushFlag)
                                 .Where(p => p.GetComponent<NortAbstract>() is AirNote)
                                 .Where(p => (p.GetComponent<NortAbstract>().timer < 0.25))
                                 .Where(p => (Mathf.Abs(transform.position.x - p.transform.position.x) <= 0.2));

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
        }
    }
}
