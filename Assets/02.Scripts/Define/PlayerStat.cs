using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : IDamageable
{
    private int curHP;
    public int CurHP { get { return curHP; } set { curHP = value; } }
    private int maxHP;
    public int MaxHP { get { return maxHP; } set { maxHP = value; } }
    private int attackDamage;
    public int AttackDamage { get { return attackDamage; } set { attackDamage = value; } }

    private float moveSpeed;
    public float MoveSpeed{ get { return moveSpeed; } set { moveSpeed = value; } }
    private float jumpForce;
    public float JumpForce { get { return jumpForce; } set { jumpForce = value; } }
    public bool isDashing = false;
    public PlayerStat(int curHP,  int attackDamage,  float moveSpeed,float jumpForce)
    {
        this.curHP = curHP;
        this.maxHP = curHP;
        AttackDamage = attackDamage;
        MoveSpeed = moveSpeed;
        this.jumpForce = jumpForce;
    }

    public void TakeDamage(int damage)
    {
        if (!isDashing) return;
        curHP -= damage;
        //TODO : 피격
    }
}
