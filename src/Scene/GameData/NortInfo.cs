using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NortInfo
{
	public float appearTime { private set; get; }
	public int nortType { private set; get; }
    public float appearPos { private set; get; }
    public float value { private set; get; }
    public float time { private set; get; }

    public NortInfo(float appearTime,int nortType,float appearPos,float value,float time)
	{
		this.appearTime = appearTime;
		this.nortType = nortType;
		this.appearPos = appearPos;
		this.time = time;
        this.value = value;
	}
}
