using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;

public class Save : MonoBehaviour
{
	bool saveFlag=false;

	// Use this for initialization
	void Start () {
		StartCoroutine (SaveScore ());
	}
	
	// Update is called once per frame
	void Update () {
		if (saveFlag&&CheckUpdate()) {
			MusicList.GetMusicInfoList () [MainGameMgr.musicNum].updateFlag=false;
			Application.LoadLevel ("MusicSelectScene");
		}
	}

	IEnumerator SaveScore()
	{
		if (Define.platform == Define.Platform.EDITOR) {
			var musicInfo=MusicList.GetMusicInfoList () [MainGameMgr.musicNum];
			string persistentPath = Application.persistentDataPath + "/" + musicInfo.fileName;
			var strList = new List<string> ();
			strList.Add("");
			strList.Add("");
			strList.Add("");

			var achieveList=new List<int>(musicInfo.GetAchievementList());
			achieveList[(int)MainGameMgr.musicLevel] = Mathf.Max (achieveList[(int)MainGameMgr.musicLevel], (int)Clear.achieveType);
			for (int i=0; i<3; i++) {
				strList [0] += achieveList[i]+"";
				if (i != 2) {
					strList [0] += ",";
				}
			}

			var scoreList = new List<List<int>> ();
			var rankList = new List<List<int>> ();
			for(int i=0;i<3;i++){
				var subScoreList=new List<int>();
				var subRankList=new List<int>();
				for(int j=0;j<3;j++){
					subScoreList.Add (musicInfo.GetScoreList()[i,j]);
					subRankList.Add(musicInfo.GetRankList()[i,j]);
				}
				scoreList.Add (subScoreList);
				rankList.Add (subRankList);
			}

			scoreList[(int)MainGameMgr.musicLevel].Add ((int)ScoreBoard.score);
			rankList [(int)MainGameMgr.musicLevel].Add((int)Clear.clearRank);

			for (int j=1; j<scoreList[(int)MainGameMgr.musicLevel].Count; j++) {
				if (scoreList [(int)MainGameMgr.musicLevel][j] > scoreList [(int)MainGameMgr.musicLevel][j - 1]) {
					var cpScore = scoreList [(int)MainGameMgr.musicLevel][j];
					var cpRank = rankList [(int)MainGameMgr.musicLevel][j];
					scoreList[(int)MainGameMgr.musicLevel][j] = scoreList [(int)MainGameMgr.musicLevel][j-1];
					scoreList [(int)MainGameMgr.musicLevel][j-1] = cpScore;
					rankList [(int)MainGameMgr.musicLevel][j] = rankList [(int)MainGameMgr.musicLevel][j-1];
					rankList [(int)MainGameMgr.musicLevel][j-1] = cpRank;
					for (int j2=j-1; j2>0; j2--) {
						if (scoreList [(int)MainGameMgr.musicLevel][j2] > scoreList [(int)MainGameMgr.musicLevel][j2 - 1]) {
							var cpScore_ = scoreList [(int)MainGameMgr.musicLevel][j2];
							var cpRank_ = rankList [(int)MainGameMgr.musicLevel][j2];
							scoreList [(int)MainGameMgr.musicLevel][j2] = scoreList [(int)MainGameMgr.musicLevel][j2 - 1];
							scoreList [(int)MainGameMgr.musicLevel][j2 - 1] = cpScore_;
							rankList [(int)MainGameMgr.musicLevel][j2] = rankList [(int)MainGameMgr.musicLevel][j2 - 1];
							rankList [(int)MainGameMgr.musicLevel][j2 - 1] = cpRank_;
						}
					}
				}
			}

			for (int i=0; i<9; i++) {
				strList[2]+=scoreList[(int)(i/3)][i%3]+"";
				strList[1]+=rankList[(int)(i/3)][i%3]+"";
				if(i!=8){
					strList[2]+=",";
					strList[1]+=",";
				}
			}

			using (var sw=new StreamWriter(persistentPath)) {
				for (int i=0; i<strList.Count; i++) {
					sw.WriteLine (strList [i]);
					yield return null;
				}
			}
		}
		else if(Define.platform==Define.Platform.ANDROID){
			var musicInfo=MusicList.GetMusicInfoList () [MainGameMgr.musicNum];
			string persistentPath = Application.persistentDataPath + "/" + musicInfo.fileName;
			var strList = new List<string> ();
			strList.Add("");
			strList.Add("");
			strList.Add("");
			
			var achieveList=new List<int>(musicInfo.GetAchievementList());
			achieveList[(int)MainGameMgr.musicLevel] = Mathf.Max (achieveList[(int)MainGameMgr.musicLevel], (int)Clear.achieveType);
			for (int i=0; i<3; i++) {
				strList [0] += achieveList[i]+"";
				if (i != 2) {
					strList [0] += ",";
				}
			}
			
			var scoreList = new List<List<int>> ();
			var rankList = new List<List<int>> ();
			for(int i=0;i<3;i++){
				var subScoreList=new List<int>();
				var subRankList=new List<int>();
				for(int j=0;j<3;j++){
					subScoreList.Add (musicInfo.GetScoreList()[i,j]);
					subRankList.Add(musicInfo.GetRankList()[i,j]);
				}
				scoreList.Add (subScoreList);
				rankList.Add (subRankList);
			}
			
			scoreList[(int)MainGameMgr.musicLevel].Add ((int)ScoreBoard.score);
			rankList [(int)MainGameMgr.musicLevel].Add((int)Clear.clearRank);
			
			for (int j=1; j<scoreList[(int)MainGameMgr.musicLevel].Count; j++) {
				if (scoreList [(int)MainGameMgr.musicLevel][j] > scoreList [(int)MainGameMgr.musicLevel][j - 1]) {
					var cpScore = scoreList [(int)MainGameMgr.musicLevel][j];
					var cpRank = rankList [(int)MainGameMgr.musicLevel][j];
					scoreList[(int)MainGameMgr.musicLevel][j] = scoreList [(int)MainGameMgr.musicLevel][j-1];
					scoreList [(int)MainGameMgr.musicLevel][j-1] = cpScore;
					rankList [(int)MainGameMgr.musicLevel][j] = rankList [(int)MainGameMgr.musicLevel][j-1];
					rankList [(int)MainGameMgr.musicLevel][j-1] = cpRank;
					for (int j2=j-1; j2>0; j2--) {
						if (scoreList [(int)MainGameMgr.musicLevel][j2] > scoreList [(int)MainGameMgr.musicLevel][j2 - 1]) {
							var cpScore_ = scoreList [(int)MainGameMgr.musicLevel][j2];
							var cpRank_ = rankList [(int)MainGameMgr.musicLevel][j2];
							scoreList [(int)MainGameMgr.musicLevel][j2] = scoreList [(int)MainGameMgr.musicLevel][j2 - 1];
							scoreList [(int)MainGameMgr.musicLevel][j2 - 1] = cpScore_;
							rankList [(int)MainGameMgr.musicLevel][j2] = rankList [(int)MainGameMgr.musicLevel][j2 - 1];
							rankList [(int)MainGameMgr.musicLevel][j2 - 1] = cpRank_;
						}
					}
				}
			}
			
			for (int i=0; i<9; i++) {
				strList[2]+=scoreList[(int)(i/3)][i%3]+"";
				strList[1]+=rankList[(int)(i/3)][i%3]+"";
				if(i!=8){
					strList[2]+=",";
					strList[1]+=",";
				}
			}
			
			using (var sw=new StreamWriter(persistentPath)) {
				for (int i=0; i<strList.Count; i++) {
					sw.WriteLine (strList [i]);
					yield return null;
				}
			}
		}
	
		StartCoroutine(MusicList.GetMusicInfoList()[MainGameMgr.musicNum].UpdateInfo());

		saveFlag = true;
	}

	bool CheckUpdate(){
		return MusicList.GetMusicInfoList () [MainGameMgr.musicNum].updateFlag;
	}

}
