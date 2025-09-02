using PlayerStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

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
