using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;

public class RunState : IState
{
    private FSM manager;
    private Parameter parameter;
    private float DeltaDis;
    public RunState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        parameter.animator.Play("BossRun");
        manager.FlipTo(parameter.alice.transform);
        if (manager.transform.localScale.x > 0)
        {
            DeltaDis = -2;
        }
        else DeltaDis = 2;
    }
    public void OnUpdate()
    {
        manager.transform.position = Vector2.MoveTowards(manager.transform.position,
            new Vector3(parameter.alice.transform.position.x + DeltaDis, manager.transform.position.y, -1),
            parameter.chaseSpeed * Time.deltaTime);
        if (DeltaDis < 0 && manager.transform.position.x <= parameter.alice.transform.position.x + DeltaDis)
        {
            if (parameter.Change)
            {
                manager.TransitionState(StateType.Attack);
            }
            else
            {
                manager.TransitionState(StateType.Idle);
            }
        }
        else if (DeltaDis > 0 && manager.transform.position.x >= parameter.alice.transform.position.x + DeltaDis)
        {
            if (parameter.Change)
            {
                manager.TransitionState(StateType.Attack);
            }
            else
            {
                manager.TransitionState(StateType.Idle);
            }
        }
    }
    public void OnExit()
    {
        DeltaDis = 0;
        manager.FlipTo(parameter.alice.transform);
        if (parameter.Change) parameter.Change = false;
    }
}

