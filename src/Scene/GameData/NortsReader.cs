using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;
using UnityEngine.UI;

public class NortsReader : SingletonMonoBehaviour<NortsReader>
{
    static readonly string[] readStartSymbol = new string[] { "EASY", "NORMAL", "HARD" };

    GameObject errorMessage;

    public static int maxCombo { private set; get; }
    public static List<NortInfo> nortInfoList { private set; get; } = new List<NortInfo>();
    public static bool endFlag { set; get; } = false;

    public void Awake()
    {
        if (this != Instance)
        {
            Destroy(this);
            return;
        }

        errorMessage = GameObject.Find("ErrorMessage");
        if (errorMessage == null)
        {
            throw new Exception("ErrorMessageが見つかりませんでした。");
        }

        DontDestroyOnLoad(this.gameObject);
    }

    // Use this for initialization
    void Start()
    {
        endFlag = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartLoad()
    {
        try { 
        StartCoroutine(Load());
        }
        catch (Exception)
        {
            errorMessage.GetComponent<Text>().text = "楽曲データの読み込みでエラーが発生しました。\n楽曲データの確認を行ってください。";
        }
    }

    IEnumerator Load()
    {
        nortInfoList.Clear();
        string SAfilePath = Application.streamingAssetsPath + "/" + MusicList.GetMusicInfoList()[MainGameMgr.musicNum].fileName;
        using (var sr = new StreamReader(SAfilePath))
        {
            int rithm = -1;
            string dataString = null;
            float takeTime = 0f;
            float phraseTime = -1f;
            float bpm = 1;

            maxCombo = 0;

            while (dataString != readStartSymbol[(int)MainGameMgr.musicLevel])
                dataString = sr.ReadLine();
            for (int i = 0; ;)
            {
                dataString = sr.ReadLine();
                if (dataString == "$")
                    break;
                if (dataString[0] == '/')
                {
                    try
                    {
                        rithm = int.Parse(new string(dataString.Skip(1).ToArray()));
                        phraseTime = (60.0f * rithm) / bpm;
                    }
                    catch (Exception)
                    {
                        errorMessage.GetComponent<Text>().text="リズムの読み込みに失敗しました。";
                        yield break;
                    }
                    continue;
                }
                if (dataString[0] == '#')
                {
                    if (rithm == -1)
                        throw new Exception();
                    try
                    {
                        bpm = float.Parse(new string(dataString.Skip(1).ToArray()));
                        phraseTime = (60.0f * rithm) / bpm;
                    }
                    catch (Exception)
                    {
                        errorMessage.GetComponent<Text>().text = "BPM読み込みに失敗しました。";
                        yield break;
                    }
                    continue;
                }

                if(dataString[0]=='%')
                {
                    int dataLength = (new string(dataString.Skip(1).ToArray())).Split('|').Length;
                    
                    int j = 0;
                    foreach (var nortsInfo in (new string(dataString.Skip(1).ToArray())).Split('|'))
                    {
                        if (nortsInfo == "")
                        {
                            errorMessage.GetComponent<Text>().text = "ノーツデータのサイズが不正です。";
                            yield break;
                        }
                        if (nortsInfo[0] != '*')
                        {
                            foreach (var nortInfo in nortsInfo.Split('&'))
                            {

                                int nortType = -1;
                                float appearPos = -1f; //0f~1f
                                float value = -1f;
                                float time = -1f;

                                int k = 0;
                                if (nortInfo.Split(',').Length != 4)
                                {
                                    errorMessage.GetComponent<Text>().text = "譜面データのサイズが不正です。";
                                    yield break;
                                }
                                foreach (var nortElement in nortInfo.Split(','))
                                {
                                    switch (k)
                                    {
                                        case 0:
                                            nortType = int.Parse(nortElement);
                                            if (4 <= nortType && nortType <= 7)
                                                maxCombo += 2;
                                            else
                                                maxCombo++;
                                            break;
                                        case 1:
                                            appearPos = float.Parse(nortElement);
                                            break;
                                        case 2:
                                            value = float.Parse(nortElement);
                                            break;
                                        case 3:
                                            time = float.Parse(nortElement) * phraseTime;
                                            break;
                                        default:
                                            errorMessage.GetComponent<Text>().text = "データの個数が多すぎます。";
                                            yield break;
                                            break;
                                    }
                                    k++;
                                }

                                nortInfoList.Add(new NortInfo(takeTime + phraseTime / dataLength * j, nortType, appearPos, value, time));
                            }
                        }
                        Debug.Log(j);
                        j++;
                    }
                    continue;
                }

                {
                    int dataLength = dataString.Split('|').Length;
                   
                    int j = 0;
                    foreach (var nortsInfo in dataString.Split('|'))
                    {
                        if (nortsInfo == "")
                        {
                            errorMessage.GetComponent<Text>().text = "ノーツデータのサイズが不正です。";
                            yield break;
                        }
                        if (nortsInfo[0] != '*')
                        {
                            foreach (var nortInfo in nortsInfo.Split('&'))
                            {

                                int nortType = -1;
                                float appearPos = -1f; //0f~1f
                                float value = -1f;
                                float time = -1f;

                                int k = 0;
                                if (nortInfo.Split(',').Length != 4)
                                {
                                    errorMessage.GetComponent<Text>().text = "譜面データのサイズが不正です。";
                                    yield break;
                                }
                                foreach (var nortElement in nortInfo.Split(','))
                                {
                                    switch (k)
                                    {
                                        case 0:
                                            nortType = int.Parse(nortElement);
                                            if (4 <= nortType && nortType <= 7)
                                                maxCombo += 2;
                                            else
                                                maxCombo++;
                                            break;
                                        case 1:
                                            appearPos = float.Parse(nortElement);
                                            break;
                                        case 2:
                                            value = float.Parse(nortElement);
                                            break;
                                        case 3:
                                            time = float.Parse(nortElement) * phraseTime;
                                            break;
                                        default:
                                            errorMessage.GetComponent<Text>().text = "データの個数が多すぎます。";
                                            yield break;
                                            break;
                                    }
                                    k++;
                                }

                                nortInfoList.Add(new NortInfo(takeTime + phraseTime / dataLength * j, nortType, appearPos, value, time));
                            }
                        }
                        j++;
                    }
                }

                takeTime += phraseTime;
                i++;
                yield return null;
            }
        }
        endFlag = true;
    }
}