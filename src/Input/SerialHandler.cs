using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Threading;
using System.Runtime.InteropServices;
using System;

public class SerialHandler : SingletonMonoBehaviour<SerialHandler>
{
    [SerializeField] string portName;
    [SerializeField] int baudRate;

    SerialPort serialPort_;
    Thread thread_;
    bool isRunning_ = false;
    int timer = 0;
    string message_;
    bool isNewMessageReceived_ = false;

    public delegate void SerialDataReceivedEventHandler(string message);
    public event SerialDataReceivedEventHandler OnDataReceived;


    public void Awake()
    {
        if (this != Instance)
        {
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(this.gameObject);

        if (Define.inputType == Define.InputType.ARDUINO)
        {
            Open();
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (isNewMessageReceived_)
        {
            Debug.Log("Push!");
            OnDataReceived(message_);
        }
        isNewMessageReceived_ = false;
    }

    void OnDestroy()
    {
        Close();
    }

    private void Open()
    {
        serialPort_ = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One);
        serialPort_.Open();
        serialPort_.DtrEnable = true;
        serialPort_.RtsEnable = true;
        serialPort_.DiscardInBuffer();
        isRunning_ = true;

        thread_ = new Thread(Read);
        thread_.Start();
    }

    private void Close()
    {
        isNewMessageReceived_ = false;
        isRunning_ = false;

        if (thread_ != null && thread_.IsAlive)
        {
            thread_.Abort();
            thread_.Join();
        }
        if (serialPort_ != null && serialPort_.IsOpen)
        {
            serialPort_.Close();
            serialPort_.Dispose();
        }
    }

    private void Read()
    {
        while (isRunning_ && serialPort_ != null && serialPort_.IsOpen)
        {

            try
            {
                timer++;
                message_ = serialPort_.ReadLine();
                isNewMessageReceived_ = true;
            }
            catch (TimeoutException e)
            {
                Debug.LogWarning(e.Message);
            }
        }
    }

    public void Write(string message)
    {
        try
        {
            serialPort_.Write(message);
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e.Message);
        }
    }
}
