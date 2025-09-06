using PlayerStates;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;
using UnityEngine.Timeline;

public class Player : MonoBehaviour
{
    private PlayerAttack playerAttack;
    private PlayerInputHandle inputHandle;

    private PlayerStatesMachine stateMachine;
    private SpriteRenderer sr;
    private Rigidbody2D rb;

    [SerializeField]PlayerStat stat;

    // PlayerStat, PlayerAttack 정보에 접근할 수 있도록 읽기 전용 프로퍼티 추가
    public PlayerStat Stat => stat; 
    public PlayerAttack Attack => playerAttack;

    private IMoveHandler moveHandle;
    private IJumpHandler jumpHandle;

    [SerializeField]private LayerMask groundLayers;

    [SerializeField] private Collider2D coll;

    private readonly float maxJumpTime = 0.3f;
    [SerializeField]private float currJumpTime;

    List<PlatformTimer> platformEffectors = new List<PlatformTimer>();
    private readonly float platformRestoreGoal = 0.3f;

    bool isGround;

    public void TestTakeDamage() //데미지 테스트용 코드 - 병합후 삭제
    {
        stat.TakeDamage(10);
    }
    
    class PlatformTimer
    {
        public PlatformEffector2D effector;
        public float timer;
    }

    void Start()
    {
        sr = transform.GetComponentInChildren<SpriteRenderer>();
        playerAttack = new();
        playerAttack.Init();
        inputHandle = new PlayerInputHandle();
        EventReset();
        inputHandle.input.PlayerInput.Jump.canceled += (a) => { if (jumpHandle.type == JumpTypes.linear && jumpHandle.state != JumpStates.doubleJump) {  currJumpTime = 0; } jumpHandle.ChangeJumpState(); };
        inputHandle.input.PlayerInput.Dash.started += OnDash;
        inputHandle.input.PlayerInput.Attack.started += OnShot;
        inputHandle.input.PlayerInput.Attack.canceled += ShotPause;
        inputHandle.input.PlayerInput.BulletChange.performed += OnBulletChange;
        stateMachine = new PlayerStatesMachine(StateType.idle, transform.GetComponentInChildren<Animator>());
        TryGetComponent<Rigidbody2D>(out rb);
        moveHandle = new LinearMove(rb);
        jumpHandle = new LinearJump(rb);

        stat = new PlayerStat(100,10,8f,16,2);
        stat.Respawn += Respawn;

        UIManager.Instance.InGameUI.PlayerHUD.SetPlayer(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (stateMachine.GetCurrType == StateType.dash) return;
        dashTimer = Math.Clamp(dashTimer+Time.deltaTime, 0, dashCooltime);
        Shot();
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
                if(stateMachine.GetCurrType != StateType.shot && stateMachine.GetCurrType != StateType.jumpShot)
                {
                    stateMachine.Change(StateType.run);
                    transform.localScale = new Vector3(inputHandle.moveDir.x > 0 ? 1 : -1, 1, 1);
                }
                moveHandle.OnMove((dir) * stat.MoveSpeed);


            }
            else
            {
                Debug.DrawRay(transform.position, dir * (coll.bounds.extents.x + 0.1f));

                if (Physics2D.Raycast(transform.position, dir, coll.bounds.extents.x + 0.1f, 1 << 7))
                {
                    Debug.Log(stateMachine.GetCurrType);
                    if (stateMachine.GetCurrType != StateType.grab)
                    {
                        if (stateMachine.GetCurrType != StateType.shot && stateMachine.GetCurrType != StateType.jumpShot)transform.localScale = new Vector3(inputHandle.moveDir.x > 0 ? 1 : -1, 1, 1);
                            
                        currJumpTime = 0f;
                        rb.velocity = Vector3.zero;
                        inputHandle.isPressingJump = false;
                        airDashed = false;
                        jumpHandle = IJumpHandler.Factory(JumpTypes.wall, rb);
                        stateMachine.Change(StateType.grab);
                        ShotPause(StateType.grab);
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
                    moveHandle.OnMove((dir) * stat.MoveSpeed);
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
                if (stateMachine.GetCurrType != StateType.shot && stateMachine.GetCurrType != StateType.jumpShot) stateMachine.Change(StateType.jump);
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

            if (stateMachine.GetCurrType != StateType.shot && stateMachine.GetCurrType != StateType.jumpShot)
            {
                if (rb.velocity == Vector2.zero)
                {
                    stateMachine.Change(StateType.idle);
                }
            }
            else
            {
                stateMachine.Change(StateType.shot, bulletDir);
                if (bulletDir == BulletDirrections.SE || bulletDir == BulletDirrections.SW || bulletDir == BulletDirrections.S) 
                {
                    ShotPause(StateType.idle);                    
                }
            }

        }
        else
        {
            if (stateMachine.GetCurrType != StateType.shot && stateMachine.GetCurrType != StateType.jumpShot)
            {
                if (rb.velocity.y < 0)
                {
                    if (stateMachine.GetCurrType != StateType.shot && stateMachine.GetCurrType != StateType.jumpShot) stateMachine.Change(StateType.fall);
                }
            }
            else stateMachine.Change(StateType.jumpShot, bulletDir);

        }
    }
    #region 대쉬

    private void OnDash(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            if (stateMachine.GetCurrType == StateType.dash || airDashed || dashTimer < dashCooltime) return;
            if (dashCoroutine != null) StopCoroutine(dashCoroutine);
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
            //rb.velocity = Vector2.zero;
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
        ShotPause(StateType.dash);

        float speed = stat.MoveSpeed * 2.5f;
        float goalDashTime = dashDistance / speed;
        float currDashTime = 0f;

        inputHandle.isPressingJump = false;

        Vector2 dir = Vector2.zero;

        if (inputHandle.moveDir.x != 0) dir = new Vector2(inputHandle.moveDir.x, 0);
        else dir = transform.localScale.x == -1 ? Vector2.left : Vector2.right;
        
        while (currDashTime < goalDashTime)
        {
            RaycastHit2D ray = Physics2D.CircleCast(transform.position, 0.3f, Vector2.down, coll.bounds.extents.y, groundLayers);
            currDashTime += Time.deltaTime;
            yield return null;
            rb.velocity = dir * (stat.MoveSpeed * 2.5f);
            if (inputHandle.isPressingJump)
            {
                DashCancel(StateType.jump);
            }
        }
        DashCancel(StateType.idle);
    }
    #endregion
    #region 공격
    
    public void OnShot(InputAction.CallbackContext ctx)
    {

        bulletDir = ConvertDirrection(inputHandle.moveDir);
        if (stateMachine.GetCurrType == StateType.dash) { DashCancel(StateType.shot); }

        if (isGround)
        {
            if (bulletDir == BulletDirrections.S|| bulletDir == BulletDirrections.SE) bulletDir = BulletDirrections.E;
            else if (bulletDir == BulletDirrections.SW) bulletDir = BulletDirrections.W;
            stateMachine.Change(StateType.shot, bulletDir);//애니메이션 전이
        }
        else stateMachine.Change(StateType.jumpShot, bulletDir);//애니메이션 전이

        fireDir = BulletDirToVector(bulletDir);
        if(bulletDir == BulletDirrections.SW || bulletDir == BulletDirrections.W || bulletDir == BulletDirrections.NW) transform.localScale = new Vector3(-1,1,1);
        else transform.localScale = Vector3.one;

        isShotting = true;
    }
    public void ShotPause(InputAction.CallbackContext ctx)
    {
        stateMachine.Change(StateType.idle);
        isShotting = false;
    }
    public void ShotPause(StateType state)
    {
        stateMachine.Change(state);
        isShotting = false;
    }
    

    private BulletDirrections ConvertDirrection(Vector2 vec)
    {
        if (vec.x == 0 && vec.y == 0) return transform.localScale.x == 1 ? BulletDirrections.E : BulletDirrections.W;//방향설정 없을 시 정면쏘도록

        if (vec.x == 0 && vec.y > 0) return BulletDirrections.N;
        else if (vec.x == 0 && vec.y < 0) return BulletDirrections.S;
        else if (vec.x > 0 && vec.y == 0) return BulletDirrections.E;
        else if (vec.x < 0 && vec.y == 0) return BulletDirrections.W;
        else if (vec.x > 0 && vec.y > 0) return BulletDirrections.NE;
        else if (vec.x < 0 && vec.y > 0) return BulletDirrections.NW;
        else if (vec.x > 0 && vec.y < 0) return BulletDirrections.SE;
        else return BulletDirrections.SW;
    }
    
    private Vector2 BulletDirToVector(BulletDirrections dir)
    {
        switch (dir)
        {
            case BulletDirrections.N:
                return Vector2.up;
            case BulletDirrections.NE:
                return Vector2.one.normalized;
            case BulletDirrections.E:
                return Vector2.right;
            case BulletDirrections.SE:
                return new Vector2(1, -1).normalized;
            case BulletDirrections.S:
                return Vector2.down;
            case BulletDirrections.SW:
                return new Vector2(-1, -1).normalized;
            case BulletDirrections.W:
                return Vector2.left;
            case BulletDirrections.NW:
                return new Vector2(-1, 1).normalized;
        }
        return Vector2.right;
    }
    private bool isShotting = false;
    float attackDelay = 0.2f; // 임시 공속값 무기객체 받으면 해당 변수로 대입
    float currAttakTime;//발사로부터의 시간,무기객체에서 받아야함
    Vector3 fireDir;//발사방향
    BulletDirrections bulletDir;
    private void Shot()
    {
        currAttakTime += Time.deltaTime;
        if (!isShotting) return;
        
        if (currAttakTime >= attackDelay)
        {
            currAttakTime = 0f;

            if (playerAttack != null)
            {
                float angle = Mathf.Atan2(fireDir.y, fireDir.x) * Mathf.Rad2Deg;
                Vector3 spawnPos = transform.position + (Vector3)fireDir;
                Quaternion spawnRot = Quaternion.Euler(0, 0, angle);

                playerAttack.Shoot(spawnPos, spawnRot);
            }
        }
    }
    
    private void OnBulletChange(InputAction.CallbackContext ctx)
    {
        playerAttack.OnBulletChange(ctx);
    }


    #endregion
    private void Respawn()
    {
        transform.position = GameManager.GetInstance.GetCheckPoint;
    }

    private void EventReset()
    {
        dashCoroutine = null;
        inputHandle.input.PlayerInput.Jump.canceled -= (a) => { if (jumpHandle.type == JumpTypes.linear && jumpHandle.state != JumpStates.doubleJump) { currJumpTime = 0; } jumpHandle.ChangeJumpState(); };
        inputHandle.input.PlayerInput.Dash.started -= OnDash;
        inputHandle.input.PlayerInput.Attack.started -= OnShot;
        inputHandle.input.PlayerInput.Attack.canceled -= ShotPause;
        inputHandle.input.PlayerInput.BulletChange.performed -= OnBulletChange;
    }
    private void OnDestroy()
    {
        EventReset();
    }
}
public enum BulletDirrections
{
    N,NE,E,SE,S,SW,W,NW
}
public class PlayerInputHandle
{
    public PlayerInputActionGenerated input;
    public Vector2 moveDir;
    [SerializeField]public bool isPressingJump;
    public PlayerInputHandle()
    {
        input = new PlayerInputActionGenerated();
        input.RemoveAllBindingOverrides();

        RemoveKeyEvent();
        RegistKeyEvent();
        

    }
    private void RemoveKeyEvent()
    {
        input.PlayerInput.Disable();
        input.PlayerInput.Move.Reset();
        input.PlayerInput.Jump.Reset();
        input.PlayerInput.Attack.Reset();
        input.PlayerInput.Dash.Reset();
        input.PlayerInput.BulletChange.Reset();
    }
    private void RegistKeyEvent()
    {
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