using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearJump : IJumpHandler
{

    public Rigidbody2D rb { get; set; }
    public JumpStates state { get; set; }
    public JumpTypes type { get; set; }

    public LinearJump(Rigidbody2D rb)
    {
        this.rb = rb;
        state = JumpStates.defaultJump;
        state = 0;
    }
    public void OnJump(Vector2 dir,float jumpForce)
    {
        rb.velocity = new Vector3(rb.velocity.x,1*jumpForce,0);
    }

    public bool CheckCondition(float time, float maxTime)
    {
        return time <= maxTime && state != JumpStates.doubleJump;
    }

    public void ChangeJumpState()
    {
        if (state < JumpStates.doubleJump)
        {
            state += 1;
        }
    }
}
