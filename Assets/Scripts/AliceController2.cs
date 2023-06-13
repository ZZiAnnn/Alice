using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class AliceController2 : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigid;
    Transform tran;

    public AudioClip attackSound;  //������Ч
    public AudioClip dropSound;  //�����Ч
    public AudioClip jumpSound;  //������Ч
    public AudioClip hurtSound;  //������Ч

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
            DelayedAction(0.5f);
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
        else if (collisionTag == "Barrier" || collisionTag == "Barrier2" || collisionTag == "fish" || collisionTag == "seaweed")
        {
            audioSource.PlayOneShot(hurtSound);
            HP -=10;
            if (isDrop) StartCoroutine(DelayedAction(0.24f));
            else StartCoroutine(DelayedAction(0.5f));
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
        for (int i = 0; i < tmp.Length; i++)
        {
            if (tmp[i]) tmp[i].GetComponent<PolygonCollider2D>().enabled = true;
        }
        for (int i = 0; i < tmp2.Length; i++)
        {
            if (tmp2[i]) tmp2[i].GetComponent<PolygonCollider2D>().enabled = true;
        }
        for (int i = 0; i < fish.Length; i++)
        {
            if (fish[i]) fish[i].GetComponent<BoxCollider2D>().enabled = true;
        }
        for (int i = 0; i < seaweed.Length; i++)
        {
            if (seaweed[i]) seaweed[i].GetComponent<BoxCollider2D>().enabled = true;
        }


    }
    IEnumerator DelayedAction(float wait)
    {
        yield return new WaitForSeconds(wait); 
        tran.position=startpos;
        animator.SetBool("hurted", true);
        tmp = GameObject.FindGameObjectsWithTag("Barrier");
        tmp2 = GameObject.FindGameObjectsWithTag("Barrier2");
        fish = GameObject.FindGameObjectsWithTag("fish");
        seaweed = GameObject.FindGameObjectsWithTag("seaweed");
        for (int i=0;i<tmp.Length;i++)
        {
            tmp[i].GetComponent<PolygonCollider2D>().enabled = false;
        }
        for (int i = 0; i < tmp2.Length; i++)
        {
            tmp2[i].GetComponent<PolygonCollider2D>().enabled = false;
        }
        for (int i = 0; i < fish.Length; i++)
        {
            fish[i].GetComponent<BoxCollider2D>().enabled = false;
        }
        for (int i = 0; i < seaweed.Length; i++)
        {
            seaweed[i].GetComponent<BoxCollider2D>().enabled = false;
        }

    }
}