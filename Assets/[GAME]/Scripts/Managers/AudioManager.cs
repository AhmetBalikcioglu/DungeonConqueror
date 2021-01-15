/****************************************************************************
** SAKARYA ÜNİVERSİTESİ
** BİLGİSAYAR VE BİLİŞİM BİLİMLERİ FAKÜLTESİ
** BİLGİSAYAR MÜHENDİSLİĞİ BÖLÜMÜ
** TASARIM ÇALIŞMASI
** 2020-2021 GÜZ DÖNEMİ
**
** ÖĞRETİM ÜYESİ..............: Prof.Dr. CEMİL ÖZ
** ÖĞRENCİ ADI................: AHMET YAŞAR BALIKÇIOĞLU
** ÖĞRENCİ NUMARASI...........: G1512.10001
** TASARIMIN ALINDIĞI GRUP....: 2T
****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public Sound[] sounds;

    private bool _isSoundOn;

    // Checking if there is a AudioManager exists and if so destroys the new one
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

    // On Start it plays music
    void Start()
    {
        Play("Music");
    }

    // Plays the sound with the given name
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

    // Changes the volume of the sound given by the parameters
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

    // Changes volumes of all sounds to either 0 or their base volume
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