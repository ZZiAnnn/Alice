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
            animator.SetBool("DropToRun",false);
        }

        if(isGrounded)
        {
            animator.SetBool("DropToRun",true);
        }

        if(Input.GetKeyDown(KeyCode.J))
        {
            animator.SetTrigger("Attack");
            if(biu==null)
            {
                biu = Instantiate(bullet,transform.position+new Vector3(0,-0.4f,0),Quaternion.identity);
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


}
