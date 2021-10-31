using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SameTimePushBar : MonoBehaviour
{
    [SerializeField] List<GameObject> connectObject;

    float speed;

    // Use this for initialization
    void Start()
    {
        speed = Define.baseSpeed + Define.speedChange * MainGameMgr.speedLevel;
        if (connectObject == null)
        {
            connectObject = new List<GameObject>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (connectObject.Exists(obj => obj == null)) 
        {
            Destroy(gameObject);
        }
        transform.Translate(0.0f, 0.0f, -speed * Time.deltaTime, Space.World);
    }

    public void AddObject(GameObject addObject)
    {
        if(connectObject==null)
        {
            connectObject = new List<GameObject>();
        }
        connectObject.Add(addObject);
    }
}
