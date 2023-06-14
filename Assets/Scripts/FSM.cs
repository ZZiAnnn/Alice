using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
using UnityEngine.UI;

public enum StateType
{
    Idle,Attack,Run
}
[Serializable]
public class Parameter
{
    public int health;
    public int maxhealth;
    public float chaseSpeed=12f;
    public float idleTime = 3f;
    public Animator animator;
    public GameObject alice;
    public int flag;
    public bool Change;
    public Transform tmptran0;
    public Transform tmptran1;
    public Transform tmptran2;
    public Transform tmptran3;
    public Image bosshealth;
    public Image bossBufferhealth;
}
public class FSM : MonoBehaviour
{
    public Parameter parameter;
    private IState currentState;
    private Dictionary<StateType, IState> states = new Dictionary<StateType, IState>();
    // Start is called before the first frame update
    void Start()
    {
        states.Add(StateType.Idle,new IdleState(this));
        states.Add(StateType.Run, new RunState(this));
        states.Add(StateType.Attack, new AttackState(this));
        TransitionState(StateType.Idle);
        parameter.maxhealth = 1000;
        parameter.health = parameter.maxhealth;
        parameter.animator = GetComponent<Animator>();
        parameter.alice = GameObject.FindWithTag("player");
        parameter.flag = 0;
        parameter.Change = false;
        BossDeathControl.hpp = parameter.health;
    }

    // Update is called once per frame
    void Update()
    {
        currentState.OnUpdate();
        if (parameter.bossBufferhealth.fillAmount > parameter.bosshealth.fillAmount) 
        {
            if (parameter.bosshealth.fillAmount == 0)
            {
                parameter.bossBufferhealth.fillAmount -= 0.002f;
            }
            else parameter.bossBufferhealth.fillAmount -= 0.0005f;
        }
        if (parameter.health <= 0.5 * parameter.maxhealth && parameter.flag == 0) 
        {
            parameter.flag = 1;
            if (Math.Abs(transform.position.x - parameter.alice.transform.position.x) > 2) 
            {
                TransitionState(StateType.Run);
                parameter.Change = true;
            }
            else
            {
                TransitionState(StateType.Attack);
            }
        }
        else if (parameter.health <= 0.3 * parameter.maxhealth && parameter.flag == 1)
        {
            parameter.flag = 2;
            if (Math.Abs(transform.position.x - parameter.alice.transform.position.x) > 2)
            {
                TransitionState(StateType.Run);
                parameter.Change = true;
            }
            else
            {
                TransitionState(StateType.Attack);
            }
        }
        else if (parameter.health <= 0.1 * parameter.maxhealth && parameter.flag == 2)
        {
            parameter.flag = 3;
            if (Math.Abs(transform.position.x - parameter.alice.transform.position.x) > 2)
            {
                TransitionState(StateType.Run);
                parameter.Change = true;
            }
            else
            {
                TransitionState(StateType.Attack);
            }
        }
    }
    public void TransitionState(StateType type)
    {
        if (currentState != null) 
        {
            currentState.OnExit();
        }
        currentState = states[type];
        currentState.OnEnter();
    }
    public void FlipTo(Transform target)
    {
        if (target != null)
        {
            if (transform.position.x > target.position.x)
            {
                transform.localScale = new Vector3(2.195811f, 2.195811f, 2.195811f);
            }
            else if (transform.position.x < target.position.x) 
            {
                transform.localScale = new Vector3(-2.195811f, 2.195811f, 2.195811f);
            }
        }
    }
    public void Attack()
    {
        if (parameter.flag == 1)
        {
            parameter.tmptran0 = transform.Find("BossAttack");
            parameter.tmptran0.gameObject.SetActive(true);
        }
        else if (parameter.flag == 2)
        {
            parameter.tmptran1 = transform.Find("BossAttack1");
            parameter.tmptran0.gameObject.SetActive(true);
            parameter.tmptran1.gameObject.SetActive(true);
        }
        else if (parameter.flag == 3)
        {
            parameter.tmptran2 = transform.Find("BossAttack2");
            parameter.tmptran3 = transform.Find("BossAttack3");
            parameter.tmptran0.gameObject.SetActive(true);
            parameter.tmptran1.gameObject.SetActive(true);
            parameter.tmptran2.gameObject.SetActive(true);
            parameter.tmptran3.gameObject.SetActive(true);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            if (parameter.Change)
            {
                TransitionState(StateType.Attack);
            }
            else
            {
                TransitionState(StateType.Idle);
            }
        }
    }
}
