using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierMove : MonoBehaviour
{
    private Transform tran;
    private Vector3 start;
    public float moveSpeed;
    float lasttime;
    // Start is called before the first frame update
    void Start()
    {
        tran = GetComponent<Transform>(); 
        start = tran.position;
        lasttime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lasttime >= 3) 
        {
            if (tran.position.x > -20.0f)
            {
                tran.position -= new Vector3(moveSpeed*Time.deltaTime, 0, 0);
            }
            else//到达结束点
            {
                tran.position = start + new Vector3(20.0f, 0, 0);
            }
        }
    }
}

