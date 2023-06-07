using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitControl : MonoBehaviour
{
    // Start is called before the first frame update
    Transform RabbitTrans;
    float lasttime;
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        RabbitTrans=GetComponent<Transform>();
        lasttime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lasttime >= 3) 
        {
            animator.SetBool("IdletoStand", true);
            if (Time.time < lasttime + 3.43)
            {
                RabbitTrans.Translate(Vector3.left * Time.deltaTime * 2.0f);
            }
            else RabbitTrans.Translate(Vector3.right * Time.deltaTime * 5.2f);
            if (RabbitTrans.position.x > 10)
            {
                Destroy(this.gameObject);
            }
        }
        
    }
}
