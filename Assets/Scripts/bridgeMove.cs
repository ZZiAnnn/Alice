using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class bridgeMove : MonoBehaviour
{
    private GameObject[] bridgeMap = new GameObject[32];
    public GameObject coinPrefab;
    float v0 = 2.0f;
    //public float accumlation = 0.5f;
    public static float currentTime = 0f;
    public static float Speed = 0;
    private float ystart, zstart;
    float lasttime;
    private GameObject coin; //金币
    private int[] coinShow = { 1, 5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60 };

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
        if (coin != null && coin.transform.position.x < -15.0f) Destroy(coin);
    }

    void BarrierController(int t)
    {
        Speed = v0 + 0.1f * t / 60;
        if (isCoinAppear(t) && coin == null)
        {
            if(t%2==1) coin = Instantiate(coinPrefab, new Vector3(12f, -1f, 0), Quaternion.Euler(0f, 0f, 0f));
            else coin = Instantiate(coinPrefab, new Vector3(12f, 0f, 0), Quaternion.Euler(0f, 0f, 0f));

        }
    }

    bool isCoinAppear(int x)
    {
        foreach (int cn in coinShow)
        {
            if (x == cn) return true;
        }
        return false;
    }

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


