using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class AliceController_BossTime : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigid;
    Transform tran;

    public AudioClip attackSound;  //攻击音效
    public AudioClip dropSound;  //落地音效
    public AudioClip jumpSound;  //奔跑音效
    public AudioClip hurtSound;  //受伤音效

    private AudioSource audioSource;
    private bool audioControl = false;
    public float walkingSpeed = 8f;
    public float jumpForce = 6.0f;
    private int cnt = 0;
    bool isGrounded = false;
    public GameObject bullet;
    private GameObject biu1, biu2;
    public float bulletSpeed = 5f;
    bool isAttacking, isJump, isDrop;
    //public float HP = 100;
    public Slider healthBar;
    //public int bulletNum = 50;
    bool isLeftOrRight;  //false表示朝左，true表示朝右
    bool biu1Direct;
    bool biu2Direct;
    float timer;

    void Start()
    {
        Audio.instance.PlayMusicByName("bgm3");
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        isAttacking = false;
        isJump = false;
        isDrop = false;
        isLeftOrRight = true;
        tran = GetComponent<Transform>();
        timer = 0;
    }

    void Update()
    {
        healthBar.value = AliceController.HP / 100;
        float verticalVelocity = rigid.velocity.y;
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else timer = 0;
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * walkingSpeed * Time.deltaTime);
            if(isLeftOrRight) transform.localScale = new Vector3(1f, 1f, 1f); // 将角色朝向左
            isLeftOrRight = false;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * walkingSpeed * Time.deltaTime);
            if (!isLeftOrRight) transform.localScale = new Vector3(-1f, 1f, 1f); // 将角色朝向左
            isLeftOrRight = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && cnt < 2)
        {
            if (cnt == 0) audioSource.PlayOneShot(jumpSound);
            isJump = true;
            isGrounded = false;
            if (verticalVelocity < 0.0f)
            {
                animator.SetBool("DropToJump", true);
            }
            else
            {
                animator.SetBool("RunToJump", true);
            }

            isDrop = false;
            rigid.velocity = new Vector2(rigid.velocity.x, 0f);
            rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            cnt++;
        }
        if (verticalVelocity < 0.0f)
        {
            animator.SetBool("RunToJump", false);
            animator.SetBool("JumpToDrop", true);
            animator.SetBool("DropToRun", false);
            isDrop = true;
        }

        if (isGrounded)
        {
            animator.SetBool("DropToRun", true);
            isJump = false;
            isDrop = false;
        }

        if (!isAttacking && Input.GetMouseButtonDown(0))//子弹无限
        {
            if (biu1 == null || biu2 == null)
            {
                if (!isJump) animator.SetTrigger("Attack");
                else isAttacking = true;
                audioSource.PlayOneShot(attackSound);
                animator.SetTrigger("Attack");
                isAttacking = true;
                if (biu1 == null)
                {
                    biu1 = Instantiate(bullet, transform.position + new Vector3(0.0f, -0.4f, 0), Quaternion.identity);
                    biu1Direct = isLeftOrRight;
                    //bulletNum--;
                }
                else
                {
                    biu2 = Instantiate(bullet, transform.position + new Vector3(0.0f, -0.4f, 0), Quaternion.identity);
                    biu2Direct = isLeftOrRight;
                    //bulletNum--;
                }
            }
        } //子弹攻击
        if (biu1 != null)
        {
            if (biu1Direct)
            {
                biu1.transform.localScale = new Vector3(1, 1, 1);
                biu1.transform.position += Vector3.right * bulletSpeed * Time.deltaTime;
                if (biu1.transform.position.x > 13)
                {
                    Destroy(biu1);
                }
            }
            else
            {
                biu1.transform.localScale = new Vector3(-1, 1, 1);
                biu1.transform.position += Vector3.left * bulletSpeed * Time.deltaTime;
                if (biu1.transform.position.x < -13)
                {
                    Destroy(biu1);
                }
            }
        }
        if (biu2 != null)
        {
            if (biu2Direct)
            {
                biu1.transform.position += Vector3.right * bulletSpeed * Time.deltaTime;
                if (biu1.transform.position.x > 13)
                {
                    Destroy(biu1);
                }
            }
            else
            {
                biu1.transform.position += Vector3.left * bulletSpeed * Time.deltaTime;
                if (biu1.transform.position.x < -13)
                {
                    Destroy(biu1);
                }
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision");
        string collisionTag = collision.gameObject.tag;
        if (collisionTag == "Ground")
        {
            if (!isGrounded)
            {
                audioSource.PlayOneShot(dropSound);
            }
            isGrounded = true;
            cnt = 0;
        }
        else if (collisionTag == "Boss" && timer == 0) 
        {
            Debug.Log("Boss!");
            audioSource.PlayOneShot(hurtSound);
            AliceController.HP -= 15;
            timer = 2;
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
        animator.SetBool("HurtToRun", false);
    }
    public void Jump()
    {
        animator.SetBool("DropToJump", false);
        animator.SetBool("JumpToDrop", false);
    }
    public void HurttoRun()
    {
        animator.SetBool("HurtToRun", true);
        animator.SetBool("hurted", false);
    }
}
