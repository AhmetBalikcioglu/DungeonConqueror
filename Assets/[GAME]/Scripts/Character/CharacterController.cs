using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour, ICharacterController
{
    private void OnEnable()
    {
        if (Managers.Instance == null)
            return;

        EventManager.OnGameStart.AddListener(ResetPosition);
    }

    private void OnDisable()
    {
        if (Managers.Instance == null)
            return;

        EventManager.OnGameStart.RemoveListener(ResetPosition);
    }

    private void ResetPosition()
    {
        transform.position = Vector3.zero;
    }

    public void Attack()
    {
        throw new System.NotImplementedException();
    }

    public void Dash()
    {
        throw new System.NotImplementedException();
    }

    public void Move()
    {
        throw new System.NotImplementedException();
    }

}
