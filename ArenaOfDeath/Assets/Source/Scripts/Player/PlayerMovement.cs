using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _speed = 2f;
    private Vector2 _input;
    private bool _moveLeft;
    private bool _moveRight;
    private readonly int _animationMoveRight = Animator.StringToHash("Move");
    private readonly int _animationMoveLeft = Animator.StringToHash("Move");

    private void Awake()
    {
        _speed = 2f;
    }

    private void OnValidate()
    {
        _animator ??= GetComponent<Animator>();
        _rigidbody ??= GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
    }
    
    private void Move()
    {
        if (_input.x is 0 and 0 && _input.y is 0 and 0)
            _animator.SetBool(_animationMoveRight, false);
        else
            _animator.SetBool(_animationMoveLeft, true);
        
        _input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _rigidbody.velocity = _input * _speed;
        Flip();
        
    }

    private void Flip()
    {
        _moveLeft = _input.x < 0;
        _moveRight = _input.x > 0;

        if (_moveLeft)
            transform.localScale = new Vector2(-1f, transform.localScale.y);
        if(_moveRight)
            transform.localScale = new Vector2(1f, transform.localScale.y);
    }
}
