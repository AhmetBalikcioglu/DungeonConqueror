using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public void ScaleHealthBar(float healthBarScale)
    {
        transform.localScale = new Vector3(healthBarScale, transform.localScale.y, transform.localScale.z);
    }
}
