using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AliceController2 : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigid;
    Transform tran;

    public AudioClip attackSound;  //攻击音效
    public AudioClip dropSound;  //落地音效
    public AudioClip jumpSound;  //起跳音效
    public AudioClip hurtSound;  //受伤音效
    public AudioClip coinSound;  //金币声音

    private AudioSource audioSource;

    public float jumpForce = 6.0f;
    private int cnt = 0;
    bool isGrounded = false;
    public GameObject bullet;
    private GameObject biu1, biu2;
    public float bulletSpeed = 50;
    bool isAttacking, isJump, isDrop;
    public static float HP = 100;
    public Slider healthBar;
    private Vector3 startpos;
    public int bulletNum = 10;
    bool flag = true;
    GameObject[] tmp;
    GameObject[] tmp2;
    GameObject[] fish;
    GameObject[] seaweed;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        audioSource.mute = true;
        StartCoroutine(DelayedAudioActivation(3.0f));
        isAttacking = false;
        isJump = false;
        isDrop = false;
        tran = GetComponent<Transform>();
        startpos = new Vector2(tran.position.x, tran.position.y);
    }

    void Update()
    {
        if(tran.position.y <= -5f && flag)
        {
            Debug.Log("-5");
            flag = false;
            HP -= 30;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //DelayedAction(0.5f);
        }
       
        healthBar.value = HP / 100;
        float verticalVelocity = rigid.velocity.y;
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
        }
        if (biu1 != null)
        {
            biu1.transform.position += Vector3.right * bulletSpeed * Time.deltaTime;
            if (biu1.transform.position.x > 10)
            {
                Destroy(biu1);
            }
        }
        if (biu2 != null)
        {
            biu2.transform.position += Vector3.right * bulletSpeed * Time.deltaTime;
            if (biu2.transform.position.x > 10)
            {
                Destroy(biu2);
            }
        }
    }

    IEnumerator DelayedAudioActivation(float wait)
    {
        yield return new WaitForSeconds(wait);
        audioSource.mute = false;
    }

    void OnGUI() // 在屏幕上显示计时器的数值
    {
        GUIStyle style = new GUIStyle(GUI.skin.label); // 创建一个新的 GUIStyle 对象

        // 调整标签的位置和大小
        Rect labelRect = new Rect(Screen.width - 170, 30, 100, 40);

        // 调整字体大小
        style.fontSize = 24;

        // 在屏幕上绘制标签
        GUI.Label(labelRect, "×" + AliceController.money.ToString(), style);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        string collisionTag = collision.gameObject.tag;
        if (collisionTag == "Ground")
        {
            if (!isGrounded)
            {
                audioSource.PlayOneShot(dropSound);
            }
            isGrounded = true;
            flag = true;
            cnt = 0;
        }
        else if (collisionTag == "Coin")
        {
            Debug.Log("coin!");
            audioSource.PlayOneShot(dropSound);
            Destroy(collision.gameObject);
            AliceController.money++;
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
    IEnumerator DelayedAction(float wait)
    {
        yield return new WaitForSeconds(wait); 
        tran.position=startpos;
        animator.SetBool("hurted", true);
    }
}
