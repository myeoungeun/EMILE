using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class WallJump : IJumpHandler
{
    public Rigidbody2D rb { get; set; }
    public JumpStates state { get; set; }
    public JumpTypes type { get; set; }
    public WallJump(Rigidbody2D rb)
    {
        this.rb = rb;
        type = JumpTypes.wall;
        this.state = 0;
    }

    public void OnJump(Vector2 jumpDir, float jumpForce)
    {
        if (jumpDir.x != 0)
        {
            rb.velocity = new Vector2(-jumpDir.x*10f, jumpForce*2);
        }
    }

    public bool CheckCondition(float time, float maxTime)
    {
        return true;
    }

    public void ChangeJumpState()
    {
        
    }
}
