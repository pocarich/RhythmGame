using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Text;
using UnityEngine.UI;

public class MusicList : SingletonMonoBehaviour<MusicList>
{
    [SerializeField] Sprite[] graphList;
    [SerializeField] AudioClip[] musicListInspecter;

    GameObject errorMessage;
    GameObject loadMessage;

    static List<MusicInfo> musicInfoList = new List<MusicInfo> ();
	static public AudioClip[] musicList { private set; get; }
	static public Sprite[] spriteList { private set; get; }
    public bool loadedFlag{ private set; get; }

	public static List<MusicInfo> GetMusicInfoList()
	{
		return musicInfoList;
	}

	public void Awake()
	{
		if(this != Instance)
		{
			Destroy(this);
			return;
		}
		
		DontDestroyOnLoad(this.gameObject);

		if (Define.fileDeleteFlag) {
			DeleteFile();
		}

        errorMessage = GameObject.Find("ErrorMessage");
        if (errorMessage == null)
        {
            throw new Exception("ErrorMessageが見つかりませんでした。");
        }

        try
        {
            StartCoroutine(Load());
        }catch(Exception)
        {
            errorMessage.GetComponent<Text>().text = "楽曲データの読み込みでエラーが発生しました。\n楽曲データの確認を行ってください。";
        }
	}    
	
	// Use this for initialization
	void Start () {
		loadedFlag = false;
		spriteList = graphList;
		musicList = musicListInspecter;

        errorMessage = GameObject.Find("ErrorMessage");
        if(errorMessage==null)
        {
            throw new Exception("ErrorMessageが見つかりませんでした。");
        }
        loadMessage = GameObject.Find("LoadMessage");
        if (loadMessage == null)
        {
            throw new Exception("LoadMessageが見つかりませんでした。");
        }
    }

    void DeleteFile(){
		for (int num=1; num<=graphList.Length; num++) {
			string persistentPath = Application.persistentDataPath + "/" + "Music" + num.ToString ("000") + ".txt";
			if(File.Exists(persistentPath)){
				File.Delete(persistentPath);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {

	}

	IEnumerator Load()
	{
        if (Define.platform == Define.Platform.EDITOR) {
            for (int num = 1; num <= graphList.Length; num++) {
                string fileName = "Music" + num.ToString("000") + ".txt";

                string musicName = null;
                int[] levelList = new int[Enum.GetNames(typeof(Define.MusicLevel)).Length];
                int[] achievementList = new int[] { 0, 0, 0 };
                int[,] rankList = new int[,] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
                int[,] scoreList = new int[,] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
                float offset = 0;

                string SAfilePath = Application.streamingAssetsPath + "/" + fileName;
                using (var sr = new StreamReader(SAfilePath)) {
                    for (int i = 0; i < 7; i++) {
                        string dataString = sr.ReadLine();
                        if (dataString == null) {
                            errorMessage.GetComponent<Text>().text = fileName + "にてデータの取得に失敗しました。";
                            yield break;
                        }

                        switch (i) {
                            case 0:
                                musicName = Encoding.Default.GetString(Encoding.Convert(Encoding.Default, Encoding.Default, Encoding.Default.GetBytes(dataString)));

                                break;
                            case 1:
                                var dataList = dataString.Split(',');
                                if (dataList.Length != Enum.GetNames(typeof(Define.MusicLevel)).Length)
                                {
                                    errorMessage.GetComponent<Text>().text = fileName + "にてレベルデータの取得に失敗しました。";
                                    yield break;
                                }
                                for (int j = 0; j < Enum.GetNames(typeof(Define.MusicLevel)).Length; j++) {
                                    levelList[j] = int.Parse(dataList[j]);
                                }
                                break;
                            case 2:
                                try {
                                    offset = float.Parse(dataString);
                                } catch (Exception) {
                                    errorMessage.GetComponent<Text>().text = fileName + "にてオフセットの取得に失敗しました。";
                                    yield break;
                                }
                                break;
                        }
                    }
                }

                string persistentPath = Application.persistentDataPath + "/" + fileName;
                if (File.Exists(persistentPath)) {
                    using (var sr = new StreamReader(persistentPath)) {
                        for (int i = 0; i < 3; i++) {
                            string dataString = sr.ReadLine();
                            if (dataString == null) {
                                errorMessage.GetComponent<Text>().text = fileName + "にてデータの取得に失敗しました。";
                                yield break;
                            }

                            switch (i) {
                                case 0:
                                    var dataList = dataString.Split(',');
                                    if (dataList.Length != Enum.GetNames(typeof(Define.MusicLevel)).Length)
                                    {
                                        errorMessage.GetComponent<Text>().text = fileName + "にて実績データの取得に失敗しました。";
                                        yield break;
                                    }
                                    for (int j = 0; j < Enum.GetNames(typeof(Define.MusicLevel)).Length; j++) {
                                        achievementList[j] = int.Parse(dataList[j]);
                                    }
                                    break;
                                case 1:
                                    dataList = dataString.Split(',');
                                    if (dataList.Length != Enum.GetNames(typeof(Define.MusicLevel)).Length * Enum.GetNames(typeof(Define.Ranking)).Length)
                                    {
                                        errorMessage.GetComponent<Text>().text = fileName + "にてランクデータの取得に失敗しました。";
                                        yield break;
                                    }
								for (int j=0; j<Enum.GetNames(typeof(Define.MusicLevel)).Length; j++) {
									for (int k=0; k<Enum.GetNames(typeof(Define.Ranking)).Length; k++) {
										rankList [j, k] = int.Parse (dataList [j * Enum.GetNames (typeof(Define.MusicLevel)).Length + k]);
									}
								}
								break;
							case 2:
								dataList = dataString.Split (',');
                                    if (dataList.Length != Enum.GetNames(typeof(Define.MusicLevel)).Length * Enum.GetNames(typeof(Define.Ranking)).Length)
                                    {
                                        errorMessage.GetComponent<Text>().text = fileName + "にてスコアデータの取得に失敗しました。";
                                        yield break;
                                    }
								for (int j=0; j<Enum.GetNames(typeof(Define.MusicLevel)).Length; j++) {
									for (int k=0; k<Enum.GetNames(typeof(Define.Ranking)).Length; k++) {
										scoreList [j, k] = int.Parse (dataList [j * Enum.GetNames (typeof(Define.MusicLevel)).Length + k]);
									}
								}
								break;
							}
						}
					}
				}

				musicInfoList.Add (new MusicInfo (fileName, musicName, levelList, achievementList, rankList, scoreList, offset));

				yield return null;
			}
			loadedFlag = true;
		} else {
			for (int num=1; num<=graphList.Length; num++) {
				string fileName = "Music" + num.ToString ("000") + ".txt";
				var www = new WWW ("jar:file://" + Application.dataPath + "!/assets/" + fileName);
				yield return www;

				string musicName = null;
				int[] levelList = new int[Enum.GetNames (typeof(Define.MusicLevel)).Length];
				int[] achievementList = new int[]{0,0,0};
				int[,] rankList = new int[,]{{0,0,0},{0,0,0},{0,0,0}};
				int[,] scoreList = new int[,]{{0,0,0},{0,0,0},{0,0,0}};
				float offset = 0;

				using (var sr=new StringReader(www.text)) {
					for (int i=0; i<7; i++) {
						string dataString = sr.ReadLine ();
						if (dataString == null) {
							throw new Exception (fileName + "にてデータの取得に失敗しました。");
						}
						
						switch (i) {
						case 0:
							musicName = Encoding.Default.GetString (Encoding.Convert (Encoding.Default, Encoding.Default, Encoding.Default.GetBytes (dataString)));

							break;
						case 1:
							var dataList = dataString.Split (',');
							if (dataList.Length != Enum.GetNames (typeof(Define.MusicLevel)).Length)
								throw new Exception (fileName + "にてレベルデータの取得に失敗しました。");
							for (int j=0; j<Enum.GetNames(typeof(Define.MusicLevel)).Length; j++) {
								levelList [j] = int.Parse (dataList [j]);
							}
							break;
						case 2:
							try {
								offset = float.Parse (dataString);
							} catch (Exception) {
								throw new Exception (fileName + "にてオフセットの取得に失敗しました。");
							}
							break;
						}
					}
				}

				string persistentPath = Application.persistentDataPath + "/" + fileName;
				if (File.Exists (persistentPath)) {
					using (var sr=new StreamReader(persistentPath)) {
						for (int i=0; i<3; i++) {
							string dataString = sr.ReadLine ();
							if (dataString == null) {
								throw new Exception (fileName + "にてデータの取得に失敗しました。");
							}

							switch (i) {
							case 0:
								var dataList = dataString.Split (',');
								if (dataList.Length != Enum.GetNames (typeof(Define.MusicLevel)).Length)
									throw new Exception (fileName + "にて実績データの取得に失敗しました。");
								for (int j=0; j<Enum.GetNames(typeof(Define.MusicLevel)).Length; j++) {
									achievementList [j] = int.Parse (dataList [j]);
								}
								break;
							case 1:
								dataList = dataString.Split (',');
								if (dataList.Length != Enum.GetNames (typeof(Define.MusicLevel)).Length * Enum.GetNames (typeof(Define.Ranking)).Length)
									throw new Exception (fileName + "にてランクデータの取得に失敗しました。");
								for (int j=0; j<Enum.GetNames(typeof(Define.MusicLevel)).Length; j++) {
									for (int k=0; k<Enum.GetNames(typeof(Define.Ranking)).Length; k++) {
										rankList [j, k] = int.Parse (dataList [j * Enum.GetNames (typeof(Define.MusicLevel)).Length + k]);
									}
								}
								break;
							case 2:
								dataList = dataString.Split (',');
								if (dataList.Length != Enum.GetNames (typeof(Define.MusicLevel)).Length * Enum.GetNames (typeof(Define.Ranking)).Length)
									throw new Exception (fileName + "にてスコアデータの取得に失敗しました。");
								for (int j=0; j<Enum.GetNames(typeof(Define.MusicLevel)).Length; j++) {
									for (int k=0; k<Enum.GetNames(typeof(Define.Ranking)).Length; k++) {
										scoreList [j, k] = int.Parse (dataList [j * Enum.GetNames (typeof(Define.MusicLevel)).Length + k]);
									}
								}
								break;
							}
						}
					}
				}
				
				musicInfoList.Add (new MusicInfo (fileName, musicName, levelList, achievementList, rankList, scoreList, offset));

				yield return null;
			
			}
			loadedFlag = true;
		}
	}
}
