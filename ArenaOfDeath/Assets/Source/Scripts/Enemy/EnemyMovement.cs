using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _speed = 1.5f;

    private void OnValidate()
    {
        _rigidbody2D ??= GetComponent<Rigidbody2D>();
        _animator ??= GetComponent<Animator>();
    }
    
    private void Update()
    {
        Movement();
    }

    public void Setup(Transform target)
    {
        _player = target;
    }
    
    private void Movement()
    {
        if (_player == null)
            return;
        
        Vector2 targetPosition = _player.position;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);
    }
}
