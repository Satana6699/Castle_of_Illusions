using System;
using CastleOfIllusions.Scripts;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using System.Collections;

public class NextLocationScript : MonoBehaviour
{
    [Header("Scene Settings")]
    [SerializeField] private string sceneName;
    [SerializeField] private float timerForLoading;
    [SerializeField] private Canvas canvas;
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private int scoreForNextLocation = 1;
    [SerializeField] private Sword swordScore;
    [SerializeField] private float typingSpeed = 0.05f;
    
    private string _baseText;
    
    private void Start()
    {
        canvas.enabled = false;
        textMeshPro.enabled = false;
        _baseText = textMeshPro.text;
    }
    
    private void LoadScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Имя сцены не указано!");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        canvas.enabled = true;
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (swordScore is not null && swordScore.Score >= scoreForNextLocation)
            {
                Invoke(nameof(LoadScene), timerForLoading);
            }
            else
            {
                textMeshPro.enabled = true;
                StartCoroutine(TypeText());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canvas.enabled = false;
        textMeshPro.enabled = false;
    }
    
    private IEnumerator TypeText()
    {
        textMeshPro.text = "";

        foreach (char c in _baseText)
        {
            textMeshPro.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
