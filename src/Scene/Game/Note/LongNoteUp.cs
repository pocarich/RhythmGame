using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class LongNoteUp : NortAbstract
{
    enum State
    {
        DOWN,
        CONTINUE,
        UP
    }

    [SerializeField] GameObject effectLong;
    [SerializeField] GameObject startNortPrefab;
    [SerializeField] GameObject tapePrefab;
    [SerializeField] GameObject endNortPrefab;
    [SerializeField] GameObject startNort;

    GameObject tape;
    GameObject endNort;
    GameObject direction;
    State state = State.DOWN;
    Vector3 touchPoint;
    float effectTImer = 0.0f;
    int size = 0;

    // Use this for initialization
    protected override void Start()
    {
        inputSystem = GameObject.Find("InputSystem").GetComponent<InputSystem>();
        soundPlayer = GameObject.Find("SoundPlayer").GetComponent<SoundPlayer>();
        pushFlag = false;
        timer = 0.0f;
        gameMgr = GameObject.Find("GameMgr");
        scoreBoard = GameObject.Find("ScoreMgr").GetComponent<ScoreBoard>();
        speed = Define.baseSpeed + Define.speedChange * MainGameMgr.speedLevel;

        startNort = (GameObject)Instantiate(startNortPrefab, transform.position, Quaternion.Euler(0.0f, 0.0f, 0.0f));
        var scaleZ = time * speed / 10.0f;
        tape = (GameObject)Instantiate(tapePrefab, new Vector3(transform.position.x, transform.position.y + 0.03f, transform.position.z + 5 * scaleZ), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        tape.transform.localScale = new Vector3(0.075f, 1.0f, scaleZ);
        if (endNort == null)
        {
            endNort = (GameObject)Instantiate(endNortPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z + 10 * scaleZ), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        }

        startNort.GetComponent<LongNortsStart>().SetTime(time);
        endNort.GetComponent<LongNortsEnd>().SetTime(time);

        downTime = Define.ToBarDistance / speed;
        beatTime = downTime;
        upTime = downTime + time;

        touchPoint = transform.position;
        touchPoint.z = 1.6f;

        if (size != 0)
        {
            startNort.transform.localScale = new Vector3(10f, 37f * size, 6f);
            tape.transform.localScale = new Vector3(0.08f * size, 1f, tape.transform.localScale.z);
            endNort.transform.localScale = new Vector3(10f, 37f * size, 6f);
        }
    }

    public void SetInfo(float time)
    {
        this.time = time;
    }

    public override void Init(int size)
    {
        this.size = size;
        nortSize = size;
        diff_x = Define.diffXRange * Environment.ScreenPercent * size;
        if (startNort != null)
        {
            startNort.transform.localScale = new Vector3(10f, 37f * size, 6f);
            tape.transform.localScale = new Vector3(0.08f * size, 1f, tape.transform.localScale.z);
            endNort.transform.localScale = new Vector3(10f, 37f * size, 6f);
        }

    }

    protected override void IsClicked()
    {
    }

    public void SetType(int type)
    {
        if (endNort == null)
        {
            speed = Define.baseSpeed + Define.speedChange * MainGameMgr.speedLevel;
            var scaleZ = time * speed / 10.0f;
            endNort = (GameObject)Instantiate(endNortPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z + 10 * scaleZ), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        }
        direction = endNort.transform.Find("direction").gameObject;
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

    // Update is called once per frame
    protected override void Update()
    {
        if (gameMgr.GetComponent<GameMgr>().state != GameMgr.State.Game)
            return;
        UpdateNorts();
        //Move ();
        UpdateTimer();
        Disappear();
    }

    private void UpdateNorts()
    {
        if (!pushFlag)
            return;
        float timeDifference;
        switch (state)
        {
            case State.DOWN:
                timeDifference = Mathf.Abs(timer - downTime);
                if (timeDifference <= Define.safeDifference)
                {

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
                    else
                    {
                        scoreBoard.Judge(Define.JudgeType.SAFE);
                        soundPlayer.PlaySound(Define.NortsType.SINGLE);
                    }

                    Destroy(startNort);
                    var obj = Instantiate(effect, new Vector3(transform.position.x, 3.0f, 1.4f), Quaternion.Euler(90.0f, 0.0f, 0.0f));
                    Destroy(obj, 0.25f);
                    tape.GetComponent<LongNortsTape>().Click();
                    state = State.CONTINUE;
                    return;
                }
                else if (timeDifference <= Define.badDifference)
                {
                    scoreBoard.Judge(Define.JudgeType.BAD);
                    scoreBoard.Judge(Define.JudgeType.MISS);
                    Destroy(startNort);
                    Destroy(tape);
                    Destroy(endNort);
                    Destroy(gameObject);
                    return;
                }
                break;
            case State.CONTINUE:
                if (!CheckPush() && CheckUpPush())
                {
                    timeDifference = Mathf.Abs(timer - upTime);
                    if (timeDifference <= Define.badDifference)
                    {
                        if (timeDifference <= Define.excellentDifference)
                        {
                            scoreBoard.Judge(Define.JudgeType.PERFECT);
                            soundPlayer.PlaySound(Define.NortsType.UP);
                        }
                        else if (timeDifference <= Define.greatDifference)
                        {
                            scoreBoard.Judge(Define.JudgeType.GREAT);
                            soundPlayer.PlaySound(Define.NortsType.UP);
                        }
                        else if (timeDifference <= Define.safeDifference)
                        {
                            scoreBoard.Judge(Define.JudgeType.SAFE);
                            soundPlayer.PlaySound(Define.NortsType.UP);
                        }
                        else
                            scoreBoard.Judge(Define.JudgeType.BAD);

                        if (tape != null)
                            Destroy(tape);
                        Destroy(endNort);
                        Destroy(gameObject);
                        if (timeDifference <= Define.safeDifference)
                        {
                            var obj = Instantiate(effect, new Vector3(transform.position.x, 3.0f, 1.4f), Quaternion.Euler(90.0f, 0.0f, 0.0f));
                            Destroy(obj, 0.25f);
                        }
                        return;
                    }
                    else {
                        scoreBoard.Judge(Define.JudgeType.MISS);
                        if (tape != null)
                            Destroy(tape);
                        Destroy(endNort);
                        Destroy(gameObject);
                        return;
                    }
                }

                if (!CheckPush())
                {
                    Debug.Log("through");
                    scoreBoard.Judge(Define.JudgeType.MISS);
                    if (tape != null)
                        Destroy(tape);
                    if (endNort != null)
                        Destroy(endNort);
                    Destroy(gameObject);
                    return;
                }

                if (timer > upTime + Define.badDifference)
                {
                    scoreBoard.Judge(Define.JudgeType.MISS);
                    if (tape != null)
                        Destroy(tape);
                    Destroy(endNort);
                    Destroy(gameObject);
                    return;
                }

                effectTImer += Time.deltaTime;

                if (effectTImer >= 0.05f)
                {
                    var obj = Instantiate(effect, new Vector3(transform.position.x, 3.0f, 1.4f), Quaternion.Euler(90.0f, 0.0f, 0.0f));
                    Destroy(obj, 0.05f);
                }
                break;
        }
    }

    private void OnDestroy()
    {
        if (startNort != null)
            Destroy(startNort);
        if (tape != null)
            Destroy(tape);
        if (endNort != null)
            Destroy(endNort);
    }

    protected override void Disappear()
    {
        if (state == State.DOWN && timer > downTime + Define.badDifference)
        {
            scoreBoard.Judge(Define.JudgeType.MISS);
            scoreBoard.Judge(Define.JudgeType.MISS);
            Destroy(startNort);
            Destroy(tape);
            Destroy(endNort);
            Destroy(gameObject);
        }
    }

    bool CheckPush()
    {
        bool[] pushFlag = new bool[5];
        if (Define.inputType == Define.InputType.KEYBOARD)
        {
            pushFlag[0] = Input.GetKey(KeyCode.D);
            pushFlag[1] = Input.GetKey(KeyCode.F);
            pushFlag[2] = Input.GetKey(KeyCode.G);
            pushFlag[3] = Input.GetKey(KeyCode.H);
            pushFlag[4] = Input.GetKey(KeyCode.J);
        }
        else
        {
            pushFlag[0] = inputSystem.input[(int)Define.ArduinoInput.BUTTON_1, (int)Define.InputState.PUSHING];
            pushFlag[1] = inputSystem.input[(int)Define.ArduinoInput.BUTTON_2, (int)Define.InputState.PUSHING];
            pushFlag[2] = inputSystem.input[(int)Define.ArduinoInput.BUTTON_3, (int)Define.InputState.PUSHING];
            pushFlag[3] = inputSystem.input[(int)Define.ArduinoInput.BUTTON_4, (int)Define.InputState.PUSHING];
            pushFlag[4] = inputSystem.input[(int)Define.ArduinoInput.BUTTON_5, (int)Define.InputState.PUSHING];
        }
        return pushFlag[appearPos];
    }

    bool CheckUpPush()
    {
        bool[] pushFlag = new bool[5];
        if (Define.inputType == Define.InputType.KEYBOARD)
        {
            pushFlag[0] = Input.GetKeyUp(KeyCode.D);
            pushFlag[1] = Input.GetKeyUp(KeyCode.F);
            pushFlag[2] = Input.GetKeyUp(KeyCode.G);
            pushFlag[3] = Input.GetKeyUp(KeyCode.H);
            pushFlag[4] = Input.GetKeyUp(KeyCode.J);
        }
        else
        {
            pushFlag[0] = inputSystem.input[(int)Define.ArduinoInput.BUTTON_1, (int)Define.InputState.OUT];
            pushFlag[1] = inputSystem.input[(int)Define.ArduinoInput.BUTTON_2, (int)Define.InputState.OUT];
            pushFlag[2] = inputSystem.input[(int)Define.ArduinoInput.BUTTON_3, (int)Define.InputState.OUT];
            pushFlag[3] = inputSystem.input[(int)Define.ArduinoInput.BUTTON_4, (int)Define.InputState.OUT];
            pushFlag[4] = inputSystem.input[(int)Define.ArduinoInput.BUTTON_5, (int)Define.InputState.OUT];
        }
        return pushFlag[appearPos];
    }
    int GetPos1(int x)
    {
        for (int i = 1; i <= 8; i++)
        {
            if (Screen.width / 2 - 232 * Environment.ScreenPercent + 66 * (i - 1) * Environment.ScreenPercent - 25 * Environment.ScreenPercent <= x && x <= Screen.width / 2 - 232 * Environment.ScreenPercent + 66 * (i - 1) * Environment.ScreenPercent + 25 * Environment.ScreenPercent)
            {
                return i;
            }
        }
        return -1;
    }

    List<int> GetPos2(int x)
    {
        var l = new List<int>();
        for (int i = 1; i <= 7; i++)
        {
            if (Screen.width / 2 - 199 * Environment.ScreenPercent + 66 * (i - 1) * Environment.ScreenPercent - 50 * Environment.ScreenPercent <= x && x <= Screen.width / 2 - 199 * Environment.ScreenPercent + 66 * (i - 1) * Environment.ScreenPercent + 50 * Environment.ScreenPercent)
            {
                l.Add(i);
            }
        }
        return l;
    }
}