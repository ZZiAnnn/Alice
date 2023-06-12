using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    private GameObject[] background=new GameObject[3];
    private float BackgroundSpeed=0.0f;
    private float ystart,zstart;
    float lasttime;
    void Start()
    {
        lasttime = Time.time;
        for (int i = 0; i < 3; ++i) 
        {
            background[i] = GameObject.Find("Background_" + (i + 1));
        }
        ystart = background[0].transform.position.y;
        zstart = background[0].transform.position.z;
    }

    void Update()
    {

        BackgroundSpeed = TilemapMove.Speed * 0.5f;

        if (Time.time - lasttime >= 3)
        {
            for (int i = 0; i < 3; ++i)
            {
                if (background[i].transform.position.x < -13.69f)
                {
                    background[i].transform.position = new Vector3(18.25f, ystart, zstart);
                }
                else
                {
                    background[i].transform.position -= new Vector3(Time.deltaTime*BackgroundSpeed, 0, 0);
                }
            }
        }
        
    }
}
