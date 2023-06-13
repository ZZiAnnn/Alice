using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishMove : MonoBehaviour
{
    private Transform tran;
    private Vector3 start;
    private BoxCollider2D myCollider; // 使用 BoxCollider2D
    public float moveSpeed = 7f;
    float lasttime;

    // Start is called before the first frame update
    void Start()
    {
        tran = GetComponent<Transform>();
        start = tran.position;
        myCollider = GetComponent<BoxCollider2D>(); // 获取 BoxCollider2D 组件的引用
    }

    // Update is called once per frame
    void Update()
    {
        if (ChangeShader.flag == 1)
        {
            EnableCollider(); // flag 为 1 时启用碰撞体
        }
        else if (ChangeShader.flag != 1)
        {
            DisableCollider(); // flag 为 -1 时禁用碰撞体
        }

        if (tran.position.x > -20.0f)
        {
            moveSpeed = TilemapMove.Speed + 2;
            tran.position -= new Vector3(Time.deltaTime * moveSpeed, 0, 0);
        }
        else // 到达结束点
        {
            tran.position = start + new Vector3(20.0f, 0, 0);
            if (tran.position.x > -20)
            {
                tran.position -= new Vector3(moveSpeed * Time.deltaTime, 0, 0);
            }
            else // 到达结束点
            {
                tran.position = start + new Vector3(20, 0, 0);
            }
        }
    }

    private void EnableCollider()
    {
        // 启用碰撞体
        if (myCollider != null && !myCollider.enabled)
        {
            myCollider.enabled = true;
            Debug.Log("Collider is enabled.");
        }
    }

    private void DisableCollider()
    {
        // 禁用碰撞体
        if (myCollider != null && myCollider.enabled)
        {
            myCollider.enabled = false;
            Debug.Log("Collider is disabled.");
        }
    }
}
