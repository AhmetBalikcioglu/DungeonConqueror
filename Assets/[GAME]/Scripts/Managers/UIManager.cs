using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class UIManager : Singleton<UIManager>
{
    public GameObject damageTextPrefab;

    public void DamageTextCall(Vector3 pos, int damage)
    {
        GameObject textGO = Instantiate(damageTextPrefab, pos + Vector3.up, Quaternion.identity);
        textGO.transform.SetParent(FindObjectOfType<Canvas>().transform);
        textGO.GetComponent<TextMeshProUGUI>().SetText(damage.ToString());
        textGO.transform.DOMoveY(textGO.transform.position.y + 1f, 1f);
        Destroy(textGO, 1f);
    }

}
