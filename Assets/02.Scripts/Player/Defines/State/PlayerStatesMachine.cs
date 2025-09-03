using PlayerStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Timeline;

public class PlayerStatesMachine : IStateMachine<IState>
{
    Dictionary<StateType, IState> dict;
    IState curr;
    public StateType GetCurrType { get { return curr.GetStateType; } }
    public Animator Anim { get; set; }

    public void Change(StateType state)
    {
        if (GetCurrType == state) return;
        curr.Exit();
        curr = dict[state];
        curr.Enter();
    }
    //공격관련 스테이트 전용
    public void Change(StateType state,BulletDirrections dir)//방향이 있는 애니메이션 전용
    {
        if (GetCurrType == state) return;
        curr.Exit();
        curr = dict[state];
        if (state == StateType.shot)
        {
            ((Shot)curr).Enter(dir);
        }
        else if (state == StateType.jumpShot)
        {
            ((JumpShot)curr).Enter(dir);
        }
        else
        {
            curr.Enter();
        }
            
    }
    public PlayerStatesMachine(StateType initialType, Animator anim)
    {
        this.Anim = anim;
        dict = new Dictionary<StateType, IState>();
        for (int i = 0; i < Enum.GetValues(typeof(StateType)).Length; i++)
        {
            dict.TryAdd((StateType)i, IState.Factory((StateType)i,this));
        }
        this.curr = dict[initialType];
    }
}
