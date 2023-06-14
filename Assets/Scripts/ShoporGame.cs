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
        Debug.Log(AliceController.inShop);
        if (AliceController.inShop == true) 
        {
            game.SetActive(false);
            shop.SetActive(true);

        }
        else
        {
            shop.SetActive(false);
            game.SetActive(true);
        }
    }
}
