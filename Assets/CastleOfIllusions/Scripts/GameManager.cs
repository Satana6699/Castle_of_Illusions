using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject endGameMenu = null;
    [SerializeField] private GameObject vinnerGameMenu = null;
    [SerializeField] private string nameFirstScene = "Forest";
    
    public void EndGame()
    {
        endGameMenu.SetActive(true);
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
}
