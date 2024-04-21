using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyHealth : MonoBehaviour,IDamage
{
    [SerializeField] private float _health = 10f;
    [SerializeField] private Animator _animator;
    [SerializeField] private Counter _counter;
    private float _dieTick = 2f;
    private bool _isDie;
    public int _enemyKillCount;
    private readonly int _onDie = Animator.StringToHash("Die");

    private void OnValidate()
    {
        _animator ??= GetComponent<Animator>();
    }

    private void Start()
    {
        _health = 1f;
        _dieTick = 2f;
    }

    public void Setup(Counter counter)
    {
        _counter = counter;
    }
    
    public void TakeDamage(float damage)
    {
        if (damage < 0)
            throw new ArgumentException("Damage most be positive");

        if (_isDie)
            return;
        
        _animator.SetTrigger("TakeHit");
        _health -= damage;

        if (_health <= 0)
        {
            Die();
        }
    }
    
    private void Die()
    {
        _isDie = true;
        _enemyKillCount++;
        _counter.EnemyKillCount(_enemyKillCount);
        _animator.SetBool(_onDie, true);
        StartCoroutine(DieTick());
    }
    
    
    private IEnumerator DieTick()
    {
        yield return new WaitForSeconds(_dieTick);
        Destroy(gameObject);
    }
}
