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
using UnityEngine.UI;

public class SoundButton : Button
{
    protected override void OnEnable()
    {
        base.OnEnable();
        onClick.AddListener(SoundOnOff);
    }

    protected override void OnDisable()
    {
        base.OnEnable();
        onClick.RemoveListener(SoundOnOff);
    }

    // Turn on or off the sounds
    private void SoundOnOff()
    {
        EventManager.OnSoundOnOff.Invoke();
    }
}
