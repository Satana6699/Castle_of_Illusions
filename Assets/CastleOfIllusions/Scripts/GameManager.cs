using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverMenu = null;
    [SerializeField] private GameObject vinnerMenu = null;
    [SerializeField] private GameObject mainMenu = null;
    [SerializeField] private GameObject pauseMenuUI = null;
    [SerializeField] private string nameFirstScene = "Forest";
    
    private bool _isPaused = false;

    private void Start()
    {
        Time.timeScale = 1f;
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
    }

    public void PauseGame()
    {
        if (mainMenu.activeSelf)
            return;
        
        Time.timeScale = 0f;
        pauseMenuUI?.SetActive(true);
        _isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseMenuUI?.SetActive(false);
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

    public void MuteAllSounds()
    {
        AudioManager.Instance.MuteAllSounds();
    }

    public void UnmuteAllSounds()
    {
        AudioManager.Instance.UnmuteAllSounds();
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
}
