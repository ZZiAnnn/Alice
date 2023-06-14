using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD
//using static UnityEngine.RuleTile.TilingRuleOutput;
=======
// using static UnityEngine.RuleTile.TilingRuleOutput;
>>>>>>> 9c9711ee735a80e2fe010c2041263d14dfad6dc8

public class IdleState : IState
{
    private FSM manager;
    private Parameter parameter;
    private float timer;
    public IdleState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        parameter.animator.Play("Idle");
    }
    public void OnUpdate()
    {
        timer += Time.deltaTime;
        if (timer >= parameter.idleTime) 
        {
            manager.TransitionState(StateType.Run);
        }
    }
    public void OnExit()
    {
        timer = 0;
    }
}
