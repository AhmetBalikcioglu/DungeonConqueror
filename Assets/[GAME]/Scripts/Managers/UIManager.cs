using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class UIManager : Singleton<UIManager>
{
    public TextMeshProUGUI inGameScoreText;
    public TextMeshProUGUI gameOverScoreText;
    public GameObject damageTextPrefab;

    private void OnEnable()
    {
        if (Managers.Instance == null)
            return;

        EventManager.OnUIUpdate.AddListener(ScoreTextUpdate);
        EventManager.OnGameEnd.AddListener(GameOverScoreTextUpdate);
    }

    private void OnDisable()
    {
        if (Managers.Instance == null)
            return;

        EventManager.OnUIUpdate.RemoveListener(ScoreTextUpdate);
        EventManager.OnGameEnd.RemoveListener(GameOverScoreTextUpdate);
    }

    private void ScoreTextUpdate()
    {
        if (inGameScoreText == null)
            return;

        inGameScoreText.SetText("SCORE\n" + ScoreManager.Instance.Score.ToString());
    }

    private void GameOverScoreTextUpdate()
    {
        if (gameOverScoreText == null)
            return;

        gameOverScoreText.SetText("GAME OVER\n\nFINAL SCORE\n" + ScoreManager.Instance.Score.ToString());
    }

    public void DamageTextCall(Vector3 pos, int damage)
    {
        GameObject textGO = Instantiate(damageTextPrefab, pos + Vector3.up, Quaternion.identity);
        textGO.transform.SetParent(FindObjectOfType<Canvas>().transform);
        textGO.GetComponent<TextMeshProUGUI>().SetText(damage.ToString());
        textGO.transform.DOMoveY(textGO.transform.position.y + 1f, 1f);
        Destroy(textGO, 1f);
    }

}
