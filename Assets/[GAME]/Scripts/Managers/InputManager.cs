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

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && CharacterManager.Instance.Player.IsControlable)
        {
            EventManager.OnPlayerAttack.Invoke();
        }
    }
    private void FixedUpdate()
    {
        if (CharacterManager.Instance.Player.IsControlable)
        {
            Vector3 input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);

            Vector3 newMove = input * _moveSpeed * Time.fixedDeltaTime;
            CharacterManager.Instance.Player.Rigidbody.velocity = Clamp(newMove);
        }
    }

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

    public float CalculateAngle(Vector3 firstPos, Vector3 secondPos)
    {
        return Mathf.Atan2(firstPos.y - secondPos.y, firstPos.x - secondPos.x) * 180 / Mathf.PI;
    }
}
