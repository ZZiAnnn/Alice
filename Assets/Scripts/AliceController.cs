using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliceController : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigid;
    private float jumpForce=5.0f;
    private int cnt=0;
    bool isGrounded=false;
    public GameObject bullet;
    private GameObject biu;
    bool isAttacking;

    void Start()
    {
        animator=GetComponent<Animator>();
        rigid=GetComponent<Rigidbody2D>();
        isAttacking = false;
    }

    void Update()
    {
        float verticalVelocity = rigid.velocity.y;
        if (Input.GetKeyDown(KeyCode.Space)&&cnt<2)
        {
            isGrounded = false;
            if (verticalVelocity < 0.0f)
            {
                animator.SetBool("DropToJump", true);
            }
            else animator.SetBool("RunToJump", true);
            rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            cnt++; 
        }
        if(verticalVelocity<0.0f)
        {
            animator.SetBool("RunToJump", false);
            animator.SetBool("JumpToDrop",true);
            animator.SetBool("DropToRun",false);
        }

        if(isGrounded)
        {
            animator.SetBool("DropToRun",true);
        }
        if(!isAttacking)
        {
            if (Input.GetMouseButtonDown(0))
            {
                animator.SetTrigger("Attack");
                isAttacking = true;
                if (biu == null)
                {
                    biu = Instantiate(bullet, transform.position + new Vector3(0, -0.4f, 0), Quaternion.identity);
                }
            }
        }
        if(biu!=null)
        {
            biu.transform.position+=new Vector3(0.1f,0,0);
            if(biu.transform.position.x>10)
            {
                Destroy(biu);
            }
        }


    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(111);
        cnt=0;
        // if (collision.gameObject.tag == "Ground")
        // {
        //     Debug.Log(222);
        //     isGrounded=true;
        // }
        isGrounded=true;
    }
    public void AttacktoRun()
    {
        animator.SetBool("AttackToRun", true);
        isAttacking = false;
    }
    public void InitAnimator()
    {
        animator.SetBool("AttackToRun", false);
        animator.SetBool("JumpToDrop", false);
        //animator.SetBool("RunToJump", false);
        animator.SetBool("DropToJump", false);
    }
    public void Jump()
    {
        animator.SetBool("DropToJump", false);
        animator.SetBool("JumpToDrop", false);
    }
}
