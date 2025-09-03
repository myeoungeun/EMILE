using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearMove : IMoveHandler
{
    public Rigidbody2D rb { get; set; }
    public LinearMove(Rigidbody2D rb)
    {
        this.rb = rb;
    }

    public void OnMove(Vector2 dir)
    {
        dir.y = rb.velocity.y;
        rb.velocity = dir;
    }
}
