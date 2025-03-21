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
    [SerializeField] private float minDistanceMusic = 1f;
    [SerializeField] private float maxDistanceMusic = 7f;
    
    
    private float _masterVolume = 1f;
    private float _musicVolume = 1f;
    private float _sfxVolume = 1f;

    public float MasterVolume { get { return _masterVolume; } }
    public float MusicVolume { get { return _musicVolume; } }
    public float SFXVolume { get { return _sfxVolume; } }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
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
        if (_musicSource.clip == music || _musicSource.clip) return;

        _musicSource.clip = music;
        _musicSource.volume = _musicVolume * _masterVolume;
        _musicSource.Play();
    }

    public void PlaySFX(AudioClip sound, Vector3 position)
    {
        if (sound == null) return;

        AudioSource source = GetFreeAudioSource(position);
        source.transform.position = position;
        source.clip = sound;
        source.volume = _sfxVolume * _masterVolume;
        source.Play();
    }

    public void PlaySFXOverride(AudioClip sound, Vector3 position)
    {
        if (sound == null) return;

        string soundName = sound.name;

        if (_activeSounds.ContainsKey(soundName))
        {
            _activeSounds[soundName].Stop();
            _activeSounds[soundName].transform.position = position;
            _activeSounds[soundName].clip = sound;
            _activeSounds[soundName].volume = _sfxVolume * _masterVolume;
            _activeSounds[soundName].Play();
        }
        else
        {
            AudioSource source = GetFreeAudioSource(position);
            source.clip = sound;
            source.Play();
            _activeSounds[soundName] = source;
        }
    }

    public void PlaySFXNoRepeat(AudioClip sound, Vector3 position)
    {
        if (sound == null) return;

        string soundName = sound.name;

        if (_activeSounds.ContainsKey(soundName) && _activeSounds[soundName].isPlaying)
        {
            _activeSounds[soundName].transform.position = position;
            return;
        }

        AudioSource source = GetFreeAudioSource(position);
        source.clip = sound;
        source.Play();
        _activeSounds[soundName] = source;
    }

    private AudioSource GetFreeAudioSource(Vector3 position)
    {
        foreach (var source in _sfxSources)
        {
            if (!source.isPlaying)
                return source;
        }

        AudioSource newSource = gameObject.AddComponent<AudioSource>();
        newSource.volume = _sfxVolume * _masterVolume;
        _sfxSources.Add(newSource);
        return newSource;
    }

    // общая громкость
    public void SetMasterVolume(float volume)
    {
        _masterVolume = volume;
        ApplyVolumeSettings();
    }

    // громкость музыки
    public void SetMusicVolume(float volume)
    {
        _musicVolume = volume;
        _musicSource.volume = _musicVolume * _masterVolume;
    }

    // громкость звуковых эффектов
    public void SetSFXVolume(float volume)
    {
        _sfxVolume = volume;
        foreach (var source in _sfxSources)
        {
            source.volume = _sfxVolume * _masterVolume;
        }
    }

    private void ApplyVolumeSettings()
    {
        _musicSource.volume = _musicVolume * _masterVolume;
        foreach (var source in _sfxSources)
        {
            source.volume = _sfxVolume * _masterVolume;
        }
    }
}
