using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum State
{
    Patrol, Attack, Hit, Dead
}
public class FSM : MonoBehaviour
{
    public State CurrentState;
    void Start()
    {
        CurrentState = State.Patrol;
    }

    void Update()
    {
        switch (CurrentState)
        {
            case State.Patrol:
                StatePatrol();
                break;
            case State.Attack:
                StateAttack();
                break;
            case State.Hit:
                StateHit();
                break;
            case State.Dead:
                StateDead();
                break;

        }
    }
    public virtual void StatePatrol()
    {

    }
    public virtual void StateAttack()
    {

    }
    public virtual void StateHit() { }
    public virtual void StateDead() { }
}
