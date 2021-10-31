using UnityEngine;
using System.Collections;

public abstract class NortAbstract : MonoBehaviour
{
	[SerializeField] protected GameObject effect;

	protected GameObject gameMgr;
    protected SoundPlayer soundPlayer;
    protected InputSystem inputSystem;

    protected float speed;
    protected float disappearTime;
    protected static ScoreBoard scoreBoard;
    protected Touch touch;

    public bool pushFlag{ protected set; get;}
	public float timer{ protected set; get;}
	public float beatTime{ protected set; get;}
	public float diff_x { protected set; get; }
	public int nortSize { protected set; get; }
	public int appearPos { protected set; get; }
    public float airAppearPos { protected set; get; }
    public float upTime { protected set; get; }
    public float downTime { protected set; get; }
    public bool endPickedFlag { set; get; }
    public float popTime { protected set; get; }
    public float time { set; get; }

    // Use this for initialization
    protected virtual void Start () {
		pushFlag = false;
		timer = 0.0f;
		gameMgr=GameObject.Find ("GameMgr");
        inputSystem = GameObject.Find("InputSystem").GetComponent<InputSystem>();
        scoreBoard = GameObject.Find ("ScoreMgr").GetComponent<ScoreBoard> ();
		speed = Define.baseSpeed + Define.speedChange * MainGameMgr.speedLevel;
		beatTime = Define.ToBarDistance / speed;
		disappearTime = beatTime + Define.badDifference;
        soundPlayer = GameObject.Find("SoundPlayer").GetComponent<SoundPlayer>();
	}
	
	// Update is called once per frame
	protected virtual void Update () {
        if (gameMgr.GetComponent<GameMgr>().state != GameMgr.State.Game)
            return;
		if(pushFlag)IsClicked ();
		Move ();
		UpdateTimer ();
		Disappear ();
	}

	public virtual void Init(int value){
		
	}

	public virtual void SetAppear(int pos,float _popTime)
	{
		appearPos = pos;
        popTime = _popTime;
	}
	
	protected abstract void IsClicked ();
	
	protected void Move()
	{
		transform.Translate (0.0f, 0.0f, -speed * Time.deltaTime, Space.World);
	}
	
	protected void UpdateTimer()
	{
		timer += Time.deltaTime;
	}
	
	protected virtual void Disappear()
	{
		if (timer > disappearTime) {
			scoreBoard.Judge(Define.JudgeType.MISS);
			Destroy(gameObject);
		}
	}
	
	public void Push(){
		pushFlag = true;
	}

	public virtual void Push(Touch touch){
		pushFlag = true;
		this.touch = touch;
	}

    public float CalcEndNotePopTime()
    {
        return Mathf.Abs(upTime - downTime - timer);
    }
}
