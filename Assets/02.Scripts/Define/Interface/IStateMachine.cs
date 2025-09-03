using PlayerStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public interface IStateMachine <T> where T : IState
{
    Animator Anim { get; set; }
    public void Change(StateType state);
}
public interface IState
{
    public StateType GetStateType { get; }
    IStateMachine<IState> stateMachine { get; set; }
    public void Enter();
    public void Execute();
    public void Exit();
    public static IState Factory(StateType type,IStateMachine<IState> stateMachine)
    {
        switch (type)
        {
            case StateType.idle:
                return new Idle(stateMachine);
            case StateType.jump:
                return new Jump(stateMachine);
            case StateType.run:
                return new Run(stateMachine);
            case StateType.shot:
                return new PlayerStates.Shot(stateMachine);
            case StateType.die:
                return new Die(stateMachine);
            case StateType.grab:
                return new Grab(stateMachine);
            case StateType.fall:
                return new Fall(stateMachine);
            case StateType.dash:
                return new Dash(stateMachine);
            case StateType.jumpShot:
                return new JumpShot(stateMachine);
            default:
                break;
        }
        return null;
    }
}
public enum StateType { idle,jump,run,shot,die,grab,fall,dash,jumpShot}