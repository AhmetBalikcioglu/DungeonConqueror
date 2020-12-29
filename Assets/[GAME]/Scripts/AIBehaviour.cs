using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AIState { idle, moving, attacking};
public class AIBehaviour : MonoBehaviour
{
    AIState AICurrentState = AIState.idle;

    public EnemyScriptableBase enemyScriptable;

    private float _moveSpeed = 2f;
    private float _moveTimer;
    private bool _inRange;
    private void OnEnable()
    {
        Initialize();
    }

    private void Update()
    {
        if (AICurrentState != AIState.moving || !GetComponentInChildren<Character>().IsControlable)
            return;

        _moveTimer = _moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, CharacterManager.Instance.Player.transform.position, _moveTimer);

        if (Vector3.Distance(transform.position, CharacterManager.Instance.Player.transform.position) < 0.5f)
        {
            // AI ATTACK
            AICurrentState = AIState.attacking;
            StartCoroutine(GetComponentInChildren<AISwordBehaviour>().SwordAttackCo());
        }
    }

    private void Initialize()
    {
        if (enemyScriptable == null)
        {
            Destroy(gameObject);
            return;
        }
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
    }

    public void InRange()
    {
        if (_inRange)
            AICurrentState = AIState.moving;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.name != "PlayerCharacter")
            return;
        _inRange = true;
        AICurrentState = AIState.moving;
        if (GetComponent<Animator>() != null)
            GetComponent<Animator>().SetTrigger("Move");
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.name != "PlayerCharacter")
            return;

        _inRange = false;

        if (AICurrentState != AIState.attacking)
        {
            AICurrentState = AIState.idle;
            if(GetComponent<Animator>() != null)
                GetComponent<Animator>().SetTrigger("Idle");
        }        
    }
}
