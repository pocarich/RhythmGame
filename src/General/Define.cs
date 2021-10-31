using UnityEngine;
using System.Collections;

static public class Define
{
	public enum Platform
	{
		EDITOR,
		ANDROID
	}

	public static readonly Platform platform=Platform.EDITOR;
    public static readonly InputType inputType = InputType.KEYBOARD;
	public static readonly bool fileDeleteFlag=false;

	public static readonly int baseWidth=608;
	public static readonly int baseHeight = 1080;
	public static readonly int baseCanvusWidth=800;
	public static readonly int baseCanvusHeight=600;

	public static readonly string textFileName="music.txt";
	public static readonly string graphFileName="graph.png";
	public static readonly string musicFileName="sound.ogg";

	public static readonly float baseSpeed=10.0f;
	public static readonly float speedChange=2.0f;
	public static readonly float ToBarDistance=35.0f-1.4f;
    public static readonly float airHeight = 0.5f;

	public static readonly float diffXRange=150.0f;
	public static readonly float FlickRangeHosei=2.0f;
	public static readonly float NeedFlickAmount=5f;

	public static readonly float excellentDifference=0.07f;
	public static readonly float greatDifference=0.12f;
	public static readonly float safeDifference=0.20f;
	public static readonly float badDifference=0.25f;

	public static readonly float borderLine = 0.7f;

	public static readonly int baseScore=100;
	public static readonly int needComboToUpScore=10;
	public static readonly int addScore=2;

	public static readonly float musicEndDelayTime=3.0f;

	public static readonly float S_BorderLine=0.98f;
	public static readonly float A_BorderLine=0.95f;
	public static readonly float B_BorderLine=0.9f;
	public static readonly float C_BorderLine=0.8f;
	public static readonly float D_BorderLine=0.7f;

    public static readonly float airInputRemainTime = 0.25f; 

	public enum MusicLevel{
		EASY,
		NORMAL,
		HARD
	}
	public enum Ranking{
		FIRST,
		SECOND,
		THIRD
	}
	public enum ClearRank{
		S=1,
		A=2,
		B=3,
		C=4,
		D=5,
		E=6,
        F=7
	}

	public enum NortsType
	{
		SINGLE,
        UP,
        DOWN,
        LONG,
        AIR
	}

	public enum JudgeType
	{
		PERFECT,
		GREAT,
		SAFE,
		BAD,
		MISS
	}

	public enum Achieve
	{
        FAILED=0,
		CLEAR=1,
		FULLCOMBO=2,
		ALLEXCELLENT=3
	}

    public enum InputType
    {
        KEYBOARD,
        ARDUINO
    }

    public enum InputState
    {
        IN,
        PUSHING,
        OUT
    }

    public enum KeyBoardInput
    {
        D,
        F,
        G,
        H,
        J
    }

    public enum ArduinoInput
    {
        BUTTON_1,
        BUTTON_2,
        BUTTON_3,
        BUTTON_4,
        BUTTON_5,
        AIR_1,
        AIR_2,
        AIR_3,
        AIR_4,
        AIR_5,
        AIR
    }
}
