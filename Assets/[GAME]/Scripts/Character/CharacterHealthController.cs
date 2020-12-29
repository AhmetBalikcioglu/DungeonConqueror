﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealthController : MonoBehaviour, IDamageable, IHealable
{
    private EnemyScriptableBase _enemyScriptable;

    private int _currentHealth;
    public int MaxHealth;
    
    public int CurrentHealth { get { return _currentHealth; } set { _currentHealth = value; } }

    private Character _character;
    public Character Character { get { return (_character == null) ? _character = GetComponent<Character>() : _character; } }

    private void OnEnable()
    {
        if (Managers.Instance == null)
            return;
        Initialize();
        ResetHealth();
        Character.OnCharacterRevive.AddListener(ResetHealth);
        EventManager.OnGameStart.AddListener(ResetHealth);

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
        if (Character.CharacterControllerType != CharacterControllerType.AI)
            return;

        _enemyScriptable = GetComponentInParent<AIBehaviour>().enemyScriptable;
        if (_enemyScriptable == null)
        {
            Destroy(transform.parent.gameObject);
            return;
        }
        MaxHealth = _enemyScriptable.health;
    }

    private void ResetHealth()
    {
        CurrentHealth = MaxHealth;
    }
    
    public void Heal(int healAmount)
    {
        CurrentHealth += healAmount;
        Character.OnCharacterHeal.Invoke();
        if (CurrentHealth >= MaxHealth)
            CurrentHealth = MaxHealth;
    }

    public void Damage(int damageAmount)
    {
        CurrentHealth -= damageAmount;
        UIManager.Instance.DamageTextCall(transform.position, damageAmount);
        Character.OnCharacterHit.Invoke();
        if (CurrentHealth <= 0)
        {
            Character.KillCharacter();
            CurrentHealth = 0;
        }
        Debug.Log(gameObject.name + " Health: " + CurrentHealth);
    }
}