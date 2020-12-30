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

    public IEnumerator SwordAttackCo()
    {
        yield return new WaitForSeconds(_attackRate);
        if (GetComponentInParent<Animator>() != null)
        {
            GetComponentInParent<Animator>().SetTrigger("Attack");
        }
        
        /*transform.DOLocalMove(new Vector3(_swordRadius, _swordRadius / 2f, 0), _attackRate);
        transform.DOLocalRotate(new Vector3(0, 0, -30f), _attackRate);

        transform.DOLocalMove(Vector3.zero, _attackRate).SetDelay(_attackRate);
        transform.DOLocalRotate(new Vector3(0, 0, -20f), _attackRate).SetDelay(_attackRate);*/

        if (Vector3.Distance(transform.parent.position, CharacterManager.Instance.Player.transform.position) <= _attackRange)
        {
            CharacterManager.Instance.Player.GetComponent<IDamageable>().Damage(_attackDamage);
        }

        yield return new WaitForSeconds(_attackRate * 3f);
        transform.GetComponentInParent<AIBehaviour>().InRange();
        yield return null;
    }
}
