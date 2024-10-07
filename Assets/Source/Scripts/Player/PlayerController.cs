using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
[RequireComponent(typeof(PlayerInput),  typeof(Damageable), typeof(TochingDirections))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] private TochingDirections _touchingDirections;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Animator _animator;
    [SerializeField] private Damageable _damageable;
    [SerializeField] private float _walkSpeed = 1f;
    [SerializeField] private float _runSpeed = 3f;
    [SerializeField] private float _airSpeed = 3f;
    [SerializeField] private float _jumpForce = 5f;
    private Vector2 _moveInput;
    
    public bool IsAlive
    {
        get
        {
            return   _animator.GetBool(AniamtionsStrings.IsAlive);
        }
    }
    
    public bool CanMove
    {
        get
        {
           return _animator.GetBool(AniamtionsStrings.canMove);
        }
    }

    [SerializeField] private bool _isFacingRight = true;
    public bool IsFasingRight
    {
        get
        {
            return _isFacingRight;
            
        }
        private set
        {
            if (_isFacingRight != value)
                transform.localScale *= new Vector2(-1, 1);
            
            _isFacingRight = value;
        }
    }
    [SerializeField] private bool _isMoving = false;
    public bool IsMoving
    {
        get
        {
           return _isMoving;
        }
        

        private set
        {
            _isMoving = value;
            _animator.SetBool(AniamtionsStrings.IsMoving, value);
        }
    }
    
    [SerializeField] private bool _isRunning = false;

    public bool IsRunning
    {
        get
        {
            return _isRunning;  
        } 
        set
        {
            _isRunning = value;
            _animator.SetBool(AniamtionsStrings.IsRunning, value);
        }
    }

    private float CurrentMoveSpeed
    {
        get
        {
            if (CanMove)
                if (_isMoving && !_touchingDirections.IsOnWall)
                    if (_touchingDirections.IsGround)
                        if (_isRunning)
                            return _runSpeed;
                        else
                            return _walkSpeed;
                    else
                        //TODO Air Move
                        return _airSpeed;
                else
                    //TODO Idle speed is 0 
                    return 0;
            else 
                //TODO Movement locked
                return 0;
        }
    }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _touchingDirections = GetComponent<TochingDirections>();
        _damageable = GetComponent<Damageable>();
    }

    private void FixedUpdate()
    {
        if (!_damageable.LockVelocity)
        {
            _rigidbody2D.velocity = new Vector2(_moveInput.x * CurrentMoveSpeed, _rigidbody2D.velocity.y);
        }
        _animator.SetFloat(AniamtionsStrings.yVelocity, _rigidbody2D.velocity.y);
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
        if (IsAlive)
        {
            IsMoving = _moveInput != Vector2.zero;
            SetFacingDirection(_moveInput);   
        }
        else
            IsMoving = false;
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFasingRight)
            IsFasingRight = true;
        else if (moveInput.x < 0 && IsFasingRight)
            IsFasingRight = false;
    }
    
    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.performed)
            IsRunning = true;
        else if (context.canceled)
            IsRunning = false;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        // TODO Check if alive as well
        if (context.started && _touchingDirections.IsGround && CanMove)
        {
            _animator.SetTrigger(AniamtionsStrings.IsJumpingTrigger);
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpForce);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
            _animator.SetTrigger(AniamtionsStrings.IsAttackTrigger);
    }

    public void OnHit(int damage, Vector2 knockBack)
    {
        _rigidbody2D.velocity= new Vector2(knockBack.x, _rigidbody2D.velocity.y + knockBack.y);
    }
    
}
