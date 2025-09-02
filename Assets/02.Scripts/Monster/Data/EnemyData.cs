using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Enemy Data", menuName ="Data/Enemy Data")]
public class EnemyData : ScriptableObject
{
    [SerializeField] private int id;
    [SerializeField] private string enemyName;
    [SerializeField] private int monsterImageId;
    // 상태
    [SerializeField] private int maxHp;
    // 현재 체력
    [SerializeField] private int attackPower;
    [SerializeField] private int defence;
    [SerializeField] private float attackSpeed;
    [SerializeField] private Monster.AttackType attackType;
    [SerializeField] private int dropItemKey;
    // 상태 이상 리스트
    [SerializeField] private float moveSpeed;
    [SerializeField] private float detectRange;
    [SerializeField] private float attackRange;
    [SerializeField] private List<int> enemyBulletId;
    // 오브젝트 태그

    public int Id {  get { return id; } }
    public string Name { get { return enemyName; } }
    public int MonsterImageId { get { return monsterImageId; } }
    public int MaxHp { get { return maxHp; } }
    public int AttackPower { get { return attackPower; } }
    public int Defence { get { return defence; } }
    public float AttackSpeed { get { return attackSpeed; } }
    public Monster.AttackType AttackType { get { return attackType; } }
    public int DropItemKey { get { return dropItemKey; } }
    public float MoveSpeed { get { return moveSpeed; } }
    public float DetectRange { get { return detectRange; } }
    public float AttackRange { get { return attackRange; } }
    public List<int> EnemyBulletId { get { return enemyBulletId; } }

}
