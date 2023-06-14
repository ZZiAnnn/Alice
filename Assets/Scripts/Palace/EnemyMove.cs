using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private Transform tran;
    private Vector3 start;
    public float moveSpeed = 8f;
    float lasttime;
    // Start is called before the first frame update
    void Start()
    {
        tran = GetComponent<Transform>(); 
        start = tran.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (tran.position.x > -20.0f)
        {
            tran.position -= new Vector3(Time.deltaTime * MainController.Speed*0.8f, 0, 0);
        }
    }
}


