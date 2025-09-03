using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBullet : MonoBehaviour
{
    private float speed;
    private int damage;
    private Vector2 dir;

    private Rigidbody2D rb;
    private Collider2D col;

    public void Init(float speed, int damage, Vector2 dir)
    {
        this.speed = speed;
        this.damage = damage;
        this.dir = dir;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = dir * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((1 << collision.gameObject.layer) == LayerMask.GetMask("Player"))
        {
            //Debug.Log("플레이어 총알 맞음");
            Destroy(gameObject);
        }
    }
}
