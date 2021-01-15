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

public class GameOverPanel : Panel
{
    // GameOverPanel is shown on game end and hidden on game restart

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
