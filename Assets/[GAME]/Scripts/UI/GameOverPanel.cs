using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanel : Panel
{
    private void OnEnable()
    {
        if (Managers.Instance == null)
            return;

        EventManager.OnGameEnd.AddListener(ShowPanel);
        EventManager.OnGameRestart.AddListener(HidePanel);
    }

    private void OnDisable()
    {
        if (Managers.Instance == null)
            return;

        EventManager.OnGameEnd.RemoveListener(ShowPanel);
        EventManager.OnGameRestart.RemoveListener(HidePanel);
    }
}
