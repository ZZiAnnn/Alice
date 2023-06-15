using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class TilemapMove : MonoBehaviour
{
    private GameObject[] tilemap=new GameObject[32];

    public GameObject mushRoomPreferb1, mushRoomPreferb2, smallMashroomPreferb, coinPreferb, signPreferb,
                        seaweedPreferb1, seaweedPreferb2, fishPreferb,shopPreferb;//Ԥ����
    public GameObject triggerPreferb; //������Ԥ����
    private GameObject barrier; //Ģ��ǽ
    private GameObject barrier2; //СĢ��ǽ
    private GameObject barrier3; //Ģ��ǽ
    private GameObject seaweed; //ˮ��
    private GameObject fish; //��
    private GameObject trigger; //������
    private GameObject shop; //�̵�
    private GameObject coin; //���
    private GameObject sign; //·��

    public float v0 = 4.0f;
    public float accumlation=0.2f;
    public static float currentTime = 0f; //��ʱ��
    public static float Speed=2.00f;
    private float maxSpeed;
    private float ystart,zstart;
    float lasttime;


    private int[] mushRoomShow1 = { 6, 16, 25, 31, 39, 47, 54, 59 }; //Ģ��ǽ���ֵ�ʱ��
    private int[] mushRoomShow2 = { 11, 22, 28, 34, 43, 51, 57, }; //Ģ��ǽ���ֵ�ʱ��
    private int[] smallMushRoomShow = { 1, 5, 14, 17, 27, 30, 32, 37, 45, 48, 50, 52 }; //СĢ��ǽ���ֵ�ʱ��
    private int[] fishShow = { 2, 5, 10, 15, 20, 24, 28, 32, 36, 40, 43, 46, 49, 52, 55, 58, 61 }; //����ֵ�ʱ��
    private int[] seaweedShow = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 15, 20, 24, 29, 32, 37, 40, 43, 47, 52, 57, 61 }; //ˮ�ݳ��ֵ�ʱ��
    private int[] coinShow = { 5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60 };


    void Start()
    {
        Audio.instance.PlayMusicByName("bgm5"); //���ű�������
        StartCoroutine(StartTimer()); //��ʼ��ʱ
        lasttime = Time.time;
        for (int i=0;i<2;++i)
        {
            tilemap[i] = GameObject.Find("Tilemap"+(i+1)); //��ȡtilemap
        }
        ystart=tilemap[0].transform.position.y;
        zstart=tilemap[0].transform.position.z;
        SimulatedAnnealing(); //ģ���˻��㷨
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
        if (barrier3 != null && barrier3.transform.position.x < -15.0f) Destroy(barrier3);
        if (fish != null && fish.transform.position.x < -15.0f) Destroy(fish);
        if (seaweed != null && seaweed.transform.position.x < -15.0f) Destroy(seaweed);
        if (coin != null && coin.transform.position.x < -15.0f) Destroy(coin);
    }
    void BarrierController(int t) //�����ϰ��������
    {
        if (t>= 60&&t<63)//finished
        {
            Speed=maxSpeed-maxSpeed/8*(t-60);
            if(shop==null) shop=Instantiate(shopPreferb, transform.position + new Vector3(25, 2.5f, 1), Quaternion.identity);
            if (sign == null) sign = Instantiate(signPreferb, transform.position + new Vector3(-11f, 2f, 0f), Quaternion.identity);
            if (trigger==null)trigger = Instantiate(triggerPreferb, transform.position + new Vector3(37.3f, 2.57f, 0), Quaternion.identity);
        }
        else if(t>=63)
        {
            Speed=0; //ֹͣ�ƶ�
        }
        else
        {
            Speed = v0 + 6.0f * t / 60; //����
            maxSpeed=Speed; //��¼����ٶ�
            if (isMushRoomAppear1(t) && barrier==null) barrier = Instantiate(mushRoomPreferb1, transform.position + new Vector3(20.0f, -0.50f, 0), Quaternion.identity); //����Ģ��ǽ
            if (isMushRoomAppear2(t) && barrier3==null) barrier3 = Instantiate(mushRoomPreferb2, transform.position + new Vector3(20.0f, 2.00f, 0), Quaternion.identity); //����Ģ��ǽ
            if(isSmallMushRoomAppear(t) && barrier2 == null) barrier2 = Instantiate(smallMashroomPreferb, transform.position + new Vector3(20.0f, 1.05f, 0), Quaternion.identity); //����СĢ��ǽ
            if (isFishAppear(t) && fish == null) fish = Instantiate(fishPreferb, new Vector3(12f, -3.7f, 0), Quaternion.Euler(0f, 0f, 0f)); //������
            if (isCoinAppear(t) && coin == null) coin = Instantiate(coinPreferb, new Vector3(12f, -1f, 0), Quaternion.Euler(0f, 0f, 0f));
            if (isSeaweedAppear(t) && seaweed == null && t % 2 == 1) seaweed = Instantiate(seaweedPreferb1, new Vector3(12f, -5f, 0), Quaternion.identity);
            else if (isSeaweedAppear(t) && seaweed == null && t % 2 == 0) seaweed = Instantiate(seaweedPreferb2, new Vector3(12f, -3f, 0), Quaternion.Euler(180f, 0f, 0f));
        }
    }

    bool isMushRoomAppear1(int x)  //Ģ��ǽ���ֵ�ʱ��
    {
        foreach (int mrs in mushRoomShow1)
        {
            if (x == mrs) return true;
        }
        return false;
    }

    bool isMushRoomAppear2(int x) //Ģ��ǽ���ֵ�ʱ��
    {
        foreach (int mrs in mushRoomShow2)
        {
            if (x == mrs) return true;
        }
        return false;
    }

    bool isSmallMushRoomAppear(int x) //СĢ��ǽ���ֵ�ʱ��
    {
        foreach (int smrs in smallMushRoomShow)
        {
            if (x == smrs) return true;
        }
        return false;
    }

    bool isSeaweedAppear(int x) //ˮ�ݳ��ֵ�ʱ��
    {
        foreach (int sw in seaweedShow)
        {
            if (x == sw) return true;
        }
        return false;
    }

    bool isFishAppear(int x) //����ֵ�ʱ��
    {
        foreach (int fs in fishShow)
        {
            if (x == fs) return true;
        }
        return false;
    }

    bool isCoinAppear(int x)
    {
        foreach (int cn in coinShow)
        {
            if (x == cn) return true;
        }
        return false;
    }


    void OnGUI() // ����Ļ����ʾ��ʱ������ֵ
    {
        int second = (int)(currentTime % 60);
        int minute = (int)currentTime / 60;
        string minutes = (minute).ToString("00"); // ת������������ʽ��
        string seconds = (second).ToString("00"); // ת����������ʽ��
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

    //ģ���˻��㷨
    public double Rand(){ //����0-1֮��������
        System.Random rd=new System.Random();
        double x=rd.NextDouble();
        return x;
    }
    //�����ϰ��������
    public double Energy(int kind,int time){
        double energy=0;
        if(kind==1){
            if(time>=60&&time<63){
                energy=0;
            }
            else if(time>=63){
                energy=100000;
            }
            else{
                energy=1000000;
            }
        }
        else if(kind==2){
            if(time>=60&&time<63){
                energy=0;
            }
            else if(time>=63){
                energy=100000;
            }
            else{
                energy=1000000;
            }
        }
        else if(kind==3){
            if(time>=60&&time<63){
                energy=0;
            }
            else if(time>=63){
                energy=100000;
            }
            else{
                energy=1000000;
            }
        }
        else if(kind==4){
            if(time>=60&&time<63){
                energy=0;
            }
            else if(time>=63){
                energy=100000;
            }
            else{
                energy=1000000;
            }
        }
        else if(kind==5){
            if(time>=60&&time<63){
                energy=0;
            }
            else if(time>=63){
                energy=100000;
            }
            else{
                energy=1000000;
            }
        }
        else if(kind==6){
            if(time>=60&&time<63){
                energy=0;
            }
            else if(time>=63){
                energy=100000;
            }
            else{
                energy=1000000;
            }
        }
        return energy;
    }
    public void SimulatedAnnealing()
    {
        //ģ���˻��㷨�ı�mushRoomShow1���ﵽ�ϰ����ں��ʵ�ʱ����ֵ�Ч��
        //��ʼ�����ϰ���ĳ���ʱ��
        int[] mushRoomShow1 = { 6, 16, 25, 31, 39, 47, 54, 59 }; //Ģ��ǽ���ֵ�ʱ��
        int[] mushRoomShow2 = { 11, 22, 28, 34, 43, 51, 57, }; //Ģ��ǽ���ֵ�ʱ��
        int[] smallMushRoomShow = { 1, 5, 14, 17, 27, 30, 32, 37, 45, 48, 50, 52 }; //СĢ��ǽ���ֵ�ʱ��
        int[] fishShow = { 2, 5, 10, 15, 20, 24, 28, 32, 36, 40, 43, 46, 49, 52, 55, 58, 61 }; //����ֵ�ʱ��
        int[] seaweedShow = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 15, 20, 24, 29, 32, 37, 40, 43, 47, 52, 57, 61 }; //ˮ�ݳ��ֵ�ʱ��
        //��ʼ����
        double energy=0;
        //��ʼ�¶�
        double T=100;
        //��ֹ�¶�
        double T_end=1;
        //�¶�˥����
        double r=0.99;
        //��ʼ״̬
        int[] mushRoomShow1_new=new int[8];
        int[] mushRoomShow2_new=new int[7];
        int[] smallMushRoomShow_new=new int[12];
        int[] fishShow_new=new int[17];
        int[] seaweedShow_new=new int[21];
        for(int i=0;i<8;i++){
            mushRoomShow1_new[i]=mushRoomShow1[i];
        }
        for(int i=0;i<7;i++){
            mushRoomShow2_new[i]=mushRoomShow2[i];
        }
        for(int i=0;i<12;i++){
            smallMushRoomShow_new[i]=smallMushRoomShow[i];
        }
        for(int i=0;i<17;i++){
            fishShow_new[i]=fishShow[i];
        }
        for(int i=0;i<21;i++){
            seaweedShow_new[i]=seaweedShow[i];
        }
        //��ʼ״̬������
        double energy_new=0;
        //����״̬
        int[] mushRoomShow1_best=new int[8];
        int[] mushRoomShow2_best=new int[7];
        int[] smallMushRoomShow_best=new int[12];
        int[] fishShow_best=new int[17];
        int[] seaweedShow_best=new int[21];
        for(int i=0;i<8;i++){
            mushRoomShow1_best[i]=mushRoomShow1[i];
        }
        for(int i=0;i<7;i++){
            mushRoomShow2_best[i]=mushRoomShow2[i];
        }
        for(int i=0;i<12;i++){
            smallMushRoomShow_best[i]=smallMushRoomShow[i];
        }
        for(int i=0;i<17;i++){
            fishShow_best[i]=fishShow[i];
        }
        for(int i=0;i<21;i++){
            seaweedShow_best[i]=seaweedShow[i];
        }
        //����״̬������
        double energy_best=0;
        //��������
        int k=0;
        //������ֹ����
        while(T>T_end){
            //������״̬
            for(int i=0;i<8;i++){
                mushRoomShow1_new[i]=mushRoomShow1[i]+(int)(Rand()*10-5);
            }
            for(int i=0;i<7;i++){
                mushRoomShow2_new[i]=mushRoomShow2[i]+(int)(Rand()*10-5);
            }
            for(int i=0;i<12;i++){
                smallMushRoomShow_new[i]=smallMushRoomShow[i]+(int)(Rand()*10-5);
            }
            for(int i=0;i<17;i++){
                fishShow_new[i]=fishShow[i]+(int)(Rand()*10-5);
            }
            for(int i=0;i<21;i++){
                seaweedShow_new[i]=seaweedShow[i]+(int)(Rand()*10-5);
            }
            //������״̬������
            energy_new=Energy(1,mushRoomShow1_new[0])+Energy(1,mushRoomShow1_new[1])+Energy(1,mushRoomShow1_new[2])+Energy(1,mushRoomShow1_new[3])+Energy(1,mushRoomShow1_new[4])+Energy(1,mushRoomShow1_new[5])+Energy(1,mushRoomShow1_new[6])+Energy(1,mushRoomShow1_new[7])
            +Energy(2,mushRoomShow2_new[0])+Energy(2,mushRoomShow2_new[1])+Energy(2,mushRoomShow2_new[2])+Energy(2,mushRoomShow2_new[3])+Energy(2,mushRoomShow2_new[4])+Energy(2,mushRoomShow2_new[5])+Energy(2,mushRoomShow2_new[6])
            +Energy(3,smallMushRoomShow_new[0])+Energy(3,smallMushRoomShow_new[1])+Energy(3,smallMushRoomShow_new[2])+Energy(3,smallMushRoomShow_new[3])+Energy(3,smallMushRoomShow_new[4])+Energy(3,smallMushRoomShow_new[5])+Energy(3,smallMushRoomShow_new[6])+Energy(3,smallMushRoomShow_new[7])+Energy(3,smallMushRoomShow_new[8])+Energy(3,smallMushRoomShow_new[9])+Energy(3,smallMushRoomShow_new[10])+Energy(3,smallMushRoomShow_new[11])
            +Energy(4,fishShow_new[0])+Energy(4,fishShow_new[1])+Energy(4,fishShow_new[2])+Energy(4,fishShow_new[3])+Energy(4,fishShow_new[4])+Energy(4,fishShow_new[5])+Energy(4,fishShow_new[6])+Energy(4,fishShow_new[7])+Energy(4,fishShow_new[8])+Energy(4,fishShow_new[9])+Energy(4,fishShow_new[10])+Energy(4,fishShow_new[11])+Energy(4,fishShow_new[12])+Energy(4,fishShow_new[13])+Energy(4,fishShow_new[14])+Energy(4,fishShow_new[15])+Energy(4,fishShow_new[16])
            +Energy(5,seaweedShow_new[0])+Energy(5,seaweedShow_new[1])+Energy(5,seaweedShow_new[2])+Energy(5,seaweedShow_new[3])+Energy(5,seaweedShow_new[4])+Energy(5,seaweedShow_new[5])+Energy(5,seaweedShow_new[6])+Energy(5,seaweedShow_new[7])+Energy(5,seaweedShow_new[8])+Energy(5,seaweedShow_new[9])+Energy(5,seaweedShow_new[10])+Energy(5,seaweedShow_new[11])+Energy(5,seaweedShow_new[12])+Energy(5,seaweedShow_new[13])+Energy(5,seaweedShow_new[14])+Energy(5,seaweedShow_new[15])+Energy(5,seaweedShow_new[16])+Energy(5,seaweedShow_new[17])+Energy(5,seaweedShow_new[18])+Energy(5,seaweedShow_new[19])+Energy(5,seaweedShow_new[20]);
            //����������
            double delta=energy_new-energy;
            //�ж��Ƿ������״̬
            if(delta<0){
                for(int i=0;i<8;i++){
                    mushRoomShow1[i]=mushRoomShow1_new[i];
                }
                for(int i=0;i<7;i++){
                    mushRoomShow2[i]=mushRoomShow2_new[i];
                }
                for(int i=0;i<12;i++){
                    smallMushRoomShow[i]=smallMushRoomShow_new[i];
                }
                for(int i=0;i<17;i++){
                    fishShow[i]=fishShow_new[i];
                }
                for(int i=0;i<21;i++){
                    seaweedShow[i]=seaweedShow_new[i];
                }
                energy=energy_new;
            }
            else{
                double p=Math.Exp(-delta/T);
                if(Rand()<p){
                    for(int i=0;i<8;i++){
                        mushRoomShow1[i]=mushRoomShow1_new[i];
                    }
                    for(int i=0;i<7;i++){
                        mushRoomShow2[i]=mushRoomShow2_new[i];
                    }
                    for(int i=0;i<12;i++){
                        smallMushRoomShow[i]=smallMushRoomShow_new[i];
                    }
                    for(int i=0;i<17;i++){
                        fishShow[i]=fishShow_new[i];
                    }
                    for(int i=0;i<21;i++){
                        seaweedShow[i]=seaweedShow_new[i];
                    }
                    energy=energy_new;
                }
            }
            //��������״̬
            if(energy<energy_best){
                for(int i=0;i<8;i++){
                    mushRoomShow1_best[i]=mushRoomShow1[i];
                }
                for(int i=0;i<7;i++){
                    mushRoomShow2_best[i]=mushRoomShow2[i];
                }
                for(int i=0;i<12;i++){
                    smallMushRoomShow_best[i]=smallMushRoomShow[i];
                }
                for(int i=0;i<17;i++){
                    fishShow_best[i]=fishShow[i];
                }
                for(int i=0;i<21;i++){
                    seaweedShow_best[i]=seaweedShow[i];
                }
                energy_best=energy;
            }
            //����
            T=T*r;
            k++;
        }
        //�������״̬
        Debug.Log("mushRoomShow1_best:");
        for(int i=0;i<8;i++){
            Debug.Log(mushRoomShow1_best[i]);
        }
        Debug.Log("mushRoomShow2_best:");
        for(int i=0;i<7;i++){
            Debug.Log(mushRoomShow2_best[i]);
        }
        Debug.Log("smallMushRoomShow_best:");
        for(int i=0;i<12;i++){
            Debug.Log(smallMushRoomShow_best[i]);
        }
        Debug.Log("fishShow_best:");
        for(int i=0;i<17;i++){
            Debug.Log(fishShow_best[i]);
        }
        Debug.Log("seaweedShow_best:");
        for(int i=0;i<21;i++){
            Debug.Log(seaweedShow_best[i]);
        }
        //�洢����״̬
        SaveBestState(mushRoomShow1_best,mushRoomShow2_best,smallMushRoomShow_best,fishShow_best,seaweedShow_best);
    }
    //�洢����״̬��ȫ�ֵ�������
    public void SaveBestState(int[]mushRoomShow1_best,int[]mushRoomShow2_best,int[]smallMushRoomShow_best,int[]fishShow_best,int[]seaweedShow_best){
        for(int i=0;i<8;i++){
            mushRoomShow1[i]=mushRoomShow1_best[i];
        }
        for(int i=0;i<7;i++){
            mushRoomShow2[i]=mushRoomShow2_best[i];
        }
        for(int i=0;i<12;i++){
            smallMushRoomShow[i]=smallMushRoomShow_best[i];
        }
        for(int i=0;i<17;i++){
            fishShow[i]=fishShow_best[i];
        }
        for(int i=0;i<21;i++){
            seaweedShow[i]=seaweedShow_best[i];
        }
    }
}


