using PlayerStates;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    List<PlatformTimer> platformEffectors = new List<PlatformTimer>();
    private readonly float platformRestoreGoal = 0.3f;

    bool isGround;

    class PlatformTimer
    {
        public PlatformEffector2D effector;
        public float timer;
    }

    void Start()
    {
        inputHandle = new PlayerInputHandle();
        inputHandle.input.PlayerInput.Jump.canceled += (a) => { if (jumpHandle.type == JumpTypes.linear && jumpHandle.state != JumpStates.doubleJump) {  currJumpTime = 0; } jumpHandle.ChangeJumpState(); };
        inputHandle.input.PlayerInput.Dash.started += OnDash;
        stateMachine = new PlayerStatesMachine(StateType.idle, transform.GetComponentInChildren<Animator>());
        TryGetComponent<Rigidbody2D>(out rb);
        moveHandle = new LinearMove(rb);
        jumpHandle = new LinearJump(rb);

        stat = new PlayerStat(100,10,8f,16);
    }

    // Update is called once per frame
    void Update()
    {
        if (stateMachine.GetCurrType == StateType.dash) return;
        dashTimer = Math.Clamp(dashTimer+Time.deltaTime, 0, dashCooltime);
        SetPlatformTimer();
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
                        transform.localScale = new Vector3(inputHandle.moveDir.x > 0 ? 1 : -1, 1, 1);
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
            if (inputHandle.moveDir.y >= 0)
            {
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
            else
            {
                if (isGround)
                {
                    //플레이어가 바닥 인식에서 벗어남

                    RaycastHit2D ray = Physics2D.CircleCast(transform.position, 0.3f, Vector2.down, coll.bounds.extents.y, 1 << 8);
                    if (ray)
                    {
                        Debug.Log(ray.collider.excludeLayers);
                        ray.collider.excludeLayers = 1 << 9;
                        ray.transform.gameObject.layer = 0;
                        if (ray.collider.usedByEffector)
                        {
                            ray.transform.TryGetComponent<PlatformEffector2D>(out PlatformEffector2D effector2D);
                            effector2D.colliderMask -= 1 << 9;
                            platformEffectors.Add(new PlatformTimer() { effector = effector2D, timer = 0f });
                        }
                    }
                }
            }

        }
        JumpReset();
    }

    private void SetPlatformTimer()
    {
        if (platformEffectors.Count <= 0) return;

        for (int i = 0; i < platformEffectors.Count; i++)
        {
            platformEffectors[i].timer += Time.deltaTime;
            if (platformRestoreGoal <= platformEffectors[i].timer)
            {
                platformEffectors[i].effector.colliderMask += 1<<9;
                platformEffectors[i].effector.gameObject.layer = 8;
                platformEffectors[i] = null;
                platformEffectors.RemoveAt(i);
                i--;
            }
        }
    }

    private void JumpReset()
    {
        isGround = Physics2D.CircleCast(transform.position, 0.3f, Vector2.down, coll.bounds.extents.y,groundLayers); ;
        if (isGround)
        {
            airDashed = false;
            jumpHandle.state = JumpStates.none;
            if (currJumpTime != default(float) || jumpHandle.type != JumpTypes.linear)
            {
                jumpHandle = IJumpHandler.Factory(JumpTypes.linear, rb);
                currJumpTime = 0f;
            }

            if(rb.velocity == Vector2.zero)
            {
                stateMachine.Change(StateType.idle);
            }


        }
        else
        {
            if (rb.velocity.y < 0)
            {
                stateMachine.Change(StateType.fall);
            }
        }
    }

    private void OnDash(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            if (stateMachine.GetCurrType == StateType.dash || inputHandle.moveDir == Vector2.zero || airDashed || dashTimer < dashCooltime) return;
            if(dashCoroutine != null)StopCoroutine(dashCoroutine);
            dashCoroutine = StartCoroutine(DashCoroutine());
        }
    }

    private void DashCancel(StateType type)
    {
        if (dashCoroutine != null)
        {
            StopCoroutine(dashCoroutine);
            stateMachine.Change(type);
            dashTimer = 0f;
            stat.isDashing = false;
            rb.velocity = Vector2.zero;
        }
    }

    private Coroutine dashCoroutine;
    [SerializeField] float dashDistance;
    private float dashCooltime = 0.5f;
    private float dashTimer = 0f;
    private bool airDashed = false;//공중에선 1회만 대쉬 가능
    private IEnumerator DashCoroutine()
    {
        airDashed = true;
        stat.isDashing = true;
        isGround = false;
        stateMachine.Change(StateType.dash);

        float speed = stat.MoveSpeed * 2.5f;
        float goalDashTime = dashDistance / speed;
        float currDashTime = 0f;
        Vector2 dir = new Vector2(inputHandle.moveDir.x, 0);
        while (currDashTime < goalDashTime)
        {
            currDashTime += Time.deltaTime;
            yield return null;
            rb.velocity = dir * (stat.MoveSpeed*2.5f);
            if (inputHandle.isPressingJump)
            {
                DashCancel(StateType.jump);
            }
        }
        DashCancel(StateType.idle);
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