using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

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
    }

    // Update is called once per frame
    void Update()
    {
        currentState.OnUpdate();
        if (parameter.health <= 0.5 * parameter.maxhealth)
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
        else if (parameter.health <= 0.1 * parameter.maxhealth)
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
}
