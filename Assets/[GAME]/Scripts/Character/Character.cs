/****************************************************************************
** SAKARYA ÜNİVERSİTESİ
** BİLGİSAYAR VE BİLİŞİM BİLİMLERİ FAKÜLTESİ
** BİLGİSAYAR MÜHENDİSLİĞİ BÖLÜMÜ
** TASARIM ÇALIŞMASI
** 2020-2021 GÜZ DÖNEMİ
**
** ÖĞRETİM ÜYESİ..............: Prof.Dr. CEMİL ÖZ
** ÖĞRENCİ ADI................: AHMET YAŞAR BALIKÇIOĞLU
** ÖĞRENCİ NUMARASI...........: G1512.10001
** TASARIMIN ALINDIĞI GRUP....: 2T
****************************************************************************/

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
    private bool _isControllable;
    public bool IsControllable { get { return _isControllable; } set { _isControllable = value; } }

    
    private void OnEnable()
    {
        if (Managers.Instance == null)
            return;
        CharacterManager.Instance.AddCharacter(this);
        if (CharacterControllerType == CharacterControllerType.Player)
        {
            EventManager.OnGameStart.AddListener(() =>
            {
                IsControllable = true;
                IsDead = false;
            });
            EventManager.OnGameEnd.AddListener(() =>
            {
                IsControllable = false;
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
                IsControllable = true;
                IsDead = false;
            });
            EventManager.OnGameEnd.RemoveListener(() =>
            {
                IsControllable = false;
                IsDead = true;
            });
        }
        else
            EventManager.OnGameRestart.RemoveListener(Dispose);
    }

    // Killing the character
    public void KillCharacter()
    {
        if (IsDead)
            return;

        IsDead = true;
        IsControllable = false;

        if (CharacterControllerType == CharacterControllerType.Player)
            GameManager.Instance.EndGame();
        else
        {
            EventManager.OnEnemyDeath.Invoke(GetComponentInParent<AIBehaviour>().ScorePoint);
            GetComponentInParent<Animator>().SetTrigger("Death");
            Destroy(transform.parent.gameObject, 2f);
        }
    }

    // Reviving the character
    public void ReviveCharacter()
    {
        if (!IsDead)
            return;

        IsDead = false;
        IsControllable = true;
        OnCharacterRevive.Invoke();
    }

    // Deleting the AIs
    public void Dispose()
    {
        if (CharacterControllerType == CharacterControllerType.Player)
            return;
        Destroy(transform.parent.gameObject);
    }
}
