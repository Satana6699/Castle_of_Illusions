using System;
using CastleOfIllusions.Scripts;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using System.Collections;

public class NextLocationScript : MonoBehaviour
{
    [Header("Settings scene")]
    [SerializeField] private string sceneName;
    [SerializeField] private float timerForLoading;

    [Header("Settings UI")]
    [SerializeField] private Canvas canvas;
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private float typingSpeed = 0.05f;

    [Header("Game Conditions")]
    [SerializeField] private int scoreForNextLocation = 1;
    [SerializeField] private Sword swordScore;

    private string _baseText;
    private bool _isTrigger = false;
    private bool _isTipeText = false;
    
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
        _isTrigger = true;
        canvas.enabled = true;
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (swordScore is not null && swordScore.Score >= scoreForNextLocation)
            {
                Invoke(nameof(LoadScene), timerForLoading);
            }
            else
            {
                if (!_isTipeText)
                {
                    StartCoroutine(TypeText());
                }            
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _isTrigger = false;
        canvas.enabled = false;
        textMeshPro.enabled = false;
        _isTipeText = false;
    }
    
    private IEnumerator TypeText()
    {
        textMeshPro.text = "";
        textMeshPro.enabled = true;
        _isTipeText = true;

        foreach (char c in _baseText)
        {
            if (!_isTrigger)
            {
                _isTipeText = false;
                break;
            }
            
            textMeshPro.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
