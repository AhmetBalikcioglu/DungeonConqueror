using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundButton : Button
{
    protected override void OnEnable()
    {
        base.OnEnable();
        onClick.AddListener(SoundOnOff);
    }

    protected override void OnDisable()
    {
        base.OnEnable();
        onClick.RemoveListener(SoundOnOff);
    }

    private void SoundOnOff()
    {
        EventManager.OnSoundOnOff.Invoke();
    }
}
