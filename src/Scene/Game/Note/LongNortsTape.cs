using UnityEngine;
using System.Collections;

public class LongNortsTape : MonoBehaviour
{
	GameObject gameMgr;
    float speed;
    bool clickedFlag = false;

    // Use this for initialization
    void Start () {
		gameMgr=GameObject.Find ("GameMgr");
		speed = Define.baseSpeed + Define.speedChange * MainGameMgr.speedLevel;
	}

	public void Click()
	{
		clickedFlag = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (gameMgr.GetComponent<GameMgr>().state != GameMgr.State.Game)
            return;
		if(clickedFlag)IsClicked ();
		Move ();
	}
	
	private void IsClicked()
	{
		float overZ = 1.6f - (transform.position.z - 5.0f * transform.localScale.z);
		if (overZ > 0.0f) {
			transform.localScale=new Vector3 (transform.localScale.x, transform.localScale.y, Mathf.Max (0.0f,transform.localScale.z-overZ/10.0f));
			transform.position=new Vector3 (transform.position.x, transform.position.y, 1.6f+5.0f*transform.localScale.z);
			if(transform.localScale.z==0.0f)
				Destroy (gameObject);
		}
	}
	
	private void Move()
	{
		transform.Translate (0.0f, 0.0f, -speed * Time.deltaTime, Space.World);
	}
}
