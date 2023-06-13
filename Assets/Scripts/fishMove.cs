using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishMove : MonoBehaviour
{
    private Transform tran;
    private Vector3 start;
    private BoxCollider2D myCollider; // ʹ�� BoxCollider2D
    public float moveSpeed = 7f;
    float lasttime;

    // Start is called before the first frame update
    void Start()
    {
        tran = GetComponent<Transform>();
        start = tran.position;
        myCollider = GetComponent<BoxCollider2D>(); // ��ȡ BoxCollider2D ���������
    }

    // Update is called once per frame
    void Update()
    {
        if (ChangeShader.flag == 1)
        {
            EnableCollider(); // flag Ϊ 1 ʱ������ײ��
        }
        else if (ChangeShader.flag != 1)
        {
            DisableCollider(); // flag Ϊ -1 ʱ������ײ��
        }

        if (tran.position.x > -20.0f)
        {
            moveSpeed = TilemapMove.Speed + 2;
            tran.position -= new Vector3(Time.deltaTime * moveSpeed, 0, 0);
        }
        else // ���������
        {
            tran.position = start + new Vector3(20.0f, 0, 0);
            if (tran.position.x > -20)
            {
                tran.position -= new Vector3(moveSpeed * Time.deltaTime, 0, 0);
            }
            else // ���������
            {
                tran.position = start + new Vector3(20, 0, 0);
            }
        }
    }

    private void EnableCollider()
    {
        // ������ײ��
        if (myCollider != null && !myCollider.enabled)
        {
            myCollider.enabled = true;
            Debug.Log("Collider is enabled.");
        }
    }

    private void DisableCollider()
    {
        // ������ײ��
        if (myCollider != null && myCollider.enabled)
        {
            myCollider.enabled = false;
            Debug.Log("Collider is disabled.");
        }
    }
}
