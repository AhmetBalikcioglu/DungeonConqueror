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
using DG.Tweening;

public class AISwordBehaviour : MonoBehaviour
{
    private int _attackDamage = 3;
    private float _swordRadius = 0.71f;
    private float _attackRange = 1f;
    private float _attackRate = 0.4f;
    private float _attackSpeed = 0.75f;

    private float _attackTimer;
    private bool _hit = false;


    // Initializing base data
    public void Initialize(EnemyScriptableBase enemyScriptable)
    {
        transform.name = enemyScriptable.weaponName;
        transform.localPosition = enemyScriptable.weaponTransformPosition;
        transform.localRotation = Quaternion.Euler(enemyScriptable.weaponTransformRotation);
        transform.localScale = enemyScriptable.weaponTransformScale;
        _attackDamage = enemyScriptable.attackDamage;
        _swordRadius = enemyScriptable.swordRadius;
        _attackRange = enemyScriptable.attackRange;
        _attackRate = enemyScriptable.attackRate;
        _attackSpeed = enemyScriptable.attackSpeed;
    }

    // AI starts attacking to the Player. After a certain amount of time if the Player is still in AttackRange, Player gets damaged
    public IEnumerator SwordAttackCo()
    {
        yield return new WaitForSeconds(_attackRate);
        if (GetComponentInParent<Animator>() != null)
        {
            GetComponentInParent<Animator>().SetTrigger("Attack");
        }

        if (Vector3.Distance(transform.parent.position, CharacterManager.Instance.Player.transform.position) <= _attackRange)
        {
            CharacterManager.Instance.Player.GetComponent<IDamageable>().Damage(_attackDamage);
        }

        yield return new WaitForSeconds(_attackRate * 3f);
        transform.GetComponentInParent<AIBehaviour>().InRange();
        yield return null;
    }
}
