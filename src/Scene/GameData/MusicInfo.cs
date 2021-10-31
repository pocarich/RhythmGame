using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class MusicInfo
{
	public string fileName{ private set; get;}
	public string musicName{ private set; get;}
	public float offset{ private set; get;}
	public Sprite sprite{ private set; get;}
	public AudioClip audioClip{ private set; get;}
    public bool updateFlag{ set; get;}

    int[] levelList = new int[Enum.GetNames(typeof(Define.MusicLevel)).Length];
    int[] achievementList = new int[Enum.GetNames(typeof(Define.MusicLevel)).Length];
    int[,] rankList = new int[Enum.GetNames(typeof(Define.MusicLevel)).Length, Enum.GetNames(typeof(Define.Ranking)).Length];
    int[,] scoreList = new int[Enum.GetNames(typeof(Define.MusicLevel)).Length, Enum.GetNames(typeof(Define.Ranking)).Length];

    public MusicInfo(string fileName,string musicName,int[] levelList,int[] achievementList ,int[,] rankList,int[,] scoreList,float offset){
		this.fileName = fileName;
		this.musicName = musicName;
		this.levelList = levelList;
		this.achievementList = achievementList;
		this.rankList = rankList;
		this.scoreList = scoreList;
		this.offset = offset;
		updateFlag = false;
	}

	public int[] GetLevelList()
	{
		return (int[])levelList.Clone();
	}

	public int[] GetAchievementList()
	{
		return (int[])achievementList.Clone ();
	}

	public int[,] GetRankList()
	{
		return (int[,])rankList.Clone ();
	}

	public int[,] GetScoreList()
	{
		return (int[,])scoreList.Clone ();
	}

	public IEnumerator UpdateInfo(){
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
		updateFlag = true;
		yield return null;
	}
}
