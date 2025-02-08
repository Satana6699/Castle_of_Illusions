using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverMenu = null;
    [SerializeField] private string nameFirstScene = "Forest";
    
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
}
