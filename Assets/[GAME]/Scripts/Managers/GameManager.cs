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

public class GameManager : Singleton<GameManager>
{
    private bool isGameStarted;
    
    public bool IsGameStarted { get { return isGameStarted; } private set { isGameStarted = value; } }

    // Starts the game if not already started
    public void StartGame()
    {
        if (IsGameStarted)
            return;

        IsGameStarted = true;
        EventManager.OnGameStart.Invoke();
    }

    // Ends the game if not already ended
    public void EndGame()
    {
        if (!IsGameStarted)
            return;

        IsGameStarted = false;
        EventManager.OnGameEnd.Invoke();
    }

    // Restarts the game if it is finished
    public void RestartGame()
    {
        if (IsGameStarted)
            return;

        EventManager.OnGameRestart.Invoke();
    }
}
