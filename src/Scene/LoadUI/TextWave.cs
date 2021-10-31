using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class TextWave : BaseMeshEffect
{
    [SerializeField] float interval = 0.1f;
    [SerializeField] float delay = 1f;
    [SerializeField] float period = 0.3f;
    [SerializeField] float amplitude = 5f;

    List<float> defaultPos = new List<float>();
    List<float> offset = new List<float>();
    List<float> moveTimer = new List<float>();
    List<bool> UpFlag = new List<bool>();
    float timer = 0f;
    bool initFlag = false;

    void Init(ref List<UIVertex> vertices)
    {
        offset.Clear();
        for (int i = 0; i < (vertices.Count / 6); i++) 
        {
            offset.Add(0f);
            moveTimer.Add(0f);
            UpFlag.Add(false);
        }
        for (int i = 0; i < vertices.Count; i++)
        {
            defaultPos.Add(vertices[i].position.y);
        }

        initFlag = true;
        Debug.Log(defaultPos[0]);
    }

    public override void ModifyMesh(UnityEngine.UI.VertexHelper vh)
    {
        if (!IsActive())
            return;

        List<UIVertex> vertices = new List<UIVertex>();
        vh.GetUIVertexStream(vertices);

        if(!initFlag)
        {
            Init(ref vertices);
        }
        TextMove(ref vertices);

        vh.Clear();
        vh.AddUIVertexTriangleStream(vertices);
    }

    void TextMove(ref List<UIVertex> vertices)
    {

        for (int i = 0; i < offset.Count; i++)
        {
            if (i * interval <= timer && !UpFlag[i])
            {
                UpFlag[i] = true;
            }

            if (UpFlag[i] && moveTimer[i] < period)
            {
                offset[i] = amplitude * Mathf.Sin(Mathf.PI / period * moveTimer[i]);
                moveTimer[i] += Time.deltaTime;
            }
            if(moveTimer[i]>=period)
            {
                offset[i] = 0f;
            }

            for (int j = 0; j < 6; j++)
            {
                var element = vertices[(i * 6) + j];
                element.position.y = defaultPos[(i * 6) + j] + offset[i];
                vertices[(i * 6) + j] = element;
            }
        }
        timer += Time.deltaTime;
        if (timer >= (offset.Count * interval + delay))
        {
            timer = 0f;
            for (int i = 0; i < offset.Count; i++)
            {
                UpFlag[i] = false;
                moveTimer[i] = 0f;
            }
        }
    }

    void Update()
    {
        base.GetComponent<Graphic>().SetVerticesDirty();
    }
}