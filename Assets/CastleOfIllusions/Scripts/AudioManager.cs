using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private SoundSettings soundSettings;
    private AudioSource _musicSource;
    private AudioSource _sfxSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // остаётся между сценами
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        _musicSource = gameObject.AddComponent<AudioSource>();
        _musicSource.loop = true;

        _sfxSource = gameObject.AddComponent<AudioSource>();
        _sfxSource.loop = false;
    }

    private void Start()
    {
        PlayMusic(soundSettings.backgroundMusic);
    }
    
    public void PlayMusic(AudioClip music)
    {
        if (_musicSource.clip == music) return;

        _musicSource.clip = music;
        _musicSource.Play();
    }

    public void PlaySFX(AudioClip sound)
    {
        if (sound != null)
            _sfxSource.PlayOneShot(sound);
    }
}