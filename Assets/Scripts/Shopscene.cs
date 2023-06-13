using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Shopscene : MonoBehaviour
{
    
    //金钱标签
    public Text moneyText;
    public Text info;

    void Start()
    {
        // 更新金钱数字
        moneyText.text = "X " + AliceController.money.ToString();
        Audio.instance.PlayMusicByName("bgm4");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //购买火鸡，增加到50点血量
    public void BuyTurkey()
    {
        if (AliceController.money >= 5)
        {
            AliceController.money -= 5;
            AliceController.HP += 50;
            if (AliceController.HP > 100) AliceController.HP = 100;
            moneyText.text = "X " + AliceController.money.ToString();
            info.text = "购买成功！";
        }else{
            info.text = "金钱不足！";
        }
    }
    //购买面包，增加25点血量
    public void BuyBread()
    {
        if (AliceController.money >= 3)
        {
            AliceController.money -= 3;
            AliceController.HP += 25;
            if (AliceController.HP > 100) AliceController.HP = 100;
            moneyText.text = "X " + AliceController.money.ToString();
            info.text = "购买成功！";
        }else{
            info.text = "金钱不足！";
        }
    }
    //购买龟壳，暂定无视上限增加20点血量
    public void BuyTurtleShell()
    {
        if (AliceController.money >= 10)
        {
            AliceController.money -= 10;
            AliceController.HP += 20;
            moneyText.text = "X " + AliceController.money.ToString();
            info.text = "购买成功！";
        }else{
            info.text = "金钱不足！";
        }
    }
    //返回游戏场景
    public void BackToGame()
    {
        AliceController.inShop=false;
    }

}


