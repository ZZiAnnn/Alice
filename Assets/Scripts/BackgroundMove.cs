using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    public GameObject mushRoom;
    private GameObject[] background=new GameObject[3];
    public float Speed=0.05f;
    private float ystart,zstart;
    float lasttime;
    void Start()
    {
        lasttime = Time.time;
        for (int i=0;i<3;++i)
        {
            background[i] = GameObject.Find("Background_"+(i+1));
        }
        ystart=background[0].transform.position.y;
        zstart=background[0].transform.position.z;
    }

    void Update()
    {
        if (Time.time - lasttime >= 3)
        {
            for (int i = 0; i < 3; ++i)
            {
                if (background[i].transform.position.x < -18.5f)
                {
                    background[i].transform.position = new Vector3(27.0f, ystart, zstart);
                    GameObject mr = Instantiate(mushRoom, transform.position + new Vector3(6.56f, -1.39f, 0), Quaternion.identity);
                }
                else
                {
                    background[i].transform.position -= new Vector3(Speed, 0, 0);
                }
            }
        }
    }
}
