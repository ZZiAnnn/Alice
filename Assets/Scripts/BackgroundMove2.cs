using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BackgroundMove2 : MonoBehaviour
{
    private GameObject[] background = new GameObject[2];
    private float BackgroundSpeed;
    private float ystart, zstart;
    float lasttime;
    void Start()
    {
        lasttime = Time.time;
        for (int i = 0; i < 2; i++)
        {
            background[i] = GameObject.Find("Cloud" + i + 1);
        }
        for (int i = 0; i < 2; ++i)
        {
            background[i] = GameObject.Find("Cloud" + (i + 1));
        }

        ystart = background[0].transform.position.y;
        zstart = background[0].transform.position.z;
    }

    void Update()
    {

        BackgroundSpeed = 1f;

        if (Time.time - lasttime >= 3)
        {

            for (int i = 0; i < 2; ++i)
            {
                if (background[i].transform.position.x < -30f)
                {
                    background[i].transform.position = new Vector3(28f, ystart, zstart);
                }
                else
                {
                    background[i].transform.position -= new Vector3(Time.deltaTime * BackgroundSpeed, 0, 0);
                }
            }
        }
    }
}
