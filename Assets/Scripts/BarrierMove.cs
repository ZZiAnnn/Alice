using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierMove : MonoBehaviour
{
    private Transform tran;
    private Vector3 start;
    public float moveSpeed = 0.03f;
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
            if (tran.position.x > -20)
            {
                tran.position -= new Vector3(moveSpeed, 0, 0);
            }
            else//���������
            {
                tran.position = start + new Vector3(20, 0, 0);
            }
        }
    }
}

