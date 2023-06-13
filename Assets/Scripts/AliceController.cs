using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AliceController : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigid;
    Transform tran;

    public AudioClip attackSound;  //π•ª˜“Ù–ß
    public AudioClip dropSound;  //¬‰µÿ“Ù–ß
    public AudioClip runSound;  //±º≈‹“Ù–ß
    public AudioClip hurtSound;  // ‹…À“Ù–ß

    private AudioSource audioSource;
    public GameObject enter;
    public GameObject End;
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
    GameObject[] tmp;
    GameObject[] tmp2;
    GameObject[] fish;
    GameObject[] seaweed;
    float horizontal;
    void Start()
    {
        HP=100;
        Time.timeScale=1;
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        audioSource.mute = true;
        StartCoroutine(DelayedAudioActivation(3.0f));
        isAttacking = false;
        isJump = false;
        isDrop = false;
        tran = GetComponent<Transform>();
        startpos = new Vector2(tran.position.x, -1);
        End.SetActive(false);
    }

    void Update()
    {
        if(TilemapMove.Speed==0)
        {
            horizontal = Input.GetAxis("Horizontal");
        }
        if(horizontal>0) tran.localScale=new Vector3(-1,1,1);
        else if(horizontal<0) tran.localScale=new Vector3(1,1,1);
        if(tran.position.x>9.11f) SceneManager.LoadScene("gameScene2");
        else if(tran.position.x>-3.2f&&tran.position.x<1.06f) 
        {
            enter.SetActive(true);
            if(Input.GetKeyDown(KeyCode.W))
            {
                SceneManager.LoadScene("shopScene");
            }
        }
        else
        {
            enter.SetActive(false);
        }
        healthBar.value = HP / 100;
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
            if (!audioSource.isPlaying) audioSource.PlayOneShot(runSound);
            animator.SetBool("DropToRun", true);
            isJump = false;
            isDrop = false;
        }
        else
        {
            if (audioSource.clip == runSound) audioSource.Pause();
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
        if(HP==0) 
        {
            Time.timeScale = 0;
            End.SetActive(true);
        }
    }
    void FixedUpdate()
    {
        if(TilemapMove.Speed==0)
        {
            float speed=5.0f;
            Vector2 position = rigid.position;
            position.x = position.x + speed * horizontal * Time.deltaTime;
            rigid.MovePosition(position);
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
