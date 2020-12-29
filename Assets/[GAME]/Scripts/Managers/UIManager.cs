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
        GameObject textGO = Instantiate(damageTextPrefab, pos + Vector3.up * 0.5f, Quaternion.identity);
        textGO.transform.SetParent(FindObjectOfType<Canvas>().transform);
        textGO.GetComponent<TextMeshProUGUI>().SetText(damage.ToString());
        textGO.transform.DOMoveY(pos.y + 0.5f, 1f);
        Destroy(textGO, 1f);
    }

}
