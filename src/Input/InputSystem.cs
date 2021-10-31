using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : SingletonMonoBehaviour<InputSystem>
{
    static readonly string[] inputChar = { "1", "2", "3", "4", "5" };

    [SerializeField] SerialHandler serialHandler;

    bool checkedFlag = false;
    float[] airInputRemainTimer = new float[10];

    public bool[,] input { private set; get; } = new bool[20, 3];
    public bool[] airInput { private set; get; } = new bool[5];
    public bool[] subInput { private set; get; } = new bool[20];

    public void Awake()
    {
        if (this != Instance)
        {
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    // Use this for initialization
    void Start()
    {
        serialHandler.OnDataReceived += AuduinoInput;
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                input[i, j] = false;
            }
        }
        for (int i = 0; i < 20; i++)
        {
            subInput[i] = false;
        }
        for(int i=0;i<airInputRemainTimer.Length;i++)
        {
            airInputRemainTimer[i] = 0f;
        }
    }
	
	// Update is called once per frame
	void Update () {
        UpdateKeyBoard();
        UpdateArduinoInput();
	}

    void UpdateAirInput()
    {
        for(int i=0;i!=airInputRemainTimer.Length;i++)
        {
            airInputRemainTimer[i] -= Time.deltaTime;
            airInputRemainTimer[i] = Mathf.Max(airInputRemainTimer[i], 0f);
            if (airInputRemainTimer[i] < 0.01f) 
            {
                airInput[i] = false;
            }
        }
    }

    void UpdateKeyBoard()
    {
        if(Define.inputType== Define.InputType.KEYBOARD)
        {
            input[(int)Define.KeyBoardInput.D, (int)Define.InputState.IN] = Input.GetKeyDown(KeyCode.D);
            input[(int)Define.KeyBoardInput.D, (int)Define.InputState.PUSHING] = Input.GetKey(KeyCode.D);
            input[(int)Define.KeyBoardInput.D, (int)Define.InputState.OUT] = Input.GetKeyUp(KeyCode.D);
            input[(int)Define.KeyBoardInput.F, (int)Define.InputState.IN] = Input.GetKeyDown(KeyCode.F);
            input[(int)Define.KeyBoardInput.F, (int)Define.InputState.PUSHING] = Input.GetKey(KeyCode.F);
            input[(int)Define.KeyBoardInput.F, (int)Define.InputState.OUT] = Input.GetKeyUp(KeyCode.F);
            input[(int)Define.KeyBoardInput.G, (int)Define.InputState.IN] = Input.GetKeyDown(KeyCode.G);
            input[(int)Define.KeyBoardInput.G, (int)Define.InputState.PUSHING] = Input.GetKey(KeyCode.G);
            input[(int)Define.KeyBoardInput.G, (int)Define.InputState.OUT] = Input.GetKeyUp(KeyCode.G);
            input[(int)Define.KeyBoardInput.H, (int)Define.InputState.IN] = Input.GetKeyDown(KeyCode.H);
            input[(int)Define.KeyBoardInput.H, (int)Define.InputState.PUSHING] = Input.GetKey(KeyCode.H);
            input[(int)Define.KeyBoardInput.G, (int)Define.InputState.OUT] = Input.GetKeyUp(KeyCode.G);//
            input[(int)Define.KeyBoardInput.J, (int)Define.InputState.IN] = Input.GetKeyDown(KeyCode.J);
            input[(int)Define.KeyBoardInput.J, (int)Define.InputState.PUSHING] = Input.GetKey(KeyCode.J);
        }
    }

    void UpdateArduinoInput()
    {
        for(int i=0;i<5;i++)
        {
            if(input[i,(int)Define.InputState.IN])
            {
                input[i, (int)Define.InputState.IN] = false;
            }
            else
            {
                if (!input[i, (int)Define.InputState.PUSHING] && subInput[i]) 
                {
                    input[i, (int)Define.InputState.IN] = true;
                }
            }

            if(input[i, (int)Define.InputState.OUT])
            {
                input[i, (int)Define.InputState.OUT] = false;
            }
            else
            {
                if (input[i, (int)Define.InputState.PUSHING] && !subInput[i])
                {
                    input[i, (int)Define.InputState.OUT] = true;
                }
            }

            input[i, (int)Define.InputState.PUSHING] = subInput[i];
        }
    }

    void AuduinoInput(string message)
    {
        var data = message.Split(new string[] { "\t" }, System.StringSplitOptions.None);
        if (data.Length < 1)
        {
            return;
        }
        for(int i=0;i<5;i++)
        {
            subInput[i] = false;
            airInput[i] = false;
        }
        for (int i = 0; i != data.Length; i++)
        {
            switch (data[i])
            {
                case "1":
                    subInput[0] = true;
                    break;
                case "2":
                    subInput[1] = true;
                    break;
                case "3":
                    subInput[2] = true;
                    break;
                case "4":
                    subInput[3] = true;
                    break;
                case "5":
                    subInput[4] = true;
                    break;
                case "6":
                    airInput[0] = true;
                    break;
                case "7":
                    airInput[1] = true;
                    break;
                case "8":
                    airInput[2] = true;
                    break;
                case "9":
                    airInput[3] = true;
                    break;
                case "10":
                    airInput[4] = true;
                    break;
            }
        }
    }
}
