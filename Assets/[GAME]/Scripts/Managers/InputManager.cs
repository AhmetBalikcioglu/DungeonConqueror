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

public class InputManager : Singleton<InputManager>
{
    [SerializeField] private float _moveSpeed = 500f;
    [SerializeField] private float _upClampY;
    [SerializeField] private float _downClampY;
    [SerializeField] private float _rightClampX;
    [SerializeField] private float _leftClampX;

    private void OnEnable()
    {
        if (Managers.Instance == null)
            return;

        EventManager.OnGameEnd.AddListener(ZeroVelocity);
    }

    private void OnDisable()
    {
        if (Managers.Instance == null)
            return;

        EventManager.OnGameEnd.RemoveListener(ZeroVelocity);
    }

    // If player is controllable and left mouse button is pressed then player attacks
    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && CharacterManager.Instance.Player.IsControllable)
        {
            EventManager.OnPlayerAttack.Invoke();
        }
    }

    // If player is controllable then player is moved according to users WASD inputs
    private void FixedUpdate()
    {
        if (CharacterManager.Instance.Player.IsControllable)
        {
            Vector3 input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);

            Vector3 newMove = input * _moveSpeed * Time.fixedDeltaTime;
            CharacterManager.Instance.Player.Rigidbody.velocity = Clamp(newMove);
        }
    }

    // Clamps the player to the given values so player wouldn't go out of the screen
    private Vector3 Clamp(Vector3 direction)
    {
        Vector3 playerPos = CharacterManager.Instance.Player.transform.position;
        if (playerPos.x >= _rightClampX && direction.x > 0 || playerPos.x <= _leftClampX && direction.x < 0)
        {
            direction.x = 0f;
        }
        if (playerPos.y >= _upClampY && direction.y > 0 || playerPos.y <= _downClampY && direction.y < 0)
        {
            direction.y = 0f;
        }
        return direction;
    }

    // Calculates the angles between the given positions
    public float CalculateAngle(Vector3 firstPos, Vector3 secondPos)
    {
        return Mathf.Atan2(firstPos.y - secondPos.y, firstPos.x - secondPos.x) * 180 / Mathf.PI;
    }

    // Zeroes the players velocity
    private void ZeroVelocity()
    {
        CharacterManager.Instance.Player.Rigidbody.velocity = Vector3.zero;
    }
}
