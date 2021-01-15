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

    // Adds the score amount and updates UI
    private void AddScore(float scoreAmount)
    {
        Score += scoreAmount;
        EventManager.OnUIUpdate.Invoke();
    }

    // Resets the score and updates UI
    public void ResetScore()
    {
        Score = 0;
        EventManager.OnUIUpdate.Invoke();
    }
}
