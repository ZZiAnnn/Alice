using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TilemapMove : MonoBehaviour
{
    private GameObject[] tilemap=new GameObject[32];
    public GameObject mushRoomPreferb;
    private GameObject mr;
    private float timer = 0f;
    public float Speed=0.05f;
    private float ystart,zstart;
    float lasttime;
    void Start()
    {
        StartCoroutine(StartTimer());
        lasttime = Time.time;
        for (int i=0;i<2;++i)
        {
            tilemap[i] = GameObject.Find("Tilemap"+(i+1));
        }
        ystart=tilemap[0].transform.position.y;
        zstart=tilemap[0].transform.position.z;
    }

    void Update()
    {
        if (Time.time - lasttime >= 3)
        {
            for (int i = 0; i < 2; ++i)
            {
                if (tilemap[i].transform.position.x < -27.7f)
                {
                   tilemap[i].transform.position = new Vector3(26.2f, ystart, zstart);
                   if(mr==null) mr = Instantiate(mushRoomPreferb, transform.position + new Vector3(20.0f, -0.5f, 0), Quaternion.identity);
                }
                else
                {
                    tilemap[i].transform.position -= new Vector3(Time.deltaTime*Speed, 0, 0);
                }
            }
        }
        BarrierController((int)timer);
    }
    void BarrierController(int t)
    {
        Debug.Log(t);
        if(t>=60)//finished
        {

        }
        else
        {

        }

    }
    void OnGUI() // 在屏幕上显示计时器的数值
    {
        int t;
        if(timer<=3) t=0;
        else t=(int)timer-3;
        GUI.Label(new Rect(10, 10, 100, 20), "Timer: " + t.ToString("F2"));
    }
    IEnumerator StartTimer()
    {
        while (true)
        {
            timer = Time.time;
            yield return null;
        }
    }
}


