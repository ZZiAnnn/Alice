using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TilemapMove : MonoBehaviour
{
    private GameObject[] tilemap=new GameObject[32];
    public float Speed=0.05f;
    private float ystart,zstart;
    float lasttime;
    void Start()
    {
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
                if (tilemap[i].transform.position.x < -27.7f)
                {
                   tilemap[i].transform.position = new Vector3(26.2f, ystart, zstart);
                }
                else
                {
                    tilemap[i].transform.position -= new Vector3(Speed, 0, 0);
                }
            }
        }
    }
}
