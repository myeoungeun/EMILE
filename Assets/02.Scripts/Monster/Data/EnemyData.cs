using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName ="Data/Enemy Data")]
public class EnemyData : ScriptableObject
{
    private int id;
    private string enemyName;
    private int monsterImageId;
    // 상태
    private int maxHp;
    // 현재 체력
    private int attackPower;
    private int defence;
    private float attackSpeed;
    private Monster.AttackType attackType;
    private int dropItemKey;
    // 상태 이상 리스트
    private float moveSpeed;
    private float detectRange;
    private float attackRange;
    private List<int> enemyBulletId;
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
