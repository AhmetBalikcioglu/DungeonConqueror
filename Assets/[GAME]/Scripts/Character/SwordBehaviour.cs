using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SwordBehaviour : MonoBehaviour
{
    private GameObject _swordHandle;
    public GameObject SwordHandle { get { return _swordHandle == null ? _swordHandle = GameObject.Find("SwordHandle") : _swordHandle; } }
    
    private GameObject _playerCenter;
    public GameObject PlayerCenter { get { return _playerCenter == null ? _playerCenter = GameObject.Find("PlayerCenter") : _playerCenter; } }

    [SerializeField] private int _attackDamage = 3;
    [SerializeField] private float _swordRadius = 0.71f;
    [SerializeField] private float _attackRange = 1f;
    [SerializeField] private float _attackRate = 0.3f;
    [SerializeField] private float _attackSpeed = 0.75f;
    private float _attackTimer;

    private Vector3 _worldPosition;
    private List<CharacterHealthController> AttackedEnemies;

    private void OnEnable()
    {
        if (Managers.Instance == null)
            return;
        _attackTimer = _attackSpeed;
        AttackedEnemies = new List<CharacterHealthController>();
        EventManager.OnPlayerAttack.AddListener(SwordAttack);
    }
    private void OnDisable()
    {
        if (Managers.Instance == null)
            return;

        EventManager.OnPlayerAttack.RemoveListener(SwordAttack);
    }

    private void Update()
    {
        SwordPointing();
        _attackTimer += Time.deltaTime;
    }
    public void SwordAttack()
    {
        if (_attackTimer < _attackSpeed)
            return;
        GetComponent<Collider>().enabled = true;
        _attackTimer = 0;

        float swordRad = InputManager.Instance.CalculateAngle(_worldPosition, SwordHandle.transform.position) * Mathf.Deg2Rad;
        transform.DOLocalMove(new Vector3(Mathf.Cos(swordRad) * _attackRange, Mathf.Sin(swordRad) * _attackRange, 0), _attackRate);
        transform.DOLocalMove(Vector3.zero, _attackRate).SetDelay(_attackRate).OnComplete(
            () => { GetComponent<Collider>().enabled = false;
                foreach (var enemy in AttackedEnemies)
                {
                    enemy.gotHit = false;
                }
                AttackedEnemies.Clear();
            }
        );
    }
    private void SwordPointing()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        _worldPosition = Camera.main.ScreenToWorldPoint(mousePos);

        SwordHandlePosSet();
        SwordAngleSet();
    }

    private void SwordHandlePosSet()
    {
        float swordRad = InputManager.Instance.CalculateAngle(_worldPosition, PlayerCenter.transform.position) * Mathf.Deg2Rad;
        SwordHandle.transform.localPosition = new Vector3(Mathf.Cos(swordRad) * _swordRadius, Mathf.Sin(swordRad) * _swordRadius, 0);
    }

    private void SwordAngleSet()
    {
        float swordAngle = InputManager.Instance.CalculateAngle(_worldPosition, SwordHandle.transform.position);
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, swordAngle - 90f));
    }
    private void OnTriggerEnter(Collider other)
    {
        IDamageable IDamageable = other.GetComponent<IDamageable>();
        CharacterHealthController CharacterHealthController = other.GetComponent<CharacterHealthController>();
        if (IDamageable == null || CharacterHealthController.gotHit)
            return;
        CharacterHealthController.gotHit = true;
        AttackedEnemies.Add(CharacterHealthController);
        other.transform.GetComponentInParent<Animator>().SetTrigger("Hit");
        IDamageable.Damage(_attackDamage);
    }
}
