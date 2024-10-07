using UnityEngine;

public class TochingDirections : MonoBehaviour
{
    [SerializeField] private CapsuleCollider2D _collider2D;
    [SerializeField] private ContactFilter2D _contactFilter2D;
    [SerializeField] private RaycastHit2D[] _groundsHit = new RaycastHit2D[5];
    [SerializeField] private RaycastHit2D[] _wallHit = new RaycastHit2D[5];
    [SerializeField] private RaycastHit2D[] _ceilingHits = new RaycastHit2D[5];
    [SerializeField] private Animator _animator;
    [SerializeField] private float _groundDistance = 0.05f;
    [SerializeField] private float _wallDistance = 0.2f;
    [SerializeField] private float _cellingDistance = 0.05f;
    
    [SerializeField] private bool _isGrounded = true;
    public bool IsGround
    {
        get => _isGrounded;
        set
        {
            _isGrounded = value;
            _animator.SetBool(AniamtionsStrings.IsGrounded, value);
        }
    }
    
    [SerializeField] private bool _isOnWall = true;
    public bool IsOnWall
    {
        get => _isOnWall;
        set
        {
            _isOnWall = value;
            _animator.SetBool(AniamtionsStrings.IsOnWall, value);
        }
    }
    
    [SerializeField] private bool _isOnCeiling;
    private Vector2 _wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;
    public bool IsOnCeiling
    {
        get => _isOnCeiling;
        set
        {
            _isOnCeiling = value;
            _animator.SetBool(AniamtionsStrings.IsOnCelling, value);
        }
    }
    
    private void Awake()
    {
        _collider2D = GetComponent<CapsuleCollider2D>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
       IsGround = _collider2D.Cast(Vector2.down, _contactFilter2D, _groundsHit, _groundDistance) > 0;
       IsOnWall = _collider2D.Cast(_wallCheckDirection, _contactFilter2D, _wallHit, _wallDistance) > 0;
       IsOnCeiling = _collider2D.Cast(Vector2.up, _contactFilter2D, _ceilingHits, _cellingDistance) > 0;
    }

}
