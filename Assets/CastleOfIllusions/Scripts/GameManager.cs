using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject noClickImage = null;
    [SerializeField] private GameObject gameOverMenu = null;
    [SerializeField] private GameObject vinnerMenu = null;
    [SerializeField] private GameObject mainMenu = null;
    [SerializeField] private GameObject pauseMenuUI = null;
    [SerializeField] private string nameFirstScene = "Forest";
    
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider soundVolumeSlider;
    
    private bool _isPaused = false;

    private void Start()
    {
        Time.timeScale = 1f;
        
        if (masterVolumeSlider && AudioManager.Instance)
            masterVolumeSlider.value = AudioManager.Instance.MasterVolume;
        
        if (musicVolumeSlider && AudioManager.Instance)
            musicVolumeSlider.value = AudioManager.Instance.MusicVolume;
        
        if (soundVolumeSlider && AudioManager.Instance)
            soundVolumeSlider.value = AudioManager.Instance.SFXVolume;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isPaused)
                ResumeGame();
            else
                PauseGame();
        }
        
        if (masterVolumeSlider)
            SetMasterVolume(masterVolumeSlider.value);
        
        if (musicVolumeSlider)
            SetMusicVolume(musicVolumeSlider.value);
        
        if (soundVolumeSlider)
            SetSFXVolume(soundVolumeSlider.value);
    }

    public void PauseGame()
    {
        if (mainMenu.activeSelf)
            return;
        
        Time.timeScale = 0f;
        pauseMenuUI?.SetActive(true);
        noClickImage?.SetActive(true);
        _isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseMenuUI?.SetActive(false);
        noClickImage?.SetActive(false);
        _isPaused = false;
    }
    
    public void EndGame()
    {
        gameOverMenu.SetActive(true);
    }
    
    public void RestartGame()
    {
        if (!string.IsNullOrEmpty(nameFirstScene))
        {
            SceneManager.LoadScene(nameFirstScene);
        }
        else
        {
            Debug.LogError("Имя сцены не указано!");
        }
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

    public void Vinner()
    {
        Time.timeScale = 0f;
        vinnerMenu.SetActive(true);
    }

    public void SetMasterVolume(float volume)
    {
        AudioManager.Instance?.SetMasterVolume(volume);
    }
    
    public void SetMusicVolume(float volume)
    {
        AudioManager.Instance?.SetMusicVolume(volume);
    }
    
    public void SetSFXVolume(float volume)
    {
        AudioManager.Instance?.SetSFXVolume(volume);
    }

    public void PlayCheckSound()
    {
        AudioManager.Instance?.PlaySFX(AudioManager.Instance?.soundSettings.playerMovementSound);
    }
}
