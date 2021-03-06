﻿/****************************************************************************
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

public class CharacterInitializer : MonoBehaviour
{
    // Initializing all of the AI data
    public void Initialize(EnemyScriptableBase enemyScriptable)
    {
        GetComponentInChildren<Character>().CharacterControllerType = CharacterControllerType.AI;
        GetComponentInChildren<Character>().IsControllable = true;
        GetComponentInChildren<Character>().IsDead = false;
        GetComponent<AIBehaviour>().Initialize(enemyScriptable);
        GetComponentInChildren<AISwordBehaviour>().Initialize(enemyScriptable);
        GetComponentInChildren<CharacterHealthController>().Initialize(enemyScriptable);
    }
}
