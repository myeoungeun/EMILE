using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster
{
    public interface IState
    {
        public void Enter();
        public void Exit();
        public void Update();
        public void FixedUpdate();
    }
}

