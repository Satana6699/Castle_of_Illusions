using System;
using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    [SerializeField] public SoundSettings soundSettings;

    private AudioSource _musicSource;
    private List<AudioSource> _sfxSources = new List<AudioSource>();
    private Dictionary<string, AudioSource> _activeSounds = new Dictionary<string, AudioSource>();
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        _musicSource = gameObject.AddComponent<AudioSource>();
        _musicSource.loop = true;
    }

    private void Start()
    {
        PlayMusic(Instance.soundSettings.backgroundMusic);
    }

    public void PlayMusic(AudioClip music)
    {
        if (_musicSource.clip == music) return;

        _musicSource.clip = music;
        _musicSource.Play();
    }

    public void PlaySFX(AudioClip sound)
    {
        if (sound == null) return;

        AudioSource source = GetFreeAudioSource();
        source.clip = sound;
        source.Play();
    }

    // перебивание одинаковых звуков
    public void PlaySFXOverride(AudioClip sound)
    {
        if (!sound) return;

        string soundName = sound.name;

        if (_activeSounds.ContainsKey(soundName))
        {
            _activeSounds[soundName].Stop();
            _activeSounds[soundName].clip = sound;
            _activeSounds[soundName].Play();
        }
        else
        {
            AudioSource source = GetFreeAudioSource();
            source.clip = sound;
            source.Play();
            _activeSounds[soundName] = source;
        }
    }

    // если уже играет, не запускаем
    public void PlaySFXNoRepeat(AudioClip sound)
    {
        if (!sound)
        {
            return;
        }

        string soundName = sound.name;

        if (_activeSounds.ContainsKey(soundName) && _activeSounds[soundName].isPlaying)
        {
            return;
        }

        AudioSource source = GetFreeAudioSource();
        source.clip = sound;
        source.Play();
        _activeSounds[soundName] = source;
    }

    private AudioSource GetFreeAudioSource()
    {
        foreach (var source in _sfxSources)
        {
            if (!source.isPlaying)
                return source;
        }

        AudioSource newSource = gameObject.AddComponent<AudioSource>();
        _sfxSources.Add(newSource);
        return newSource;
    }
    
    public void MuteAllSounds()
    {
        _musicSource.mute = true;
        foreach (var source in _sfxSources)
        {
            source.mute = true;
        }
    }

    public void UnmuteAllSounds()
    {
        _musicSource.mute = false;
        foreach (var source in _sfxSources)
        {
            source.mute = false;
        }
    }

}
