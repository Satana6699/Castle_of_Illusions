using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StartBossFight : MonoBehaviour
{
    [SerializeField] private GameObject[] platforms;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ActivatePlatforms();
            Destroy(gameObject);
        }
    }

    private void ActivatePlatforms()
    {
        foreach (GameObject platform in platforms)
        {
            platform.SetActive(true);
        }
    }
}
