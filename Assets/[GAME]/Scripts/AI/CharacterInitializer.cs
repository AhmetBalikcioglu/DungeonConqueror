using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInitializer : MonoBehaviour
{
    public void Initialize(EnemyScriptableBase enemyScriptable)
    {
        GetComponentInChildren<Character>().CharacterControllerType = CharacterControllerType.AI;
        GetComponentInChildren<Character>().IsControlable = true;
        GetComponentInChildren<Character>().IsDead = false;
        GetComponent<AIBehaviour>().Initialize(enemyScriptable);
        GetComponentInChildren<AISwordBehaviour>().Initialize(enemyScriptable);
        GetComponentInChildren<CharacterHealthController>().Initialize(enemyScriptable);
    }
}
