using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IJumpHandler
{
    Rigidbody2D rb { get; set; }
    JumpStates state { get; set; }
    JumpTypes type { get; set; }
    bool CheckCondition(float time,float maxTime);
    void OnJump(Vector2 jumpDir,float jumpForce);
    void ChangeJumpState();
    public static IJumpHandler Factory(JumpTypes type, Rigidbody2D rb)
    {
        switch (type)
        {
            case JumpTypes.linear:
                return new LinearJump(rb);
            case JumpTypes.wall:
                return new WallJump(rb);
            default:
                break;
        }
        return null;
    }
}
public enum JumpTypes { linear,wall}
public enum JumpStates { none, defaultJump, doubleJump }