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
    bool isAttacking, isJump;
    public AliceHP Alice_hp;
    void Start()
    {
        animator=GetComponent<Animator>();
        rigid=GetComponent<Rigidbody2D>();
        Alice_hp = GetComponent<AliceHP>();
        isAttacking = false;
        isJump = false;
    }

    void Update()
    {
        float verticalVelocity = rigid.velocity.y;
        if (Input.GetKeyDown(KeyCode.Space)&&cnt<2)
        {
            isJump = true;
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
            isJump = false;
        }
        if(!isAttacking)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!isJump) animator.SetTrigger("Attack");
                else isAttacking = true;
                if (biu == null)
                {
                    animator.SetTrigger("Attack");
                    isAttacking = true;
                    biu = Instantiate(bullet, transform.position + new Vector3(0.0f, -0.4f, 0), Quaternion.identity);
                }
            }
        }
        if(biu!=null)
        {
            biu.transform.position+=new Vector3(0.05f,0,0);
            if(biu.transform.position.x>10)
            {
                Destroy(biu);
            }
        }

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded=true;
            cnt=0;
        }
        else if(collision.gameObject.tag == "Barrier")
        {
            Alice_hp.HP-=10;
        }
    }
    public void AttacktoRun()
    {
        animator.SetBool("AttackToRun", true);
    }
    public void InitAnimator()
    {
        isAttacking = false;
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
