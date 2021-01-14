using System;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public Sound[] sounds;

    private bool _isSoundOn;

    void Awake()
    {
        if (AudioManager.Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
        _isSoundOn = true;
    }

    private void OnEnable()
    {
        if (Managers.Instance == null)
            return;
        EventManager.OnEnemyHit.AddListener(() => { AudioManager.Instance.Play("Hit"); });
        EventManager.OnSoundOnOff.AddListener(SoundOnOff);
    }

    private void OnDisable()
    {
        if (Managers.Instance == null)
            return;
        EventManager.OnEnemyHit.RemoveListener(() => { AudioManager.Instance.Play("Hit"); });
        EventManager.OnSoundOnOff.RemoveListener(SoundOnOff);

    }

    void Start()
    {
        Play("Music");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }

    public void ChangeVolume(string name, float volume)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.volume = volume;
    }

    private void SoundOnOff()
    {
        if (_isSoundOn)
            for (int i = 0; i < sounds.Length; i++)
            {
                sounds[i].source.volume = 0f;
            }
        else
            for (int i = 0; i < sounds.Length; i++)
            {
                sounds[i].source.volume = sounds[i].volume;
            }
        _isSoundOn = !_isSoundOn;
    }
}