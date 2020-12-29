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


    public void AddCharacter(Character character)
    {
        if (!Characters.Contains(character))
            Characters.Add(character);
    }

    public void RemoveCharacter(Character character)
    {
        if (Characters.Contains(character))
            Characters.Remove(character);
    }
}