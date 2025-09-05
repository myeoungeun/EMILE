using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster
{
    public enum AttackType
    {
        Melee,
        Ranged
    }

    public enum EnemyStateType
    {
        Idle,
        Detect,
        Attack,
        Phase1,
        Phase2,
        Rush
    }

    public static class Layers
    {
        public const string Player = "Player";
    }

    public static class AnimatorParams
    {
        public const string InDetectRange = "InDetectRange";
        public const string InAttackRange = "InAttackRange";
    }

}
