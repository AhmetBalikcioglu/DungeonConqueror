using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    private float _score;

    public float Score { get { return _score; } private set { _score = value; } }

    private void OnEnable()
    {
        if (Managers.Instance == null)
            return;

        EventManager.OnEnemyDeath.AddListener(AddScore);
        EventManager.OnGameStart.AddListener(ResetScore);
    }

    private void OnDisable()
    {
        if (Managers.Instance == null)
            return;

        EventManager.OnEnemyDeath.RemoveListener(AddScore);
        EventManager.OnGameStart.RemoveListener(ResetScore);
    }

    private void AddScore(float scoreAmount)
    {
        Score += scoreAmount;
        EventManager.OnUIUpdate.Invoke();
    }

    public void ResetScore()
    {
        Score = 0;
        EventManager.OnUIUpdate.Invoke();
    }
}
