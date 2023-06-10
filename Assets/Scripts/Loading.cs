using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    Animator animator;
    Transform tran;
    // Start is called before the first frame update
    void Start()
    {
        animator = GameObject.Find("Rabbit").GetComponent<Animator>();
        animator.SetBool("IdletoStand", true);
        tran = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.name == "Alice")
        {
            tran.Translate(Vector3.right * Time.deltaTime * 4);
        }
        else
        {
            tran.Translate(Vector3.right * Time.deltaTime * 4.5f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(this.gameObject);
    }
    public void InitAnimator()
    {
        ;
    }
}
