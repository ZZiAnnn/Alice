using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliceController : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigid;
    private float jumpForce=5.0f;
    private int cnt=0;

    void Start()
    {
        animator=GetComponent<Animator>();
        rigid=GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)&&cnt<2)
        {
            animator.SetBool("RunToJump",true);
            rigid.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            cnt=cnt+1; 
        }
        float verticalVelocity = rigid.velocity.y;
        if(verticalVelocity<=0.0f)
        {
            animator.SetBool("RunToJump",false);
            animator.SetBool("JumpToDrop",true);
        }
        bool isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.001f);
        if(isGrounded)
        {
            cnt=0;
            animator.SetBool("DropToRun",true);
        }

    }


}
