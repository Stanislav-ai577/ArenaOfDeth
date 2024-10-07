using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{ 
    public UnityEvent<int, Vector2> OnDamageHit;
    
    [SerializeField] private Animator _animator;
    [SerializeField] private bool _isInvincible = false;
    [SerializeField] private float _timeSinceHit = 0;
    [SerializeField] private float _invincibilityTime = 0.25f;
    
    [SerializeField] private int _maxHealth = 100;
    public int MaxHealth{
        get
        {
            return _maxHealth; 
            
        }
        set
        {
            _maxHealth = value; 
            
        } 
    }
    
    [SerializeField] private int _currentHealth = 100;
    public int CurrentHealth
    {
        get
        {
            return _currentHealth;
        }
        set
        {
            _currentHealth = value;
            if (_currentHealth <= 0)
            {
                IsAlive = false;
            }
        }
    }
    
    [SerializeField] private bool _isAlive = true;
    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
        set
        {
            _isAlive = value;
            _animator.SetBool(AniamtionsStrings.IsAlive, value);
            Debug.Log("IsAlive set" + value);
        }
    }

    public bool LockVelocity
    {
        get
        {
            return _animator.GetBool(AniamtionsStrings.LockVelocity);
        }
        set
        {
            _animator.SetBool(AniamtionsStrings.LockVelocity, value);
        }
    }
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_isInvincible)
        {
            if (_timeSinceHit > _invincibilityTime)
            {
                _isInvincible = false;
                _timeSinceHit = 0;
            }
            _timeSinceHit += Time.deltaTime;
        }
    }
    
    public bool Hit(int damage, Vector3 knockBack)
    {
        if (IsAlive && !_isInvincible)
        {
            CurrentHealth -= damage;
            _isInvincible = true;
            _animator.SetTrigger(AniamtionsStrings.HitTrigger);
            LockVelocity = true;
            OnDamageHit?.Invoke(damage, knockBack);
            return true;
        }
        return false;
    }
}
