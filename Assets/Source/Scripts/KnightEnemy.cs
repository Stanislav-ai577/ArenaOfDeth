using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D), typeof(TochingDirections), typeof(Animator))]
[RequireComponent(typeof(Damageable))]
public class KnightEnemy : MonoBehaviour
{
    public enum WalkableDirection
    {
        Right,
        Left
    }
    
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Animator _animator;
    [SerializeField] private TochingDirections _touchingDirections;
    [SerializeField] private DetectionZone _attackZone;
    [SerializeField] private WalkableDirection _walkableDirection;
    [SerializeField] private Damageable _damageable;
    [SerializeField] private float _walkSpeed = 1f;
    
    [SerializeField] private bool _hasTarget = false;
    public bool HasTarget
    {
        get
        {
            return _hasTarget;
        }
        private set
        {
            _hasTarget = value;
            _animator.SetBool(AniamtionsStrings.hasTarget, value);
        }
    }

    public bool CanMove
    {
        get
        {
          return  _animator.GetBool(AniamtionsStrings.canMove);
        }
            
    }


    [SerializeField] private Vector2 _walkDirectionVector = Vector2.right;
    public WalkableDirection WalkDirection
    {
        get => _walkableDirection;
        set
        {
            if (_walkableDirection != value)
            {
                //TODO Flip Direction
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

                if (value == WalkableDirection.Right)
                {
                    _walkDirectionVector = Vector2.right;
                }else if (value == WalkableDirection.Left)
                {
                    _walkDirectionVector = Vector2.left;
                }
            }
            _walkableDirection = value;
        }
    }
    
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _touchingDirections = GetComponent<TochingDirections>();
        _animator = GetComponent<Animator>();
        _damageable = GetComponent<Damageable>();
    }

    private void Update()
    {
        HasTarget = _attackZone.DetectedColliders.Count > 0;
    }


    private void FixedUpdate()
    {
        if (_touchingDirections.IsGround && _touchingDirections.IsOnWall)
        {
            FlipDirections();
        }

        if (!_damageable.LockVelocity)
        {
            if (CanMove)
            {
                _rigidbody2D.velocity = new Vector2(_walkSpeed * _walkDirectionVector.x, _rigidbody2D.velocity.y);
            }
        }
    }

    private void FlipDirections()
    {
        if (WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        }else if (WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right;
        }
        else
        {
            Debug.LogError("Current walkable direction is not set to legal values of right or left");
        }
    }

    public void OnHit(int damage, Vector2 knockBack)
    {
        _rigidbody2D.velocity= new Vector2(knockBack.x, _rigidbody2D.velocity.y + knockBack.y);
    }
    
}
