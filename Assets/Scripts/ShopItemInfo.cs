using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopItemInfo : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{

    public Text infoText;//显示商品购买信息的文本
    
    //鼠标进入
    public void OnPointerEnter(PointerEventData eventData)
    {
        //移动到购买按钮上时，显示商品信息
        if (eventData.pointerEnter.name == "buy1")
        {
            infoText.text = "购买香喷喷的烤鸡，回复50%血量！";
        }
        else if (eventData.pointerEnter.name == "buy2")
        {
            infoText.text = "购买体力面包，增加25%血量！";
        }
        else if (eventData.pointerEnter.name == "buy3")
        {
            infoText.text = "购买乌龟壳，可以抵御下两次伤害！";
        }
        else if (eventData.pointerEnter.name == "buy4")
        {
            infoText.text = "购买魔杖，可以在战斗中祝你一臂之力！";
        }
    }
    //鼠标离开
    public void OnPointerExit(PointerEventData eventData)
    {
        //清空商品信息
        infoText.text = "";
    }
}
