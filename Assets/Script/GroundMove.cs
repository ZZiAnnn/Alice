using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMove : MonoBehaviour
{
    private Transform tran;
    private Vector3 start;
    private float moveSpeed = 0.03f;
    // Start is called before the first frame update
    void Start()
    {
        tran = GetComponent<Transform>(); 
        start = tran.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (tran.position.x >-20)
        {
            tran.position -= new Vector3(moveSpeed, 0, 0);
        }
        else//到达结束点
        {
            tran.position = start+new Vector3(20,0,0);
        }
    }

        
}

