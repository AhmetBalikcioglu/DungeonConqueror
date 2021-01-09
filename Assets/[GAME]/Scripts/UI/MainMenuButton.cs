using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButton : Button
{
    protected override void OnEnable()
    {
        base.OnEnable();
        onClick.AddListener(RestartGame);
    }

    protected override void OnDisable()
    {
        base.OnEnable();
        onClick.RemoveListener(RestartGame);
    }

    private void RestartGame()
    {
        GameManager.Instance.RestartGame();
    }
}