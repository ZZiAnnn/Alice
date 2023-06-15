using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureControl : MonoBehaviour
{
    public GameObject objectPrefab; // 预制体物体
    public GameObject enter;
    public GameObject coinPrefab;
    private GameObject[] obj = new GameObject[5]; 
    Animator animator;
    bool flag;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        flag = false;
    }
    void Update()
    {
        if (flag)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                animator.SetBool("opened", true);
                enter.SetActive(false);

                // 生成物体
                GameObject newObject = Instantiate(objectPrefab, new Vector3(9f, -2.5f, 0), Quaternion.identity);

                obj[0] = Instantiate(coinPrefab, new Vector3(2f, -2.5f, 0), Quaternion.identity);
                obj[1] = Instantiate(coinPrefab, new Vector3(3f, -2.8f, 0), Quaternion.identity);
                obj[2] = Instantiate(coinPrefab, new Vector3(4f, -3f, 0), Quaternion.identity);
                obj[3] = Instantiate(coinPrefab, new Vector3(5f, -3.5f, 0), Quaternion.identity);
                obj[4] = Instantiate(coinPrefab, new Vector3(6f, -3.2f, 0), Quaternion.identity);
            }
        
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        enter.SetActive(true);
        flag = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        enter.SetActive(false);
        flag = false;
    }
}
