using PlayerStates;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private PlayerInputHandle inputHandle;

    private PlayerStatesMachine stateMachine;

    private Rigidbody2D rb;

    [SerializeField]PlayerStat stat;

    private IMoveHandler moveHandle;
    private IJumpHandler jumpHandle;

    [SerializeField]private LayerMask groundLayers;

    [SerializeField] private Collider2D coll;

    private readonly float maxJumpTime = 0.3f;
    [SerializeField]private float currJumpTime;

    bool isGround;

    void Start()
    {
        inputHandle = new PlayerInputHandle();
        inputHandle.input.PlayerInput.Jump.canceled += (a) => { if (jumpHandle.type == JumpTypes.linear && jumpHandle.state != JumpStates.doubleJump) {  currJumpTime = 0; } jumpHandle.ChangeJumpState(); };
        stateMachine = new PlayerStatesMachine(StateType.idle, transform.GetComponentInChildren<Animator>());
        TryGetComponent<Rigidbody2D>(out rb);
        moveHandle = new LinearMove(rb);
        jumpHandle = new LinearJump(rb);

        stat = new PlayerStat(100,10,8f,16);
    }

    // Update is called once per frame
    void Update()
    {
        OnMove();
        OnJump();
    }
    private void OnMove()
    {
        if (inputHandle.moveDir.x != 0f) 
        {
            Vector2 dir = (inputHandle.moveDir.x > 0 ? Vector2.right : Vector2.left);

            if (isGround)
            {
                stateMachine.Change(StateType.run);

                moveHandle.OnMove((dir) * stat.MoveSpeed);

                transform.localScale = new Vector3(inputHandle.moveDir.x > 0 ? 1 : -1, 1, 1);
            }
            else
            {
                Debug.DrawRay(transform.position, dir * (coll.bounds.extents.x + 0.1f));

                if (Physics2D.Raycast(transform.position, dir, coll.bounds.extents.x + 0.1f, 1 << 7))
                {
                    if (stateMachine.GetCurrType != StateType.grab)
                    {
                        currJumpTime = 0f;
                        rb.velocity = Vector3.zero;
                        inputHandle.isPressingJump = false;
                        
                        jumpHandle = IJumpHandler.Factory(JumpTypes.wall, rb);
                        stateMachine.Change(StateType.grab);
                    }
                    else
                    {
                        rb.velocity = Vector3.zero;
                    }

                    return;
                }
                else
                {
                    if(jumpHandle.type == JumpTypes.wall&& stateMachine.GetCurrType == StateType.grab) jumpHandle = IJumpHandler.Factory(JumpTypes.linear, rb);
                }

            }

            return;
        }


    }
    private void OnJump()
    {
        if (inputHandle.isPressingJump && jumpHandle.CheckCondition(currJumpTime,maxJumpTime))
        {
            Debug.Log("´©¸§");
            currJumpTime += Time.deltaTime;
            jumpHandle.OnJump(inputHandle.moveDir, stat.JumpForce);
            stateMachine.Change(StateType.jump);
            if (jumpHandle.type == JumpTypes.wall)
            {
                jumpHandle = IJumpHandler.Factory(JumpTypes.linear, rb);
                inputHandle.moveDir = Vector2.zero;
                inputHandle.isPressingJump = false;
            }
            return;
        }
        JumpReset();

    }
    private void JumpReset()
    {
        Debug.DrawRay(transform.position, Vector3.down * coll.bounds.extents.y);
        isGround = Physics2D.Raycast(transform.position, Vector3.down, coll.bounds.extents.y+0.3f, groundLayers);
        if (isGround)
        {
            jumpHandle.state = JumpStates.none;
            if (currJumpTime != default(float) || jumpHandle.type != JumpTypes.linear)
            {
                jumpHandle = IJumpHandler.Factory(JumpTypes.linear, rb);
                currJumpTime = 0f;
            }
            if(stateMachine.GetCurrType == StateType.jump || stateMachine.GetCurrType == StateType.fall)
            {
                stateMachine.Change(StateType.idle);
            }
        }
    }
}
public class PlayerInputHandle
{
    public PlayerInputActionGenerated input;
    public Vector2 moveDir;
    [SerializeField]public bool isPressingJump;
    public PlayerInputHandle()
    {
        input = new PlayerInputActionGenerated();
        input.PlayerInput.Enable();
        input.PlayerInput.Move.canceled += OnMove;
        input.PlayerInput.Move.performed += OnMove;
        input.PlayerInput.Jump.canceled += OnJump;
        input.PlayerInput.Jump.performed += OnJump;
    }
    private void OnMove(InputAction.CallbackContext cb)
    {
        moveDir = cb.ReadValue<Vector2>();
    }
    private void OnJump(InputAction.CallbackContext cb)
    {
        isPressingJump = !cb.canceled;
    }
}