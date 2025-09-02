using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster
{
    public interface IDamageable
    {
        public void TakeDamage(int damage);
    }
}
