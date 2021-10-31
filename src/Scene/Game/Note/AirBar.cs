using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBar : MonoBehaviour
{
    float speed;

    // Use this for initialization
    void Start () {
        speed = Define.baseSpeed + Define.speedChange * MainGameMgr.speedLevel;
    }

    // Update is called once per frame
    void Update () {
        transform.Translate(0.0f, 0.0f, -speed * Time.deltaTime, Space.World);
    }
}
