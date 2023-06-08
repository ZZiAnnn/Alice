using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TilemapMove : MonoBehaviour
{
    private GameObject[] tilemap=new GameObject[32];
    public GameObject mushRoomPreferb,smallMashroomPreferb,floatSteepPreferb;
    private GameObject barrier;
    public float v0=2.0f;
    public float accumlation=0.2f;
    public static  float currentTime = 0f;
    public static float Speed=2.00f;
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
                }
                else
                {
                    tilemap[i].transform.position -= new Vector3(Time.deltaTime*Speed, 0, 0);
                }
            }
        }
        BarrierController((int)currentTime);
        BarrierDestroy();
    }
    void BarrierDestroy()
    {
        if(barrier!=null&&barrier.transform.position.x<-10.0f) Destroy(barrier);
    }
    void BarrierController(int t)
    {
        Speed=2+6.0f*t/60;
        //Debug.Log(t);
        if(t>=60)//finished
        {

        }
        else if(t==3&&barrier==null) barrier = Instantiate(mushRoomPreferb, transform.position + new Vector3(20.0f, -0.5f, 0), Quaternion.identity);
        else
        {

        }

    }
    void OnGUI() // 在屏幕上显示计时器的数值
    {
        int second = (int)(currentTime % 60) - 3;
        int minute = (int)currentTime / 60;
        string minutes = (minute).ToString("00"); // 转换分钟数并格式化
        string seconds = (second).ToString("00"); // 转换秒数并格式化
        if (second < 0) GUI.Label(new Rect(10, 10, 100, 20), "Timer: 00:00");
        else GUI.Label(new Rect(10, 10, 100, 20), "Timer: " + minutes + ":" + seconds);
    }
    IEnumerator StartTimer()
    {
        while (true)
        {
            currentTime = Time.time;
            yield return null;
        }
    }
}


