using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerHealth : MonoBehaviour,IDamage
{
    [SerializeField] private float _health = 10f;
    [SerializeField] private Animator _animator;
    [SerializeField] private PauseService _pauseService;
    private bool _isDie = false;
    
    private void OnValidate()
    {
        _animator = GetComponent<Animator>();
    }

    public void Setup(PauseService pauseService)
    {
        _pauseService = pauseService;
    }
    
    public void TakeDamage(float damage)
    {
        if (damage < 0)
            throw new ArgumentException("Damage most be positive.");

        if (_isDie)
            return;

        _health -= damage;

        if (_health <= 0)
        {
            Die(); 
            _pauseService.Pause();
        }
        
    }

    private void Die()
    {
        _isDie = true;
        Destroy(gameObject);
    }
}
