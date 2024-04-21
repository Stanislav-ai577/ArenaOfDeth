using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private PolygonCollider2D _attackArea;
    [SerializeField] private Animator _animator;
    private Coroutine _attackTick;
    private float _attackDelay = 0.5f;
    private bool _isAttack = true;
    private readonly int OnAttack = Animator.StringToHash("Attack");
    [field: SerializeField] public int Damage { get; private set; }

    private void Awake()
    {
        _attackArea.gameObject.SetActive(!_isAttack);
    }

    private void OnValidate()
    {
        _animator ??= GetComponent<Animator>();
    }

    private void Start()
    {
        _attackDelay = 0.5f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    private void Attack()
    {
        _attackArea.gameObject.SetActive(_isAttack);
        _animator.SetTrigger(OnAttack);
        _attackTick = StartCoroutine(AttackTick());
    }

    private IEnumerator AttackTick()
    {
        yield return new WaitForSeconds(_attackDelay);
        _attackArea.gameObject.SetActive(!_isAttack);
    }
}
