using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    private FSM manager;
    private Parameter parameter;
    private AnimatorStateInfo animatorStateInfo;
    public AttackState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        parameter.animator.Play("BossAttack");
        Debug.Log("attack");
        manager.FlipTo(parameter.alice.transform);
    }
    public void OnUpdate()
    {
        animatorStateInfo = parameter.animator.GetCurrentAnimatorStateInfo(0);
        if (animatorStateInfo.normalizedTime >= 0.95f) 
        {
            manager.TransitionState(StateType.Idle);
        }
    }
    public void OnExit()
    {
        if(parameter.tmptran0 != null)
        {
            parameter.tmptran0.gameObject.SetActive(false);
        }
        if (parameter.tmptran1 != null)
        {
            parameter.tmptran1.gameObject.SetActive(false);
        }
        if (parameter.tmptran2 != null)
        {
            parameter.tmptran2.gameObject.SetActive(false);
        }
        if (parameter.tmptran3 != null)
        {
            parameter.tmptran3.gameObject.SetActive(false);
        }
    }
    
}
