using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    [SerializeField] SerialHandler serialHandler;
    [SerializeField] float delayTime;
   
    InputSystem inputSystem;
    float timer = 0f;
    bool pushFlag = false;

    // Use this for initialization
    void Start () {
        serialHandler = GameObject.Find("SerialHandler").GetComponent<SerialHandler>();
        inputSystem = GameObject.Find("InputSystem").GetComponent<InputSystem>();
        if (Define.inputType == Define.InputType.ARDUINO)
        {
            serialHandler.Write("4");
        }
    }

    // Update is called once per frame
    void Update () {
        if (!pushFlag)
        {
            switch (Define.inputType)
            {
                case Define.InputType.KEYBOARD:
                    if (Input.GetMouseButtonDown(0))
                    {
                        pushFlag = true;
                        GameObject.Find("MusicPlayer").GetComponent<MusicPlayerTitle>().pushFlag = true;
                        GameObject.Find("SoundPlayer").GetComponent<AudioSource>().Play();
                        GameObject.Find("White").GetComponent<TitleWhite>().StartFadeOut();
                        if (Define.inputType == Define.InputType.ARDUINO)
                        {
                            serialHandler.Write("5");
                        }
                    }
                    break;
                case Define.InputType.ARDUINO:
                    if(inputSystem.input[(int)Define.ArduinoInput.BUTTON_3, (int)Define.InputState.IN])
                    {
                        pushFlag = true;
                        GameObject.Find("MusicPlayer").GetComponent<MusicPlayerTitle>().pushFlag = true;
                        GameObject.Find("SoundPlayer").GetComponent<AudioSource>().Play();
                        GameObject.Find("White").GetComponent<TitleWhite>().StartFadeOut();
                        if (Define.inputType == Define.InputType.ARDUINO)
                        {
                            serialHandler.Write("5");
                        }
                    }
                    break;
            }
           
        }
        else
        {
            timer += Time.deltaTime;
            if(timer>=delayTime)
            {
                Application.LoadLevel("MusicSelectScene");
            }
        }

    }
}
