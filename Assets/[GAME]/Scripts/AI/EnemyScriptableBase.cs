using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Enemy", order = 1)]
public class EnemyScriptableBase : ScriptableObject
{
    [Header("ObjectRelated")]
    public string prefabName;
    public RuntimeAnimatorController characterAnimator;

    [Header("SpriteRelated")]
    public Sprite characterSprite;
    public Sprite weaponSprite;

    [Header("CharacterRelated")]
    public string characterName;
    public float movementSpeed;
    public float health;
    public Vector3 boxColliderCenter;
    public Vector3 boxColliderSize;

    [Header("WeaponRelated")]
    public string weaponName;
    public Vector3 weaponTransformPosition;
    public Vector3 weaponTransformRotation;
    public Vector3 weaponTransformScale;
    public int attackDamage;
    public float swordRadius;
    public float attackRange;
    public float attackRate;
    public float attackSpeed;
}