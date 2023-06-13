using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class bridgeMove : MonoBehaviour
{
    private GameObject[] bridgeMap = new GameObject[32];

    public float v0 = 1.0f;
    //public float accumlation = 0.5f;
    public static float currentTime = 0f;
    public static float Speed = 2.00f;
    private float ystart, zstart;
    float lasttime;

    void Start()
    {
        Audio.instance.PlayMusicByName("bgm2");
        StartCoroutine(StartTimer());
        lasttime = Time.time;
        for (int i = 0; i < 4; ++i)
        {
            bridgeMap[i] = GameObject.Find("bridge" + (i + 1));
        }
        ystart = bridgeMap[0].transform.position.y;
        zstart = bridgeMap[0].transform.position.z;
    }
    void Update()
    {
        if (Time.time - lasttime >= 3)
        {
            for (int i = 0; i < 4; ++i)
            {
                if (bridgeMap[i].transform.position.x < -18.69f)
                {
                    bridgeMap[i].transform.position = new Vector3(23.58f, ystart, zstart);
                }
                else
                {
                    bridgeMap[i].transform.position -= new Vector3(Time.deltaTime * Speed, 0, 0);
                }
            }
        }
        BarrierController((int)currentTime);
        BarrierDestroy();
    }
    void BarrierDestroy()
    {

    }
    void BarrierController(int t)
    {
        Speed = v0 + 0.1f * t / 60;
        
    }

    /*
    bool isMushRoomAppear(int x)
    {
        foreach (int mrs in mushRoomShow)
        {
            if (x == mrs) return true;
        }
        return false;
    }

    bool isSmallMushRoomAppear(int x)
    {
        foreach (int smrs in smallMushRoomShow)
        {
            if (x == smrs) return true;
        }
        return false;
    }

    bool isSeaweedAppear(int x)
    {
        foreach (int sw in seaweedShow)
        {
            if (x == sw) return true;
        }
        return false;
    }

    bool isFishAppear(int x)
    {
        foreach (int fs in fishShow)
        {
            if (x == fs) return true;
        }
        return false;
    }
    */

    void OnGUI() // 在屏幕上显示计时器的数值
    {
        int second = (int)(currentTime % 60);
        int minute = (int)currentTime / 60;
        string minutes = (minute).ToString("00"); // 转换分钟数并格式化
        string seconds = (second).ToString("00"); // 转换秒数并格式化
        //if (second < 0&& minute == 0) GUI.Label(new Rect(10, 10, 100, 20), "Timer: 00:00");
        //else
        GUI.Label(new Rect(10, 10, 100, 20), "Timer: " + minutes + ":" + seconds);
    }
    IEnumerator StartTimer()
    {

        while (true)
        {
            currentTime = Time.time - lasttime;
            if (currentTime <= 3) currentTime = 0;
            else currentTime -= 3;
            yield return null;
        }
    }
}


