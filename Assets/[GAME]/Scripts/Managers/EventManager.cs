using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    public static UnityEvent OnGameStart = new UnityEvent();

    public static UnityEvent OnPlayerHit = new UnityEvent();
    public static UnityEvent OnPlayerAttack = new UnityEvent();
    public static UnityEvent OnPlayerHeal = new UnityEvent();
    public static UnityEvent OnPlayerDeath = new UnityEvent();

    public static UnityEvent OnEnemyHit = new UnityEvent();
    public static UnityEvent OnEnemyDeath = new UnityEvent();

    public static UnityEvent OnLevelFail = new UnityEvent();

}
