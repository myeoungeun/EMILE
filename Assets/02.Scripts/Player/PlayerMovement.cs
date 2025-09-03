using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 inputValue;
    public bool lookDirectionRight = true; //Attack.cs에서 바라보는 방향 판별용. 기본은 오른쪽 방향이라 true

    [Header("Move Settings")]
    [SerializeField] private float moveSpeed = 10.0f;
    [SerializeField] private float jumpForce = 15.0f;
    [SerializeField] private float maxJumptime = 0.3f;

    [Header("Jump Settings")]
    [SerializeField] private int maxJumpCount = 1;
    [SerializeField] private float groundCheckDistance = 0.1f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float jumpCooldown = 0.15f;

    [Header("Dash Settings")]
    [SerializeField] private float dashSpeed = 20.0f;
    [SerializeField] private float dashDistance = 5.0f;
    [SerializeField] private float dashCooldown = 1.0f;

    private int currentJumpCount = 0;
    private bool isJump = false;
    private bool jumpButtonHeld = false;
    private float jumptimeCount = 0;
    private float lastJumpTime = 0f;
    private bool canJump = true;

    private bool isDashing = false;
    private bool canDash = true;
    private Vector2 dashDirection;
    private float lastDashTime = 0f;

    private Coroutine dashCoroutine;

    // 점프 중 대쉬 입력 시 두 행동을 모두 봉쇄할 상태 플래그 추가
    private bool dashDuringJump = false;
    private bool inputLocked = false; // 점프 중 대쉬 후 착지 전까지 입력 잠금

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        UpdateAnimationState();
    }

    private void FixedUpdate()
    {
        if (!isDashing && !inputLocked)
        {
            Move();
            Jump();
        }
        else if (isDashing)
        {
            // 대쉬 중에는 이동과 점프 무시
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        // inputLocked가 true여도 입력을 받되, FixedUpdate에서 이동 제어함
        if (context.phase == InputActionPhase.Performed)
        {
            inputValue = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            inputValue = Vector2.zero;
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if (canDash && !isDashing && Time.time - lastDashTime > dashCooldown && !inputLocked)
            {
                if (IsJumping())
                {
                    dashDuringJump = true;
                    inputLocked = true;  // 점프 중 대쉬시 입력 잠금
                }

                if (dashCoroutine != null)
                    StopCoroutine(dashCoroutine);

                dashCoroutine = StartCoroutine(DashRoutine());
            }
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (inputLocked) return; // 입력 잠금 상태면 점프 불가
        if (isDashing) return;   // 대쉬 중에도 점프 불가

        if (context.phase == InputActionPhase.Started)
        {
            if (currentJumpCount < maxJumpCount && canJump && Time.time - lastJumpTime > jumpCooldown)
            {
                isJump = true;
                jumpButtonHeld = true;
                jumptimeCount = 0f;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                currentJumpCount++;
                lastJumpTime = Time.time;
                canJump = false;

                animator.SetBool("isJumping", true);
                animator.SetBool("isFalling", false);
            }
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            jumpButtonHeld = false;
            canJump = true;
            if (rb.velocity.y <= 0)
            {
                isJump = false;
            }
        }
    }

    private IEnumerator DashRoutine()
    {
        isDashing = true;
        canDash = false;
        lastDashTime = Time.time;

        dashDirection = inputValue != Vector2.zero ? inputValue.normalized : new Vector2(Mathf.Sign(transform.localScale.x), 0);

        float dashTime = dashDistance / dashSpeed;
        float elapsed = 0f;

        animator.Play("Dash", -1, 0f);

        while (elapsed < dashTime)
        {
            rb.velocity = dashDirection * dashSpeed;
            elapsed += Time.deltaTime;
            yield return null;
        }

        EndDash();

        if (dashDuringJump)
        {
            // 착지 전까지 입력 잠금 지속
            yield return StartCoroutine(WaitForLanding());
            dashDuringJump = false;
            inputLocked = false;
        }
    }

    private IEnumerator WaitForLanding()
    {
        while (!IsGrounded())
        {
            yield return null;
        }
    }

    private void EndDash()
    {
        isDashing = false;
        canDash = true;
        dashCoroutine = null;

        rb.velocity = new Vector2(0, rb.velocity.y);

        // 대쉬 끝나면 입력값 초기화 해줘 이동/점프가 자동으로 안되도록
        isJump = false;
        jumpButtonHeld = false;
    }

    private void Move()
    {
        if (!IsJumping())
        {
            if (inputValue != Vector2.zero)
            {
                lookDirectionRight = inputValue.x > 0 ? true : false; //플레이어가 바라보는 방향
                transform.localScale = new Vector3(Mathf.Sign(inputValue.x), 1, 1);
                animator.SetBool("isMoving", true);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }
        }

        rb.velocity = new Vector2(inputValue.x * moveSpeed, rb.velocity.y);
    }

    private void Jump()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * Time.fixedDeltaTime;
        }

        if (jumpButtonHeld && isJump && jumptimeCount < maxJumptime)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumptimeCount += Time.fixedDeltaTime;
        }

        if (IsGrounded())
        {
            currentJumpCount = 0;
        }
    }

    private void UpdateAnimationState()
    {
        if (isDashing) return;

        if (!IsGrounded())
        {
            if (rb.velocity.y > 0.01f)
            {
                animator.SetBool("isJumping", true);
                animator.SetBool("isFalling", false);
            }
            else if (rb.velocity.y < -0.01f)
            {
                animator.SetBool("isJumping", false);
                animator.SetBool("isFalling", true);
            }
        }
        else
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);
        }
    }

    private bool IsJumping()
    {
        return !IsGrounded() && (animator.GetBool("isJumping") || animator.GetBool("isFalling"));
    }

    private bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 start = transform.position;
        Vector3 end = start + Vector3.down * groundCheckDistance;
        Gizmos.DrawLine(start, end);
        Gizmos.DrawSphere(end, 0.05f);

        if (isDashing)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, dashDirection * 2f);
        }
    }
}
