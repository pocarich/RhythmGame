using UnityEngine;
using System.Collections;

public class WallMove : MonoBehaviour
{
	GameObject gameMgr;
    float speed;
    float moveValue = 0.0f;
    Vector3 firstPosition;

    // Use this for initialization
    void Start () {
		speed = Define.baseSpeed + Define.speedChange * MainGameMgr.speedLevel;
		firstPosition = transform.position;
		gameMgr=GameObject.Find ("GameMgr");
	}
	
	// Update is called once per frame
	void Update () {
        if (gameMgr.GetComponent<GameMgr>().state != GameMgr.State.Game)
            return;
		moveValue += speed*Time.deltaTime;
		transform.Translate(0.0f,0.0f,-speed*Time.deltaTime);
		if (moveValue >= 8.0f) {
			moveValue=moveValue-8.0f;
			transform.position=new Vector3(firstPosition.x,firstPosition.y,firstPosition.z-moveValue);
		}
	}
}
