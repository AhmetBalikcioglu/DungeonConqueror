using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPool : MonoBehaviour
{
    [SerializeField] private int healAmount;
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<IHealable>() != null && other.GetComponent<Character>().CharacterControllerType != CharacterControllerType.Player)
            return;
        other.GetComponentInParent<CharacterHealthController>().Heal(healAmount);
    }
}
