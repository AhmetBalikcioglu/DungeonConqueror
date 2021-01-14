using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealthController : MonoBehaviour, IDamageable, IHealable
{
    private float _currentHealth;
    public float MaxHealth;
    
    public float CurrentHealth { get { return _currentHealth; } set { _currentHealth = value; } }

    private Character _character;
    public Character Character { get { return (_character == null) ? _character = GetComponent<Character>() : _character; } }

    private ParticleSystem _healingEffect;
    [SerializeField] private GameObject _bloodEffect;

    private HealthBar _healthBar;

    [HideInInspector] public bool gotHit;

    private void OnEnable()
    {
        if (Managers.Instance == null)
            return;
        Character.OnCharacterRevive.AddListener(ResetHealth);
        EventManager.OnGameStart.AddListener(ResetHealth);
        if (Character.CharacterControllerType == CharacterControllerType.Player)
            Initialize();
    }

    private void OnDisable()
    {
        if (Managers.Instance == null)
            return;

        Character.OnCharacterRevive.RemoveListener(ResetHealth);
        EventManager.OnGameStart.RemoveListener(ResetHealth);
    }
    private void Initialize()
    {
        _healingEffect = GetComponentInChildren<ParticleSystem>();
        _healthBar = GetComponentInChildren<HealthBar>();
        ResetHealth();
    }
    public void Initialize(EnemyScriptableBase enemyScriptable)
    {
        gotHit = false;
        _healthBar = transform.parent.GetComponentInChildren<HealthBar>();
        MaxHealth = enemyScriptable.health;
        ResetHealth();
    }

    private void ResetHealth()
    {
        CurrentHealth = MaxHealth;
        _healthBar.ScaleHealthBar(1f);
    }
    
    public void Heal(float healAmount)
    {
        CurrentHealth += healAmount;
        if (CurrentHealth >= MaxHealth)
            CurrentHealth = MaxHealth;
        else if(!_healingEffect.isPlaying)
            _healingEffect.Play();
        _healthBar.ScaleHealthBar(CurrentHealth / MaxHealth);
    }

    public void Damage(int damageAmount)
    {
        if (CurrentHealth <= 0)
            return;
        CurrentHealth -= damageAmount;
        UIManager.Instance.DamageTextCall(transform.position, damageAmount);
        Instantiate(_bloodEffect, transform.position, Quaternion.identity);
        if (CurrentHealth <= 0)
        {
            Character.KillCharacter();
            CurrentHealth = 0;
        }
        _healthBar.ScaleHealthBar(CurrentHealth / MaxHealth);
        //Debug.Log(gameObject.name + " Health: " + CurrentHealth);
    }
}