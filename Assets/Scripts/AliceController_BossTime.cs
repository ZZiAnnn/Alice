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

    public AudioClip attackSound;  //������Ч
    public AudioClip dropSound;  //�����Ч
    public AudioClip runSound;  //������Ч
    public AudioClip hurtSound;  //������Ч

    private AudioSource audioSource;
    private bool audioControl = false;

    public float jumpForce = 6.0f;
    private int cnt = 0;
    bool isGrounded = false;
    public GameObject bullet;
    private GameObject biu1, biu2;
    public float bulletSpeed = 50;
    bool isAttacking, isJump, isDrop;
    //public float HP = 100;
    public Slider healthBar;
    public int bulletNum = 10;
    bool isLeftOrRight;  //false��ʾ����true��ʾ����

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        isAttacking = false;
        isJump = false;
        isDrop = false;
        isLeftOrRight = true;
        tran = GetComponent<Transform>();
    }

    void Update()
    {
        healthBar.value = AliceController.HP / 100;
        float verticalVelocity = rigid.velocity.y;
        if (Input.GetKeyDown(KeyCode.Space) && cnt < 2)
        {

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

        if (!isAttacking && Input.GetMouseButtonDown(0) && bulletNum > 0)
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
                    bulletNum--;
                }
                else
                {
                    biu2 = Instantiate(bullet, transform.position + new Vector3(0.0f, -0.4f, 0), Quaternion.identity);
                    bulletNum--;
                }
            }
        } //�ӵ�����
        if (biu1 != null)
        {
            if (isLeftOrRight)
            {
                biu1.transform.position += Vector3.right * bulletSpeed * Time.deltaTime;
                if (biu1.transform.position.x > 10)
                {
                    Destroy(biu1);
                }
            }
            else
            {
                biu1.transform.position += Vector3.left * bulletSpeed * Time.deltaTime;
                if (biu1.transform.position.x < -10)
                {
                    Destroy(biu1);
                }
            }
        }
        if (biu2 != null)
        {
            if (isLeftOrRight)
            {
                biu1.transform.position += Vector3.right * bulletSpeed * Time.deltaTime;
                if (biu1.transform.position.x > 10)
                {
                    Destroy(biu1);
                }
            }
            else
            {
                biu1.transform.position += Vector3.left * bulletSpeed * Time.deltaTime;
                if (biu1.transform.position.x < -10)
                {
                    Destroy(biu1);
                }
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision");

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
    IEnumerator DelayedAction(float wait)
    {
        yield return new WaitForSeconds(wait);
        animator.SetBool("hurted", true);   
    }
}
