using System;
using CastleOfIllusions.Scripts;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Serialization;

public class NextLocationScript : MonoBehaviour
{
    [Header("Settings scene")]
    [SerializeField] private string sceneName;
    [SerializeField] private float timerForLoading;

    [Header("Settings UI")]
    [SerializeField] private Canvas interactiveCanvas;

    private void Start()
    {
        interactiveCanvas.enabled = false;
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
        interactiveCanvas.enabled = true;
        if (Input.GetKey(KeyCode.E))
        {
            Invoke(nameof(LoadScene), timerForLoading);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        interactiveCanvas.enabled = false;
    }
}
