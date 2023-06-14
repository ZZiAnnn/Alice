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
        manager.FlipTo(parameter.alice.transform);
        animatorStateInfo = parameter.animator.GetCurrentAnimatorStateInfo(0);
    }
    public void OnUpdate()
    {
        if (!animatorStateInfo.IsName("Base Layer.BossAttack")) 
        {
            manager.TransitionState(StateType.Idle);
            Debug.Log("!!!");
        }
    }
    public void OnExit()
    {

    }
}
