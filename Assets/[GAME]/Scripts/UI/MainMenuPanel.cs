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

public class MainMenuPanel : Panel
{
    // MainMenuPanel is shown on game restart and hidden on game start
    private void OnEnable()
    {
        if (Managers.Instance == null)
            return;

        EventManager.OnGameStart.AddListener(HidePanel);
        EventManager.OnGameRestart.AddListener(ShowPanel);
    }

    private void OnDisable()
    {
        if (Managers.Instance == null)
            return;

        EventManager.OnGameStart.RemoveListener(HidePanel);
        EventManager.OnGameRestart.RemoveListener(ShowPanel);
    }
}
