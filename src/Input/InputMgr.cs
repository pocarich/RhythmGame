using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class InputMgr : SingletonMonoBehaviour<InputMgr>
{
    GameObject gameMgr;
    InputSystem inputSystem;

    // Use this for initialization
    void Start()
    {
        gameMgr = GameObject.Find("GameMgr");
        inputSystem=GameObject.Find("InputSystem").GetComponent<InputSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameMgr.GetComponent<GameMgr>().state != GameMgr.State.Game)
            return;
        _Input();
    }

    void _Input()
    {
        InputPush();
        InputAir();
    }

    void InputPush()
    {
        bool[] pushFlag = new bool[5];

        if (Define.inputType == Define.InputType.KEYBOARD)
        {
            pushFlag[0] = Input.GetKeyDown(KeyCode.D);
            pushFlag[1] = Input.GetKeyDown(KeyCode.F);
            pushFlag[2] = Input.GetKeyDown(KeyCode.G);
            pushFlag[3] = Input.GetKeyDown(KeyCode.H);
            pushFlag[4] = Input.GetKeyDown(KeyCode.J);
        }
        else if (Define.inputType == Define.InputType.ARDUINO)
        {
            pushFlag[0] = inputSystem.input[(int)Define.ArduinoInput.BUTTON_1, (int)Define.InputState.IN];
            pushFlag[1] = inputSystem.input[(int)Define.ArduinoInput.BUTTON_2, (int)Define.InputState.IN];
            pushFlag[2] = inputSystem.input[(int)Define.ArduinoInput.BUTTON_3, (int)Define.InputState.IN];
            pushFlag[3] = inputSystem.input[(int)Define.ArduinoInput.BUTTON_4, (int)Define.InputState.IN];
            pushFlag[4] = inputSystem.input[(int)Define.ArduinoInput.BUTTON_5, (int)Define.InputState.IN];
        }
        bool isPush = false;
        for (int i = 0; i < 5; i++)
        {
            if (pushFlag[i])
            {
                isPush = true;
                break;
            }
        }
        if (!isPush)
        {
            return;
        }
        GameObject[] nortsList = GameObject.FindGameObjectsWithTag("Nort");

        for (int i = 0; i < 5; i++)
        {
            if (pushFlag[i])
            {
                var selectedNotes = nortsList.Where(p => !p.GetComponent<NortAbstract>().pushFlag)
                       .Where(p => !(p.GetComponent<NortAbstract>() is AirNote))
                       .Where(p => (p.GetComponent<NortAbstract>().appearPos == i))
                       .Where(p => Mathf.Abs(p.GetComponent<NortAbstract>().timer - p.GetComponent<NortAbstract>().beatTime) <= Define.badDifference)
                       .OrderByDescending(p => p.GetComponent<NortAbstract>().timer);

                if (selectedNotes.Count() != 0)
                {
                    selectedNotes.First().GetComponent<NortAbstract>().Push();
                }
            }
        }
    }

    void InputAir()
    {
        GameObject[] nortsList = GameObject.FindGameObjectsWithTag("Nort");

        for (int i = 0; i < 5; i++)
        {
            if (inputSystem.airInput[i])
            {
                var selectedNotes = nortsList.Where(p => !p.GetComponent<NortAbstract>().pushFlag)
                       .Where(p => p.GetComponent<NortAbstract>() is AirNote)
                       .Where(p => (Mathf.Abs(p.GetComponent<NortAbstract>().airAppearPos - (float)i)<0.01f))
                       .Where(p => Mathf.Abs(p.GetComponent<NortAbstract>().timer - p.GetComponent<NortAbstract>().beatTime) <= Define.badDifference)
                       .OrderByDescending(p => p.GetComponent<NortAbstract>().timer);

                if (selectedNotes.Count() != 0)
                {
                    selectedNotes.First().GetComponent<NortAbstract>().Push();
                }
            }
        }
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

