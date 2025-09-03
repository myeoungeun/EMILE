
namespace PlayerStates
{
    using System;
    using Unity;
    using UnityEngine;
    public class Idle : IState
    {
        public StateType GetStateType => StateType.idle;

        public IStateMachine<IState> stateMachine { get; set; }

        public Idle(IStateMachine<IState> state)
        {
            this.stateMachine = state;
        }

        public void Enter()
        {
            this.stateMachine.Anim.Play("Idle");
        }

        public void Execute()
        {
            
        }

        public void Exit()
        {
             
        }
    }
    public class Run : IState
    {
        public StateType GetStateType => StateType.run;

        public IStateMachine<IState> stateMachine { get; set; }

        public Run(IStateMachine<IState> state)
        {
            this.stateMachine = state;
        }

        public void Enter()
        {
            stateMachine.Anim.Play("Run");
        }

        public void Execute()
        {
             
        }

        public void Exit()
        {
             
        }
    }
    public class Attack : IState
    {
        public StateType GetStateType => StateType.attack;

        public IStateMachine<IState> stateMachine { get; set; }

        public Attack(IStateMachine<IState> state)
        {
            this.stateMachine = state;
        }
        public void Enter()
        {
            stateMachine.Anim.Play("Attack");
        }

        public void Execute()
        {
             
        }

        public void Exit()
        {
             
        }
    }
    public class Jump: IState
    {
        public StateType GetStateType => StateType.jump;

        public IStateMachine<IState> stateMachine { get; set; }

        public Jump(IStateMachine<IState> state)
        {
            this.stateMachine = state;
        }

        public void Enter()
        {
            stateMachine.Anim.Play("Jump");
        }

        public void Execute()
        {
             
        }

        public void Exit()
        {
             
        }
    }
    public class Die : IState
    {
        public StateType GetStateType => StateType.die;

        public IStateMachine<IState> stateMachine { get; set; }

        public Die(IStateMachine<IState> state)
        {
            this.stateMachine = state;
        }

        public void Enter()
        {
            stateMachine.Anim.Play("Die");
        }

        public void Execute()
        {
             
        }

        public void Exit()
        {
             
        }
    }
    public class Grab : IState
    {
        public StateType GetStateType => StateType.grab;

        public IStateMachine<IState> stateMachine { get; set; }

        public Grab(IStateMachine<IState> state)
        {
            this.stateMachine = state;
        }

        public void Enter()
        {
            stateMachine.Anim.Play("Grab");
        }

        public void Execute()
        {
             
        }

        public void Exit()
        {
             
        }
    }
    public class Fall : IState
    {
        public StateType GetStateType => StateType.fall;

        public IStateMachine<IState> stateMachine { get; set; }

        public Fall(IStateMachine<IState> state)
        {
            this.stateMachine = state;
        }

        public void Enter()
        {
            stateMachine.Anim.Play("Fall");
        }

        public void Execute()
        {
             
        }

        public void Exit()
        {
             
        }
    }
    public class Dash : IState
    {
        public StateType GetStateType => StateType.dash;
        
        public IStateMachine<IState> stateMachine { get; set; }

        public Dash(IStateMachine<IState> state)
        {
            this.stateMachine = state;
        }

        public void Enter()
        {
            stateMachine.Anim.Play("Dash");
        }

        public void Execute()
        {
             
        }

        public void Exit()
        {
             
        }
    }
}


