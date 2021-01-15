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

public class HealPool : MonoBehaviour
{
    [SerializeField] private float _healAmount;

    // Heals the player with the heal amount
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<IHealable>() != null && other.GetComponent<Character>().CharacterControllerType != CharacterControllerType.Player)
            return;
        other.GetComponentInParent<CharacterHealthController>().Heal(_healAmount);
    }
}
