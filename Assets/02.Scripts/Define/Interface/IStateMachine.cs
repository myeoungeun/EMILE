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
    public static IState Factory(StateType type,IStateMachine<IState> state)
    {
        switch (type)
        {
            case StateType.idle:
                return new Idle(state);
            case StateType.jump:
                return new Jump(state);
            case StateType.run:
                return new Run(state);
            case StateType.attack:
                return new PlayerStates.Attack(state);
            case StateType.die:
                return new Die(state);
            case StateType.grab:
                return new Grab(state);
            case StateType.fall:
                return new Fall(state);
            default:
                break;
        }
        return null;
    }
}
public enum StateType { idle,jump,run,attack,die,grab,fall}