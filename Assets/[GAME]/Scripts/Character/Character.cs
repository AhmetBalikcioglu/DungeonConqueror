using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum CharacterControllerType { Player, AI}

public class Character : MonoBehaviour
{
    public CharacterControllerType CharacterControllerType = CharacterControllerType.Player;

    private Rigidbody _rigidbody;
    public Rigidbody Rigidbody { get { return (_rigidbody == null) ? _rigidbody = GetComponent<Rigidbody>() : _rigidbody; } }

    private Collider _collider;
    public Collider Collider { get { return (_collider == null) ? _collider = GetComponent<Collider>() : _collider; } }

    #region Events

    [HideInInspector]
    public UnityEvent OnCharacterRevive = new UnityEvent();

    #endregion

    private bool _isDead;
    public bool IsDead { get { return (_isDead); } set { _isDead = value; } }
    private bool _isControlable;
    public bool IsControlable { get { return _isControlable; } set { _isControlable = value; } }

    
    private void OnEnable()
    {
        if (Managers.Instance == null)
            return;
        CharacterManager.Instance.AddCharacter(this);
        if (CharacterControllerType == CharacterControllerType.Player)
        {
            EventManager.OnGameStart.AddListener(() =>
            {
                IsControlable = true;
                IsDead = false;
            });
            EventManager.OnGameEnd.AddListener(() =>
            {
                IsControlable = false;
                IsDead = true;
            });
        }
        else
            EventManager.OnGameRestart.AddListener(Dispose);
    }

    private void OnDisable()
    {
        if (Managers.Instance == null)
            return;

        CharacterManager.Instance.RemoveCharacter(this);
        if (CharacterControllerType == CharacterControllerType.Player)
        {
            EventManager.OnGameStart.RemoveListener(() =>
            {
                IsControlable = true;
                IsDead = false;
            });
            EventManager.OnGameEnd.RemoveListener(() =>
            {
                IsControlable = false;
                IsDead = true;
            });
        }
        else
            EventManager.OnGameRestart.RemoveListener(Dispose);
    }

    public void KillCharacter()
    {
        if (IsDead)
            return;

        IsDead = true;
        IsControlable = false;

        if (CharacterControllerType == CharacterControllerType.Player)
            GameManager.Instance.EndGame();
        else
        {
            EventManager.OnEnemyDeath.Invoke(GetComponentInParent<AIBehaviour>().ScorePoint);
            GetComponentInParent<Animator>().SetTrigger("Death");
            Destroy(transform.parent.gameObject, 2f);
        }
    }

    public void ReviveCharacter()
    {
        if (!IsDead)
            return;

        IsDead = false;
        IsControlable = true;
        OnCharacterRevive.Invoke();
    }

    public void Dispose()
    {
        if (CharacterControllerType == CharacterControllerType.Player)
            return;
        Destroy(transform.parent.gameObject);
    }
}
