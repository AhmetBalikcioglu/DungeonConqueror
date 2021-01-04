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
    public UnityEvent OnCharacterHit = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnCharacterHeal = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnCharacterDie = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnCharacterRevive = new UnityEvent();

    #endregion

    private bool _isDead;
    public bool IsDead { get { return (_isDead); } set { _isDead = value; } }
    private bool _isControlable;
    public bool IsControlable { get { return _isControlable; } set { _isControlable = value; } }

    /*private void Initialize()
    {
        IsControlable = true;
        IsDead = false;
    }*/
    private void OnEnable()
    {
        if (Managers.Instance == null)
            return;
        //Initialize();
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
    }

    public void KillCharacter()
    {
        if (IsDead)
            return;

        IsDead = true;
        IsControlable = false;
        OnCharacterDie.Invoke();

        if (CharacterControllerType == CharacterControllerType.Player)
            EventManager.OnLevelFail.Invoke();
        else
        {
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
}
