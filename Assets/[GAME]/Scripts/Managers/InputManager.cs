using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    [SerializeField] private float _moveSpeed = 500f;

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
            CharacterManager.Instance.Player.Rigidbody.velocity = newMove;
        }
    }

    public float CalculateAngle(Vector3 firstPos, Vector3 secondPos)
    {
        return Mathf.Atan2(firstPos.y - secondPos.y, firstPos.x - secondPos.x) * 180 / Mathf.PI;
    }
}
