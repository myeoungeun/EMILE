using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster
{
    public interface IAttackable
    {
        public void StartAttack();
        public void StopAttack();
    }
}
