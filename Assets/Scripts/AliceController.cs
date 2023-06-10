using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class AliceController : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigid;
    Transform tran;

    public AudioClip attackSound;  //������Ч
    public AudioClip dropSound;  //�����Ч
    public AudioClip runSound;  //������Ч

    private AudioSource audioSource;
    private bool audioControl = false;

    private float jumpForce=4.5f;
    private int cnt=0;
    bool isGrounded=false;
    public GameObject bullet;
    private GameObject biu;
    public float bulletSpeed = 50;
    bool isAttacking, isJump, isDrop;
    public float HP = 100;
    public Slider healthBar;
    private Vector3 startpos;
    public int bulletNum = 10;
    GameObject[] tmp;
    GameObject[] tmp2;
    GameObject[] barrier;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator =GetComponent<Animator>();
        rigid=GetComponent<Rigidbody2D>();
        audioSource.mute = true;
        StartCoroutine(DelayedAudioActivation(3.0f));
        isAttacking = false;
        isJump = false;
        isDrop = false;
        tran =GetComponent<Transform>();
        startpos = new Vector2(tran.position.x, -1);
    }

    void Update()
    {
        healthBar.value = HP / 100;
        float verticalVelocity = rigid.velocity.y;
        if (Input.GetKeyDown(KeyCode.Space)&&cnt<2)
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
            rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            cnt++;
        }
        if(verticalVelocity<0.0f)
        {
            animator.SetBool("RunToJump", false);
            animator.SetBool("JumpToDrop",true);
            animator.SetBool("DropToRun",false);
            isDrop = true;
        }

        if(isGrounded)
        {
            if (!audioSource.isPlaying) audioSource.PlayOneShot(runSound);
            animator.SetBool("DropToRun",true);
            isJump = false;
            isDrop = false;
        }

        if(!isAttacking&& Input.GetMouseButtonDown(0)&&bulletNum>0)
        {
            if (biu == null)
            {
                if (!isJump) animator.SetTrigger("Attack");
                else isAttacking = true;
                audioSource.PlayOneShot(attackSound);
                animator.SetTrigger("Attack");
                isAttacking = true;
                biu = Instantiate(bullet, transform.position + new Vector3(0.0f, -0.4f, 0), Quaternion.identity);
                bulletNum--;
            }
        }
        if(biu!=null)
        {
            biu.transform.position += Vector3.right * bulletSpeed * Time.deltaTime;
            if (biu.transform.position.x>10)
            {
                 Destroy(biu);
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
        if (collision.gameObject.tag == "Ground")
        {
            if (!isGrounded)
            {
                audioSource.PlayOneShot(dropSound);
            }
            isGrounded =true;
            cnt=0;

        }
        else if(collision.gameObject.tag == "Barrier" || collision.gameObject.tag == "Barrier2")
        {
            HP-=10;
            if (isDrop) StartCoroutine(DelayedAction(0.24f));
            else StartCoroutine(DelayedAction(1.0f));
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
    }
    IEnumerator DelayedAction(float wait)
    {
        yield return new WaitForSeconds(wait); 
        tran.position=startpos;
        animator.SetBool("hurted", true);
        tmp = GameObject.FindGameObjectsWithTag("Barrier");
        tmp2 = GameObject.FindGameObjectsWithTag("Barrier2");
        for (int i=0;i<tmp.Length;i++)
        {
            tmp[i].GetComponent<PolygonCollider2D>().enabled = false;
        }
        for (int i = 0; i < tmp2.Length; i++)
        {
            tmp2[i].GetComponent<PolygonCollider2D>().enabled = false;
        }
    }
}
