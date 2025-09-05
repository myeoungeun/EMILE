using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : IDamageable
{
    public event Action<int, int> OnHPChanged; // 현재HP, 최대HP
    public event Action<int> OnLifeChanged; // 목숨 수
    public event Action Respawn;


    private int life;
    public int Life { get { return life; } set { life = value; } }
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
    public PlayerStat(int curHP,  int attackDamage,  float moveSpeed, float jumpForce, int life)
    {
        this.curHP = curHP;
        this.maxHP = curHP;
        AttackDamage = attackDamage;
        MoveSpeed = moveSpeed;
        this.jumpForce = jumpForce;
        this.life = life;
    }

    public void TakeDamage(int damage)
    {
        if (isDashing || life <= 0) return;
        curHP -= damage;
        //TODO : 피격
        // HP 변경 이벤트 호출
        OnHPChanged?.Invoke(curHP, maxHP);

        // HP가 0 이하라면 목숨 감소
        if (curHP <= 0)
        {
            life--;
            OnLifeChanged?.Invoke(life);

            if (life > 0)
            {
                curHP = maxHP;
                OnHPChanged?.Invoke(curHP, maxHP); // 부활 시 HP 회복 이벤트
                Respawn?.Invoke();
            }
            else
            {
                curHP = 0;
                life = 0;
                UIManager.Instance.GameOverUI.Open();
                Time.timeScale = 0;
            }
        }
    }
}
