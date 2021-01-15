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

    // Updates the score text
    private void ScoreTextUpdate()
    {
        if (inGameScoreText == null)
            return;

        inGameScoreText.SetText("SCORE\n" + ScoreManager.Instance.Score.ToString());
    }

    // Updates the Game over screen score text
    private void GameOverScoreTextUpdate()
    {
        if (gameOverScoreText == null)
            return;

        gameOverScoreText.SetText("GAME OVER\n\nFINAL SCORE\n" + ScoreManager.Instance.Score.ToString());
    }

    // Instantiates the damageTextPrefab on the given position with the text updated by the given damage amount
    public void DamageTextCall(Vector3 pos, int damage)
    {
        GameObject textGO = Instantiate(damageTextPrefab, pos + Vector3.up, Quaternion.identity);
        textGO.transform.SetParent(FindObjectOfType<Canvas>().transform);
        textGO.GetComponent<TextMeshProUGUI>().SetText(damage.ToString());
        textGO.transform.DOMoveY(textGO.transform.position.y + 1f, 1f);
        Destroy(textGO, 1f);
    }

}
