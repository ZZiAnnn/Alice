using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoporGame : MonoBehaviour
{
    public GameObject game;
    public GameObject shop;
    void Start()
    {
        game.SetActive(true);
        shop.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(AliceController.inShop==false)
        {
            shop.SetActive(true);
            game.SetActive(false);
        }
        else
        {
            game.SetActive(true);
            shop.SetActive(false);
        }
    }
}
