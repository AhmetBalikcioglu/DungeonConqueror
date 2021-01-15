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

public class CharacterManager : Singleton<CharacterManager>
{
    private List<Character> _characters;
    public List<Character> Characters { get { return (_characters == null) ? _characters = new List<Character>() : _characters; } set { _characters = value; } }

    private Character _player;
    public Character Player
    {
        get
        {
            if (_player == null)
            {
                foreach (var character in Characters)
                {
                    if (character.CharacterControllerType == CharacterControllerType.Player)
                        _player = character;
                }
            }

            return _player;
        }

        set
        {
            _player = value;
        }
    }

    private List<Character> _aiCharacters;
    public List<Character> AICharacters
    {
        get
        {
            if (_aiCharacters == null || _aiCharacters.Count == 0)
            {
                foreach (var character in Characters)
                {
                    if (character.CharacterControllerType == CharacterControllerType.AI)
                    {
                        if (!_aiCharacters.Contains(character))
                            _aiCharacters.Add(character);
                    }
                }
            }

            return _aiCharacters;
        }

        set
        {
            _aiCharacters = value;
        }
    }

    // Adding the given character to the Characters List if not already added
    public void AddCharacter(Character character)
    {
        if (!Characters.Contains(character))
            Characters.Add(character);
    }

    // Removing the given character to the Characters List if the list contains it
    public void RemoveCharacter(Character character)
    {
        if (Characters.Contains(character))
            Characters.Remove(character);
    }
}