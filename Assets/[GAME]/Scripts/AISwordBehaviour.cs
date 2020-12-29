using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AISwordBehaviour : MonoBehaviour
{
    private EnemyScriptableBase _enemyScriptable;

    private int _attackDamage = 3;
    private float _swordRadius = 0.71f;
    private float _attackRange = 1f;
    private float _attackRate = 0.4f;
    private float _attackSpeed = 0.75f;

    private float _attackTimer;
    private bool hit = false;

    private void OnEnable()
    {
        Initialize();
    }
    private void Initialize()
    {
        _enemyScriptable = GetComponentInParent<AIBehaviour>().enemyScriptable;
        if (_enemyScriptable == null)
        {
            Destroy(transform.parent.gameObject);
            return;
        }
        transform.name = _enemyScriptable.weaponName;
        transform.localPosition = _enemyScriptable.weaponTransformPosition;
        transform.localRotation = Quaternion.Euler(_enemyScriptable.weaponTransformRotation);
        transform.localScale = _enemyScriptable.weaponTransformScale;
        _attackDamage = _enemyScriptable.attackDamage;
        _swordRadius = _enemyScriptable.swordRadius;
        _attackRange = _enemyScriptable.attackRange;
        _attackRate = _enemyScriptable.attackRate;
        _attackSpeed = _enemyScriptable.attackSpeed;
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
