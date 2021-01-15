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

public enum AIState { idle, moving, attacking};
public class AIBehaviour : MonoBehaviour
{
    AIState AICurrentState = AIState.idle;

    private float _moveSpeed = 2f;
    private float _moveTimer;
    private bool _inRange;

    private float _scorePoint;
    public float ScorePoint { get { return _scorePoint; } private set { _scorePoint = value; } }

    private void Update()
    {
        if (AICurrentState != AIState.moving || !GetComponentInChildren<Character>().IsControllable)
            return;

        _moveTimer = _moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, CharacterManager.Instance.Player.transform.position, _moveTimer);

        if (Vector3.Distance(transform.position, CharacterManager.Instance.Player.transform.position) < 0.5f)
        {
            AICurrentState = AIState.attacking;
            StartCoroutine(GetComponentInChildren<AISwordBehaviour>().SwordAttackCo());
        }
    }

    // Initializing base data
    public void Initialize(EnemyScriptableBase enemyScriptable)
    {
        GetComponentInChildren<Character>().transform.name = enemyScriptable.characterName;
        _moveSpeed = enemyScriptable.movementSpeed;
        gameObject.name = enemyScriptable.prefabName;
        GetComponent<Animator>().runtimeAnimatorController = enemyScriptable.characterAnimator;
        List<SpriteRenderer> sprites = new List<SpriteRenderer>();
        GetComponentsInChildren<SpriteRenderer>(sprites);
        sprites[0].sprite = enemyScriptable.characterSprite;
        sprites[1].sprite = enemyScriptable.weaponSprite;
        GetComponentInChildren<BoxCollider>().center = enemyScriptable.boxColliderCenter;
        GetComponentInChildren<BoxCollider>().size = enemyScriptable.boxColliderSize;
        ScorePoint = enemyScriptable.scorePoint;
    }

    // Checking if still in range if so setting the current state to moving
    public void InRange()
    {
        if (_inRange)
            AICurrentState = AIState.moving;
    }

    // Checking if the trigger is Player if so AI starts to move closer to Player
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.name != "PlayerCharacter")
            return;
        _inRange = true;
        AICurrentState = AIState.moving;
        if (GetComponent<Animator>() != null)
            GetComponent<Animator>().SetTrigger("Move");
    }

    // Checking if the trigger is Player if so AI stops moving
    private void OnTriggerExit(Collider collision)
    {
        if (collision.name != "PlayerCharacter")
            return;

        _inRange = false;

        if (AICurrentState == AIState.moving)
        {
            AICurrentState = AIState.idle;
            if(GetComponent<Animator>() != null)
                GetComponent<Animator>().SetTrigger("Idle");
        }
    }
}
