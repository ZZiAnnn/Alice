using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TilemapMove : MonoBehaviour
{
    private GameObject[] tilemap=new GameObject[32];
    public GameObject mushRoomPreferb1, mushRoomPreferb2, smallMashroomPreferb, seaweedPreferb1, seaweedPreferb2, fishPreferb;//, floatSteepPreferb;
    public GameObject triggerPreferb;

    private GameObject barrier;
    private GameObject barrier2;
    private GameObject seaweed;
    private GameObject fish;
    private GameObject trigger;

    public float v0 = 4.0f;
    public float accumlation=0.2f;
    public static float currentTime = 0f;
    public static float Speed=2.00f;
    private float ystart,zstart;
    float lasttime;
    private int[] mushRoomShow = { 6, 11, 16, 22, 25, 28, 31, 34, 39, 43, 47, 51, 54, 57, 59 };
    private int[] smallMushRoomShow = { 1, 5, 14, 17, 27, 30, 32, 37, 45, 48, 50, 52 };
    private int[] fishShow = { 2, 5, 10, 15, 20, 24, 28, 32, 36, 40, 43, 46, 49, 52, 55, 58, 61 };
    private int[] seaweedShow = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 15, 20, 24, 29, 32, 37, 40, 43, 47, 52, 57, 61 };

    void Start()
    {
        Audio.instance.PlayMusicByName("bgm2");
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
                if (tilemap[i].transform.position.x < -20.69f)
                {
                   tilemap[i].transform.position = new Vector3(20.58f, ystart, zstart);
                }
                else
                {
                    tilemap[i].transform.position -= new Vector3(Time.deltaTime * Speed, 0, 0);
                }
            }
        }
        BarrierController((int)currentTime);
        BarrierDestroy();
    }
    void BarrierDestroy()
    {
        if (barrier != null && barrier.transform.position.x < -15.0f) Destroy(barrier);
        if (barrier2 != null && barrier2.transform.position.x < -15.0f) Destroy(barrier2);
        if (fish != null && fish.transform.position.x < -15.0f) Destroy(fish);
        if (seaweed != null && seaweed.transform.position.x < -15.0f) Destroy(seaweed);
    }
    void BarrierController(int t)
    {
        Speed = v0 + 6.0f * t / 60;
        if (t >= 60)//finished
        {
            if(trigger==null)trigger = Instantiate(triggerPreferb, transform.position + new Vector3(0, 0, 0), Quaternion.identity);
        }
        else
        {
            if (isMushRoomAppear(t) && barrier == null && t % 2 == 1) barrier = Instantiate(mushRoomPreferb1, transform.position + new Vector3(20.0f, -0.50f, 0), Quaternion.identity);
            else if (isMushRoomAppear(t) && barrier == null && t % 2 == 0) barrier = Instantiate(mushRoomPreferb2, transform.position + new Vector3(20.0f, 2.00f, 0), Quaternion.identity);
            if(isSmallMushRoomAppear(t) && barrier2 == null) barrier2 = Instantiate(smallMashroomPreferb, transform.position + new Vector3(20.0f, 1.05f, 0), Quaternion.identity);
            if (isFishAppear(t) && fish == null) fish = Instantiate(fishPreferb, new Vector3(12f, -3.7f, 0), Quaternion.Euler(0f, 0f, 0f));
            //下面控制水草生成
            if (isSeaweedAppear(t) && seaweed == null && t % 2 == 1) seaweed = Instantiate(seaweedPreferb1, new Vector3(12f, -5f, 0), Quaternion.identity);
            else if (isSeaweedAppear(t) && seaweed == null && t % 2 == 0) seaweed = Instantiate(seaweedPreferb2, new Vector3(12f, -3f, 0), Quaternion.Euler(180f, 0f, 0f));
        }
    }

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


