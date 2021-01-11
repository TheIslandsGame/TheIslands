using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Util;

public class GameInputHandler : MonoBehaviour, PlayerInput.IPlayerActions
{
    private const float MovementModifier = 30.0F;
    private const float MovementThreshold = 0.1F;

    public GameObject spriteRenderer;
    public Rigidbody2D body;
    public Collider2D groundCollider;
    public DrawMode drawGizmos = DrawMode.Always;
    public Vector2 renderOffset = new Vector2(0, 0.8F);
    public float moveSpeed = 0.3F;
    public float maxTurnaroundTime = 1.0F; // max turn duration in seconds
    public float maxJumpTime = 0.5F; // max duration a player will be propelled upwards when jumping, in seconds
    public float jumpForce = 0.3F;
    public bool onGround = true; // TODO implement
    public Animator animator;

    private Vector2 _currentMovementDirection = Vector2.down; // where the player is currently moving
    private Vector2 _targetMovementDirection; // where the player wants to move
    private Vector2 _currentMovementDirectionStart; // where the player was moving when the inputs changed
    private float _timeElapsed;
    private float _jumpTimer;
    private PlayerInput _input;
    private bool _isJumping;
    private LayerMask _layerMask;
    private static readonly int IdSpeedX = Animator.StringToHash("xVelocity");
    private static readonly int IdSpeedY = Animator.StringToHash("yVelocity");
    private static readonly int IdSpeedTotal = Animator.StringToHash("totalVelocity");
    private static readonly int IdSprinting = Animator.StringToHash("Sprinting");
    private static readonly int IdOnGround = Animator.StringToHash("onGround");
    private static readonly int IdTriggerJump = Animator.StringToHash("Jump");

    private void Awake()
    {
        _timeElapsed = maxTurnaroundTime;
        _targetMovementDirection = Vector2.down;
        _jumpTimer = 0.0F;
        _isJumping = false;
        _currentMovementDirectionStart = _currentMovementDirection;
        _input = new PlayerInput();
        _input.Player.SetCallbacks(this);
        _layerMask = LayerMask.GetMask("Floor");
    }

    private void OnEnable()
    {
        _input.Player.Enable();
    }

    private void OnDisable()
    {
        _input.Player.Disable();
    }

    private void OnDrawGizmos()
    {
        if (drawGizmos == DrawMode.Always)
        {
            RenderDebugInformation();
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (drawGizmos == DrawMode.OnSelected)
        {
            RenderDebugInformation();
        }
    }

    private void RenderDebugInformation()
    {
        Debug.DrawRay(body.position + renderOffset,
            new Vector3(_targetMovementDirection.x, _targetMovementDirection.y, 0) * 2, Color.green);
        Debug.DrawRay(body.position + renderOffset,
            new Vector3(_currentMovementDirection.x, _currentMovementDirection.y, -0.1F) * 2, Color.yellow);
    }

    private void Update()
    {
        if (_timeElapsed < maxTurnaroundTime)
        {
            _currentMovementDirection = Vector2.Lerp(_currentMovementDirectionStart, _targetMovementDirection,
                _timeElapsed / maxTurnaroundTime);
            _timeElapsed = Mathf.Clamp(_timeElapsed + Time.deltaTime, 0, maxTurnaroundTime);
        }
        else
        {
            _currentMovementDirection = _targetMovementDirection;
        }

        if (_isJumping)
        {
            if (_jumpTimer >= maxJumpTime)
            {
                _isJumping = false;
            }
            _jumpTimer += Time.deltaTime;
        }
        else if (_jumpTimer > 0.0F)
        {
            _jumpTimer -= Time.deltaTime;
        }

        if (_currentMovementDirection.x > 0)
        {
            spriteRenderer.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            spriteRenderer.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        float speed = Math.Abs(body.velocity.magnitude / MovementModifier);
        float speedX = Math.Abs(body.velocity.x / MovementModifier);
        float speedY = body.velocity.y / MovementModifier;
        animator.SetFloat(IdSpeedTotal, speed >= MovementThreshold ? speed : 0.0F);
        animator.SetFloat(IdSpeedX, speed >= MovementThreshold ? speedX : 0.0F);
        animator.SetFloat(IdSpeedY, speed >= MovementThreshold ? speedY : 0.0F);
        animator.SetBool(IdSprinting, speedX >= MovementThreshold * 2);
    }

    private void FixedUpdate()
    {
        animator.ResetTrigger(IdTriggerJump);
        float yVelocity = body.velocity.y;

        // TODO don't set y velocity when sideways movement is restricted
        // else players can grab onto walls
        if (_jumpTimer <= 0.0F && yVelocity > 0) // player moved upwards but didn't jump
                                                 // TODO OR player has been falling for some time, need to fix this
        {
            yVelocity = 0;
        }

        body.velocity = new Vector2(Math.Sign(_currentMovementDirection.x) * (moveSpeed * MovementModifier), yVelocity);

        onGround = Physics2D.IsTouchingLayers(groundCollider, _layerMask);
        
        if (_isJumping)
        {
            if (onGround)
            {
                body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                animator.SetTrigger(IdTriggerJump);
                onGround = false;
            }
        }
        else
        {
            if (!onGround && body.velocity.y < MovementModifier)
            {
                //TODO slide downwards?
            }
        }
        animator.SetBool(IdOnGround, onGround);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 oldTarget = _targetMovementDirection;
        _targetMovementDirection = context.ReadValue<Vector2>().normalized;
        if (_targetMovementDirection.magnitude == 0)
        {
            _currentMovementDirection = _currentMovementDirection * 0.3F;
        }

        float angle = Vector2.Angle(oldTarget, _targetMovementDirection);
        float delta = Mathf.Clamp(angle / 180.0F, 0.0F, 1.0F);

        if (onGround && _targetMovementDirection.y >= MovementThreshold)
        {
            _isJumping = true;
        }
        else if (_targetMovementDirection.y < MovementThreshold)
        {
            _isJumping = false;
        }

        _timeElapsed = delta / maxTurnaroundTime;
        _currentMovementDirectionStart = _currentMovementDirection;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Vector2 look = context.ReadValue<Vector2>();
        //Debug.Log("Look: " + look);
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        Debug.Log("Fired!");
    }

    public void OnAltFire(InputAction.CallbackContext context)
    {
        Debug.Log("Alt fired!");
    }
}