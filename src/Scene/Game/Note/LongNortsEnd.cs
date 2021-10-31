using UnityEngine;
using System.Collections;

public class LongNortsEnd : MonoBehaviour
{
	GameObject gameMgr;
    float speed;

    public float timer { private set; get; }

    // Use this for initialization
    void Start () {
       // timer = 0f;
		gameMgr=GameObject.Find ("GameMgr");
		speed = Define.baseSpeed + Define.speedChange * MainGameMgr.speedLevel;
	}
	
	// Update is called once per frame
	void Update () {
        if (gameMgr.GetComponent<GameMgr>().state != GameMgr.State.Game)
            return;
		Move ();
        timer += Time.deltaTime;
	}

    public void SetTime(float timer)
    {
        this.timer = 0f;
        this.timer -= timer;
    }
	
	private void Move()
	{
		transform.Translate (0.0f, 0.0f, -speed * Time.deltaTime, Space.World);
	}
}
