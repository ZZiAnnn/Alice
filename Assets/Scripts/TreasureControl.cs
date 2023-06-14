using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureControl : MonoBehaviour
{
    public GameObject enter;
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
